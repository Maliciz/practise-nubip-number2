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
            Color textNormal = Color.FromArgb(220, 200, 255);    // Лавандовий
            Color textHighlight = Color.FromArgb(255, 105, 180);   // Рожевий неон (Neon Pink)
            Color textAccent = Color.FromArgb(0, 255, 200);       // Кібер-бірюзовий (Cyber Cyan)
            Color bgControl = Color.FromArgb(14, 6, 22);          // Найтемніший фіолетовий для списків/інпутів

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
            string bgUrl = "";
            string bgFile1 = Path.Combine(imgPath, "background.txt");
            string bgFile2 = Path.Combine(imgPath, "bacground.txt");
            
            if (File.Exists(bgFile1)) bgUrl = File.ReadAllText(bgFile1).Trim();
            if (string.IsNullOrEmpty(bgUrl) && File.Exists(bgFile2)) bgUrl = File.ReadAllText(bgFile2).Trim();

            if (!string.IsNullOrEmpty(bgUrl))
            {
                _ = LoadImageToControlAsync(form, bgUrl);
            }

            // 4. Зчитування URL фону панелей (з Panel.txt)
            string panelUrl = "";
            string panelFile = Path.Combine(imgPath, "Panel.txt");
            if (File.Exists(panelFile)) panelUrl = File.ReadAllText(panelFile).Trim();

            // 5. Рекурсивне застосування стилів до дочірніх елементів
            ApplyToControls(form.Controls, backColor, panelColor, textNormal, textHighlight, textAccent, bgControl, mainFont, titleFont, panelUrl);
        }

        private static void ApplyToControls(Control.ControlCollection controls, Color bg, Color panelBg, Color fg, Color fgHighlight, Color fgAccent, Color bgCtrl, Font font, Font titleFont, string panelUrl)
        {
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
                    grp.BackColor = Color.Transparent; // Робимо прозорим, щоб бачити фон форми
                    ApplyToControls(grp.Controls, bg, panelBg, fg, fgHighlight, fgAccent, bgCtrl, font, titleFont, panelUrl);
                }
                else if (ctrl is Panel pnl)
                {
                    pnl.BackColor = panelBg;
                    if (!string.IsNullOrEmpty(panelUrl))
                    {
                        _ = LoadImageToControlAsync(pnl, panelUrl);
                    }
                    ApplyToControls(pnl.Controls, bg, panelBg, fg, fgHighlight, fgAccent, bgCtrl, font, titleFont, panelUrl);
                }
                else if (ctrl is Label lbl)
                {
                    if (lbl.Name.Contains("Header") || lbl.Name.Contains("Stats"))
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
    }
}
