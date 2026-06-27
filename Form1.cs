using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace practise_nubip_number2
{
    /// <summary>
    /// Головна форма додатку "Файловий менеджер".
    /// </summary>
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            ThemeHelper.Apply2000sAnimeTheme(this);
            this.Text = "✧ File-Manager v1.0.4 ✧";

            lblDiskSpace.Text = "(｀・ω・´) Вільний простір: очікування...";
            lblFileDetails.Text = "(=^･^=) обери файл для перегляду деталей.";

            RefreshDrivesList();
        }


        private void RefreshDrivesList()
        {
            cmbDrives.Items.Clear();
            try
            {
                DriveInfo[] drives = DriveInfo.GetDrives();
                foreach (DriveInfo drive in drives)
                {
                    // Додаємо лише ті диски, які готові до роботи (мають медіа)
                    if (drive.IsReady)
                    {
                        cmbDrives.Items.Add(drive.Name);
                    }
                }

                if (cmbDrives.Items.Count > 0)
                {
                    cmbDrives.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Не знайдено доступних дисків у системі!", "Попередження", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні дисків: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Вибір диска: оновлення деревоподібної структури папок та обсягу вільного місця.
        /// </summary>
        private void cmbDrives_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDrives.SelectedItem == null) return;

            string? selectedDrive = cmbDrives.SelectedItem.ToString();
            if (string.IsNullOrEmpty(selectedDrive)) return;
            
            // 1. Оновлення інформації про обсяг пам'яті
            UpdateDiskSpaceIndicator(selectedDrive);

            // 2. Оновлення ієрархії папок у tvFolders (ліниве завантаження)
            UpdateFoldersTree(selectedDrive);
        }

        /// <summary>
        /// Оновлює індикатор Used/Free Space на основі обраного диска.
        /// </summary>
        private void UpdateDiskSpaceIndicator(string driveName)
        {
            try
            {
                DriveInfo drive = new DriveInfo(driveName);
                if (drive.IsReady)
                {
                    long totalSize = drive.TotalSize;
                    long freeSpace = drive.TotalFreeSpace;
                    long usedSpace = totalSize - freeSpace;

                    // Обчислення відсотка використаного місця
                    double percentUsed = ((double)usedSpace / totalSize) * 100;
                    pbDiskSpace.Value = (int)percentUsed;

                    lblDiskSpace.Text = $"Вільне місце: {FormatBytes(freeSpace)}\nЗагальний обсяг: {FormatBytes(totalSize)}";
                }
                else
                {
                    pbDiskSpace.Value = 0;
                    lblDiskSpace.Text = "Диск не готовий";
                }
            }
            catch (Exception ex)
            {
                pbDiskSpace.Value = 0;
                lblDiskSpace.Text = "Помилка отримання даних";
                MessageBox.Show($"Помилка отримання інформації про диск: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ініціалізує кореневий вузол дерева папок для обраного диска.
        /// </summary>
        private void UpdateFoldersTree(string drivePath)
        {
            tvFolders.Nodes.Clear();
            try
            {
                TreeNode rootNode = new TreeNode(drivePath)
                {
                    Tag = drivePath
                };
                
                // Додаємо пустий вузол-заглушку, щоб вузол можна було розгорнути
                rootNode.Nodes.Add(new TreeNode() { Tag = "DUMMY" });
                tvFolders.Nodes.Add(rootNode);

                // Розгортаємо кореневу директорію
                rootNode.Expand();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка ініціалізації дерева папок: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Подія перед розгортанням вузла дерева: реалізує ліниве завантаження підпапок.
        /// </summary>
        private void tvFolders_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode? parentNode = e.Node;
            if (parentNode == null) return;
            
            // Якщо перший дочірній елемент є заглушкою "DUMMY", виконуємо завантаження
            if (parentNode.Nodes.Count == 1 && parentNode.Nodes[0]?.Tag != null && parentNode.Nodes[0].Tag?.ToString() == "DUMMY")
            {
                parentNode.Nodes.Clear();
                string? path = parentNode.Tag?.ToString();
                if (string.IsNullOrEmpty(path)) return;

                try
                {
                    // Зчитуємо піддиректорії
                    string[] subDirs = Directory.GetDirectories(path);
                    foreach (string dir in subDirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode childNode = new TreeNode(di.Name)
                        {
                            Tag = di.FullName
                        };
                        
                        // Додаємо заглушку для підпапки
                        childNode.Nodes.Add(new TreeNode() { Tag = "DUMMY" });
                        parentNode.Nodes.Add(childNode);
                    }
                }
                catch (UnauthorizedAccessException)
                {
                    // Пропускаємо папки, до яких користувач не має доступу
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка зчитування папки: {ex.Message}", "Помилка", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Подія після вибору папки: розрахунок статистики та оновлення файлів.
        /// </summary>
        private void tvFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == null || e.Node.Tag == null) return;

            string? selectedPath = e.Node.Tag.ToString();
            if (string.IsNullOrEmpty(selectedPath)) return;
            
            // Очищення полів деталей та вводу
            lblFileDetails.Text = "Оберіть файл для перегляду деталей.";
            txtInput.Text = string.Empty;

            // 1. Рахуємо статистику (кількість файлів та сумарний розмір)
            CalculateFolderStats(selectedPath);

            // 2. Заповнюємо cmbExtensions унікальними розширеннями файлів
            PopulateExtensionsFilter(selectedPath);

            // 3. Відображаємо файли на основі фільтра розширень
            RefreshFilesList();
        }

        /// <summary>
        /// Розраховує кількість та сумарний розмір файлів безпосередньо у папці.
        /// </summary>
        private void CalculateFolderStats(string path)
        {
            int fileCount = 0;
            long totalSize = 0;

            try
            {
                if (Directory.Exists(path))
                {
                    string[] files = Directory.GetFiles(path);
                    fileCount = files.Length;

                    foreach (string file in files)
                    {
                        try
                        {
                            FileInfo fi = new FileInfo(file);
                            totalSize += fi.Length;
                        }
                        catch (FileNotFoundException) { } // Файл міг бути видалений
                        catch (UnauthorizedAccessException) { }
                    }

                    lblFolderStats.Text = $"📁 Файлів: {fileCount} | Обсяг: {FormatBytes(totalSize)} (✿◠‿◠)";
                }
            }
            catch (UnauthorizedAccessException)
            {
                lblFolderStats.Text = "Доступ обмежено! (＃`Д´)";
            }
            catch (Exception ex)
            {
                lblFolderStats.Text = "Сталася помилка";
                MessageBox.Show($"Не вдалося отримати статистику папки: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Заповнює ComboBox фільтра розширень унікальними розширеннями із вибраної папки.
        /// </summary>
        private void PopulateExtensionsFilter(string folderPath)
        {
            cmbExtensions.Items.Clear();
            cmbExtensions.Items.Add("*.*"); // Варіант за замовчуванням (Всі файли)

            try
            {
                if (Directory.Exists(folderPath))
                {
                    var files = Directory.GetFiles(folderPath);
                    var extensions = files
                        .Select(f => Path.GetExtension(f).ToLower())
                        .Where(ext => !string.IsNullOrEmpty(ext))
                        .Distinct()
                        .OrderBy(ext => ext)
                        .Select(ext => $"*{ext}")
                        .ToArray();

                    foreach (var ext in extensions)
                    {
                        cmbExtensions.Items.Add(ext);
                    }
                }
            }
            catch (UnauthorizedAccessException) { }
            catch (Exception) { }

            // Встановлюємо перший елемент (*.*) як активний
            cmbExtensions.SelectedIndex = 0;
        }

        /// <summary>
        /// Відображає файли у списку ListBox відповідно до вибраного розширення.
        /// </summary>
        private void RefreshFilesList()
        {
            lbFiles.Items.Clear();

            if (tvFolders.SelectedNode == null || tvFolders.SelectedNode.Tag == null)
                return;

            string? folderPath = tvFolders.SelectedNode.Tag.ToString();
            if (string.IsNullOrEmpty(folderPath))
                return;
            string selectedFilter = cmbExtensions.SelectedItem?.ToString() ?? "*.*";

            try
            {
                if (Directory.Exists(folderPath))
                {
                    string[] allFiles = Directory.GetFiles(folderPath);

                    foreach (string file in allFiles)
                    {
                        string ext = Path.GetExtension(file).ToLower();

                        // Якщо обрано "*.*" або розширення файлу збігається з маскою фільтра
                        if (selectedFilter == "*.*" || selectedFilter.EndsWith(ext))
                        {
                            lbFiles.Items.Add(new FileItem(file));
                        }
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Відсутній доступ до файлів цієї директорії!", "Помилка доступу", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при завантаженні файлів: {ex.Message}", "Помилка", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Подія зміни фільтра розширень.
        /// </summary>
        private void cmbExtensions_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshFilesList();
        }

        /// <summary>
        /// Відображає детальну інформацію про вибраний файл.
        /// </summary>
        private void lbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbFiles.SelectedItem is FileItem selectedFile)
            {
                try
                {
                    FileInfo fi = new FileInfo(selectedFile.FullPath);

                    if (fi.Exists)
                    {
                        lblFileDetails.Text = 
                            $"Ім'я:\n{fi.Name} (✿◠‿◠)\n\n" +
                            $"Повний шлях:\n{fi.FullName}\n\n" +
                            $"Розмір:\n{FormatBytes(fi.Length)} ({fi.Length} байт)\n\n" +
                            $"Створено:\n{fi.CreationTime:dd.MM.yyyy HH:mm:ss}\n\n" +
                            $"Змінено:\n{fi.LastWriteTime:dd.MM.yyyy HH:mm:ss}\n\n" +
                            $"Атрибути:\n" +
                            $"{( (fi.Attributes & FileAttributes.ReadOnly) != 0 ? "☑ Тільки для читання (ReadOnly)" : "☐ Тільки для читання (ReadOnly)" )}\n" +
                            $"{( (fi.Attributes & FileAttributes.Hidden) != 0 ? "☑ Прихований (Hidden)" : "☐ Прихований (Hidden)" )}\n" +
                            $"{( (fi.Attributes & FileAttributes.Archive) != 0 ? "☑ Архівний (Archive)" : "☐ Архівний (Archive)" )}";
                        
                        // Записуємо поточне ім'я файлу в текстове поле для зручного редагування
                        txtInput.Text = Path.GetFileNameWithoutExtension(fi.Name);
                    }
                    else
                    {
                        lblFileDetails.Text = "(✖﹏✖) Файл не існує.";
                    }
                }
                catch (Exception ex)
                {
                    lblFileDetails.Text = $"(＞﹏＜) Не вдалося отримати деталі файлу:\n{ex.Message}";
                }
            }
            else
            {
                lblFileDetails.Text = "(=^･^=) обери файл для перегляду деталей.";
            }
        }

        /// <summary>
        /// Обробник кнопки "Змінити ім'я" (змінює назву файлу, зберігаючи поточне розширення).
        /// </summary>
        private void btnChangeName_Click(object sender, EventArgs e)
        {
            if (!(lbFiles.SelectedItem is FileItem selectedFile))
            {
                MessageBox.Show("Оберіть файл для перейменування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newName = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(newName))
            {
                MessageBox.Show("Введіть нове ім'я файлу!", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string? directory = Path.GetDirectoryName(selectedFile.FullPath);
                if (directory == null) return;

                string ext = Path.GetExtension(selectedFile.FullPath);
                string newFullPath = Path.Combine(directory, newName + ext);

                if (File.Exists(newFullPath))
                {
                    MessageBox.Show("Файл з такою назвою вже існує в цій папці!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.Move(selectedFile.FullPath, newFullPath);
                RefreshCurrentFolderAfterOperation();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Немає прав доступу для перейменування файлу!", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час перейменування: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обробник кнопки "Змінити розширення" (змінює розширення файлу, зберігаючи поточну назву).
        /// </summary>
        private void btnChangeExt_Click(object sender, EventArgs e)
        {
            if (!(lbFiles.SelectedItem is FileItem selectedFile))
            {
                MessageBox.Show("Оберіть файл для зміни розширення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newExt = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(newExt))
            {
                MessageBox.Show("Введіть нове розширення!", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Додаємо крапку до розширення, якщо її немає
            if (!newExt.StartsWith("."))
            {
                newExt = "." + newExt;
            }

            try
            {
                string? directory = Path.GetDirectoryName(selectedFile.FullPath);
                if (directory == null) return;

                string nameWithoutExt = Path.GetFileNameWithoutExtension(selectedFile.FullPath);
                string newFullPath = Path.Combine(directory, nameWithoutExt + newExt);

                if (File.Exists(newFullPath))
                {
                    MessageBox.Show("Файл з таким розширенням вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.Move(selectedFile.FullPath, newFullPath);
                RefreshCurrentFolderAfterOperation();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Немає прав доступу для зміни розширення!", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час зміни розширення: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обробник кнопки "Перейменувати повністю" (змінює ім'я разом із розширенням).
        /// </summary>
        private void btnRenameFull_Click(object sender, EventArgs e)
        {
            if (!(lbFiles.SelectedItem is FileItem selectedFile))
            {
                MessageBox.Show("Оберіть файл для перейменування!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newFullName = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(newFullName))
            {
                MessageBox.Show("Введіть повне ім'я файлу (з розширенням)!", "Помилка валідації", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string? directory = Path.GetDirectoryName(selectedFile.FullPath);
                if (directory == null) return;

                string newFullPath = Path.Combine(directory, newFullName);

                if (File.Exists(newFullPath))
                {
                    MessageBox.Show("Файл з вказаним повним ім'ям вже існує!", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.Move(selectedFile.FullPath, newFullPath);
                RefreshCurrentFolderAfterOperation();
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Немає прав доступу для перейменування файлу!", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка під час перейменування: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обробник кнопки "Вилучити" (видаляє вибраний файл після підтвердження).
        /// </summary>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!(lbFiles.SelectedItem is FileItem selectedFile))
            {
                MessageBox.Show("(=^･^=)спочатку обери файл для видалення!", "Увага", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string fileName = Path.GetFileName(selectedFile.FullPath);
            var confirmResult = MessageBox.Show($"ти дійсно бажаєш безповоротно видалити файл \"{fileName}\"? (＞﹏＜)",
                "Підтвердження видалення", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmResult == DialogResult.Yes)
            {
                try
                {
                    File.Delete(selectedFile.FullPath);
                    RefreshCurrentFolderAfterOperation();
                }
                catch (UnauthorizedAccessException)
                {
                    MessageBox.Show("(＃`Д´) Немає прав доступу для видалення файлу!", "Помилка доступу", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка при видаленні файлу: {ex.Message}", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// Обробник кнопки "Пошук...": відкриває вікно пошуку файлів у поточному каталозі.
        /// </summary>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (tvFolders.SelectedNode == null || tvFolders.SelectedNode.Tag == null)
            {
                MessageBox.Show("(=^･^=) обери папку для пошуку у лівій панелі!", "Попередження", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string? selectedFolder = tvFolders.SelectedNode.Tag.ToString();
            if (string.IsNullOrEmpty(selectedFolder)) return;
            
            // Створюємо та відкриваємо модальне вікно пошуку
            using (FormSearch searchForm = new FormSearch(selectedFolder))
            {
                searchForm.ShowDialog(this);
            }
        }

        /// <summary>
        /// Оновлює інтерфейс після операцій з файлами (видалення або перейменування).
        /// </summary>
        private void RefreshCurrentFolderAfterOperation()
        {
            if (tvFolders.SelectedNode == null || tvFolders.SelectedNode.Tag == null) return;
            
            string? path = tvFolders.SelectedNode.Tag.ToString();
            if (string.IsNullOrEmpty(path)) return;
            
            txtInput.Text = string.Empty;
            lblFileDetails.Text = "Оберіть файл для перегляду деталей.";
            
            CalculateFolderStats(path);
            PopulateExtensionsFilter(path);
            RefreshFilesList();
        }

        /// <summary>
        /// Хелпер для перетворення байтів у зрозумілий для користувача формат (Б, КБ, МБ, ГБ).
        /// </summary>
        private string FormatBytes(long bytes)
        {
            string[] suffix = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            double formattedSize = bytes;
            int counter = 0;

            while (Math.Round(formattedSize / 1024) >= 1)
            {
                formattedSize /= 1024;
                counter++;
                if (counter >= suffix.Length - 1) break;
            }

            return $"{formattedSize:F2} {suffix[counter]}";
        }
    }

    /// <summary>
    /// Допоміжний клас для представлення файлу у списку ListBox.
    /// Відображає лише коротке ім'я файлу, але зберігає повний шлях.
    /// </summary>
    public class FileItem
    {
        public string FullPath { get; }
        public string Name => Path.GetFileName(FullPath);

        public FileItem(string fullPath)
        {
            FullPath = fullPath;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
