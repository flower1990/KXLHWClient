﻿namespace ComputerExam.BusicWorkOffLine
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
            this.tsbHomeWork = new System.Windows.Forms.ToolStripButton();
            this.tsbExercise = new System.Windows.Forms.ToolStripButton();
            this.tsbUseManual = new System.Windows.Forms.ToolStripButton();
            this.tsbExit = new System.Windows.Forms.ToolStripButton();
            this.pnlContainer = new System.Windows.Forms.Panel();
            this.pnlBackground = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.pnlBackground.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
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
            this.tsbHomeWork,
            this.tsbExercise,
            this.tsbUseManual,
            this.tsbExit});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.ShowItemToolTips = false;
            this.toolStrip1.Size = new System.Drawing.Size(194, 476);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbHomeWork
            // 
            this.tsbHomeWork.AutoSize = false;
            this.tsbHomeWork.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsbHomeWork.BackgroundImage")));
            this.tsbHomeWork.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tsbHomeWork.Checked = true;
            this.tsbHomeWork.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tsbHomeWork.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbHomeWork.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbHomeWork.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbHomeWork.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbHomeWork.Name = "tsbHomeWork";
            this.tsbHomeWork.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsbHomeWork.Size = new System.Drawing.Size(192, 49);
            this.tsbHomeWork.Text = "我的作业";
            this.tsbHomeWork.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbHomeWork.Click += new System.EventHandler(this.tsbHomeWork_Click);
            this.tsbHomeWork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbExercise
            // 
            this.tsbExercise.AutoSize = false;
            this.tsbExercise.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsbExercise.BackgroundImage")));
            this.tsbExercise.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tsbExercise.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbExercise.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExercise.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExercise.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExercise.Name = "tsbExercise";
            this.tsbExercise.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsbExercise.Size = new System.Drawing.Size(192, 49);
            this.tsbExercise.Text = "考前练习";
            this.tsbExercise.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbExercise.Click += new System.EventHandler(this.tsbExercise_Click);
            this.tsbExercise.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbUseManual
            // 
            this.tsbUseManual.AutoSize = false;
            this.tsbUseManual.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsbUseManual.BackgroundImage")));
            this.tsbUseManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tsbUseManual.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbUseManual.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbUseManual.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbUseManual.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbUseManual.Name = "tsbUseManual";
            this.tsbUseManual.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsbUseManual.Size = new System.Drawing.Size(192, 49);
            this.tsbUseManual.Text = "使用手册";
            this.tsbUseManual.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tsbUseManual.Click += new System.EventHandler(this.tsbUseManual_Click);
            this.tsbUseManual.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tsbHomeWork_MouseDown);
            // 
            // tsbExit
            // 
            this.tsbExit.AutoSize = false;
            this.tsbExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tsbExit.BackgroundImage")));
            this.tsbExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.tsbExit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tsbExit.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.tsbExit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbExit.Name = "tsbExit";
            this.tsbExit.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.tsbExit.Size = new System.Drawing.Size(192, 49);
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
            this.pnlContainer.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pnlContainer.Location = new System.Drawing.Point(206, 128);
            this.pnlContainer.Name = "pnlContainer";
            this.pnlContainer.Size = new System.Drawing.Size(609, 482);
            this.pnlContainer.TabIndex = 2;
            // 
            // pnlBackground
            // 
            this.pnlBackground.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlBackground.BackColor = System.Drawing.Color.Transparent;
            this.pnlBackground.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pnlBackground.BackgroundImage")));
            this.pnlBackground.Controls.Add(this.lblTitle);
            this.pnlBackground.Location = new System.Drawing.Point(0, 0);
            this.pnlBackground.Name = "pnlBackground";
            this.pnlBackground.Size = new System.Drawing.Size(815, 122);
            this.pnlBackground.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(66, 44);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(322, 35);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "考学练 作业客户端";
            // 
            // frmBusicWorkMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(167)))), ((int)(((byte)(221)))));
            this.ClientSize = new System.Drawing.Size(815, 610);
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
            this.pnlBackground.ResumeLayout(false);
            this.pnlBackground.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBackground;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbHomeWork;
        private System.Windows.Forms.ToolStripButton tsbExercise;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ToolStripButton tsbExit;
        private System.Windows.Forms.ToolStripButton tsbUseManual;
    }
}