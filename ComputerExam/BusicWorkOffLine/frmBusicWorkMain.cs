﻿using ComputerExam.BLL;
using ComputerExam.Model;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComputerExam.BusicWorkOffLine
{
    public partial class frmBusicWorkMain : Form
    {
        PublicClass publicClass = new PublicClass();
        B_Service bService = new B_Service();

        private void FormBind(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();

            pnlContainer.Controls.Clear();
            pnlContainer.Controls.Add(form);
        }

        //重写父类方法，来改变系统关闭按钮动作
        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x0112;
            const int SC_CLOSE = 0xF060;
            if (m.Msg == WM_SYSCOMMAND && (int)m.WParam == SC_CLOSE)
            {
                //弹出提示框，采集用户意愿，判断是否退出程序
                DialogResult result =
                    MessageBox.Show("您确定退出吗？", "询问",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    Environment.Exit(0);
                }
                else
                {
                    return;
                }
            }
            base.WndProc(ref m);
        }

        public frmBusicWorkMain()
        {
            InitializeComponent();
            CommonUtil.InitialBackgroundImage(Globals.BGTitle, pnlBackground);
            this.Text = string.Format("{0} {1}", Globals.SystemVersion, UserConfigSettings.Instance.ReadSetting("系统标题"));
            lblTitle.Text = UserConfigSettings.Instance.ReadSetting("系统标题");
        }

        private void frmBusicWorkMain_Load(object sender, EventArgs e)
        {
            PublicClass.SetFormSize(this);

            tsbHomeWork_Click(this, e);
        }

        private void tsbHomeWork_Click(object sender, EventArgs e)
        {
            FormBind(new frmHomeWork());
        }

        private void tsbExercise_Click(object sender, EventArgs e)
        {
            FormBind(new frmExercise());
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("您确定退出吗？", "询问",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                //退出
                Environment.Exit(0);
            }
        }

        private void tsbHomeWork_MouseDown(object sender, MouseEventArgs e)
        {
            foreach (ToolStripItem item in toolStrip1.Items)
            {
                if (item is ToolStripButton)
                {
                    ToolStripButton stripButton = item as ToolStripButton;
                    stripButton.Checked = false;
                }
            }

            ToolStripButton button = sender as ToolStripButton;
            button.Checked = true;
        }

        private void tsbUseManual_Click(object sender, EventArgs e)
        {
            string useManualPath = Application.StartupPath + @"\SowerTestClient\System\朔日（云+）作业客户端使用手册.doc";
            if (File.Exists(useManualPath))
            {
                Process.Start(useManualPath);
            }
            else
            {
                PublicClass.ShowErrorMessageOk("系统没有找到使用手册，请重新安装作业系统。");
            }
        }
    }
}
