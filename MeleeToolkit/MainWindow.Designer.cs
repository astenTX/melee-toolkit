namespace MeleeToolkit
{
    partial class MainWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.tabDiscImage = new System.Windows.Forms.TabPage();
            this.discImageTabControl = new System.Windows.Forms.TabControl();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.discImageGroupBox = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Button_openfile = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.filesystemSplitContainer = new System.Windows.Forms.SplitContainer();
            this.filesystemTreeView = new System.Windows.Forms.TreeView();
            this.filesystemImageList = new System.Windows.Forms.ImageList(this.components);
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.Button_Export = new System.Windows.Forms.Button();
            this.Button_Open = new System.Windows.Forms.Button();
            this.tabDatFile = new System.Windows.Forms.TabPage();
            this.datFileTabControl = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.datFileGroupGox = new System.Windows.Forms.GroupBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.nodesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.nodesTreeView = new System.Windows.Forms.TreeView();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Offset = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VariableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.texturesSplitContainer = new System.Windows.Forms.SplitContainer();
            this.textureListBox = new System.Windows.Forms.ListBox();
            this.buttonReplaceTexture = new System.Windows.Forms.Button();
            this.buttonExportTexture = new System.Windows.Forms.Button();
            this.textureInfoLabel = new System.Windows.Forms.Label();
            this.texturePictureBox = new System.Windows.Forms.PictureBox();
            this.replaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extractToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openDiscImageDialog = new System.Windows.Forms.OpenFileDialog();
            this.openDatFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.openTextureDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveTextureDialog = new System.Windows.Forms.SaveFileDialog();
            this.saveDatFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.mainTabControl.SuspendLayout();
            this.tabDiscImage.SuspendLayout();
            this.discImageTabControl.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.discImageGroupBox.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.filesystemSplitContainer)).BeginInit();
            this.filesystemSplitContainer.Panel1.SuspendLayout();
            this.filesystemSplitContainer.Panel2.SuspendLayout();
            this.filesystemSplitContainer.SuspendLayout();
            this.tabDatFile.SuspendLayout();
            this.datFileTabControl.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.datFileGroupGox.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nodesSplitContainer)).BeginInit();
            this.nodesSplitContainer.Panel1.SuspendLayout();
            this.nodesSplitContainer.Panel2.SuspendLayout();
            this.nodesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturesSplitContainer)).BeginInit();
            this.texturesSplitContainer.Panel1.SuspendLayout();
            this.texturesSplitContainer.Panel2.SuspendLayout();
            this.texturesSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturePictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // mainTabControl
            // 
            this.mainTabControl.Controls.Add(this.tabDiscImage);
            this.mainTabControl.Controls.Add(this.tabDatFile);
            this.mainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabControl.Location = new System.Drawing.Point(0, 0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(627, 489);
            this.mainTabControl.TabIndex = 2;
            // 
            // tabDiscImage
            // 
            this.tabDiscImage.Controls.Add(this.discImageTabControl);
            this.tabDiscImage.Location = new System.Drawing.Point(4, 22);
            this.tabDiscImage.Name = "tabDiscImage";
            this.tabDiscImage.Padding = new System.Windows.Forms.Padding(3);
            this.tabDiscImage.Size = new System.Drawing.Size(619, 463);
            this.tabDiscImage.TabIndex = 1;
            this.tabDiscImage.Text = "Disc Image";
            this.tabDiscImage.UseVisualStyleBackColor = true;
            // 
            // discImageTabControl
            // 
            this.discImageTabControl.Controls.Add(this.tabPage5);
            this.discImageTabControl.Controls.Add(this.tabPage4);
            this.discImageTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.discImageTabControl.Location = new System.Drawing.Point(3, 3);
            this.discImageTabControl.Name = "discImageTabControl";
            this.discImageTabControl.SelectedIndex = 0;
            this.discImageTabControl.Size = new System.Drawing.Size(613, 457);
            this.discImageTabControl.TabIndex = 3;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.discImageGroupBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(605, 431);
            this.tabPage5.TabIndex = 1;
            this.tabPage5.Text = "Info";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // discImageGroupBox
            // 
            this.discImageGroupBox.Controls.Add(this.label6);
            this.discImageGroupBox.Controls.Add(this.Button_openfile);
            this.discImageGroupBox.Controls.Add(this.label7);
            this.discImageGroupBox.Controls.Add(this.linkLabel3);
            this.discImageGroupBox.Location = new System.Drawing.Point(6, 6);
            this.discImageGroupBox.Name = "discImageGroupBox";
            this.discImageGroupBox.Size = new System.Drawing.Size(300, 264);
            this.discImageGroupBox.TabIndex = 12;
            this.discImageGroupBox.TabStop = false;
            this.discImageGroupBox.Text = "Disc Image Info";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label6.Location = new System.Drawing.Point(6, 110);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(288, 26);
            this.label6.TabIndex = 4;
            this.label6.Text = "None";
            this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_openfile
            // 
            this.Button_openfile.Location = new System.Drawing.Point(92, 169);
            this.Button_openfile.Name = "Button_openfile";
            this.Button_openfile.Size = new System.Drawing.Size(110, 23);
            this.Button_openfile.TabIndex = 11;
            this.Button_openfile.Text = "Open Disc Image";
            this.Button_openfile.UseVisualStyleBackColor = true;
            this.Button_openfile.Click += new System.EventHandler(this.Button_openfile_Click);
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.Location = new System.Drawing.Point(6, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(288, 26);
            this.label7.TabIndex = 3;
            this.label7.Text = "None";
            this.label7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkLabel3
            // 
            this.linkLabel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel3.Location = new System.Drawing.Point(3, 16);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(294, 150);
            this.linkLabel3.TabIndex = 2;
            this.linkLabel3.Text = "Name\r\n\r\n\r\n\r\n\r\nVersion";
            this.linkLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.filesystemSplitContainer);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(605, 431);
            this.tabPage4.TabIndex = 0;
            this.tabPage4.Text = "Filesystem";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // filesystemSplitContainer
            // 
            this.filesystemSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesystemSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.filesystemSplitContainer.Name = "filesystemSplitContainer";
            // 
            // filesystemSplitContainer.Panel1
            // 
            this.filesystemSplitContainer.Panel1.Controls.Add(this.filesystemTreeView);
            // 
            // filesystemSplitContainer.Panel2
            // 
            this.filesystemSplitContainer.Panel2.Controls.Add(this.button6);
            this.filesystemSplitContainer.Panel2.Controls.Add(this.label4);
            this.filesystemSplitContainer.Panel2.Controls.Add(this.label5);
            this.filesystemSplitContainer.Panel2.Controls.Add(this.linkLabel2);
            this.filesystemSplitContainer.Panel2.Controls.Add(this.Button_Export);
            this.filesystemSplitContainer.Panel2.Controls.Add(this.Button_Open);
            this.filesystemSplitContainer.Size = new System.Drawing.Size(599, 425);
            this.filesystemSplitContainer.SplitterDistance = 396;
            this.filesystemSplitContainer.TabIndex = 0;
            // 
            // filesystemTreeView
            // 
            this.filesystemTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filesystemTreeView.HideSelection = false;
            this.filesystemTreeView.ImageIndex = 0;
            this.filesystemTreeView.ImageList = this.filesystemImageList;
            this.filesystemTreeView.Location = new System.Drawing.Point(0, 0);
            this.filesystemTreeView.Name = "filesystemTreeView";
            this.filesystemTreeView.SelectedImageIndex = 0;
            this.filesystemTreeView.Size = new System.Drawing.Size(396, 425);
            this.filesystemTreeView.TabIndex = 1;
            this.filesystemTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            // 
            // filesystemImageList
            // 
            this.filesystemImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("filesystemImageList.ImageStream")));
            this.filesystemImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.filesystemImageList.Images.SetKeyName(0, "file.png");
            this.filesystemImageList.Images.SetKeyName(1, "character.png");
            this.filesystemImageList.Images.SetKeyName(2, "stage.png");
            this.filesystemImageList.Images.SetKeyName(3, "folder.png");
            this.filesystemImageList.Images.SetKeyName(4, "smashball.png");
            // 
            // button6
            // 
            this.button6.Enabled = false;
            this.button6.Location = new System.Drawing.Point(40, 339);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(120, 23);
            this.button6.TabIndex = 10;
            this.button6.Text = "Replace Selected File";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.Location = new System.Drawing.Point(0, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(199, 26);
            this.label4.TabIndex = 9;
            this.label4.Text = "None";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label5.Location = new System.Drawing.Point(0, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(199, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "None";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkLabel2
            // 
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel2.Location = new System.Drawing.Point(0, 52);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(199, 150);
            this.linkLabel2.TabIndex = 7;
            this.linkLabel2.Text = "Name\r\n\r\n\r\n\r\n\r\nSize";
            this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Button_Export
            // 
            this.Button_Export.Enabled = false;
            this.Button_Export.Location = new System.Drawing.Point(40, 368);
            this.Button_Export.Name = "Button_Export";
            this.Button_Export.Size = new System.Drawing.Size(120, 23);
            this.Button_Export.TabIndex = 5;
            this.Button_Export.Text = "Export Selected File";
            this.Button_Export.UseVisualStyleBackColor = true;
            this.Button_Export.Click += new System.EventHandler(this.Button_Export_Click);
            // 
            // Button_Open
            // 
            this.Button_Open.Enabled = false;
            this.Button_Open.Location = new System.Drawing.Point(40, 310);
            this.Button_Open.Name = "Button_Open";
            this.Button_Open.Size = new System.Drawing.Size(120, 23);
            this.Button_Open.TabIndex = 6;
            this.Button_Open.Text = "Load Selected File";
            this.Button_Open.UseVisualStyleBackColor = true;
            this.Button_Open.Click += new System.EventHandler(this.Button_Open_Click);
            // 
            // tabDatFile
            // 
            this.tabDatFile.Controls.Add(this.datFileTabControl);
            this.tabDatFile.Location = new System.Drawing.Point(4, 22);
            this.tabDatFile.Name = "tabDatFile";
            this.tabDatFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabDatFile.Size = new System.Drawing.Size(619, 463);
            this.tabDatFile.TabIndex = 2;
            this.tabDatFile.Text = "DAT File";
            this.tabDatFile.UseVisualStyleBackColor = true;
            // 
            // datFileTabControl
            // 
            this.datFileTabControl.Controls.Add(this.tabPage3);
            this.datFileTabControl.Controls.Add(this.tabPage1);
            this.datFileTabControl.Controls.Add(this.tabPage2);
            this.datFileTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.datFileTabControl.Location = new System.Drawing.Point(3, 3);
            this.datFileTabControl.Name = "datFileTabControl";
            this.datFileTabControl.SelectedIndex = 0;
            this.datFileTabControl.Size = new System.Drawing.Size(613, 457);
            this.datFileTabControl.TabIndex = 4;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.datFileGroupGox);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(605, 431);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Info";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // datFileGroupGox
            // 
            this.datFileGroupGox.Controls.Add(this.button5);
            this.datFileGroupGox.Controls.Add(this.label3);
            this.datFileGroupGox.Controls.Add(this.label2);
            this.datFileGroupGox.Controls.Add(this.linkLabel1);
            this.datFileGroupGox.Controls.Add(this.button4);
            this.datFileGroupGox.Controls.Add(this.button3);
            this.datFileGroupGox.Location = new System.Drawing.Point(6, 6);
            this.datFileGroupGox.Name = "datFileGroupGox";
            this.datFileGroupGox.Size = new System.Drawing.Size(300, 264);
            this.datFileGroupGox.TabIndex = 2;
            this.datFileGroupGox.TabStop = false;
            this.datFileGroupGox.Text = "DAT File Info";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(90, 143);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(111, 23);
            this.button5.TabIndex = 5;
            this.button5.Text = "Open DAT File";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label3.Location = new System.Drawing.Point(6, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(288, 26);
            this.label3.TabIndex = 4;
            this.label3.Text = "None";
            this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(288, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "None";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 0);
            this.linkLabel1.Location = new System.Drawing.Point(3, 16);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(294, 150);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.Text = "Name\r\n\r\n\r\n\r\n\r\nSize";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Location = new System.Drawing.Point(90, 220);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(111, 23);
            this.button4.TabIndex = 1;
            this.button4.Text = "Save to Disc Image";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Enabled = false;
            this.button3.Location = new System.Drawing.Point(90, 191);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(111, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "Save to File";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.nodesSplitContainer);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(605, 431);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Nodes";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // nodesSplitContainer
            // 
            this.nodesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.nodesSplitContainer.Name = "nodesSplitContainer";
            // 
            // nodesSplitContainer.Panel1
            // 
            this.nodesSplitContainer.Panel1.Controls.Add(this.nodesTreeView);
            // 
            // nodesSplitContainer.Panel2
            // 
            this.nodesSplitContainer.Panel2.Controls.Add(this.dataGridView1);
            this.nodesSplitContainer.Size = new System.Drawing.Size(599, 425);
            this.nodesSplitContainer.SplitterDistance = 308;
            this.nodesSplitContainer.TabIndex = 0;
            this.nodesSplitContainer.TabStop = false;
            // 
            // nodesTreeView
            // 
            this.nodesTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nodesTreeView.Location = new System.Drawing.Point(0, 0);
            this.nodesTreeView.Name = "nodesTreeView";
            this.nodesTreeView.Size = new System.Drawing.Size(308, 425);
            this.nodesTreeView.TabIndex = 3;
            this.nodesTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView2_AfterSelect);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.LightGray;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Offset,
            this.VariableName,
            this.Value});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.Size = new System.Drawing.Size(287, 425);
            this.dataGridView1.TabIndex = 1;
            // 
            // Offset
            // 
            this.Offset.FillWeight = 40F;
            this.Offset.HeaderText = "Offset";
            this.Offset.Name = "Offset";
            this.Offset.ReadOnly = true;
            // 
            // VariableName
            // 
            this.VariableName.HeaderText = "Name";
            this.VariableName.Name = "VariableName";
            this.VariableName.ReadOnly = true;
            // 
            // Value
            // 
            this.Value.HeaderText = "Value";
            this.Value.Name = "Value";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.texturesSplitContainer);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(605, 431);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Textures";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // texturesSplitContainer
            // 
            this.texturesSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.texturesSplitContainer.Location = new System.Drawing.Point(3, 3);
            this.texturesSplitContainer.Name = "texturesSplitContainer";
            // 
            // texturesSplitContainer.Panel1
            // 
            this.texturesSplitContainer.Panel1.Controls.Add(this.textureListBox);
            // 
            // texturesSplitContainer.Panel2
            // 
            this.texturesSplitContainer.Panel2.BackColor = System.Drawing.Color.LightGray;
            this.texturesSplitContainer.Panel2.Controls.Add(this.buttonReplaceTexture);
            this.texturesSplitContainer.Panel2.Controls.Add(this.buttonExportTexture);
            this.texturesSplitContainer.Panel2.Controls.Add(this.textureInfoLabel);
            this.texturesSplitContainer.Panel2.Controls.Add(this.texturePictureBox);
            this.texturesSplitContainer.Size = new System.Drawing.Size(599, 425);
            this.texturesSplitContainer.SplitterDistance = 240;
            this.texturesSplitContainer.TabIndex = 0;
            this.texturesSplitContainer.TabStop = false;
            // 
            // textureListBox
            // 
            this.textureListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textureListBox.FormattingEnabled = true;
            this.textureListBox.IntegralHeight = false;
            this.textureListBox.Location = new System.Drawing.Point(0, 0);
            this.textureListBox.Name = "textureListBox";
            this.textureListBox.Size = new System.Drawing.Size(240, 425);
            this.textureListBox.TabIndex = 0;
            this.textureListBox.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // buttonReplaceTexture
            // 
            this.buttonReplaceTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReplaceTexture.Enabled = false;
            this.buttonReplaceTexture.Location = new System.Drawing.Point(277, 370);
            this.buttonReplaceTexture.Name = "buttonReplaceTexture";
            this.buttonReplaceTexture.Size = new System.Drawing.Size(75, 23);
            this.buttonReplaceTexture.TabIndex = 2;
            this.buttonReplaceTexture.Text = "Replace";
            this.buttonReplaceTexture.UseVisualStyleBackColor = true;
            this.buttonReplaceTexture.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonExportTexture
            // 
            this.buttonExportTexture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonExportTexture.Enabled = false;
            this.buttonExportTexture.Location = new System.Drawing.Point(277, 399);
            this.buttonExportTexture.Name = "buttonExportTexture";
            this.buttonExportTexture.Size = new System.Drawing.Size(75, 23);
            this.buttonExportTexture.TabIndex = 1;
            this.buttonExportTexture.Text = "Export";
            this.buttonExportTexture.UseVisualStyleBackColor = true;
            this.buttonExportTexture.Click += new System.EventHandler(this.button1_Click);
            // 
            // textureInfoLabel
            // 
            this.textureInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textureInfoLabel.AutoSize = true;
            this.textureInfoLabel.BackColor = System.Drawing.Color.Transparent;
            this.textureInfoLabel.Location = new System.Drawing.Point(6, 381);
            this.textureInfoLabel.Margin = new System.Windows.Forms.Padding(0);
            this.textureInfoLabel.Name = "textureInfoLabel";
            this.textureInfoLabel.Size = new System.Drawing.Size(42, 39);
            this.textureInfoLabel.TabIndex = 1;
            this.textureInfoLabel.Text = "Size:\r\nFormat:\r\nColors:";
            // 
            // texturePictureBox
            // 
            this.texturePictureBox.BackColor = System.Drawing.Color.Transparent;
            this.texturePictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.texturePictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.texturePictureBox.Location = new System.Drawing.Point(0, 0);
            this.texturePictureBox.Name = "texturePictureBox";
            this.texturePictureBox.Size = new System.Drawing.Size(355, 425);
            this.texturePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.texturePictureBox.TabIndex = 0;
            this.texturePictureBox.TabStop = false;
            // 
            // replaceToolStripMenuItem
            // 
            this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
            this.replaceToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.replaceToolStripMenuItem.Text = "Replace";
            // 
            // extractAllToolStripMenuItem
            // 
            this.extractAllToolStripMenuItem.Name = "extractAllToolStripMenuItem";
            this.extractAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.extractAllToolStripMenuItem.Text = "Extract All";
            // 
            // extractToolStripMenuItem
            // 
            this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
            this.extractToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.extractToolStripMenuItem.Text = "Extract";
            // 
            // openDiscImageDialog
            // 
            this.openDiscImageDialog.Filter = "Disc images (.gcm, .iso)|*.gcm; *.iso|All files|*.*";
            this.openDiscImageDialog.FilterIndex = 0;
            this.openDiscImageDialog.InitialDirectory = "%UserProfile%\\\\Desktop";
            this.openDiscImageDialog.Title = "Open Disc Image";
            // 
            // openDatFileDialog
            // 
            this.openDatFileDialog.Filter = "DAT files (.dat, .usd)|*.dat; *.usd|All files|*.*";
            this.openDatFileDialog.InitialDirectory = "%UserProfile%\\\\Desktop";
            this.openDatFileDialog.RestoreDirectory = true;
            this.openDatFileDialog.Title = "Open DAT File";
            // 
            // openTextureDialog
            // 
            this.openTextureDialog.Filter = ".png files|*.png";
            this.openTextureDialog.FilterIndex = 0;
            this.openTextureDialog.Title = "Replace Texture";
            // 
            // saveTextureDialog
            // 
            this.saveTextureDialog.DefaultExt = "png";
            this.saveTextureDialog.Filter = ".png files|*.png";
            this.saveTextureDialog.InitialDirectory = "%userprofile%\\\\Desktop";
            this.saveTextureDialog.Title = "Export Texture";
            // 
            // saveDatFileDialog
            // 
            this.saveDatFileDialog.DefaultExt = "dat";
            this.saveDatFileDialog.Filter = ".dat files|*.dat; *.usd|All files|*.*";
            this.saveDatFileDialog.InitialDirectory = "%userprofile%\\\\Desktop";
            this.saveDatFileDialog.Title = "Save DAT File";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(627, 489);
            this.Controls.Add(this.mainTabControl);
            this.Name = "MainWindow";
            this.Text = "Melee Toolkit";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.mainTabControl.ResumeLayout(false);
            this.tabDiscImage.ResumeLayout(false);
            this.discImageTabControl.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.discImageGroupBox.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.filesystemSplitContainer.Panel1.ResumeLayout(false);
            this.filesystemSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.filesystemSplitContainer)).EndInit();
            this.filesystemSplitContainer.ResumeLayout(false);
            this.tabDatFile.ResumeLayout(false);
            this.datFileTabControl.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.datFileGroupGox.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.nodesSplitContainer.Panel1.ResumeLayout(false);
            this.nodesSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nodesSplitContainer)).EndInit();
            this.nodesSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.texturesSplitContainer.Panel1.ResumeLayout(false);
            this.texturesSplitContainer.Panel2.ResumeLayout(false);
            this.texturesSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.texturesSplitContainer)).EndInit();
            this.texturesSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.texturePictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage tabDiscImage;
        private System.Windows.Forms.ImageList filesystemImageList;
        private System.Windows.Forms.TabPage tabDatFile;
        private System.Windows.Forms.SplitContainer nodesSplitContainer;
        private System.Windows.Forms.TreeView nodesTreeView;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TabControl datFileTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.SplitContainer texturesSplitContainer;
        private System.Windows.Forms.ListBox textureListBox;
        private System.Windows.Forms.PictureBox texturePictureBox;
        private System.Windows.Forms.ToolStripMenuItem replaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extractToolStripMenuItem;
        private System.Windows.Forms.Label textureInfoLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Offset;
        private System.Windows.Forms.DataGridViewTextBoxColumn VariableName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.Button buttonExportTexture;
        private System.Windows.Forms.Button buttonReplaceTexture;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.GroupBox datFileGroupGox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabControl discImageTabControl;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer filesystemSplitContainer;
        private System.Windows.Forms.TreeView filesystemTreeView;
        private System.Windows.Forms.Button Button_Export;
        private System.Windows.Forms.Button Button_Open;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button Button_openfile;
        private System.Windows.Forms.GroupBox discImageGroupBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.OpenFileDialog openDiscImageDialog;
        private System.Windows.Forms.OpenFileDialog openDatFileDialog;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.OpenFileDialog openTextureDialog;
        private System.Windows.Forms.SaveFileDialog saveTextureDialog;
        private System.Windows.Forms.SaveFileDialog saveDatFileDialog;
    }
}

