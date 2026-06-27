namespace practise_nubip_number2;

partial class Form1
{
    private System.ComponentModel.IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        cmbDrives = new ComboBox();
        tvFolders = new TreeView();
        cmbExtensions = new ComboBox();
        pbDiskSpace = new ProgressBar();
        lblDiskSpace = new Label();
        lbFiles = new ListBox();
        grpDetails = new GroupBox();
        lblFileDetails = new Label();
        lblFolderStats = new Label();
        lblInput = new Label();
        txtInput = new TextBox();
        btnChangeName = new Button();
        btnChangeExt = new Button();
        btnRenameFull = new Button();
        btnDelete = new Button();
        btnSearch = new Button();
        lblDrivesHeader = new Label();
        lblFoldersHeader = new Label();
        lblFilesHeader = new Label();
        lblExtHeader = new Label();
        grpDetails.SuspendLayout();
        SuspendLayout();
        // 
        // cmbDrives
        // 
        cmbDrives.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbDrives.Location = new Point(12, 32);
        cmbDrives.Name = "cmbDrives";
        cmbDrives.Size = new Size(276, 23);
        cmbDrives.TabIndex = 1;
        cmbDrives.SelectedIndexChanged += cmbDrives_SelectedIndexChanged;
        // 
        // tvFolders
        // 
        tvFolders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
        tvFolders.Location = new Point(12, 85);
        tvFolders.Name = "tvFolders";
        tvFolders.Size = new Size(276, 310);
        tvFolders.TabIndex = 3;
        tvFolders.BeforeExpand += tvFolders_BeforeExpand;
        tvFolders.AfterSelect += tvFolders_AfterSelect;
        // 
        // cmbExtensions
        // 
        cmbExtensions.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        cmbExtensions.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbExtensions.Location = new Point(12, 425);
        cmbExtensions.Name = "cmbExtensions";
        cmbExtensions.Size = new Size(276, 23);
        cmbExtensions.TabIndex = 5;
        cmbExtensions.SelectedIndexChanged += cmbExtensions_SelectedIndexChanged;
        // 
        // pbDiskSpace
        // 
        pbDiskSpace.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        pbDiskSpace.Location = new Point(12, 460);
        pbDiskSpace.Name = "pbDiskSpace";
        pbDiskSpace.Size = new Size(276, 20);
        pbDiskSpace.TabIndex = 6;
        // 
        // lblDiskSpace
        // 
        lblDiskSpace.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblDiskSpace.Location = new Point(12, 485);
        lblDiskSpace.Name = "lblDiskSpace";
        lblDiskSpace.Size = new Size(276, 35);
        lblDiskSpace.TabIndex = 7;
        lblDiskSpace.Text = "Вільне місце: завантаження...";
        // 
        // lbFiles
        // 
        lbFiles.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lbFiles.FormattingEnabled = true;
        lbFiles.HorizontalScrollbar = true;
        lbFiles.ItemHeight = 15;
        lbFiles.Location = new Point(298, 32);
        lbFiles.Name = "lbFiles";
        lbFiles.Size = new Size(380, 394);
        lbFiles.TabIndex = 9;
        lbFiles.SelectedIndexChanged += lbFiles_SelectedIndexChanged;
        // 
        // grpDetails
        // 
        grpDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
        grpDetails.Controls.Add(lblFileDetails);
        grpDetails.Location = new Point(688, 12);
        grpDetails.Name = "grpDetails";
        grpDetails.Size = new Size(250, 414);
        grpDetails.TabIndex = 10;
        grpDetails.TabStop = false;
        grpDetails.Text = "Параметри файлу";
        // 
        // lblFileDetails
        // 
        lblFileDetails.Dock = DockStyle.Fill;
        lblFileDetails.Location = new Point(3, 19);
        lblFileDetails.Name = "lblFileDetails";
        lblFileDetails.Padding = new Padding(8);
        lblFileDetails.Size = new Size(244, 392);
        lblFileDetails.TabIndex = 0;
        lblFileDetails.Text = "Оберіть файл для перегляду деталей.";
        // 
        // lblFolderStats
        // 
        lblFolderStats.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblFolderStats.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        lblFolderStats.Location = new Point(298, 435);
        lblFolderStats.Name = "lblFolderStats";
        lblFolderStats.Size = new Size(380, 20);
        lblFolderStats.TabIndex = 11;
        lblFolderStats.Text = "Файлів: 0 | Загальний розмір: 0 Б";
        // 
        // lblInput
        // 
        lblInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        lblInput.Location = new Point(298, 460);
        lblInput.Name = "lblInput";
        lblInput.Size = new Size(380, 15);
        lblInput.TabIndex = 12;
        lblInput.Text = "Нове ім'я або розширення:";
        // 
        // txtInput
        // 
        txtInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
        txtInput.Location = new Point(298, 480);
        txtInput.Name = "txtInput";
        txtInput.Size = new Size(640, 23);
        txtInput.TabIndex = 13;
        // 
        // btnChangeName
        // 
        btnChangeName.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnChangeName.Location = new Point(298, 515);
        btnChangeName.Name = "btnChangeName";
        btnChangeName.Size = new Size(110, 30);
        btnChangeName.TabIndex = 14;
        btnChangeName.Text = "Змінити ім'я";
        btnChangeName.UseVisualStyleBackColor = true;
        btnChangeName.Click += btnChangeName_Click;
        // 
        // btnChangeExt
        // 
        btnChangeExt.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnChangeExt.Location = new Point(414, 515);
        btnChangeExt.Name = "btnChangeExt";
        btnChangeExt.Size = new Size(130, 30);
        btnChangeExt.TabIndex = 15;
        btnChangeExt.Text = "Змінити розшир.";
        btnChangeExt.UseVisualStyleBackColor = true;
        btnChangeExt.Click += btnChangeExt_Click;
        // 
        // btnRenameFull
        // 
        btnRenameFull.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnRenameFull.Location = new Point(550, 515);
        btnRenameFull.Name = "btnRenameFull";
        btnRenameFull.Size = new Size(140, 30);
        btnRenameFull.TabIndex = 16;
        btnRenameFull.Text = "Перейменувати";
        btnRenameFull.UseVisualStyleBackColor = true;
        btnRenameFull.Click += btnRenameFull_Click;
        // 
        // btnDelete
        // 
        btnDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnDelete.Location = new Point(696, 515);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(100, 30);
        btnDelete.TabIndex = 17;
        btnDelete.Text = "Вилучити";
        btnDelete.UseVisualStyleBackColor = true;
        btnDelete.Click += btnDelete_Click;
        // 
        // btnSearch
        // 
        btnSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        btnSearch.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
        btnSearch.Location = new Point(822, 515);
        btnSearch.Name = "btnSearch";
        btnSearch.Size = new Size(116, 30);
        btnSearch.TabIndex = 18;
        btnSearch.Text = "Пошук...";
        btnSearch.UseVisualStyleBackColor = true;
        btnSearch.Click += btnSearch_Click;
        // 
        // lblDrivesHeader
        // 
        lblDrivesHeader.AutoSize = true;
        lblDrivesHeader.Location = new Point(12, 14);
        lblDrivesHeader.Name = "lblDrivesHeader";
        lblDrivesHeader.Size = new Size(95, 15);
        lblDrivesHeader.TabIndex = 0;
        lblDrivesHeader.Text = "Фізичний диск:";
        // 
        // lblFoldersHeader
        // 
        lblFoldersHeader.AutoSize = true;
        lblFoldersHeader.Location = new Point(12, 67);
        lblFoldersHeader.Name = "lblFoldersHeader";
        lblFoldersHeader.Size = new Size(101, 15);
        lblFoldersHeader.TabIndex = 2;
        lblFoldersHeader.Text = "Структура папок:";
        // 
        // lblFilesHeader
        // 
        lblFilesHeader.AutoSize = true;
        lblFilesHeader.Location = new Point(298, 14);
        lblFilesHeader.Name = "lblFilesHeader";
        lblFilesHeader.Size = new Size(94, 15);
        lblFilesHeader.TabIndex = 8;
        lblFilesHeader.Text = "Файли у папці:";
        // 
        // lblExtHeader
        // 
        lblExtHeader.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        lblExtHeader.AutoSize = true;
        lblExtHeader.Location = new Point(12, 407);
        lblExtHeader.Name = "lblExtHeader";
        lblExtHeader.Size = new Size(110, 15);
        lblExtHeader.TabIndex = 4;
        lblExtHeader.Text = "Фільтр розширень:";
        // 
        // Form1
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(950, 561);
        Controls.Add(lblExtHeader);
        Controls.Add(lblFilesHeader);
        Controls.Add(lblFoldersHeader);
        Controls.Add(lblDrivesHeader);
        Controls.Add(btnSearch);
        Controls.Add(btnDelete);
        Controls.Add(btnRenameFull);
        Controls.Add(btnChangeExt);
        Controls.Add(btnChangeName);
        Controls.Add(txtInput);
        Controls.Add(lblInput);
        Controls.Add(lblFolderStats);
        Controls.Add(grpDetails);
        Controls.Add(lbFiles);
        Controls.Add(lblDiskSpace);
        Controls.Add(pbDiskSpace);
        Controls.Add(cmbExtensions);
        Controls.Add(tvFolders);
        Controls.Add(cmbDrives);
        Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
        MinimumSize = new Size(966, 600);
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Файловий менеджер";
        Load += Form1_Load;
        grpDetails.ResumeLayout(false);
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private ComboBox cmbDrives;
    private TreeView tvFolders;
    private ComboBox cmbExtensions;
    private ProgressBar pbDiskSpace;
    private Label lblDiskSpace;
    private ListBox lbFiles;
    private GroupBox grpDetails;
    private Label lblFileDetails;
    private Label lblFolderStats;
    private Label lblInput;
    private TextBox txtInput;
    private Button btnChangeName;
    private Button btnChangeExt;
    private Button btnRenameFull;
    private Button btnDelete;
    private Button btnSearch;
    private Label lblDrivesHeader;
    private Label lblFoldersHeader;
    private Label lblFilesHeader;
    private Label lblExtHeader;
}
