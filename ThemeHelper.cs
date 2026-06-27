using System;
using System.Drawing;
using System.Windows.Forms;

namespace practise_nubip_number2
{
    /// <summary>
    /// Допоміжний клас для застосування стилізації у стилі аніме-кіберпанку 2000-х (на зразок Serial Experiments Lain, Winamp).
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
            ApplyToControls(form.Controls, backColor, panelColor, textNormal, textHighlight, textAccent, bgControl, mainFont, titleFont);
        }

        private static void ApplyToControls(Control.ControlCollection controls, Color bg, Color panelBg, Color fg, Color fgHighlight, Color fgAccent, Color bgCtrl, Font font, Font titleFont)
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
                    grp.BackColor = bg;
                    ApplyToControls(grp.Controls, bg, panelBg, fg, fgHighlight, fgAccent, bgCtrl, font, titleFont);
                }
                else if (ctrl is Panel pnl)
                {
                    pnl.BackColor = panelBg;
                    ApplyToControls(pnl.Controls, bg, panelBg, fg, fgHighlight, fgAccent, bgCtrl, font, titleFont);
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
                }
                else if (ctrl is CheckBox chk)
                {
                    chk.ForeColor = fg;
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
    }
}
