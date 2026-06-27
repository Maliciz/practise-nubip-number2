using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace practise_nubip_number2
{
    /// <summary>
    /// Форма пошуку файлів за фільтрами (атрибути, дата, розмір, шаблон імені).
    /// </summary>
    public partial class FormSearch : Form
    {
        // Початкова директорія для рекурсивного пошуку
        private readonly string _startDirectory;

        /// <summary>
        /// Конструктор форми пошуку. Приймає шлях до папки, з якої починати пошук.
        /// </summary>
        /// <param name="startDirectory">Шлях до вибраної папки на головній формі</param>
        public FormSearch(string startDirectory)
        {
            InitializeComponent();
            _startDirectory = startDirectory;
            
            // Встановлюємо початкове значення дати як сьогоднішній день
            dtpDate.Value = DateTime.Today;
        }

        /// <summary>
        /// Обробник натискання кнопки "Пошук".
        /// </summary>
        private void btnFind_Click(object sender, EventArgs e)
        {
            lbSearchResults.Items.Clear();
            btnFind.Enabled = false;
            Cursor = Cursors.WaitCursor;

            try
            {
                if (Directory.Exists(_startDirectory))
                {
                    // Запуск рекурсивного пошуку
                    SearchDirectoryRecursive(_startDirectory);
                }
                else
                {
                    MessageBox.Show("Визначена початкова директорія не існує!", "Помилка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка при ініціалізації пошуку: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnFind.Enabled = true;
                Cursor = Cursors.Default;

                if (lbSearchResults.Items.Count == 0)
                {
                    MessageBox.Show("Файлів за вказаними критеріями не знайдено.", "Результат пошуку", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// Рекурсивний обхід папок та пошук файлів з урахуванням фільтрів.
        /// </summary>
        /// <param name="currentDir">Поточна директорія обходу</param>
        private void SearchDirectoryRecursive(string currentDir)
        {
            string[]? files = null;

            // 1. Отримуємо файли в поточній папці (обробляємо помилку доступу)
            try
            {
                files = Directory.GetFiles(currentDir);
            }
            catch (UnauthorizedAccessException)
            {
                // Пропускаємо папки, до яких немає доступу (наприклад, System Volume Information)
                return;
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }

            if (files != null)
            {
                string pattern = txtPattern.Text.Trim();
                if (string.IsNullOrEmpty(pattern))
                {
                    pattern = "*.*";
                }

                foreach (string filePath in files)
                {
                    try
                    {
                        FileInfo fileInfo = new FileInfo(filePath);

                        // Фільтр 1: Шаблон імені файлу
                        if (!IsFileMatchingPattern(fileInfo.Name, pattern))
                            continue;

                        // Фільтр 2: Атрибут "Тільки для читання"
                        if (chkReadOnly.Checked && (fileInfo.Attributes & FileAttributes.ReadOnly) == 0)
                            continue;

                        // Фільтр 3: Атрибут "Прихований"
                        if (chkHidden.Checked && (fileInfo.Attributes & FileAttributes.Hidden) == 0)
                            continue;

                        // Фільтр 4: Атрибут "Архівний"
                        if (chkArchive.Checked && (fileInfo.Attributes & FileAttributes.Archive) == 0)
                            continue;

                        // Фільтр 5: Дата створення (шукаємо створені у вказаний день або пізніше)
                        if (fileInfo.CreationTime.Date < dtpDate.Value.Date)
                            continue;

                        // Фільтр 6: Максимальний розмір файлу в байтах (0 означає відсутність ліміту)
                        if (numSize.Value > 0 && fileInfo.Length > (long)numSize.Value)
                            continue;

                        // Якщо файл відповідає всім критеріям, додаємо повний шлях до списку
                        lbSearchResults.Items.Add(fileInfo.FullName);
                    }
                    catch (FileNotFoundException)
                    {
                        // Файл був видалений під час сканування
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // Немає доступу до конкретного файлу
                    }
                    catch (Exception)
                    {
                        // Інші непередбачувані помилки для окремого файлу
                    }
                }
            }

            // 2. Рекурсивно заходимо в піддиректорії
            string[]? subDirectories = null;
            try
            {
                subDirectories = Directory.GetDirectories(currentDir);
            }
            catch (UnauthorizedAccessException)
            {
                return;
            }
            catch (Exception)
            {
                return;
            }

            if (subDirectories != null)
            {
                foreach (string subDir in subDirectories)
                {
                    SearchDirectoryRecursive(subDir);
                }
            }
        }

        /// <summary>
        /// Перевіряє, чи ім'я файлу відповідає вказаній масці (наприклад, *.txt, photo?.jpg).
        /// </summary>
        private bool IsFileMatchingPattern(string fileName, string pattern)
        {
            if (pattern == "*.*" || pattern == "*")
                return true;

            try
            {
                // Перетворюємо маску файлу у регулярний вираз
                string regexPattern = "^" + Regex.Escape(pattern)
                                                 .Replace("\\*", ".*")
                                                 .Replace("\\?", ".") + "$";

                return Regex.IsMatch(fileName, regexPattern, RegexOptions.IgnoreCase);
            }
            catch
            {
                // У разі некоректного регулярного виразу
                return false;
            }
        }
    }
}
