namespace ComputerExam.BusicWork
{
    partial class frmDownWork
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnAddVideoFile = new System.Windows.Forms.Button();
            this.btnAddEnvfile = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSubject = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboJobState = new System.Windows.Forms.ComboBox();
            this.btnDoJob = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.JobNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowAnalysis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAllowReSubmitScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllowReSubmitScoreCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWSubmitTimeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChapterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnvFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEnable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsScoreToCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManagerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCaculateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecialtyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublicUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamStartDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamEndDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSingleGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobDownLoadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScoreSubmitted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalExamTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountDownLoadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsUploadAnswerFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VideoDownLoadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.dgvResult);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 431);
            this.panel1.TabIndex = 13;
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.AllowUserToOrderColumns = true;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobNo,
            this.学生姓名,
            this.ShowScore,
            this.ShowAnalysis,
            this.ExamMode,
            this.CreateTime,
            this.IsAllowReSubmitScore,
            this.AllowReSubmitScoreCount,
            this.HWSubmitTimeType,
            this.CourseName,
            this.ChapterName,
            this.HWName,
            this.EnvFileName,
            this.FileName,
            this.FilePath,
            this.IsEnable,
            this.IsScoreToCenter,
            this.ManagerName,
            this.IsCaculateTime,
            this.ClassID,
            this.ClassName,
            this.SpecialtyID,
            this.CityID,
            this.PublicUserID,
            this.ExamStartDateTime,
            this.ExamEndDateTime,
            this.IsPay,
            this.IsSingleGrade,
            this.JobDownLoadState,
            this.ScoreSubmitted,
            this.TotalExamTime,
            this.AccountDownLoadState,
            this.IsUploadAnswerFile,
            this.VideoDownLoadState});
            this.dgvResult.Location = new System.Drawing.Point(12, 108);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(855, 311);
            this.dgvResult.TabIndex = 13;
            this.dgvResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResult_CellFormatting);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnAddVideoFile);
            this.panel2.Controls.Add(this.btnAddEnvfile);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cboSubject);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.cboJobState);
            this.panel2.Controls.Add(this.btnDoJob);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(855, 90);
            this.panel2.TabIndex = 11;
            // 
            // btnAddVideoFile
            // 
            this.btnAddVideoFile.Location = new System.Drawing.Point(373, 39);
            this.btnAddVideoFile.Name = "btnAddVideoFile";
            this.btnAddVideoFile.Size = new System.Drawing.Size(75, 23);
            this.btnAddVideoFile.TabIndex = 35;
            this.btnAddVideoFile.Text = "添加视频";
            this.btnAddVideoFile.UseVisualStyleBackColor = true;
            this.btnAddVideoFile.Click += new System.EventHandler(this.btnAddVideoFile_Click);
            // 
            // btnAddEnvfile
            // 
            this.btnAddEnvfile.Location = new System.Drawing.Point(292, 39);
            this.btnAddEnvfile.Name = "btnAddEnvfile";
            this.btnAddEnvfile.Size = new System.Drawing.Size(75, 23);
            this.btnAddEnvfile.TabIndex = 34;
            this.btnAddEnvfile.Text = "添加帐套";
            this.btnAddEnvfile.UseVisualStyleBackColor = true;
            this.btnAddEnvfile.Click += new System.EventHandler(this.btnAddEnvfile_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 32;
            this.label5.Text = "科目名称：";
            // 
            // cboSubject
            // 
            this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubject.FormattingEnabled = true;
            this.cboSubject.Location = new System.Drawing.Point(86, 41);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(200, 20);
            this.cboSubject.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "作业类型：";
            // 
            // cboJobState
            // 
            this.cboJobState.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJobState.FormattingEnabled = true;
            this.cboJobState.Location = new System.Drawing.Point(86, 15);
            this.cboJobState.Name = "cboJobState";
            this.cboJobState.Size = new System.Drawing.Size(200, 20);
            this.cboJobState.TabIndex = 31;
            // 
            // btnDoJob
            // 
            this.btnDoJob.Location = new System.Drawing.Point(373, 13);
            this.btnDoJob.Name = "btnDoJob";
            this.btnDoJob.Size = new System.Drawing.Size(75, 23);
            this.btnDoJob.TabIndex = 24;
            this.btnDoJob.Text = "做作业";
            this.btnDoJob.UseVisualStyleBackColor = true;
            this.btnDoJob.Click += new System.EventHandler(this.btnDoJob_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(292, 13);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "账套文件";
            this.ofdOpenFile.Filter = "账套文件|*.casf*";
            this.ofdOpenFile.Title = "账套文件";
            // 
            // JobNo
            // 
            this.JobNo.DataPropertyName = "JobNo";
            this.JobNo.HeaderText = "序号";
            this.JobNo.Name = "JobNo";
            this.JobNo.ReadOnly = true;
            // 
            // 学生姓名
            // 
            this.学生姓名.DataPropertyName = "RealName";
            this.学生姓名.HeaderText = "学生姓名";
            this.学生姓名.Name = "学生姓名";
            this.学生姓名.ReadOnly = true;
            // 
            // ShowScore
            // 
            this.ShowScore.DataPropertyName = "ShowScore";
            this.ShowScore.HeaderText = "ShowScore";
            this.ShowScore.Name = "ShowScore";
            this.ShowScore.ReadOnly = true;
            this.ShowScore.Visible = false;
            // 
            // ShowAnalysis
            // 
            this.ShowAnalysis.DataPropertyName = "ShowAnalysis";
            this.ShowAnalysis.HeaderText = "ShowAnalysis";
            this.ShowAnalysis.Name = "ShowAnalysis";
            this.ShowAnalysis.ReadOnly = true;
            this.ShowAnalysis.Visible = false;
            // 
            // ExamMode
            // 
            this.ExamMode.DataPropertyName = "ExamMode";
            this.ExamMode.HeaderText = "ExamMode";
            this.ExamMode.Name = "ExamMode";
            this.ExamMode.ReadOnly = true;
            this.ExamMode.Visible = false;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "CreateTime";
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            this.CreateTime.Visible = false;
            // 
            // IsAllowReSubmitScore
            // 
            this.IsAllowReSubmitScore.DataPropertyName = "IsAllowReSubmitScore";
            this.IsAllowReSubmitScore.HeaderText = "IsAllowReSubmitScore";
            this.IsAllowReSubmitScore.Name = "IsAllowReSubmitScore";
            this.IsAllowReSubmitScore.ReadOnly = true;
            this.IsAllowReSubmitScore.Visible = false;
            // 
            // AllowReSubmitScoreCount
            // 
            this.AllowReSubmitScoreCount.DataPropertyName = "AllowReSubmitScoreCount";
            this.AllowReSubmitScoreCount.HeaderText = "AllowReSubmitScoreCount";
            this.AllowReSubmitScoreCount.Name = "AllowReSubmitScoreCount";
            this.AllowReSubmitScoreCount.ReadOnly = true;
            this.AllowReSubmitScoreCount.Visible = false;
            // 
            // HWSubmitTimeType
            // 
            this.HWSubmitTimeType.DataPropertyName = "HWSubmitTimeType";
            this.HWSubmitTimeType.HeaderText = "HWSubmitTimeType";
            this.HWSubmitTimeType.Name = "HWSubmitTimeType";
            this.HWSubmitTimeType.ReadOnly = true;
            this.HWSubmitTimeType.Visible = false;
            // 
            // CourseName
            // 
            this.CourseName.DataPropertyName = "CourseName";
            this.CourseName.HeaderText = "科目名称";
            this.CourseName.Name = "CourseName";
            this.CourseName.ReadOnly = true;
            // 
            // ChapterName
            // 
            this.ChapterName.DataPropertyName = "ChapterName";
            this.ChapterName.HeaderText = "章节名称";
            this.ChapterName.Name = "ChapterName";
            this.ChapterName.ReadOnly = true;
            // 
            // HWName
            // 
            this.HWName.DataPropertyName = "HWName";
            this.HWName.HeaderText = "作业名称";
            this.HWName.Name = "HWName";
            this.HWName.ReadOnly = true;
            // 
            // EnvFileName
            // 
            this.EnvFileName.DataPropertyName = "EnvFileName";
            this.EnvFileName.HeaderText = "账套名称";
            this.EnvFileName.Name = "EnvFileName";
            this.EnvFileName.ReadOnly = true;
            // 
            // FileName
            // 
            this.FileName.DataPropertyName = "FileName";
            this.FileName.HeaderText = "FileName";
            this.FileName.Name = "FileName";
            this.FileName.ReadOnly = true;
            this.FileName.Visible = false;
            // 
            // FilePath
            // 
            this.FilePath.DataPropertyName = "FilePath";
            this.FilePath.HeaderText = "FilePath";
            this.FilePath.Name = "FilePath";
            this.FilePath.ReadOnly = true;
            this.FilePath.Visible = false;
            // 
            // IsEnable
            // 
            this.IsEnable.DataPropertyName = "IsEnable";
            this.IsEnable.HeaderText = "IsEnable";
            this.IsEnable.Name = "IsEnable";
            this.IsEnable.ReadOnly = true;
            this.IsEnable.Visible = false;
            // 
            // IsScoreToCenter
            // 
            this.IsScoreToCenter.DataPropertyName = "IsScoreToCenter";
            this.IsScoreToCenter.HeaderText = "IsScoreToCenter";
            this.IsScoreToCenter.Name = "IsScoreToCenter";
            this.IsScoreToCenter.ReadOnly = true;
            this.IsScoreToCenter.Visible = false;
            // 
            // ManagerName
            // 
            this.ManagerName.DataPropertyName = "ManagerName";
            this.ManagerName.HeaderText = "ManagerName";
            this.ManagerName.Name = "ManagerName";
            this.ManagerName.ReadOnly = true;
            this.ManagerName.Visible = false;
            // 
            // IsCaculateTime
            // 
            this.IsCaculateTime.DataPropertyName = "IsCaculateTime";
            this.IsCaculateTime.HeaderText = "IsCaculateTime";
            this.IsCaculateTime.Name = "IsCaculateTime";
            this.IsCaculateTime.ReadOnly = true;
            this.IsCaculateTime.Visible = false;
            // 
            // ClassID
            // 
            this.ClassID.DataPropertyName = "ClassID";
            this.ClassID.HeaderText = "ClassID";
            this.ClassID.Name = "ClassID";
            this.ClassID.ReadOnly = true;
            this.ClassID.Visible = false;
            // 
            // ClassName
            // 
            this.ClassName.DataPropertyName = "ClassName";
            this.ClassName.HeaderText = "ClassName";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            this.ClassName.Visible = false;
            // 
            // SpecialtyID
            // 
            this.SpecialtyID.DataPropertyName = "SpecialtyID";
            this.SpecialtyID.HeaderText = "SpecialtyID";
            this.SpecialtyID.Name = "SpecialtyID";
            this.SpecialtyID.ReadOnly = true;
            this.SpecialtyID.Visible = false;
            // 
            // CityID
            // 
            this.CityID.DataPropertyName = "CityID";
            this.CityID.HeaderText = "CityID";
            this.CityID.Name = "CityID";
            this.CityID.ReadOnly = true;
            this.CityID.Visible = false;
            // 
            // PublicUserID
            // 
            this.PublicUserID.DataPropertyName = "PublicUserID";
            this.PublicUserID.HeaderText = "PublicUserID";
            this.PublicUserID.Name = "PublicUserID";
            this.PublicUserID.ReadOnly = true;
            this.PublicUserID.Visible = false;
            // 
            // ExamStartDateTime
            // 
            this.ExamStartDateTime.DataPropertyName = "ExamStartDateTime";
            dataGridViewCellStyle1.Format = "D";
            dataGridViewCellStyle1.NullValue = null;
            this.ExamStartDateTime.DefaultCellStyle = dataGridViewCellStyle1;
            this.ExamStartDateTime.HeaderText = "开始时间";
            this.ExamStartDateTime.Name = "ExamStartDateTime";
            this.ExamStartDateTime.ReadOnly = true;
            this.ExamStartDateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExamEndDateTime
            // 
            this.ExamEndDateTime.DataPropertyName = "ExamEndDateTime";
            dataGridViewCellStyle2.Format = "d";
            dataGridViewCellStyle2.NullValue = null;
            this.ExamEndDateTime.DefaultCellStyle = dataGridViewCellStyle2;
            this.ExamEndDateTime.HeaderText = "结束时间";
            this.ExamEndDateTime.Name = "ExamEndDateTime";
            this.ExamEndDateTime.ReadOnly = true;
            this.ExamEndDateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // IsPay
            // 
            this.IsPay.DataPropertyName = "IsPay";
            this.IsPay.HeaderText = "IsPay";
            this.IsPay.Name = "IsPay";
            this.IsPay.ReadOnly = true;
            this.IsPay.Visible = false;
            // 
            // IsSingleGrade
            // 
            this.IsSingleGrade.DataPropertyName = "IsSingleGrade";
            this.IsSingleGrade.HeaderText = "IsSingleGrade";
            this.IsSingleGrade.Name = "IsSingleGrade";
            this.IsSingleGrade.ReadOnly = true;
            this.IsSingleGrade.Visible = false;
            // 
            // JobDownLoadState
            // 
            this.JobDownLoadState.DataPropertyName = "JobDownLoadState";
            this.JobDownLoadState.HeaderText = "作业下载状态";
            this.JobDownLoadState.Name = "JobDownLoadState";
            this.JobDownLoadState.ReadOnly = true;
            this.JobDownLoadState.Width = 120;
            // 
            // ScoreSubmitted
            // 
            this.ScoreSubmitted.DataPropertyName = "ScoreSubmitted";
            this.ScoreSubmitted.HeaderText = "作业上交状态";
            this.ScoreSubmitted.Name = "ScoreSubmitted";
            this.ScoreSubmitted.ReadOnly = true;
            this.ScoreSubmitted.Width = 120;
            // 
            // TotalExamTime
            // 
            this.TotalExamTime.DataPropertyName = "TotalExamTime";
            this.TotalExamTime.HeaderText = "TotalExamTime";
            this.TotalExamTime.Name = "TotalExamTime";
            this.TotalExamTime.ReadOnly = true;
            this.TotalExamTime.Visible = false;
            // 
            // AccountDownLoadState
            // 
            this.AccountDownLoadState.DataPropertyName = "AccountDownLoadState";
            this.AccountDownLoadState.HeaderText = "账套下载状态";
            this.AccountDownLoadState.Name = "AccountDownLoadState";
            this.AccountDownLoadState.ReadOnly = true;
            this.AccountDownLoadState.Width = 120;
            // 
            // IsUploadAnswerFile
            // 
            this.IsUploadAnswerFile.DataPropertyName = "IsUploadAnswerFile";
            this.IsUploadAnswerFile.HeaderText = "IsUploadAnswerFile";
            this.IsUploadAnswerFile.Name = "IsUploadAnswerFile";
            this.IsUploadAnswerFile.ReadOnly = true;
            this.IsUploadAnswerFile.Visible = false;
            // 
            // VideoDownLoadState
            // 
            this.VideoDownLoadState.DataPropertyName = "VideoDownLoadState";
            this.VideoDownLoadState.HeaderText = "视频下载状态";
            this.VideoDownLoadState.Name = "VideoDownLoadState";
            this.VideoDownLoadState.ReadOnly = true;
            // 
            // frmDownWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(879, 431);
            this.Controls.Add(this.panel1);
            this.Name = "frmDownWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "已下载作业";
            this.Load += new System.EventHandler(this.frmDownWork_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnDoJob;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSubject;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboJobState;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnAddEnvfile;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.Button btnAddVideoFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 学生姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowAnalysis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsAllowReSubmitScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllowReSubmitScoreCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWSubmitTimeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChapterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnvFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsScoreToCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ManagerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsCaculateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialtyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PublicUserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamStartDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamEndDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsPay;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsSingleGrade;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobDownLoadState;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScoreSubmitted;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalExamTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountDownLoadState;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsUploadAnswerFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn VideoDownLoadState;
    }
}