﻿namespace ComputerExam.BusicWork
{
    partial class frmAddTopicDB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddTopicDB));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnAddTopicDB = new System.Windows.Forms.Button();
            this.btnDeleteTopicDB = new System.Windows.Forms.Button();
            this.dgvTopicDB = new System.Windows.Forms.DataGridView();
            this.TopicDBCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubjectName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopicDBVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.视频资源 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TopicFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnDeleteFile = new System.Windows.Forms.Button();
            this.dgvFiles = new System.Windows.Forms.DataGridView();
            this.FileNames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.btnAddVideoFile = new System.Windows.Forms.Button();
            this.btnDelVideoFile = new System.Windows.Forms.Button();
            this.dgvVideoFiles = new System.Windows.Forms.DataGridView();
            this.VideoFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VideoFileVersion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VideoFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnClose = new System.Windows.Forms.Button();
            this.ofdOpenTopicDB = new System.Windows.Forms.OpenFileDialog();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.ofdOpenVideoFile = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopicDB)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideoFiles)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(617, 80);
            this.panel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(88, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(185, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "对考试题库进行添加、删除等操作";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(88, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(83, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "考试题库维护";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(13, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(50, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl1.ItemSize = new System.Drawing.Size(94, 18);
            this.tabControl1.Location = new System.Drawing.Point(12, 98);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(617, 210);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.White;
            this.tabPage1.Controls.Add(this.btnAddTopicDB);
            this.tabPage1.Controls.Add(this.btnDeleteTopicDB);
            this.tabPage1.Controls.Add(this.dgvTopicDB);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(609, 184);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "考试题库维护";
            // 
            // btnAddTopicDB
            // 
            this.btnAddTopicDB.FlatAppearance.BorderSize = 0;
            this.btnAddTopicDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTopicDB.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTopicDB.Image")));
            this.btnAddTopicDB.Location = new System.Drawing.Point(9, 155);
            this.btnAddTopicDB.Name = "btnAddTopicDB";
            this.btnAddTopicDB.Size = new System.Drawing.Size(76, 24);
            this.btnAddTopicDB.TabIndex = 4;
            this.btnAddTopicDB.Text = "添加题库";
            this.btnAddTopicDB.UseVisualStyleBackColor = true;
            this.btnAddTopicDB.Click += new System.EventHandler(this.btnAddTopicDB_Click);
            // 
            // btnDeleteTopicDB
            // 
            this.btnDeleteTopicDB.FlatAppearance.BorderSize = 0;
            this.btnDeleteTopicDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTopicDB.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteTopicDB.Image")));
            this.btnDeleteTopicDB.Location = new System.Drawing.Point(87, 155);
            this.btnDeleteTopicDB.Name = "btnDeleteTopicDB";
            this.btnDeleteTopicDB.Size = new System.Drawing.Size(76, 24);
            this.btnDeleteTopicDB.TabIndex = 4;
            this.btnDeleteTopicDB.Text = "删除题库";
            this.btnDeleteTopicDB.UseVisualStyleBackColor = true;
            this.btnDeleteTopicDB.Click += new System.EventHandler(this.btnDeleteTopicDB_Click);
            // 
            // dgvTopicDB
            // 
            this.dgvTopicDB.AllowUserToAddRows = false;
            this.dgvTopicDB.AllowUserToDeleteRows = false;
            this.dgvTopicDB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTopicDB.BackgroundColor = System.Drawing.Color.White;
            this.dgvTopicDB.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTopicDB.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TopicDBCode,
            this.SubjectName,
            this.TopicDBVersion,
            this.FileName,
            this.视频资源,
            this.TopicFilePath});
            this.dgvTopicDB.Location = new System.Drawing.Point(0, 0);
            this.dgvTopicDB.Name = "dgvTopicDB";
            this.dgvTopicDB.ReadOnly = true;
            this.dgvTopicDB.RowTemplate.Height = 23;
            this.dgvTopicDB.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTopicDB.Size = new System.Drawing.Size(609, 146);
            this.dgvTopicDB.TabIndex = 3;
            // 
            // TopicDBCode
            // 
            this.TopicDBCode.DataPropertyName = "TopicDBCode";
            this.TopicDBCode.HeaderText = "题库代码";
            this.TopicDBCode.Name = "TopicDBCode";
            this.TopicDBCode.ReadOnly = true;
            // 
            // SubjectName
            // 
            this.SubjectName.DataPropertyName = "SubjectName";
            this.SubjectName.HeaderText = "科目名称";
            this.SubjectName.Name = "SubjectName";
            this.SubjectName.ReadOnly = true;
            this.SubjectName.Width = 150;
            // 
            // TopicDBVersion
            // 
            this.TopicDBVersion.DataPropertyName = "TopicDBVersion";
            this.TopicDBVersion.HeaderText = "题库版本";
            this.TopicDBVersion.Name = "TopicDBVersion";
            this.TopicDBVersion.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "外部文件";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            // 
            // 视频资源
            // 
            this.视频资源.DataPropertyName = "VideoFileName";
            this.视频资源.HeaderText = "视频资源";
            this.视频资源.Name = "视频资源";
            this.视频资源.ReadOnly = true;
            // 
            // TopicFilePath
            // 
            this.TopicFilePath.DataPropertyName = "TopicFilePath";
            this.TopicFilePath.HeaderText = "题库文件路径";
            this.TopicFilePath.Name = "TopicFilePath";
            this.TopicFilePath.ReadOnly = true;
            this.TopicFilePath.Visible = false;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.White;
            this.tabPage2.Controls.Add(this.btnAddFile);
            this.tabPage2.Controls.Add(this.btnDeleteFile);
            this.tabPage2.Controls.Add(this.dgvFiles);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(609, 184);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "外部文件维护";
            // 
            // btnAddFile
            // 
            this.btnAddFile.FlatAppearance.BorderSize = 0;
            this.btnAddFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddFile.Image")));
            this.btnAddFile.Location = new System.Drawing.Point(6, 155);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(76, 24);
            this.btnAddFile.TabIndex = 5;
            this.btnAddFile.Text = "添加文件";
            this.btnAddFile.UseVisualStyleBackColor = true;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnDeleteFile
            // 
            this.btnDeleteFile.FlatAppearance.BorderSize = 0;
            this.btnDeleteFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteFile.Image")));
            this.btnDeleteFile.Location = new System.Drawing.Point(87, 155);
            this.btnDeleteFile.Name = "btnDeleteFile";
            this.btnDeleteFile.Size = new System.Drawing.Size(76, 24);
            this.btnDeleteFile.TabIndex = 6;
            this.btnDeleteFile.Text = "删除文件";
            this.btnDeleteFile.UseVisualStyleBackColor = true;
            this.btnDeleteFile.Click += new System.EventHandler(this.btnDeleteFile_Click);
            // 
            // dgvFiles
            // 
            this.dgvFiles.AllowUserToAddRows = false;
            this.dgvFiles.AllowUserToDeleteRows = false;
            this.dgvFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFiles.BackgroundColor = System.Drawing.Color.White;
            this.dgvFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FileNames,
            this.FileVersion,
            this.FilePath});
            this.dgvFiles.Location = new System.Drawing.Point(3, 3);
            this.dgvFiles.Name = "dgvFiles";
            this.dgvFiles.ReadOnly = true;
            this.dgvFiles.RowTemplate.Height = 23;
            this.dgvFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvFiles.Size = new System.Drawing.Size(603, 146);
            this.dgvFiles.TabIndex = 0;
            // 
            // FileNames
            // 
            this.FileNames.DataPropertyName = "FileName";
            this.FileNames.HeaderText = "文件名";
            this.FileNames.Name = "FileNames";
            this.FileNames.ReadOnly = true;
            this.FileNames.Width = 150;
            // 
            // FileVersion
            // 
            this.FileVersion.DataPropertyName = "FileVersion";
            this.FileVersion.HeaderText = "版本（修改日期）";
            this.FileVersion.Name = "FileVersion";
            this.FileVersion.ReadOnly = true;
            this.FileVersion.Width = 150;
            // 
            // FilePath
            // 
            this.FilePath.DataPropertyName = "FilePath";
            this.FilePath.HeaderText = "文件路径";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Visible = false;
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.White;
            this.tabPage3.Controls.Add(this.btnAddVideoFile);
            this.tabPage3.Controls.Add(this.btnDelVideoFile);
            this.tabPage3.Controls.Add(this.dgvVideoFiles);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(609, 184);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "视频文件维护";
            // 
            // btnAddVideoFile
            // 
            this.btnAddVideoFile.FlatAppearance.BorderSize = 0;
            this.btnAddVideoFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddVideoFile.Image = ((System.Drawing.Image)(resources.GetObject("btnAddVideoFile.Image")));
            this.btnAddVideoFile.Location = new System.Drawing.Point(6, 155);
            this.btnAddVideoFile.Name = "btnAddVideoFile";
            this.btnAddVideoFile.Size = new System.Drawing.Size(76, 24);
            this.btnAddVideoFile.TabIndex = 7;
            this.btnAddVideoFile.Text = "添加文件";
            this.btnAddVideoFile.UseVisualStyleBackColor = true;
            this.btnAddVideoFile.Click += new System.EventHandler(this.btnAddVideoFile_Click);
            // 
            // btnDelVideoFile
            // 
            this.btnDelVideoFile.FlatAppearance.BorderSize = 0;
            this.btnDelVideoFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelVideoFile.Image = ((System.Drawing.Image)(resources.GetObject("btnDelVideoFile.Image")));
            this.btnDelVideoFile.Location = new System.Drawing.Point(87, 155);
            this.btnDelVideoFile.Name = "btnDelVideoFile";
            this.btnDelVideoFile.Size = new System.Drawing.Size(76, 24);
            this.btnDelVideoFile.TabIndex = 8;
            this.btnDelVideoFile.Text = "删除文件";
            this.btnDelVideoFile.UseVisualStyleBackColor = true;
            this.btnDelVideoFile.Click += new System.EventHandler(this.btnDelVideoFile_Click);
            // 
            // dgvVideoFiles
            // 
            this.dgvVideoFiles.AllowUserToAddRows = false;
            this.dgvVideoFiles.AllowUserToDeleteRows = false;
            this.dgvVideoFiles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVideoFiles.BackgroundColor = System.Drawing.Color.White;
            this.dgvVideoFiles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVideoFiles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.VideoFileName,
            this.VideoFileVersion,
            this.VideoFilePath});
            this.dgvVideoFiles.Location = new System.Drawing.Point(3, 3);
            this.dgvVideoFiles.Name = "dgvVideoFiles";
            this.dgvVideoFiles.ReadOnly = true;
            this.dgvVideoFiles.RowTemplate.Height = 23;
            this.dgvVideoFiles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVideoFiles.Size = new System.Drawing.Size(603, 146);
            this.dgvVideoFiles.TabIndex = 1;
            // 
            // VideoFileName
            // 
            this.VideoFileName.DataPropertyName = "FileName";
            this.VideoFileName.HeaderText = "文件名";
            this.VideoFileName.Name = "VideoFileName";
            this.VideoFileName.ReadOnly = true;
            this.VideoFileName.Width = 150;
            // 
            // VideoFileVersion
            // 
            this.VideoFileVersion.DataPropertyName = "FileVersion";
            this.VideoFileVersion.HeaderText = "版本（修改日期）";
            this.VideoFileVersion.Name = "VideoFileVersion";
            this.VideoFileVersion.ReadOnly = true;
            this.VideoFileVersion.Width = 150;
            // 
            // VideoFilePath
            // 
            this.VideoFilePath.DataPropertyName = "FilePath";
            this.VideoFilePath.HeaderText = "文件路径";
            this.VideoFilePath.Name = "VideoFilePath";
            this.VideoFilePath.ReadOnly = true;
            this.VideoFilePath.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::ComputerExam.Properties.Resources.按钮03;
            this.btnClose.Location = new System.Drawing.Point(553, 314);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(76, 24);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ofdOpenTopicDB
            // 
            this.ofdOpenTopicDB.FileName = "题库文件";
            this.ofdOpenTopicDB.Filter = "题库文件(*.sdb,*.srk)|*.sdb;*.srk";
            this.ofdOpenTopicDB.Title = "题库文件";
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "账套文件";
            this.ofdOpenFile.Filter = "账套文件|*.casf*";
            this.ofdOpenFile.Title = "账套文件";
            // 
            // ofdOpenVideoFile
            // 
            this.ofdOpenVideoFile.FileName = "视频资源包";
            this.ofdOpenVideoFile.Filter = "视频资源包|*.zip*";
            this.ofdOpenVideoFile.Title = "视频资源包";
            // 
            // frmAddTopicDB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(641, 349);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAddTopicDB";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考试题库维护";
            this.Load += new System.EventHandler(this.frmAddTopicDB_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTopicDB)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFiles)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVideoFiles)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnDeleteTopicDB;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.DataGridView dgvTopicDB;
        private System.Windows.Forms.DataGridView dgvFiles;
        private System.Windows.Forms.OpenFileDialog ofdOpenTopicDB;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileNames;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.Button btnDeleteFile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgvVideoFiles;
        private System.Windows.Forms.Button btnAddVideoFile;
        private System.Windows.Forms.Button btnDelVideoFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoFileVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn TopicDBCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubjectName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TopicDBVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn 视频资源;
        private System.Windows.Forms.DataGridViewTextBoxColumn TopicFilePath;
        private System.Windows.Forms.OpenFileDialog ofdOpenVideoFile;
        private System.Windows.Forms.Button btnAddTopicDB;
    }
}