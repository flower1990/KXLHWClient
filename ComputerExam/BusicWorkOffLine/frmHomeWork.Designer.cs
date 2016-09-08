namespace ComputerExam.BusicWorkOffLine
{
    partial class frmHomeWork
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDoJob = new System.Windows.Forms.Button();
            this.btnRestoreJob = new System.Windows.Forms.Button();
            this.ofdOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.IsUploadAnswerFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AccountDownLoadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScoreSubmitted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobDownLoadState = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsSingleGrade = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsPay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamEndDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamStartDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PublicUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecialtyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsCalculateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ManagerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsScoreToCenter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsEnable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWFilePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EnvFileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChapterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWSubmitTimeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AllocReSubmitScoreCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsAllowReSubmitScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamMode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowAnalysis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ShowScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.学生姓名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.JobNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
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
            this.panel1.Size = new System.Drawing.Size(863, 424);
            this.panel1.TabIndex = 13;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDoJob);
            this.panel2.Controls.Add(this.btnRestoreJob);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(839, 63);
            this.panel2.TabIndex = 11;
            // 
            // btnDoJob
            // 
            this.btnDoJob.Enabled = false;
            this.btnDoJob.Location = new System.Drawing.Point(102, 15);
            this.btnDoJob.Name = "btnDoJob";
            this.btnDoJob.Size = new System.Drawing.Size(75, 23);
            this.btnDoJob.TabIndex = 14;
            this.btnDoJob.Text = "做作业";
            this.btnDoJob.UseVisualStyleBackColor = true;
            this.btnDoJob.Click += new System.EventHandler(this.btnDoJob_Click);
            // 
            // btnRestoreJob
            // 
            this.btnRestoreJob.Location = new System.Drawing.Point(21, 15);
            this.btnRestoreJob.Name = "btnRestoreJob";
            this.btnRestoreJob.Size = new System.Drawing.Size(75, 23);
            this.btnRestoreJob.TabIndex = 13;
            this.btnRestoreJob.Text = "还原作业";
            this.btnRestoreJob.UseVisualStyleBackColor = true;
            this.btnRestoreJob.Click += new System.EventHandler(this.btnRestoreJob_Click);
            // 
            // ofdOpenFile
            // 
            this.ofdOpenFile.FileName = "作业文件";
            this.ofdOpenFile.Filter = "作业文件|*.zip*";
            this.ofdOpenFile.Title = "作业文件";
            // 
            // IsUploadAnswerFile
            // 
            this.IsUploadAnswerFile.DataPropertyName = "IsUploadAnswerFile";
            this.IsUploadAnswerFile.HeaderText = "IsUploadAnswerFile";
            this.IsUploadAnswerFile.Name = "IsUploadAnswerFile";
            this.IsUploadAnswerFile.ReadOnly = true;
            this.IsUploadAnswerFile.Visible = false;
            // 
            // AccountDownLoadState
            // 
            this.AccountDownLoadState.DataPropertyName = "AccountDownLoadState";
            this.AccountDownLoadState.HeaderText = "账套下载状态";
            this.AccountDownLoadState.Name = "AccountDownLoadState";
            this.AccountDownLoadState.ReadOnly = true;
            this.AccountDownLoadState.Width = 120;
            // 
            // ClassID
            // 
            this.ClassID.DataPropertyName = "ClassID";
            this.ClassID.HeaderText = "ClassID";
            this.ClassID.Name = "ClassID";
            this.ClassID.ReadOnly = true;
            this.ClassID.Visible = false;
            // 
            // ScoreSubmitted
            // 
            this.ScoreSubmitted.DataPropertyName = "ScoreSubmitted";
            this.ScoreSubmitted.HeaderText = "作业上交状态";
            this.ScoreSubmitted.Name = "ScoreSubmitted";
            this.ScoreSubmitted.ReadOnly = true;
            this.ScoreSubmitted.Width = 120;
            // 
            // JobDownLoadState
            // 
            this.JobDownLoadState.DataPropertyName = "JobDownLoadState";
            this.JobDownLoadState.HeaderText = "作业下载状态";
            this.JobDownLoadState.Name = "JobDownLoadState";
            this.JobDownLoadState.ReadOnly = true;
            this.JobDownLoadState.Width = 120;
            // 
            // IsSingleGrade
            // 
            this.IsSingleGrade.DataPropertyName = "IsSingleGrade";
            this.IsSingleGrade.HeaderText = "IsSingleGrade";
            this.IsSingleGrade.Name = "IsSingleGrade";
            this.IsSingleGrade.ReadOnly = true;
            this.IsSingleGrade.Visible = false;
            // 
            // IsPay
            // 
            this.IsPay.DataPropertyName = "IsPay";
            this.IsPay.HeaderText = "IsPay";
            this.IsPay.Name = "IsPay";
            this.IsPay.ReadOnly = true;
            this.IsPay.Visible = false;
            // 
            // ExamEndDateTime
            // 
            this.ExamEndDateTime.DataPropertyName = "ExamEndDateTime";
            dataGridViewCellStyle4.Format = "d";
            dataGridViewCellStyle4.NullValue = null;
            this.ExamEndDateTime.DefaultCellStyle = dataGridViewCellStyle4;
            this.ExamEndDateTime.HeaderText = "结束时间";
            this.ExamEndDateTime.Name = "ExamEndDateTime";
            this.ExamEndDateTime.ReadOnly = true;
            this.ExamEndDateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ExamStartDateTime
            // 
            this.ExamStartDateTime.DataPropertyName = "ExamStartDateTime";
            dataGridViewCellStyle3.Format = "D";
            dataGridViewCellStyle3.NullValue = null;
            this.ExamStartDateTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.ExamStartDateTime.HeaderText = "开始时间";
            this.ExamStartDateTime.Name = "ExamStartDateTime";
            this.ExamStartDateTime.ReadOnly = true;
            this.ExamStartDateTime.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PublicUserID
            // 
            this.PublicUserID.DataPropertyName = "PublicUserID";
            this.PublicUserID.HeaderText = "PublicUserID";
            this.PublicUserID.Name = "PublicUserID";
            this.PublicUserID.ReadOnly = true;
            this.PublicUserID.Visible = false;
            // 
            // CityID
            // 
            this.CityID.DataPropertyName = "CityID";
            this.CityID.HeaderText = "CityID";
            this.CityID.Name = "CityID";
            this.CityID.ReadOnly = true;
            this.CityID.Visible = false;
            // 
            // SpecialtyID
            // 
            this.SpecialtyID.DataPropertyName = "SpecialtyID";
            this.SpecialtyID.HeaderText = "SpecialtyID";
            this.SpecialtyID.Name = "SpecialtyID";
            this.SpecialtyID.ReadOnly = true;
            this.SpecialtyID.Visible = false;
            // 
            // ClassName
            // 
            this.ClassName.DataPropertyName = "ClassName";
            this.ClassName.HeaderText = "ClassName";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
            this.ClassName.Visible = false;
            // 
            // IsCalculateTime
            // 
            this.IsCalculateTime.DataPropertyName = "IsCalculateTime";
            this.IsCalculateTime.HeaderText = "IsCalculateTime";
            this.IsCalculateTime.Name = "IsCalculateTime";
            this.IsCalculateTime.ReadOnly = true;
            this.IsCalculateTime.Visible = false;
            // 
            // ManagerName
            // 
            this.ManagerName.DataPropertyName = "ManagerName";
            this.ManagerName.HeaderText = "ManagerName";
            this.ManagerName.Name = "ManagerName";
            this.ManagerName.ReadOnly = true;
            this.ManagerName.Visible = false;
            // 
            // IsScoreToCenter
            // 
            this.IsScoreToCenter.DataPropertyName = "IsScoreToCenter";
            this.IsScoreToCenter.HeaderText = "IsScoreToCenter";
            this.IsScoreToCenter.Name = "IsScoreToCenter";
            this.IsScoreToCenter.ReadOnly = true;
            this.IsScoreToCenter.Visible = false;
            // 
            // IsEnable
            // 
            this.IsEnable.DataPropertyName = "IsEnable";
            this.IsEnable.HeaderText = "IsEnable";
            this.IsEnable.Name = "IsEnable";
            this.IsEnable.ReadOnly = true;
            this.IsEnable.Visible = false;
            // 
            // HWFilePath
            // 
            this.HWFilePath.DataPropertyName = "HWFilePath";
            this.HWFilePath.HeaderText = "HWFilePath";
            this.HWFilePath.Name = "HWFilePath";
            this.HWFilePath.ReadOnly = true;
            this.HWFilePath.Visible = false;
            // 
            // HWFileName
            // 
            this.HWFileName.DataPropertyName = "HWFileName";
            this.HWFileName.HeaderText = "HWFileName";
            this.HWFileName.Name = "HWFileName";
            this.HWFileName.ReadOnly = true;
            this.HWFileName.Visible = false;
            // 
            // EnvFileName
            // 
            this.EnvFileName.DataPropertyName = "EnvFileName";
            this.EnvFileName.HeaderText = "账套名称";
            this.EnvFileName.Name = "EnvFileName";
            this.EnvFileName.ReadOnly = true;
            // 
            // HWName
            // 
            this.HWName.DataPropertyName = "HWName";
            this.HWName.HeaderText = "作业名称";
            this.HWName.Name = "HWName";
            this.HWName.ReadOnly = true;
            // 
            // ChapterName
            // 
            this.ChapterName.DataPropertyName = "ChapterName";
            this.ChapterName.HeaderText = "章节名称";
            this.ChapterName.Name = "ChapterName";
            this.ChapterName.ReadOnly = true;
            // 
            // CourseName
            // 
            this.CourseName.DataPropertyName = "CourseName";
            this.CourseName.HeaderText = "科目名称";
            this.CourseName.Name = "CourseName";
            this.CourseName.ReadOnly = true;
            // 
            // HWSubmitTimeType
            // 
            this.HWSubmitTimeType.DataPropertyName = "HWSubmitTimeType";
            this.HWSubmitTimeType.HeaderText = "HWSubmitTimeType";
            this.HWSubmitTimeType.Name = "HWSubmitTimeType";
            this.HWSubmitTimeType.ReadOnly = true;
            this.HWSubmitTimeType.Visible = false;
            // 
            // AllocReSubmitScoreCount
            // 
            this.AllocReSubmitScoreCount.DataPropertyName = "AllocReSubmitScoreCount";
            this.AllocReSubmitScoreCount.HeaderText = "AllocReSubmitScoreCount";
            this.AllocReSubmitScoreCount.Name = "AllocReSubmitScoreCount";
            this.AllocReSubmitScoreCount.ReadOnly = true;
            this.AllocReSubmitScoreCount.Visible = false;
            // 
            // IsAllowReSubmitScore
            // 
            this.IsAllowReSubmitScore.DataPropertyName = "IsAllowReSubmitScore";
            this.IsAllowReSubmitScore.HeaderText = "IsAllowReSubmitScore";
            this.IsAllowReSubmitScore.Name = "IsAllowReSubmitScore";
            this.IsAllowReSubmitScore.ReadOnly = true;
            this.IsAllowReSubmitScore.Visible = false;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "CreateTime";
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            this.CreateTime.Visible = false;
            // 
            // ExamMode
            // 
            this.ExamMode.DataPropertyName = "ExamMode";
            this.ExamMode.HeaderText = "ExamMode";
            this.ExamMode.Name = "ExamMode";
            this.ExamMode.ReadOnly = true;
            this.ExamMode.Visible = false;
            // 
            // ShowAnalysis
            // 
            this.ShowAnalysis.DataPropertyName = "ShowAnalysis";
            this.ShowAnalysis.HeaderText = "ShowAnalysis";
            this.ShowAnalysis.Name = "ShowAnalysis";
            this.ShowAnalysis.ReadOnly = true;
            this.ShowAnalysis.Visible = false;
            // 
            // ShowScore
            // 
            this.ShowScore.DataPropertyName = "ShowScore";
            this.ShowScore.HeaderText = "ShowScore";
            this.ShowScore.Name = "ShowScore";
            this.ShowScore.ReadOnly = true;
            this.ShowScore.Visible = false;
            // 
            // 学生姓名
            // 
            this.学生姓名.DataPropertyName = "RealName";
            this.学生姓名.HeaderText = "学生姓名";
            this.学生姓名.Name = "学生姓名";
            this.学生姓名.ReadOnly = true;
            // 
            // JobNo
            // 
            this.JobNo.DataPropertyName = "JobNo";
            this.JobNo.HeaderText = "序号";
            this.JobNo.Name = "JobNo";
            this.JobNo.ReadOnly = true;
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
            this.AllocReSubmitScoreCount,
            this.HWSubmitTimeType,
            this.CourseName,
            this.ChapterName,
            this.HWName,
            this.EnvFileName,
            this.HWFileName,
            this.HWFilePath,
            this.IsEnable,
            this.IsScoreToCenter,
            this.ManagerName,
            this.IsCalculateTime,
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
            this.ClassID,
            this.AccountDownLoadState,
            this.IsUploadAnswerFile});
            this.dgvResult.Location = new System.Drawing.Point(12, 81);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(839, 331);
            this.dgvResult.TabIndex = 12;
            this.dgvResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResult_CellFormatting);
            // 
            // frmHomeWork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 424);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "frmHomeWork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "我的作业";
            this.Load += new System.EventHandler(this.frmHomeWork_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnRestoreJob;
        private System.Windows.Forms.Button btnDoJob;
        private System.Windows.Forms.OpenFileDialog ofdOpenFile;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn 学生姓名;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn ShowAnalysis;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamMode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsAllowReSubmitScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn AllocReSubmitScoreCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWSubmitTimeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChapterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWName;
        private System.Windows.Forms.DataGridViewTextBoxColumn EnvFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWFileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWFilePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsEnable;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsScoreToCenter;
        private System.Windows.Forms.DataGridViewTextBoxColumn ManagerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsCalculateTime;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassID;
        private System.Windows.Forms.DataGridViewTextBoxColumn AccountDownLoadState;
        private System.Windows.Forms.DataGridViewTextBoxColumn IsUploadAnswerFile;
    }
}