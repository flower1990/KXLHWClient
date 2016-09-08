namespace ComputerExam.BusicWork
{
    partial class frmWorkBrowse
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAverage = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTrainingCount = new System.Windows.Forms.Label();
            this.dgvResult = new System.Windows.Forms.DataGridView();
            this.JobScoreNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StudentCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RealName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ClassName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CourseName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChapterName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TeacherName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalScore = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScoreSubmitted = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ExamEndDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWSubmitTimeType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ScoreSubmitTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HWPublicUserID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DepartmentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SpecialtyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboJob = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSubject = new System.Windows.Forms.ComboBox();
            this.btnScoreDetail = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.dgvResult);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 386);
            this.panel1.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.lblAverage);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.lblTrainingCount);
            this.panel3.Location = new System.Drawing.Point(12, 108);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(825, 60);
            this.panel3.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(243, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 24);
            this.label4.TabIndex = 13;
            this.label4.Text = "练习平均分：";
            // 
            // lblAverage
            // 
            this.lblAverage.AutoSize = true;
            this.lblAverage.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblAverage.ForeColor = System.Drawing.Color.Green;
            this.lblAverage.Location = new System.Drawing.Point(403, 18);
            this.lblAverage.Name = "lblAverage";
            this.lblAverage.Size = new System.Drawing.Size(22, 24);
            this.lblAverage.TabIndex = 14;
            this.lblAverage.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "练习次数：";
            // 
            // lblTrainingCount
            // 
            this.lblTrainingCount.AutoSize = true;
            this.lblTrainingCount.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTrainingCount.ForeColor = System.Drawing.Color.Green;
            this.lblTrainingCount.Location = new System.Drawing.Point(159, 18);
            this.lblTrainingCount.Name = "lblTrainingCount";
            this.lblTrainingCount.Size = new System.Drawing.Size(22, 24);
            this.lblTrainingCount.TabIndex = 14;
            this.lblTrainingCount.Text = "0";
            // 
            // dgvResult
            // 
            this.dgvResult.AllowUserToAddRows = false;
            this.dgvResult.AllowUserToDeleteRows = false;
            this.dgvResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResult.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.JobScoreNo,
            this.StudentCode,
            this.RealName,
            this.ClassName,
            this.CourseName,
            this.ChapterName,
            this.HWID,
            this.HWName,
            this.TeacherName,
            this.TotalScore,
            this.ScoreSubmitted,
            this.ExamEndDateTime,
            this.HWSubmitTimeType,
            this.ScoreSubmitTime,
            this.HWPublicUserID,
            this.DepartmentID,
            this.SpecialtyID,
            this.Stat});
            this.dgvResult.Location = new System.Drawing.Point(12, 174);
            this.dgvResult.Name = "dgvResult";
            this.dgvResult.ReadOnly = true;
            this.dgvResult.RowTemplate.Height = 23;
            this.dgvResult.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvResult.Size = new System.Drawing.Size(825, 200);
            this.dgvResult.TabIndex = 13;
            this.dgvResult.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvResult_CellFormatting);
            // 
            // JobScoreNo
            // 
            this.JobScoreNo.DataPropertyName = "JobScoreNo";
            this.JobScoreNo.HeaderText = "序号";
            this.JobScoreNo.Name = "JobScoreNo";
            this.JobScoreNo.ReadOnly = true;
            // 
            // StudentCode
            // 
            this.StudentCode.DataPropertyName = "StudentCode";
            this.StudentCode.HeaderText = "StudentCode";
            this.StudentCode.Name = "StudentCode";
            this.StudentCode.ReadOnly = true;
            this.StudentCode.Visible = false;
            // 
            // RealName
            // 
            this.RealName.DataPropertyName = "RealName";
            this.RealName.HeaderText = "姓名";
            this.RealName.Name = "RealName";
            this.RealName.ReadOnly = true;
            // 
            // ClassName
            // 
            this.ClassName.DataPropertyName = "ClassName";
            this.ClassName.HeaderText = "班级";
            this.ClassName.Name = "ClassName";
            this.ClassName.ReadOnly = true;
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
            this.ChapterName.HeaderText = "章节";
            this.ChapterName.Name = "ChapterName";
            this.ChapterName.ReadOnly = true;
            // 
            // HWID
            // 
            this.HWID.DataPropertyName = "HWID";
            this.HWID.HeaderText = "HWID";
            this.HWID.Name = "HWID";
            this.HWID.ReadOnly = true;
            this.HWID.Visible = false;
            // 
            // HWName
            // 
            this.HWName.DataPropertyName = "HWName";
            this.HWName.HeaderText = "作业名称";
            this.HWName.Name = "HWName";
            this.HWName.ReadOnly = true;
            this.HWName.Width = 200;
            // 
            // TeacherName
            // 
            this.TeacherName.DataPropertyName = "TeacherName";
            this.TeacherName.HeaderText = "TeacherName";
            this.TeacherName.Name = "TeacherName";
            this.TeacherName.ReadOnly = true;
            this.TeacherName.Visible = false;
            // 
            // TotalScore
            // 
            this.TotalScore.DataPropertyName = "TotalScore";
            this.TotalScore.HeaderText = "作业成绩";
            this.TotalScore.Name = "TotalScore";
            this.TotalScore.ReadOnly = true;
            // 
            // ScoreSubmitted
            // 
            this.ScoreSubmitted.DataPropertyName = "ScoreSubmitted";
            this.ScoreSubmitted.HeaderText = "ScoreSubmitted";
            this.ScoreSubmitted.Name = "ScoreSubmitted";
            this.ScoreSubmitted.ReadOnly = true;
            this.ScoreSubmitted.Visible = false;
            // 
            // ExamEndDateTime
            // 
            this.ExamEndDateTime.DataPropertyName = "ExamEndDateTime";
            this.ExamEndDateTime.HeaderText = "ExamEndDateTime";
            this.ExamEndDateTime.Name = "ExamEndDateTime";
            this.ExamEndDateTime.ReadOnly = true;
            this.ExamEndDateTime.Visible = false;
            // 
            // HWSubmitTimeType
            // 
            this.HWSubmitTimeType.DataPropertyName = "HWSubmitTimeType";
            this.HWSubmitTimeType.HeaderText = "HWSubmitTimeType";
            this.HWSubmitTimeType.Name = "HWSubmitTimeType";
            this.HWSubmitTimeType.ReadOnly = true;
            this.HWSubmitTimeType.Visible = false;
            // 
            // ScoreSubmitTime
            // 
            this.ScoreSubmitTime.DataPropertyName = "ScoreSubmitTime";
            this.ScoreSubmitTime.HeaderText = "提交时间";
            this.ScoreSubmitTime.Name = "ScoreSubmitTime";
            this.ScoreSubmitTime.ReadOnly = true;
            // 
            // HWPublicUserID
            // 
            this.HWPublicUserID.DataPropertyName = "HWPublicUserID";
            this.HWPublicUserID.HeaderText = "HWPublicUserID";
            this.HWPublicUserID.Name = "HWPublicUserID";
            this.HWPublicUserID.ReadOnly = true;
            this.HWPublicUserID.Visible = false;
            // 
            // DepartmentID
            // 
            this.DepartmentID.DataPropertyName = "DepartmentID";
            this.DepartmentID.HeaderText = "DepartmentID";
            this.DepartmentID.Name = "DepartmentID";
            this.DepartmentID.ReadOnly = true;
            this.DepartmentID.Visible = false;
            // 
            // SpecialtyID
            // 
            this.SpecialtyID.DataPropertyName = "SpecialtyID";
            this.SpecialtyID.HeaderText = "SpecialtyID";
            this.SpecialtyID.Name = "SpecialtyID";
            this.SpecialtyID.ReadOnly = true;
            this.SpecialtyID.Visible = false;
            // 
            // Stat
            // 
            this.Stat.DataPropertyName = "Stat";
            this.Stat.HeaderText = "提交状态";
            this.Stat.Name = "Stat";
            this.Stat.ReadOnly = true;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cboJob);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.cboSubject);
            this.panel2.Controls.Add(this.btnScoreDetail);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Location = new System.Drawing.Point(12, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(825, 90);
            this.panel2.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 32;
            this.label2.Text = "作业名称：";
            // 
            // cboJob
            // 
            this.cboJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJob.FormattingEnabled = true;
            this.cboJob.Location = new System.Drawing.Point(86, 38);
            this.cboJob.Name = "cboJob";
            this.cboJob.Size = new System.Drawing.Size(200, 20);
            this.cboJob.TabIndex = 31;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 26;
            this.label5.Text = "课程名称：";
            // 
            // cboSubject
            // 
            this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubject.FormattingEnabled = true;
            this.cboSubject.Location = new System.Drawing.Point(86, 12);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(200, 20);
            this.cboSubject.TabIndex = 25;
            // 
            // btnScoreDetail
            // 
            this.btnScoreDetail.Location = new System.Drawing.Point(292, 36);
            this.btnScoreDetail.Name = "btnScoreDetail";
            this.btnScoreDetail.Size = new System.Drawing.Size(75, 23);
            this.btnScoreDetail.TabIndex = 13;
            this.btnScoreDetail.Text = "成绩明细";
            this.btnScoreDetail.UseVisualStyleBackColor = true;
            this.btnScoreDetail.Visible = false;
            this.btnScoreDetail.Click += new System.EventHandler(this.btnScoreDetail_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(292, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // frmWorkBrowse
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(849, 386);
            this.Controls.Add(this.panel1);
            this.Name = "frmWorkBrowse";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "成绩浏览";
            this.Load += new System.EventHandler(this.frmBrowse_Load);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResult)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvResult;
        private System.Windows.Forms.Button btnScoreDetail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSubject;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAverage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTrainingCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn JobScoreNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn StudentCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn RealName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ClassName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CourseName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChapterName;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWID;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TeacherName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalScore;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScoreSubmitted;
        private System.Windows.Forms.DataGridViewTextBoxColumn ExamEndDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWSubmitTimeType;
        private System.Windows.Forms.DataGridViewTextBoxColumn ScoreSubmitTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn HWPublicUserID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DepartmentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SpecialtyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Stat;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboJob;
    }
}