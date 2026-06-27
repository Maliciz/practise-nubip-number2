# Інструкція з налаштування інтерфейсу (UI Setup Instructions)

Цей документ містить опис елементів управління (Controls) та їхніх властивостей для створення візуального інтерфейсу у конструкторі Visual Studio (Windows Forms Designer) відповідно до вимог класичного макета Delphi/C++Builder.

---

## Головна форма (`Form1`)

Встановіть наступні властивості для самої форми:
- **Name**: `Form1`
- **Text**: `Файловий менеджер`
- **Font**: `Segoe UI; 9pt`
- **MinimumSize**: `966, 600`
- **StartPosition**: `CenterScreen`

### Елементи управління (Controls):

| Контрол (Тип) | Ім'я (`Name`) | Опис та важливі властивості |
| :--- | :--- | :--- |
| **Label** | `lblDrivesHeader` | Text: `Фізичний диск:`, Location: `12, 14` |
| **ComboBox** | `cmbDrives` | DropDownStyle: `DropDownList`, Location: `12, 32`, Size: `276, 23` |
| **Label** | `lblFoldersHeader` | Text: `Структура папок:`, Location: `12, 67` |
| **TreeView** | `tvFolders` | Location: `12, 85`, Size: `276, 310`, Anchor: `Top, Bottom, Left` |
| **Label** | `lblExtHeader` | Text: `Фільтр розширень:`, Location: `12, 407`, Anchor: `Bottom, Left` |
| **ComboBox** | `cmbExtensions` | DropDownStyle: `DropDownList`, Location: `12, 425`, Size: `276, 23`, Anchor: `Bottom, Left` |
| **ProgressBar** | `pbDiskSpace` | Location: `12, 460`, Size: `276, 20`, Anchor: `Bottom, Left` |
| **Label** | `lblDiskSpace` | Text: `Вільне місце...`, Location: `12, 485`, Size: `276, 35`, Anchor: `Bottom, Left` |
| **Label** | `lblFilesHeader` | Text: `Файли у папці:`, Location: `298, 14` |
| **ListBox** | `lbFiles` | Location: `298, 32`, Size: `380, 394`, Anchor: `Top, Bottom, Left, Right`, HorizontalScrollbar: `True` |
| **Label** | `lblFolderStats` | Text: `Файлів: 0 \| Загальний розмір...`, Location: `298, 435`, Size: `380, 20`, Anchor: `Bottom, Left, Right`, Font: `Segoe UI; 9pt; Bold` |
| **GroupBox** | `grpDetails` | Text: `Параметри файлу`, Location: `688, 12`, Size: `250, 414`, Anchor: `Top, Bottom, Right` |
| **Label** | `lblFileDetails` | *(розмістити всередині `grpDetails`)*, Dock: `Fill`, Text: `Оберіть файл...`, Padding: `8` |
| **Label** | `lblInput` | Text: `Нове ім'я або розширення:`, Location: `298, 460`, Size: `380, 15`, Anchor: `Bottom, Left, Right` |
| **TextBox** | `txtInput` | Location: `298, 480`, Size: `640, 23`, Anchor: `Bottom, Left, Right` |
| **Button** | `btnChangeName` | Text: `Змінити ім'я`, Location: `298, 515`, Size: `110, 30`, Anchor: `Bottom, Left` |
| **Button** | `btnChangeExt` | Text: `Змінити розшир.`, Location: `414, 515`, Size: `130, 30`, Anchor: `Bottom, Left` |
| **Button** | `btnRenameFull` | Text: `Перейменувати`, Location: `550, 515`, Size: `140, 30`, Anchor: `Bottom, Left` |
| **Button** | `btnDelete` | Text: `Вилучити`, Location: `696, 515`, Size: `100, 30`, Anchor: `Bottom, Left` |
| **Button** | `btnSearch` | Text: `Пошук...`, Location: `822, 515`, Size: `116, 30`, Anchor: `Bottom, Right`, Font: `Segoe UI; 9pt; Bold` |

---

## Форма пошуку (`FormSearch`)

Встановіть наступні властивості для форми:
- **Name**: `FormSearch`
- **Text**: `Пошук файлів`
- **Font**: `Segoe UI; 9pt`
- **MinimumSize**: `600, 500`
- **StartPosition**: `CenterParent` *(для гарного вирівнювання щодо головного вікна)*

### Елементи управління (Controls):

1. **Panel** (під назвою `pnlFilters`):
   - Dock: `Top`
   - Height: `180`
   
   *Розмістіть наступні елементи всередині `pnlFilters`:*
   - **CheckBox** `chkReadOnly`: Text: `Тільки для читання`, Location: `14, 25`
   - **CheckBox** `chkHidden`: Text: `Прихований`, Location: `14, 56`
   - **CheckBox** `chkArchive`: Text: `Архівний`, Location: `14, 88`
   - **Label** `lblDate`: Text: `Дата створення (з/після):`, Location: `238, 8`
   - **DateTimePicker** `dtpDate`: Format: `Short`, Location: `238, 26`, Size: `185, 23`
   - **Label** `lblSize`: Text: `Макс. розмір (байт, 0=всі):`, Location: `238, 59`
   - **NumericUpDown** `numSize`: Maximum: `2147483647`, Location: `238, 77`, Size: `185, 23`
   - **Label** `lblPattern`: Text: `Шаблон назви:`, Location: `238, 110`
   - **TextBox** `txtPattern`: Text: `*.*`, Location: `238, 128`, Size: `185, 23`
   - **Button** `btnFind`: Text: `Пошук`, Location: `447, 23`, Size: `115, 128`, Anchor: `Top, Right`

2. **ListBox** (під назвою `lbSearchResults`):
   - Dock: `Fill`
   - HorizontalScrollbar: `True`
