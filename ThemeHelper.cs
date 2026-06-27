using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practise_nubip_number2
{
    /// <summary>
    /// Допоміжний клас для застосування стилізації у стилі аніме-кіберпанку 2000-х (на зразок Serial Experiments Lain, Winamp)
    /// з динамічним завантаженням фонів з мережі та іконки з файлу.
    /// </summary>
    public static class ThemeHelper
    {
        public static void Apply2000sAnimeTheme(Form form)
        {
            Color backColor = Color.FromArgb(24, 12, 36);        // Глибокий темно-фіолетовий
            Color panelColor = Color.FromArgb(36, 18, 54);       // Панельний фіолетовий
            Color textNormal = Color.FromArgb(180, 165, 220);    // Лавандовий
            Color textHighlight = Color.FromArgb(250, 180, 250);   // Темно-рожевий неон (Dark Pink)
            Color textAccent = Color.FromArgb(0, 255, 200);       // Кібер-бірюзовий (Cyber Cyan)
            Color bgControl = Color.FromArgb(14, 6, 22);    
            Color transparentGrp = Color.FromArgb(40, 14, 6, 22);      // Найтемніший фіолетовий для списків/інпутів

            form.BackColor = backColor;
            form.ForeColor = textNormal;

            Font mainFont = new Font("Segoe UI", 9F, FontStyle.Bold);
            Font titleFont = new Font("Segoe UI", 10F, FontStyle.Bold);

            // Спроба використати класичний японський шрифт у стилі 2000-х (MS Gothic)
            try
            {
                using (Font testFont = new Font("MS Gothic", 9))
                {
                    if (testFont.Name == "MS Gothic")
                    {
                        mainFont = new Font("MS Gothic", 9F, FontStyle.Bold);
                        titleFont = new Font("MS Gothic", 10F, FontStyle.Bold);
                    }
                }
            }
            catch { }

            form.Font = mainFont;

            // 1. Пошук папки "img" за деревом директорій вгору
            string imgPath = FindImgDirectory();

            // 2. Завантаження іконки форми (ico.png)
            string icoPath = Path.Combine(imgPath, "ico.png");
            if (File.Exists(icoPath))
            {
                try
                {
                    using (Bitmap bmp = new Bitmap(icoPath))
                    {
                        IntPtr hIcon = bmp.GetHicon();
                        form.Icon = Icon.FromHandle(hIcon);
                    }
                }
                catch { }
            }

            // 3. Зчитування URL фону форми (з background.txt або bacground.txt)
            string bgUrl = "https://i.pinimg.com/originals/38/a9/89/38a989df19d45eb33fea760e87210192.gif";
            string bgFile1 = Path.Combine(imgPath, "background.txt");
            string bgFile2 = Path.Combine(imgPath, "bacground.txt");
            
            if (File.Exists(bgFile1)) bgUrl = File.ReadAllText(bgFile1).Trim();
            if (string.IsNullOrEmpty(bgUrl) && File.Exists(bgFile2)) bgUrl = File.ReadAllText(bgFile2).Trim();

            if (!string.IsNullOrEmpty(bgUrl))
            {
                _ = LoadImageToControlAsync(form, bgUrl);
            }

            // 4. Зчитування URL фону панелей (з Panel.txt) або завантаження локального файлу
            string localPanelPath = Path.Combine(imgPath, "image.png");
            string panelUrl = "https://i.pinimg.com/originals/a5/72/86/a5728664014feb0863f26a7d64a1bd77.gif";
            string panelFile = Path.Combine(imgPath, "Panel.txt");
            if (File.Exists(panelFile)) panelUrl = File.ReadAllText(panelFile).Trim();

            // 5. Рекурсивне застосування стилів до дочірніх елементів
            ApplyToControls(form.Controls, backColor, panelColor, textNormal, textHighlight, textAccent, bgControl, mainFont, titleFont, panelUrl, localPanelPath);
        }

        private static void ApplyToControls(Control.ControlCollection controls, Color bg, Color panelBg, Color fg, Color fgHighlight, Color fgAccent, Color bgCtrl, Font font, Font titleFont, string panelUrl, string localPanelPath)
        {
            Color transparentGrp = Color.FromArgb(128, bgCtrl.R, bgCtrl.G, bgCtrl.B);
            foreach (Control ctrl in controls)
            {
                ctrl.Font = font;

                if (ctrl is Button btn)
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.BackColor = panelBg;
                    btn.ForeColor = fgHighlight;
                    btn.FlatAppearance.BorderColor = fgHighlight;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(60, 30, 90);
                    btn.FlatAppearance.MouseDownBackColor = Color.FromArgb(90, 45, 135);
                }
                else if (ctrl is TextBox txt)
                {
                    txt.BackColor = bgCtrl;
                    txt.ForeColor = fgAccent;
                    txt.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is ComboBox cmb)
                {
                    cmb.BackColor = bgCtrl;
                    cmb.ForeColor = fgAccent;
                }
                else if (ctrl is TreeView tv)
                {
                    tv.BackColor = bgCtrl;
                    tv.ForeColor = fgAccent;
                    tv.LineColor = fgHighlight;
                    tv.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is ListBox lb)
                {
                    lb.BackColor = bgCtrl;
                    lb.ForeColor = fgAccent;
                    lb.BorderStyle = BorderStyle.FixedSingle;
                }
                else if (ctrl is GroupBox grp)
                {
                    grp.ForeColor = fgHighlight;
                    grp.BackColor = transparentGrp;
                    
                    if (!string.IsNullOrEmpty(localPanelPath))
                    {
                        string imgDir = Path.GetDirectoryName(localPanelPath) ?? "";
                        string icoPath = Path.Combine(imgDir, "f250ad6b44f83aa6c176c5d295f1466d.gif");
                        if (File.Exists(icoPath))
                        {
                            try
                            {
                                Image gifImg = Image.FromFile(icoPath);

                                // Запуск анімації кадрів GIF
                                if (ImageAnimator.CanAnimate(gifImg))
                                {
                                    ImageAnimator.Animate(gifImg, (s, ev) =>
                                    {
                                        if (grp.IsDisposed) return;
                                        try
                                        {
                                            grp.BeginInvoke(new Action(() => grp.Invalidate()));
                                        }
                                        catch { }
                                    });
                                }

                                // Додаємо обробник малювання, який оновлює кадри та малює GIF з 50% прозорістю
                                grp.Paint += (s, pe) =>
                                {
                                    try
                                    {
                                        ImageAnimator.UpdateFrames(gifImg);

                                        System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix();
                                        matrix.Matrix33 = 0.5f; // 50% прозорості для кадрів GIF

                                        using (System.Drawing.Imaging.ImageAttributes attr = new System.Drawing.Imaging.ImageAttributes())
                                        {
                                            attr.SetColorMatrix(matrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                                            
                                            pe.Graphics.DrawImage(
                                                gifImg, 
                                                grp.ClientRectangle, 
                                                0, 0, gifImg.Width, gifImg.Height, 
                                                GraphicsUnit.Pixel, 
                                                attr
                                            );
                                        }
                                    }
                                    catch { }
                                };

                                // Визволяємо ресурси при закритті форми
                                grp.Disposed += (s, ev) =>
                                {
                                    try { gifImg.Dispose(); } catch { }
                                };
                            }
                            catch { }
                        }
                    }
                    
                    ApplyToControls(grp.Controls, bg, panelBg, fg, fgHighlight, fgAccent, transparentGrp, font, titleFont, panelUrl, localPanelPath);
                }
                else if (ctrl is Panel pnl)
                {
                    pnl.BackColor = panelBg;
                    bool loadedLocal = false;
                    
                    if (!string.IsNullOrEmpty(localPanelPath) && File.Exists(localPanelPath))
                    {
                        try
                        {
                            pnl.BackgroundImage = Image.FromFile(localPanelPath);
                            pnl.BackgroundImageLayout = ImageLayout.Stretch;
                            loadedLocal = true;
                        }
                        catch { }
                    }

                    if (!loadedLocal && !string.IsNullOrEmpty(panelUrl))
                    {
                        _ = LoadImageToControlAsync(pnl, panelUrl);
                    }
                    ApplyToControls(pnl.Controls, bg, panelBg, fg, fgHighlight, fgAccent, bgCtrl, font, titleFont, panelUrl, localPanelPath);
                }
                else if (ctrl is Label lbl)
                {
                    if (lbl.Name.Contains("Details") || lbl.Name.Contains("FileDetails"))
                    {
                        lbl.ForeColor = Color.White;
                    }
                    else if (lbl.Name.Contains("Header") || lbl.Name.Contains("Stats"))
                    {
                        lbl.ForeColor = fgAccent;
                    }
                    else
                    {
                        lbl.ForeColor = fg;
                    }
                    // Робимо фони лейблів прозорими для кращого вигляду поверх картинок
                    lbl.BackColor = Color.Transparent;
                }
                else if (ctrl is CheckBox chk)
                {
                    chk.ForeColor = fg;
                    chk.BackColor = Color.Transparent;
                }
                else if (ctrl is DateTimePicker dtp)
                {
                    dtp.CalendarMonthBackground = bgCtrl;
                    dtp.CalendarTitleBackColor = panelBg;
                    dtp.CalendarTitleForeColor = fgHighlight;
                }
                else if (ctrl is NumericUpDown num)
                {
                    num.BackColor = bgCtrl;
                    num.ForeColor = fgAccent;
                }
            }
        }

        /// <summary>
        /// Шукає каталог "img" у батьківських папках.
        /// </summary>
        private static string FindImgDirectory()
        {
            string? baseDir = AppDomain.CurrentDomain.BaseDirectory;
            while (!string.IsNullOrEmpty(baseDir))
            {
                string imgPath = Path.Combine(baseDir, "img");
                if (Directory.Exists(imgPath))
                {
                    return imgPath;
                }
                string? parent = Path.GetDirectoryName(baseDir);
                if (parent == baseDir) break;
                baseDir = parent;
            }
            // Резервний варіант в робочій директорії проекту
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "img");
        }

        /// <summary>
        /// Асинхронно завантажує зображення за URL і встановлює його як BackgroundImage елемента.
        /// </summary>
        private static async Task LoadImageToControlAsync(Control ctrl, string url)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Встановлюємо таймаут, щоб не затримувати програму за відсутності мережі
                    client.Timeout = TimeSpan.FromSeconds(6);
                    byte[] data = await client.GetByteArrayAsync(url);
                    
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        Image img = Image.FromStream(ms);
                        
                        // Повертаємо керування в головний UI-потік для оновлення елемента
                        ctrl.BeginInvoke(new Action(() =>
                        {
                            ctrl.BackgroundImage = img;
                            ctrl.BackgroundImageLayout = ImageLayout.Stretch;
                        }));
                    }
                }
            }
            catch
            {
                // Не вдалося завантажити картинку — залишається дефолтний колір
            }
        }

        /// <summary>
        /// Змінює прозорість зображення за допомогою ColorMatrix.
        /// </summary>
        private static Image SetImageOpacity(Image image, float opacity)
        {
            try
            {
                Bitmap bmp = new Bitmap(image.Width, image.Height);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    System.Drawing.Imaging.ColorMatrix matrix = new System.Drawing.Imaging.ColorMatrix();
                    matrix.Matrix33 = opacity; // Значення прозорості за індексом [3, 3]
                    
                    System.Drawing.Imaging.ImageAttributes attributes = new System.Drawing.Imaging.ImageAttributes();
                    attributes.SetColorMatrix(matrix, System.Drawing.Imaging.ColorMatrixFlag.Default, System.Drawing.Imaging.ColorAdjustType.Bitmap);
                    
                    g.DrawImage(image, new Rectangle(0, 0, bmp.Width, bmp.Height), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes);
                }
                return bmp;
            }
            catch
            {
                return image;
            }
        }
    }
}
