//namespace SimilarImage.Ui
//{
//    partial class MainForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.components = new System.ComponentModel.Container();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
//            this.dbRecordCountLabel = new System.Windows.Forms.Label();
//            this.label2 = new System.Windows.Forms.Label();
//            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
//            this.imageSetsListbox = new System.Windows.Forms.ListBox();
//            this.imageSetsGroupBox = new System.Windows.Forms.GroupBox();
//            this.dataBaseGroupBox = new System.Windows.Forms.GroupBox();
//            this.cleanupActionsGroupBox = new System.Windows.Forms.GroupBox();
//            this.cleanSetsButton = new System.Windows.Forms.Button();
//            this.cleanMissingButton = new System.Windows.Forms.Button();
//            this.confirmCleanupCheckbox = new System.Windows.Forms.CheckBox();
//            this.moveDuplicatesButton = new System.Windows.Forms.Button();
//            this.selectedImageActionsGroupBox = new System.Windows.Forms.GroupBox();
//            this.isDuplicateButton = new System.Windows.Forms.Button();
//            this.clearDuplicateButton = new System.Windows.Forms.Button();
//            this.similarityParametersGroupBox = new System.Windows.Forms.GroupBox();
//            this.label3 = new System.Windows.Forms.Label();
//            this.similarityThresholdnumericUpDown = new System.Windows.Forms.NumericUpDown();
//            this.extraSetAnalysisCheckbox = new System.Windows.Forms.CheckBox();
//            this.similarityRangePercentagenumericUpDown = new System.Windows.Forms.NumericUpDown();
//            this.analyseButton = new System.Windows.Forms.Button();
//            this.imageDetailPropertyGrid = new System.Windows.Forms.PropertyGrid();
//            this.imagesGridView = new System.Windows.Forms.DataGridView();
//            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
//            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
//            this.ScanGroupBox = new System.Windows.Forms.GroupBox();
//            this.clearDatabaseCheckbox = new System.Windows.Forms.CheckBox();
//            this.stopButton = new System.Windows.Forms.Button();
//            this.progressGroupBox = new System.Windows.Forms.GroupBox();
//            this.progressLabel = new System.Windows.Forms.Label();
//            this.progressBar1 = new System.Windows.Forms.ProgressBar();
//            this.startbutton = new System.Windows.Forms.Button();
//            this.label1 = new System.Windows.Forms.Label();
//            this.imagesDirectoryTextBox = new System.Windows.Forms.TextBox();
//            this.selectDirectoryButton = new System.Windows.Forms.Button();
//            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
//            this.imageResultsGroupbox = new System.Windows.Forms.GroupBox();
//            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
//            this.imageSetsGroupBox.SuspendLayout();
//            this.dataBaseGroupBox.SuspendLayout();
//            this.cleanupActionsGroupBox.SuspendLayout();
//            this.selectedImageActionsGroupBox.SuspendLayout();
//            this.similarityParametersGroupBox.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.similarityThresholdnumericUpDown)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.similarityRangePercentagenumericUpDown)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.imagesGridView)).BeginInit();
//            this.statusStrip1.SuspendLayout();
//            this.ScanGroupBox.SuspendLayout();
//            this.progressGroupBox.SuspendLayout();
//            this.tableLayoutPanel1.SuspendLayout();
//            this.imageResultsGroupbox.SuspendLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // dbRecordCountLabel
//            // 
//            this.dbRecordCountLabel.AutoSize = true;
//            this.dbRecordCountLabel.Location = new System.Drawing.Point(12, 43);
//            this.dbRecordCountLabel.Name = "dbRecordCountLabel";
//            this.dbRecordCountLabel.Size = new System.Drawing.Size(61, 13);
//            this.dbRecordCountLabel.TabIndex = 6;
//            this.dbRecordCountLabel.Text = "DbRecords";
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(63, 22);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(82, 13);
//            this.label2.TabIndex = 9;
//            this.label2.Text = "Similarity Range";
//            // 
//            // imageSetsListbox
//            // 
//            this.imageSetsListbox.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.imageSetsListbox.FormattingEnabled = true;
//            this.imageSetsListbox.Location = new System.Drawing.Point(3, 16);
//            this.imageSetsListbox.Name = "imageSetsListbox";
//            this.imageSetsListbox.Size = new System.Drawing.Size(237, 476);
//            this.imageSetsListbox.TabIndex = 6;
//            this.imageSetsListbox.SelectedIndexChanged += new System.EventHandler(this.imageSetsListbox_SelectedIndexChanged);
//            // 
//            // imageSetsGroupBox
//            // 
//            this.imageSetsGroupBox.AutoSize = true;
//            this.imageSetsGroupBox.Controls.Add(this.dataBaseGroupBox);
//            this.imageSetsGroupBox.Controls.Add(this.imageSetsListbox);
//            this.imageSetsGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.imageSetsGroupBox.Location = new System.Drawing.Point(3, 183);
//            this.imageSetsGroupBox.Name = "imageSetsGroupBox";
//            this.tableLayoutPanel1.SetRowSpan(this.imageSetsGroupBox, 2);
//            this.imageSetsGroupBox.Size = new System.Drawing.Size(243, 495);
//            this.imageSetsGroupBox.TabIndex = 7;
//            this.imageSetsGroupBox.TabStop = false;
//            this.imageSetsGroupBox.Text = "Image Sets";
//            // 
//            // dataBaseGroupBox
//            // 
//            this.dataBaseGroupBox.Controls.Add(this.cleanupActionsGroupBox);
//            this.dataBaseGroupBox.Controls.Add(this.selectedImageActionsGroupBox);
//            this.dataBaseGroupBox.Controls.Add(this.similarityParametersGroupBox);
//            this.dataBaseGroupBox.Location = new System.Drawing.Point(46, 88);
//            this.dataBaseGroupBox.Name = "dataBaseGroupBox";
//            this.dataBaseGroupBox.Size = new System.Drawing.Size(1120, 84);
//            this.dataBaseGroupBox.TabIndex = 13;
//            this.dataBaseGroupBox.TabStop = false;
//            this.dataBaseGroupBox.Text = "Database";
//            this.dataBaseGroupBox.Visible = false;
//            // 
//            // cleanupActionsGroupBox
//            // 
//            this.cleanupActionsGroupBox.Controls.Add(this.cleanSetsButton);
//            this.cleanupActionsGroupBox.Controls.Add(this.cleanMissingButton);
//            this.cleanupActionsGroupBox.Controls.Add(this.confirmCleanupCheckbox);
//            this.cleanupActionsGroupBox.Controls.Add(this.moveDuplicatesButton);
//            this.cleanupActionsGroupBox.Location = new System.Drawing.Point(629, 11);
//            this.cleanupActionsGroupBox.Name = "cleanupActionsGroupBox";
//            this.cleanupActionsGroupBox.Size = new System.Drawing.Size(344, 67);
//            this.cleanupActionsGroupBox.TabIndex = 16;
//            this.cleanupActionsGroupBox.TabStop = false;
//            this.cleanupActionsGroupBox.Text = "Cleanup Actions";
//            // 
//            // cleanSetsButton
//            // 
//            this.cleanSetsButton.Location = new System.Drawing.Point(246, 19);
//            this.cleanSetsButton.Name = "cleanSetsButton";
//            this.cleanSetsButton.Size = new System.Drawing.Size(65, 34);
//            this.cleanSetsButton.TabIndex = 18;
//            this.cleanSetsButton.Tag = "True";
//            this.cleanSetsButton.Text = "Clean Sets";
//            this.cleanSetsButton.UseVisualStyleBackColor = true;
//            this.cleanSetsButton.Click += new System.EventHandler(this.cleanSetsButton_Click);
//            // 
//            // cleanMissingButton
//            // 
//            this.cleanMissingButton.Location = new System.Drawing.Point(158, 19);
//            this.cleanMissingButton.Name = "cleanMissingButton";
//            this.cleanMissingButton.Size = new System.Drawing.Size(65, 34);
//            this.cleanMissingButton.TabIndex = 17;
//            this.cleanMissingButton.Tag = "True";
//            this.cleanMissingButton.Text = "Clean Missing Files";
//            this.cleanMissingButton.UseVisualStyleBackColor = true;
//            this.cleanMissingButton.Click += new System.EventHandler(this.cleanMissingButton_Click);
//            // 
//            // confirmCleanupCheckbox
//            // 
//            this.confirmCleanupCheckbox.AutoSize = true;
//            this.confirmCleanupCheckbox.Location = new System.Drawing.Point(91, 23);
//            this.confirmCleanupCheckbox.Name = "confirmCleanupCheckbox";
//            this.confirmCleanupCheckbox.Size = new System.Drawing.Size(61, 17);
//            this.confirmCleanupCheckbox.TabIndex = 16;
//            this.confirmCleanupCheckbox.Text = "Confirm";
//            this.confirmCleanupCheckbox.UseVisualStyleBackColor = true;
//            // 
//            // moveDuplicatesButton
//            // 
//            this.moveDuplicatesButton.Location = new System.Drawing.Point(20, 19);
//            this.moveDuplicatesButton.Name = "moveDuplicatesButton";
//            this.moveDuplicatesButton.Size = new System.Drawing.Size(65, 34);
//            this.moveDuplicatesButton.TabIndex = 15;
//            this.moveDuplicatesButton.Tag = "True";
//            this.moveDuplicatesButton.Text = "Move Duplicates";
//            this.moveDuplicatesButton.UseVisualStyleBackColor = true;
//            this.moveDuplicatesButton.Click += new System.EventHandler(this.moveDuplicatesButton_Click);
//            // 
//            // selectedImageActionsGroupBox
//            // 
//            this.selectedImageActionsGroupBox.Controls.Add(this.isDuplicateButton);
//            this.selectedImageActionsGroupBox.Controls.Add(this.clearDuplicateButton);
//            this.selectedImageActionsGroupBox.Location = new System.Drawing.Point(421, 11);
//            this.selectedImageActionsGroupBox.Name = "selectedImageActionsGroupBox";
//            this.selectedImageActionsGroupBox.Size = new System.Drawing.Size(201, 67);
//            this.selectedImageActionsGroupBox.TabIndex = 15;
//            this.selectedImageActionsGroupBox.TabStop = false;
//            this.selectedImageActionsGroupBox.Text = "Selected image actions";
//            // 
//            // isDuplicateButton
//            // 
//            this.isDuplicateButton.Location = new System.Drawing.Point(19, 19);
//            this.isDuplicateButton.Name = "isDuplicateButton";
//            this.isDuplicateButton.Size = new System.Drawing.Size(64, 34);
//            this.isDuplicateButton.TabIndex = 13;
//            this.isDuplicateButton.Tag = "True";
//            this.isDuplicateButton.Text = "Duplicate";
//            this.isDuplicateButton.UseVisualStyleBackColor = true;
//            this.isDuplicateButton.Click += new System.EventHandler(this.isDuplicateButton_Click);
//            // 
//            // clearDuplicateButton
//            // 
//            this.clearDuplicateButton.Location = new System.Drawing.Point(104, 19);
//            this.clearDuplicateButton.Name = "clearDuplicateButton";
//            this.clearDuplicateButton.Size = new System.Drawing.Size(64, 34);
//            this.clearDuplicateButton.TabIndex = 14;
//            this.clearDuplicateButton.Tag = "False";
//            this.clearDuplicateButton.Text = "Not Duplicate";
//            this.clearDuplicateButton.UseVisualStyleBackColor = true;
//            this.clearDuplicateButton.Click += new System.EventHandler(this.clearDuplicateButton_Click);
//            // 
//            // similarityParametersGroupBox
//            // 
//            this.similarityParametersGroupBox.Controls.Add(this.label3);
//            this.similarityParametersGroupBox.Controls.Add(this.similarityThresholdnumericUpDown);
//            this.similarityParametersGroupBox.Controls.Add(this.extraSetAnalysisCheckbox);
//            this.similarityParametersGroupBox.Controls.Add(this.dbRecordCountLabel);
//            this.similarityParametersGroupBox.Controls.Add(this.label2);
//            this.similarityParametersGroupBox.Controls.Add(this.similarityRangePercentagenumericUpDown);
//            this.similarityParametersGroupBox.Controls.Add(this.analyseButton);
//            this.similarityParametersGroupBox.Location = new System.Drawing.Point(57, 11);
//            this.similarityParametersGroupBox.Name = "similarityParametersGroupBox";
//            this.similarityParametersGroupBox.Size = new System.Drawing.Size(358, 67);
//            this.similarityParametersGroupBox.TabIndex = 12;
//            this.similarityParametersGroupBox.TabStop = false;
//            this.similarityParametersGroupBox.Text = "Similarity Parameters";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(184, 43);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(97, 13);
//            this.label3.TabIndex = 20;
//            this.label3.Text = "Similarity Threshold";
//            // 
//            // similarityThresholdnumericUpDown
//            // 
//             this.similarityThresholdnumericUpDown.Location = new System.Drawing.Point(142, 41);
//            this.similarityThresholdnumericUpDown.Maximum = new decimal(new int[] {
//            99,
//            0,
//            0,
//            0});
//            this.similarityThresholdnumericUpDown.Minimum = new decimal(new int[] {
//            50,
//            0,
//            0,
//            0});
//            this.similarityThresholdnumericUpDown.Size = new System.Drawing.Size(42, 20);
//            this.similarityThresholdnumericUpDown.TabIndex = 19;
//            // 
//            // extraSetAnalysisCheckbox
//            // 
//            this.extraSetAnalysisCheckbox.AutoSize = true;
//              this.extraSetAnalysisCheckbox.Location = new System.Drawing.Point(171, 21);
//            this.extraSetAnalysisCheckbox.Name = "extraSetAnalysisCheckbox";
//            this.extraSetAnalysisCheckbox.Size = new System.Drawing.Size(110, 17);
//            this.extraSetAnalysisCheckbox.TabIndex = 18;
//            this.extraSetAnalysisCheckbox.Text = "Extra Set Analysis";
//            this.extraSetAnalysisCheckbox.UseVisualStyleBackColor = true;
//            this.extraSetAnalysisCheckbox.CheckedChanged += new System.EventHandler(this.extraSetAnalysisCheckbox_CheckedChanged);
//            // 
//            // similarityRangePercentagenumericUpDown
//            // 
//             this.similarityRangePercentagenumericUpDown.Location = new System.Drawing.Point(15, 20);
//            this.similarityRangePercentagenumericUpDown.Maximum = new decimal(new int[] {
//            25,
//            0,
//            0,
//            0});
//            this.similarityRangePercentagenumericUpDown.Minimum = new decimal(new int[] {
//            1,
//            0,
//            0,
//            0});
//            this.similarityRangePercentagenumericUpDown.Name = "similarityRangePercentagenumericUpDown";
//            this.similarityRangePercentagenumericUpDown.Size = new System.Drawing.Size(42, 20);
//            this.similarityRangePercentagenumericUpDown.TabIndex = 8;
//           // 
//            // analyseButton
//            // 
//            this.analyseButton.Location = new System.Drawing.Point(297, 19);
//            this.analyseButton.Name = "analyseButton";
//            this.analyseButton.Size = new System.Drawing.Size(55, 34);
//            this.analyseButton.TabIndex = 10;
//            this.analyseButton.Text = "Analyse";
//            this.analyseButton.UseVisualStyleBackColor = true;
//            this.analyseButton.Click += new System.EventHandler(this.analyseButton_Click);
//            // 
//            // imageDetailPropertyGrid
//            // 
//            this.tableLayoutPanel1.SetColumnSpan(this.imageDetailPropertyGrid, 4);
//            this.imageDetailPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.imageDetailPropertyGrid.HelpVisible = false;
//            this.imageDetailPropertyGrid.LineColor = System.Drawing.SystemColors.ControlDark;
//            this.imageDetailPropertyGrid.Location = new System.Drawing.Point(252, 484);
//            this.imageDetailPropertyGrid.Name = "imageDetailPropertyGrid";
//            this.imageDetailPropertyGrid.Size = new System.Drawing.Size(994, 194);
//            this.imageDetailPropertyGrid.TabIndex = 7;
//            this.imageDetailPropertyGrid.ToolbarVisible = false;
//            // 
//            // imagesGridView
//            // 
//            this.imagesGridView.AllowUserToAddRows = false;
//            this.imagesGridView.BackgroundColor = System.Drawing.Color.White;
//            this.imagesGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.imagesGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
//            this.imagesGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            this.imagesGridView.ColumnHeadersVisible = false;
//            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
//            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
//            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
//            dataGridViewCellStyle1.NullValue = null;
//            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
//            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
//            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
//            this.imagesGridView.DefaultCellStyle = dataGridViewCellStyle1;
//            this.imagesGridView.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.imagesGridView.Location = new System.Drawing.Point(3, 16);
//            this.imagesGridView.Name = "imagesGridView";
//            this.imagesGridView.RowHeadersVisible = false;
//            this.imagesGridView.Size = new System.Drawing.Size(988, 276);
//            this.imagesGridView.TabIndex = 8;
//            this.imagesGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.imagesGridView_CellContentClick);
//            this.imagesGridView.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.imagesGridView_CellContentDoubleClick);
//            // 
//            // statusStrip1
//            // 
//            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            this.toolStripStatusLabel1});
//            this.statusStrip1.Location = new System.Drawing.Point(20, 719);
//            this.statusStrip1.Name = "statusStrip1";
//            this.statusStrip1.Size = new System.Drawing.Size(1249, 22);
//            this.statusStrip1.TabIndex = 10;
//            this.statusStrip1.Text = "statusStrip1";
//            // 
//            // toolStripStatusLabel1
//            // 
//            this.toolStripStatusLabel1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
//            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
//            this.toolStripStatusLabel1.Overflow = System.Windows.Forms.ToolStripItemOverflow.Always;
//            this.toolStripStatusLabel1.Size = new System.Drawing.Size(1234, 17);
//            this.toolStripStatusLabel1.Spring = true;
//            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
//            this.toolStripStatusLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
//            // 
//            // ScanGroupBox
//            // 
//            this.tableLayoutPanel1.SetColumnSpan(this.ScanGroupBox, 5);
//            this.ScanGroupBox.Controls.Add(this.clearDatabaseCheckbox);
//            this.ScanGroupBox.Controls.Add(this.stopButton);
//            this.ScanGroupBox.Controls.Add(this.progressGroupBox);
//            this.ScanGroupBox.Controls.Add(this.startbutton);
//            this.ScanGroupBox.Controls.Add(this.label1);
//            this.ScanGroupBox.Controls.Add(this.imagesDirectoryTextBox);
//            this.ScanGroupBox.Controls.Add(this.selectDirectoryButton);
//            this.ScanGroupBox.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.ScanGroupBox.Location = new System.Drawing.Point(3, 3);
//            this.ScanGroupBox.Name = "ScanGroupBox";
//            this.ScanGroupBox.Size = new System.Drawing.Size(1243, 84);
//            this.ScanGroupBox.TabIndex = 2;
//            this.ScanGroupBox.TabStop = false;
//            this.ScanGroupBox.Text = "Initial Scan Options";
//            // 
//            // clearDatabaseCheckbox
//            // 
//            this.clearDatabaseCheckbox.AutoSize = true;
//            this.clearDatabaseCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
//            this.clearDatabaseCheckbox.Location = new System.Drawing.Point(129, 61);
//            this.clearDatabaseCheckbox.Name = "clearDatabaseCheckbox";
//            this.clearDatabaseCheckbox.Size = new System.Drawing.Size(99, 17);
//            this.clearDatabaseCheckbox.TabIndex = 7;
//            this.clearDatabaseCheckbox.Text = "Clear Database";
//            this.clearDatabaseCheckbox.UseVisualStyleBackColor = true;
//            // 
//            // stopButton
//            // 
//            this.stopButton.Location = new System.Drawing.Point(497, 28);
//            this.stopButton.Name = "stopButton";
//            this.stopButton.Size = new System.Drawing.Size(55, 44);
//            this.stopButton.TabIndex = 5;
//            this.stopButton.Text = "Stop";
//            this.stopButton.UseVisualStyleBackColor = true;
//            this.stopButton.Click += new System.EventHandler(this.stopButton_Click);
//            // 
//            // progressGroupBox
//            // 
//            this.progressGroupBox.Controls.Add(this.progressLabel);
//            this.progressGroupBox.Controls.Add(this.progressBar1);
//            this.progressGroupBox.Location = new System.Drawing.Point(582, 12);
//            this.progressGroupBox.Name = "progressGroupBox";
//            this.progressGroupBox.Size = new System.Drawing.Size(358, 66);
//            this.progressGroupBox.TabIndex = 5;
//            this.progressGroupBox.TabStop = false;
//            this.progressGroupBox.Text = "Progress";
//            // 
//            // progressLabel
//            // 
//            this.progressLabel.AutoSize = true;
//            this.progressLabel.Location = new System.Drawing.Point(13, 45);
//            this.progressLabel.Name = "progressLabel";
//            this.progressLabel.Size = new System.Drawing.Size(21, 13);
//            this.progressLabel.TabIndex = 5;
//            this.progressLabel.Text = "0%";
//            // 
//            // progressBar1
//            // 
//            this.progressBar1.Location = new System.Drawing.Point(16, 18);
//            this.progressBar1.Name = "progressBar1";
//            this.progressBar1.Size = new System.Drawing.Size(313, 23);
//            this.progressBar1.TabIndex = 0;
//            // 
//            // startbutton
//            // 
//            this.startbutton.Location = new System.Drawing.Point(433, 28);
//            this.startbutton.Name = "startbutton";
//            this.startbutton.Size = new System.Drawing.Size(58, 44);
//            this.startbutton.TabIndex = 4;
//            this.startbutton.Text = "Start";
//            this.startbutton.UseVisualStyleBackColor = true;
//            this.startbutton.Click += new System.EventHandler(this.startbutton_Click);
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(6, 30);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(117, 13);
//            this.label1.TabIndex = 3;
//            this.label1.Text = "Select folder to analyse";
//            // 
//            // imagesDirectoryTextBox
//            // 
//            this.imagesDirectoryTextBox.Location = new System.Drawing.Point(129, 30);
//            this.imagesDirectoryTextBox.Name = "imagesDirectoryTextBox";
//            this.imagesDirectoryTextBox.ReadOnly = true;
//            this.imagesDirectoryTextBox.Size = new System.Drawing.Size(233, 20);
//            this.imagesDirectoryTextBox.TabIndex = 1;
//        // 
//            // selectDirectoryButton
//            // 
//            this.selectDirectoryButton.Location = new System.Drawing.Point(368, 28);
//            this.selectDirectoryButton.Name = "selectDirectoryButton";
//            this.selectDirectoryButton.Size = new System.Drawing.Size(47, 23);
//            this.selectDirectoryButton.TabIndex = 0;
//            this.selectDirectoryButton.Text = "...";
//            this.selectDirectoryButton.UseVisualStyleBackColor = true;
//            this.selectDirectoryButton.Click += new System.EventHandler(this.selectDirectoryButton_Click);
//            // 
//            // tableLayoutPanel1
//            // 
//            this.tableLayoutPanel1.ColumnCount = 5;
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
//            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
//            this.tableLayoutPanel1.Controls.Add(this.imageDetailPropertyGrid, 1, 3);
//            this.tableLayoutPanel1.Controls.Add(this.ScanGroupBox, 0, 0);
//            this.tableLayoutPanel1.Controls.Add(this.imageSetsGroupBox, 0, 2);
//            this.tableLayoutPanel1.Controls.Add(this.imageResultsGroupbox, 1, 2);
//            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.tableLayoutPanel1.Location = new System.Drawing.Point(20, 60);
//            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
//            this.tableLayoutPanel1.RowCount = 4;
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
//            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
//            this.tableLayoutPanel1.Size = new System.Drawing.Size(1249, 681);
//            this.tableLayoutPanel1.TabIndex = 14;
//            // 
//            // imageResultsGroupbox
//            // 
//            this.tableLayoutPanel1.SetColumnSpan(this.imageResultsGroupbox, 4);
//            this.imageResultsGroupbox.Controls.Add(this.imagesGridView);
//            this.imageResultsGroupbox.Dock = System.Windows.Forms.DockStyle.Fill;
//            this.imageResultsGroupbox.Location = new System.Drawing.Point(252, 183);
//            this.imageResultsGroupbox.Name = "imageResultsGroupbox";
//            this.imageResultsGroupbox.Size = new System.Drawing.Size(994, 295);
//            this.imageResultsGroupbox.TabIndex = 14;
//            this.imageResultsGroupbox.TabStop = false;
//            this.imageResultsGroupbox.Text = "Image Results";
//            // 
//            // metroStyleManager1
//            // 
//            this.metroStyleManager1.Owner = null;
//            // 
//            // MainForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(1289, 761);
//            this.Controls.Add(this.statusStrip1);
//            this.Controls.Add(this.tableLayoutPanel1);
//            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
//            this.KeyPreview = true;
//            this.Name = "MainForm";
//            this.Text = "Similar Image";
//            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
//            this.Load += new System.EventHandler(this.Form1_Load);
//            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
//            this.imageSetsGroupBox.ResumeLayout(false);
//            this.dataBaseGroupBox.ResumeLayout(false);
//            this.cleanupActionsGroupBox.ResumeLayout(false);
//            this.cleanupActionsGroupBox.PerformLayout();
//            this.selectedImageActionsGroupBox.ResumeLayout(false);
//            this.similarityParametersGroupBox.ResumeLayout(false);
//            this.similarityParametersGroupBox.PerformLayout();
//            ((System.ComponentModel.ISupportInitialize)(this.similarityThresholdnumericUpDown)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.similarityRangePercentagenumericUpDown)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.imagesGridView)).EndInit();
//            this.statusStrip1.ResumeLayout(false);
//            this.statusStrip1.PerformLayout();
//            this.ScanGroupBox.ResumeLayout(false);
//            this.ScanGroupBox.PerformLayout();
//            this.progressGroupBox.ResumeLayout(false);
//            this.progressGroupBox.PerformLayout();
//            this.tableLayoutPanel1.ResumeLayout(false);
//            this.tableLayoutPanel1.PerformLayout();
//            this.imageResultsGroupbox.ResumeLayout(false);
//            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion
//        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
//        private System.Windows.Forms.ListBox imageSetsListbox;
//        private System.Windows.Forms.GroupBox imageSetsGroupBox;
//        private System.Windows.Forms.DataGridView imagesGridView;
//        private System.Windows.Forms.StatusStrip statusStrip1;
//        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.NumericUpDown similarityRangePercentagenumericUpDown;
//        private System.Windows.Forms.Button analyseButton;
//        private System.Windows.Forms.GroupBox similarityParametersGroupBox;
//        private System.Windows.Forms.Label dbRecordCountLabel;
//        private System.Windows.Forms.PropertyGrid imageDetailPropertyGrid;
//        private System.Windows.Forms.GroupBox dataBaseGroupBox;
//        private System.Windows.Forms.GroupBox selectedImageActionsGroupBox;
//        private System.Windows.Forms.Button isDuplicateButton;
//        private System.Windows.Forms.Button clearDuplicateButton;
//        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
//        private System.Windows.Forms.GroupBox ScanGroupBox;
//        private System.Windows.Forms.CheckBox clearDatabaseCheckbox;
//        private System.Windows.Forms.Button stopButton;
//        private System.Windows.Forms.GroupBox progressGroupBox;
//        private System.Windows.Forms.Label progressLabel;
//        private System.Windows.Forms.ProgressBar progressBar1;
//        private System.Windows.Forms.Button startbutton;
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.TextBox imagesDirectoryTextBox;
//        private System.Windows.Forms.Button selectDirectoryButton;
//        private System.Windows.Forms.GroupBox imageResultsGroupbox;
//        private System.Windows.Forms.GroupBox cleanupActionsGroupBox;
//        private System.Windows.Forms.CheckBox confirmCleanupCheckbox;
//        private System.Windows.Forms.Button moveDuplicatesButton;
//        private System.Windows.Forms.Button cleanMissingButton;
//        private System.Windows.Forms.CheckBox extraSetAnalysisCheckbox;
//        private System.Windows.Forms.Label label3;
//        private System.Windows.Forms.NumericUpDown similarityThresholdnumericUpDown;
//        private System.Windows.Forms.Button cleanSetsButton;
//        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
//    }
//}

