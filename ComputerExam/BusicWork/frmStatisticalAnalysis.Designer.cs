namespace ComputerExam.BusicWork
{
    partial class frmStatisticalAnalysis
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartMyJob = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.cboJob = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cboChartType = new System.Windows.Forms.ComboBox();
            this.cboSubject = new System.Windows.Forms.ComboBox();
            this.chartLine = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chartMyJob)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLine)).BeginInit();
            this.SuspendLayout();
            // 
            // chartMyJob
            // 
            this.chartMyJob.AccessibleDescription = "";
            this.chartMyJob.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.Angle = 90;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.Title = "作业（名称、状态）";
            chartArea1.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.AxisY.Title = "成绩（分）";
            chartArea1.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea1.Name = "ChartArea1";
            this.chartMyJob.ChartAreas.Add(chartArea1);
            this.chartMyJob.Location = new System.Drawing.Point(12, 108);
            this.chartMyJob.Name = "chartMyJob";
            series1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            series1.ChartArea = "ChartArea1";
            series1.Color = System.Drawing.Color.DodgerBlue;
            series1.EmptyPointStyle.LabelForeColor = System.Drawing.Color.Empty;
            series1.Label = "#VAL";
            series1.LegendText = "#VALX";
            series1.Name = "Series1";
            series1.XValueMember = "HwNameState";
            series1.YValueMembers = "TotalScore";
            this.chartMyJob.Series.Add(series1);
            this.chartMyJob.Size = new System.Drawing.Size(1095, 314);
            this.chartMyJob.TabIndex = 0;
            this.chartMyJob.Text = "chart1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(569, 36);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.cboJob);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.cboChartType);
            this.panel1.Controls.Add(this.cboSubject);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1095, 90);
            this.panel1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 34;
            this.label2.Text = "作业名称：";
            // 
            // cboJob
            // 
            this.cboJob.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboJob.FormattingEnabled = true;
            this.cboJob.Location = new System.Drawing.Point(86, 38);
            this.cboJob.Name = "cboJob";
            this.cboJob.Size = new System.Drawing.Size(200, 20);
            this.cboJob.TabIndex = 33;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(292, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 24;
            this.label1.Text = "图表类型：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "课程名称：";
            // 
            // cboChartType
            // 
            this.cboChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChartType.FormattingEnabled = true;
            this.cboChartType.Location = new System.Drawing.Point(363, 38);
            this.cboChartType.Name = "cboChartType";
            this.cboChartType.Size = new System.Drawing.Size(200, 20);
            this.cboChartType.TabIndex = 22;
            // 
            // cboSubject
            // 
            this.cboSubject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSubject.FormattingEnabled = true;
            this.cboSubject.Location = new System.Drawing.Point(86, 12);
            this.cboSubject.Name = "cboSubject";
            this.cboSubject.Size = new System.Drawing.Size(200, 20);
            this.cboSubject.TabIndex = 22;
            // 
            // chartLine
            // 
            this.chartLine.AccessibleDescription = "";
            this.chartLine.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.Title = "练习（次）";
            chartArea2.AxisX.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea2.AxisY.Title = "成绩（分）";
            chartArea2.AxisY.TitleAlignment = System.Drawing.StringAlignment.Far;
            chartArea2.Name = "ChartArea1";
            this.chartLine.ChartAreas.Add(chartArea2);
            this.chartLine.Location = new System.Drawing.Point(12, 108);
            this.chartLine.Name = "chartLine";
            series2.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.DiagonalRight;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.DodgerBlue;
            series2.EmptyPointStyle.LabelForeColor = System.Drawing.Color.Empty;
            series2.Label = "#VAL";
            series2.LegendText = "#VALX";
            series2.Name = "Series1";
            series2.XValueMember = "JobScoreNo";
            series2.YValueMembers = "TotalScore";
            this.chartLine.Series.Add(series2);
            this.chartLine.Size = new System.Drawing.Size(1095, 314);
            this.chartLine.TabIndex = 3;
            this.chartLine.Text = "chart1";
            // 
            // frmStatisticalAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1119, 434);
            this.Controls.Add(this.chartLine);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chartMyJob);
            this.Name = "frmStatisticalAnalysis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "统计分析";
            this.Load += new System.EventHandler(this.frmStatisticalAnalysis_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartMyJob)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartLine)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartMyJob;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSubject;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboChartType;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartLine;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboJob;
    }
}