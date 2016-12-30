using ComputerExam.BLL;
using ComputerExam.Model;
using ComputerExam.Properties;
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

namespace ComputerExam.BusicWork
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
            //CommonUtil.InitialBackgroundImage(Globals.BGTitle, pnlBackground);
            this.Text = string.Format("{0} {1}", UserConfigSettings.Instance.ReadSetting("系统标题"), Globals.SystemVersion);
            //lblTitle.Text = UserConfigSettings.Instance.ReadSetting("系统标题");
        }

        private void frmBusicWorkMain_Load(object sender, EventArgs e)
        {
            PublicClass.SetFormSize(this);
            tsbNotice_Click(this, e);
        }

        private void tsbHomeWork_Click(object sender, EventArgs e)
        {
            FormBind(new frmHomeWork());
            ChangeBackgroundImage(tsbHomeWork);
        }

        private void tsbDownWork_Click(object sender, EventArgs e)
        {
            FormBind(new frmDownWork());
            ChangeBackgroundImage(tsbDownWork);
        }

        private void tsbBrowse_Click(object sender, EventArgs e)
        {
            FormBind(new frmWorkBrowse());
            ChangeBackgroundImage(tsbWorkBrowse);
        }

        private void tsbMyJobStatistics_Click(object sender, EventArgs e)
        {
            FormBind(new frmStatisticalAnalysis());
            ChangeBackgroundImage(tsbMyJobStatistics);
        }

        private void tsbExercise_Click(object sender, EventArgs e)
        {
            FormBind(new frmExercise());
            ChangeBackgroundImage(tsbExercise);
        }

        private void tsbExerciseBrowse_Click(object sender, EventArgs e)
        {
            FormBind(new frmExerciseBrowse());
            ChangeBackgroundImage(tsbExercise);
        }

        private void tsbExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("您确定退出吗？", "询问",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result == DialogResult.OK)
            {
                //退出
                if (Globals.IsOnline) bService.ExitSystem(PublicClass.StudentCode);
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
            //button.Checked = true;
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

        private void tsbResource_Click(object sender, EventArgs e)
        {
            FormBind(new frmResource());
            ChangeBackgroundImage(tsbResource);
        }

        private void tsbNotice_Click(object sender, EventArgs e)
        {
            FormBind(new frmNotice());
            ChangeBackgroundImage(tsbNotice);
        }

        private void ChangeBackgroundImage(ToolStripLabel tsb)
        {
            tsbNotice.BackgroundImage = Resources.公告浏览2;
            tsbNotice.ForeColor = Color.Black;
            tsbHomeWork.BackgroundImage = Resources.我的作业2;
            tsbHomeWork.ForeColor = Color.Black;
            tsbDownWork.BackgroundImage = Resources.已下载作业2;
            tsbDownWork.ForeColor = Color.Black;
            tsbWorkBrowse.BackgroundImage = Resources.成绩浏览2;
            tsbWorkBrowse.ForeColor = Color.Black;
            tsbMyJobStatistics.BackgroundImage = Resources.作业完成情况统计2;
            tsbMyJobStatistics.ForeColor = Color.Black;
            tsbExercise.BackgroundImage = Resources.考前练习2;
            tsbExercise.ForeColor = Color.Black;
            tsbResource.BackgroundImage = Resources.资源下载2;
            tsbResource.ForeColor = Color.Black;
            tsbUseManual.BackgroundImage = Resources.使用手册2;
            tsbUseManual.ForeColor = Color.Black;
            tsbExit.BackgroundImage = Resources.退出系统2;
            tsbExit.ForeColor = Color.Black;

            tsb.ForeColor = Color.White;
            switch (tsb.Name)
            {
                case "tsbNotice":
                    tsb.BackgroundImage = Resources.公告浏览1;
                    break;
                case "tsbHomeWork":
                    tsb.BackgroundImage = Resources.我的作业1;
                    break;
                case "tsbDownWork":
                    tsb.BackgroundImage = Resources.已下载作业1;
                    break;
                case "tsbWorkBrowse":
                    tsb.BackgroundImage = Resources.成绩浏览1;
                    break;
                case "tsbMyJobStatistics":
                    tsb.BackgroundImage = Resources.作业完成情况统计1;
                    break;
                case "tsbExercise":
                    tsb.BackgroundImage = Resources.考前练习1;
                    break;
                case "tsbResource":
                    tsb.BackgroundImage = Resources.资源下载1;
                    break;
                case "tsbUseManual":
                    tsb.BackgroundImage = Resources.使用手册1;
                    break;
                case "tsbExit":
                    tsb.BackgroundImage = Resources.退出系统1;
                    break;
                default:
                    break;
            }
        }
    }
}
