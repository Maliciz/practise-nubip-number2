namespace practise_nubip_number2;

partial class FormSearch
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
        pnlFilters = new Panel();
        btnFind = new Button();
        txtPattern = new TextBox();
        lblPattern = new Label();
        numSize = new NumericUpDown();
        lblSize = new Label();
        dtpDate = new DateTimePicker();
        lblDate = new Label();
        chkArchive = new CheckBox();
        chkHidden = new CheckBox();
        chkReadOnly = new CheckBox();
        lbSearchResults = new ListBox();
        pnlFilters.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)numSize).BeginInit();
        SuspendLayout();
        // 
        // pnlFilters
        // 
        pnlFilters.Controls.Add(btnFind);
        pnlFilters.Controls.Add(txtPattern);
        pnlFilters.Controls.Add(lblPattern);
        pnlFilters.Controls.Add(numSize);
        pnlFilters.Controls.Add(lblSize);
        pnlFilters.Controls.Add(dtpDate);
        pnlFilters.Controls.Add(lblDate);
        pnlFilters.Controls.Add(chkArchive);
        pnlFilters.Controls.Add(chkHidden);
        pnlFilters.Controls.Add(chkReadOnly);
        pnlFilters.Dock = DockStyle.Top;
        pnlFilters.Location = new Point(0, 0);
        pnlFilters.Name = "pnlFilters";
        pnlFilters.Size = new Size(584, 180);
        pnlFilters.TabIndex = 0;
        // 
        // btnFind
        // 
        btnFind.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnFind.Location = new Point(447, 23);
        btnFind.Name = "btnFind";
        btnFind.Size = new Size(115, 128);
        btnFind.TabIndex = 10;
        btnFind.Text = "Пошук";
        btnFind.UseVisualStyleBackColor = true;
        btnFind.Click += btnFind_Click;
        // 
        // txtPattern
        // 
        txtPattern.Location = new Point(238, 128);
        txtPattern.Name = "txtPattern";
        txtPattern.Size = new Size(185, 23);
        txtPattern.TabIndex = 9;
        txtPattern.Text = "*.*";
        // 
        // lblPattern
        // 
        lblPattern.AutoSize = true;
        lblPattern.Location = new Point(238, 110);
        lblPattern.Name = "lblPattern";
        lblPattern.Size = new Size(95, 15);
        lblPattern.TabIndex = 8;
        lblPattern.Text = "Шаблон назви:";
        // 
        // numSize
        // 
        numSize.Location = new Point(238, 77);
        numSize.Maximum = new decimal(new int[] { 2147483647, 0, 0, 0 });
        numSize.Name = "numSize";
        numSize.Size = new Size(185, 23);
        numSize.TabIndex = 7;
        // 
        // lblSize
        // 
        lblSize.AutoSize = true;
        lblSize.Location = new Point(238, 59);
        lblSize.Name = "lblSize";
        lblSize.Size = new Size(157, 15);
        lblSize.TabIndex = 6;
        lblSize.Text = "Макс. розмір (байт, 0=всі):";
        // 
        // dtpDate
        // 
        dtpDate.Format = DateTimePickerFormat.Short;
        dtpDate.Location = new Point(238, 26);
        dtpDate.Name = "dtpDate";
        dtpDate.Size = new Size(185, 23);
        dtpDate.TabIndex = 5;
        // 
        // lblDate
        // 
        lblDate.AutoSize = true;
        lblDate.Location = new Point(238, 8);
        lblDate.Name = "lblDate";
        lblDate.Size = new Size(149, 15);
        lblDate.TabIndex = 4;
        lblDate.Text = "Дата створення (з/після):";
        // 
        // chkArchive
        // 
        chkArchive.AutoSize = true;
        chkArchive.Location = new Point(14, 88);
        chkArchive.Name = "chkArchive";
        chkArchive.Size = new Size(81, 19);
        chkArchive.TabIndex = 2;
        chkArchive.Text = "Архівний";
        chkArchive.UseVisualStyleBackColor = true;
        // 
        // chkHidden
        // 
        chkHidden.AutoSize = true;
        chkHidden.Location = new Point(14, 56);
        chkHidden.Name = "chkHidden";
        chkHidden.Size = new Size(88, 19);
        chkHidden.TabIndex = 1;
        chkHidden.Text = "Прихований";
        chkHidden.UseVisualStyleBackColor = true;
        // 
        // chkReadOnly
        // 
        chkReadOnly.AutoSize = true;
        chkReadOnly.Location = new Point(14, 25);
        chkReadOnly.Name = "chkReadOnly";
        chkReadOnly.Size = new Size(125, 19);
        chkReadOnly.TabIndex = 0;
        chkReadOnly.Text = "Тільки для читання";
        chkReadOnly.UseVisualStyleBackColor = true;
        // 
        // lbSearchResults
        // 
        lbSearchResults.Dock = DockStyle.Fill;
        lbSearchResults.FormattingEnabled = true;
        lbSearchResults.HorizontalScrollbar = true;
        lbSearchResults.ItemHeight = 15;
        lbSearchResults.Location = new Point(0, 180);
        lbSearchResults.Name = "lbSearchResults";
        lbSearchResults.Size = new Size(584, 281);
        lbSearchResults.TabIndex = 1;
        // 
        // FormSearch
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(584, 461);
        Controls.Add(lbSearchResults);
        Controls.Add(pnlFilters);
        MinimumSize = new Size(600, 500);
        Name = "FormSearch";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Пошук файлів";
        pnlFilters.ResumeLayout(false);
        pnlFilters.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)numSize).EndInit();
        ResumeLayout(false);
    }

    #endregion

    private Panel pnlFilters;
    private CheckBox chkArchive;
    private CheckBox chkHidden;
    private CheckBox chkReadOnly;
    private Label lblDate;
    private DateTimePicker dtpDate;
    private Label lblSize;
    private NumericUpDown numSize;
    private Label lblPattern;
    private TextBox txtPattern;
    private Button btnFind;
    private ListBox lbSearchResults;
}
