namespace ComputerExam.BusicWork
{
    partial class frmBusicWorkMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBusicWorkMain));
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbNotice = new System.Windows.Forms.ToolStripLabel();
            this.tsbHomeWork = new System.Windows.Forms.ToolStripLabel();
            this.tsbDownWork = new System.Windows.Forms.ToolStripLabel();
            this.tsbWorkBrowse = new System.Windows.Forms.ToolStripLabel();
            this.tsbMyJobStatistics = new System.Windows.Forms.ToolStripLabel();
            this.tsbExercise = new System.Windows.Forms.ToolStripLabel();
            this.tsbResource = new System.Windows.Forms.ToolStripLabel();
            this.tsbUseManual = new System.Windows.Forms.ToolStripLabel();
            this.tsbExit = new System.Windows.Forms.ToolStripLabel();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(224)))), ((int)(((byte)(241)))));
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Location = new System.Drawing.Point(0, 128);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 482);
            this.panel2.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNotice,
            this.tsbHomeWork,
            this.tsbDownWork,
            this.tsbWorkBrowse,
            this.tsbMyJobStatistics,
            this.tsbExercise,
            this.tsbResource,
            this.tsbUseManual,
            this.tsbExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(200, 476);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbNotice
            // 
            this.tsbNotice.AutoSize = false;
            this.tsbNotice.BackgroundImage = global::ComputerExam.Properties.Resources.公告浏览2;
            this.tsbNotice.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbNotice.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbNotice.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbNotice.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNotice.Margin = new System.Windows.Forms.Padding(0);
            this.tsbNotice.Name = "tsbNotice";
            this.tsbNotice.Size = new System.Drawing.Size(200, 49);
            this.tsbNotice.Text = "公告浏览";
            this.tsbNotice.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbNotice.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbNotice.ToolTipText = "公告浏览";
            this.tsbNotice.Click += new System.EventHandler(this.tsbNotice_Click);
            // 
            // tsbHomeWork
            // 
            this.tsbHomeWork.AutoSize = false;
            this.tsbHomeWork.BackgroundImage = global::ComputerExam.Properties.Resources.我的作业2;
            this.tsbHomeWork.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbHomeWork.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbHomeWork.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHomeWork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHomeWork.Margin = new System.Windows.Forms.Padding(0);
            this.tsbHomeWork.Name = "tsbHomeWork";
            this.tsbHomeWork.Size = new System.Drawing.Size(200, 49);
            this.tsbHomeWork.Text = "我的作业";
            this.tsbHomeWork.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbHomeWork.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            this.tsbHomeWork.Click += new System.EventHandler(this.tsbHomeWork_Click);
            this.tsbHomeWork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbDownWork
            // 
            this.tsbDownWork.AutoSize = false;
            this.tsbDownWork.BackgroundImage = global::ComputerExam.Properties.Resources.已下载作业2;
            this.tsbDownWork.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbDownWork.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbDownWork.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDownWork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDownWork.Margin = new System.Windows.Forms.Padding(0);
            this.tsbDownWork.Name = "tsbDownWork";
            this.tsbDownWork.Size = new System.Drawing.Size(200, 49);
            this.tsbDownWork.Text = "   已下载作业";
            this.tsbDownWork.Click += new System.EventHandler(this.tsbDownWork_Click);
            this.tsbDownWork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbWorkBrowse
            // 
            this.tsbWorkBrowse.AutoSize = false;
            this.tsbWorkBrowse.BackgroundImage = global::ComputerExam.Properties.Resources.成绩浏览2;
            this.tsbWorkBrowse.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbWorkBrowse.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbWorkBrowse.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbWorkBrowse.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbWorkBrowse.Margin = new System.Windows.Forms.Padding(0);
            this.tsbWorkBrowse.Name = "tsbWorkBrowse";
            this.tsbWorkBrowse.Size = new System.Drawing.Size(200, 49);
            this.tsbWorkBrowse.Text = "      作业成绩浏览";
            this.tsbWorkBrowse.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbWorkBrowse.Click += new System.EventHandler(this.tsbBrowse_Click);
            this.tsbWorkBrowse.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbMyJobStatistics
            // 
            this.tsbMyJobStatistics.AutoSize = false;
            this.tsbMyJobStatistics.BackgroundImage = global::ComputerExam.Properties.Resources.作业完成情况统计2;
            this.tsbMyJobStatistics.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbMyJobStatistics.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbMyJobStatistics.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbMyJobStatistics.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbMyJobStatistics.Margin = new System.Windows.Forms.Padding(0);
            this.tsbMyJobStatistics.Name = "tsbMyJobStatistics";
            this.tsbMyJobStatistics.Size = new System.Drawing.Size(200, 49);
            this.tsbMyJobStatistics.Text = "      完成情况统计";
            this.tsbMyJobStatistics.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbMyJobStatistics.Click += new System.EventHandler(this.tsbMyJobStatistics_Click);
            this.tsbMyJobStatistics.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbExercise
            // 
            this.tsbExercise.AutoSize = false;
            this.tsbExercise.BackgroundImage = global::ComputerExam.Properties.Resources.考前练习2;
            this.tsbExercise.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbExercise.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExercise.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExercise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExercise.Margin = new System.Windows.Forms.Padding(0);
            this.tsbExercise.Name = "tsbExercise";
            this.tsbExercise.Size = new System.Drawing.Size(200, 49);
            this.tsbExercise.Text = "考前练习";
            this.tsbExercise.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbExercise.Click += new System.EventHandler(this.tsbExercise_Click);
            this.tsbExercise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbResource
            // 
            this.tsbResource.AutoSize = false;
            this.tsbResource.BackgroundImage = global::ComputerExam.Properties.Resources.资源下载2;
            this.tsbResource.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbResource.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbResource.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbResource.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbResource.Margin = new System.Windows.Forms.Padding(0);
            this.tsbResource.Name = "tsbResource";
            this.tsbResource.Size = new System.Drawing.Size(200, 49);
            this.tsbResource.Text = "资源下载";
            this.tsbResource.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbResource.Click += new System.EventHandler(this.tsbResource_Click);
            this.tsbResource.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbUseManual
            // 
            this.tsbUseManual.AutoSize = false;
            this.tsbUseManual.BackgroundImage = global::ComputerExam.Properties.Resources.使用手册2;
            this.tsbUseManual.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbUseManual.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbUseManual.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbUseManual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUseManual.Margin = new System.Windows.Forms.Padding(0);
            this.tsbUseManual.Name = "tsbUseManual";
            this.tsbUseManual.Size = new System.Drawing.Size(200, 49);
            this.tsbUseManual.Text = "使用手册";
            this.tsbUseManual.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbUseManual.Click += new System.EventHandler(this.tsbUseManual_Click);
            this.tsbUseManual.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbExit
            // 
            this.tsbExit.AutoSize = false;
            this.tsbExit.BackgroundImage = global::ComputerExam.Properties.Resources.退出系统2;
            this.tsbExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Margin = new System.Windows.Forms.Padding(0);
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Size = new System.Drawing.Size(200, 49);
            this.tsbExit.Text = "退出系统";
            this.tsbExit.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbExit.Click += new System.EventHandler(this.tsbExit_Click);
            this.tsbExit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // pnlContainer
            // 
            this.pnlContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlContainer.BackColor = System.Drawing.Color.White;
            this.pnlContainer.Location = new System.Drawing.Point(206, 128);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(707, 482);
            this.pnlContainer.TabIndex = 2;
            // 
            // pnlBackground
            // 
            this.pnlBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBackground.BackColor = System.Drawing.Color.Transparent;
            this.pnlBackground.BackgroundImage = global::ComputerExam.Properties.Resources.背景;
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(913, 122);
            this.pnlBackground.TabIndex = 0;
            // 
            // frmBusicWorkMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(913, 610);
            this.Controls.Add(this.pnlContainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlBackground);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBusicWorkMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "考学练 作业客户端 v5.0.B0727";
            this.Load += new System.EventHandler(this.frmBusicWorkMain_Load);
            this.panel2.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel tsbHomeWork;
        private System.Windows.Forms.ToolStripLabel tsbDownWork;
        private System.Windows.Forms.ToolStripLabel tsbWorkBrowse;
        private System.Windows.Forms.ToolStripLabel tsbExercise;
        private System.Windows.Forms.ToolStripLabel tsbExit;
        private System.Windows.Forms.ToolStripLabel tsbMyJobStatistics;
        private System.Windows.Forms.ToolStripLabel tsbUseManual;
        private System.Windows.Forms.ToolStripLabel tsbResource;
        private System.Windows.Forms.ToolStripLabel tsbNotice;
    }
}