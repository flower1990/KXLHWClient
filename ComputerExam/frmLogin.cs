using ComputerExam.BLL;
using ComputerExam.BusicWork;
using ComputerExam.Common;
using ComputerExam.Model;
using ComputerExam.StepWizard;
using ComputerExam.Util;
using ComputerExam.Util.WebReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace ComputerExam
{
    public delegate bool IsShowFormDelegate();

    public partial class frmLogin : Form
    {
        #region 全局变量
        string fileHost = "";

        ListUtil listUtil = new ListUtil();
        OpaqueCommand cmd = new OpaqueCommand();
        ServiceUtil serviceUtil = new ServiceUtil();

        B_Service bService = new B_Service();

        private const int INTERNET_CONNECTION_MODEM = 1;
        private const int INTERNET_CONNECTION_LAN = 2;
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("winInet.dll")]
        private static extern bool InternetGetConnectedState(ref int dwFlag, int dwReserved);
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        #endregion

        /// <summary>
        /// 显示登陆窗体.(公共静态方法)
        /// </summary>        
        public static bool Login()
        {
            frmLogin form = new frmLogin();
            DialogResult result = form.ShowDialog();
            bool ret = (result == DialogResult.OK);
            return ret;
        }
        /// <summary>
        /// 判断本地的连接状态
        /// </summary>
        /// <returns></returns>
        private static bool LocalConnectionStatus()
        {
            System.Int32 dwFlag = new Int32();
            if (!InternetGetConnectedState(ref dwFlag, 0))
            {
                Msg.ShowError("网络未连接，请检查网络！");
                return false;
            }
            else
            {
                if ((dwFlag & INTERNET_CONNECTION_MODEM) != 0)
                {
                    return true;
                }
                else if ((dwFlag & INTERNET_CONNECTION_LAN) != 0)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 登陆验证
        /// </summary>
        /// <returns></returns>
        private bool LoginValidate()
        {
            try
            {
                string message = string.Empty;
                string realName = string.Empty;
                string account = txtAccount.Text.Trim();
                string password = txtPassord.Text.Trim();
                DateTime serverTime = DateTime.Now;
                PublicClass.StudentCode = account;
                PublicClass.StudentPwd = password;
                RememberAccount(account, password);
                if (rdoOnline.Checked)
                {
                    CreateService();
                    realName = bService.GetUserInfo(account, password, out message, out serverTime);
                    if (string.IsNullOrEmpty(realName))
                    {
                        Msg.ShowError(message);
                        return false;
                    }
                    PublicClass.ExamineeName = realName;
                    Globals.ServerTime = serverTime;
                    InitialBaseData();
                }
                return true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
                PublicClass.ShowMessageOk(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 初始化基础数据
        /// </summary>
        private void InitialBaseData()
        {
            string message = "";

            CommonUtil.listJobState = listUtil.GetJobState();
            CommonUtil.listSubject = bService.GetExamSubject(PublicClass.StudentCode, out message);
            CommonUtil.listSubject.Insert(0, new M_MyJobSubject() { CourseID = "0", CourseName = "---请选择---" });
            CommonUtil.listMyJob = bService.GetMyJob(PublicClass.StudentCode, "1900-1-1", "2099-1-1", -1, out fileHost);
            CommonUtil.listResourceType = bService.GetResourceType();
            CommonUtil.listResourceType.Insert(0, new M_ResourceType() { ID = 0, Name = "---请选择---" });
            CommonUtil.listResourceModel = listUtil.GetResourceModel();
            CommonUtil.listChartType = listUtil.GetChartType();
            Globals.DownType = bService.GetSystemPara("作业中心配置", "作业上传FTP");
        }
        /// <summary>
        /// 记住账号
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        private void RememberAccount(string account, string password)
        {
            if (cbAccount.Checked)
            {
                UserConfigSettings.Instance.WriteSetting("账号", account);
                UserConfigSettings.Instance.WriteSetting("密码", DES.EncryStrHexUTF8(password, account));
            }
            else
            {
                UserConfigSettings.Instance.RemoveSetting("账号");
                UserConfigSettings.Instance.RemoveSetting("密码");
            }
        }
        /// <summary>
        /// 创建服务
        /// </summary>
        private void CreateService()
        {
            string ip = "http://" + UserConfigSettings.Instance.ReadSetting("服务地址");
            string path = UserConfigSettings.Instance.ReadSetting("虚拟目录");
            string url = ip + path;
            Globals.SERVICE = GetReJobDataHandler(url);
        }
        /// <summary>
        /// 获取服务执行程序
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public JobDataHandler GetReJobDataHandler(string url)
        {
            JobDataHandler rjdh = new JobDataHandler();
            rjdh.Url = url;
            rjdh.Timeout = 30000;

            MySoapHeader soapHeader = new MySoapHeader();
            soapHeader.AuthCode = "2015071794367825";

            rjdh.MySoapHeaderValue = soapHeader;

            return rjdh;
        }
        /// <summary>
        /// 检查更新
        /// </summary>
        /// <returns></returns>
        private bool CheckUpdateApp()
        {
            try
            {
                Int32 dwFlag = new Int32();
                string autoUpdatePath = Application.StartupPath + @"\ComputerExam.Update.exe";

                if (!InternetGetConnectedState(ref dwFlag, 0)) return false;
                if (!PublicClass.CheckForUpdate()) return false;
                if (!File.Exists(autoUpdatePath)) return false;

                Process.Start(autoUpdatePath);
                Environment.Exit(0);
            }
            catch (WebException)
            {
                LogHelper.WriteLog(typeof(frmLogin), "当前更新服务不可用，请联系管理员。");
                return false;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                return false;
            }

            return true;
        }
        /// <summary>
        /// 显示主窗体
        /// </summary>
        /// <returns></returns>
        private bool IsShowForm()
        {
            if (CheckUpdateApp())
            {
                return false;
            }
            else
            {
                if (LoginValidate())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        private bool IsShowFormOffLine()
        {
            if (LoginValidate())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 验证信息
        /// </summary>
        /// <returns></returns>
        private bool ValidateEmpty()
        {
            if (txtAccount.Text.Trim() == string.Empty)
            {
                PublicClass.ShowMessageOk("请输入账号！");
                txtAccount.Focus();
                return false;
            }

            if (txtPassord.Text.Trim() == string.Empty)
            {
                PublicClass.ShowMessageOk("请输入密码！");
                txtPassord.Focus();
                return false;
            }

            return true;
        }

        public frmLogin()
        {
            InitializeComponent();
            CommonUtil.InitialBackgroundImage(Globals.BGLogin, this);
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            var account = string.Empty;
            var password = string.Empty;
            var remember = string.Empty;
            try
            {
                account = UserConfigSettings.Instance.ReadSetting("账号");
                password = UserConfigSettings.Instance.ReadSetting("密码");
                remember = UserConfigSettings.Instance.ReadSetting("记住账号");
                txtAccount.Text = account;
                txtPassord.Text = DES.DecryStrHexUTF8(password, account);
                if (remember != "" && remember == "是")
                {
                    cbAccount.Checked = true;
                }
                else
                {
                    cbAccount.Checked = false;
                }
                rdoOnline.Checked = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool result = false;
            if (rdoOnline.Checked)
            {
                if (!LocalConnectionStatus()) return;
            }
            if (!ValidateEmpty()) return;

            CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
            {
                if (rdoOnline.Checked)
                {
                    result = IsShowForm();
                    if (result)
                    {
                        Program.IsOnline = true;
                        Globals.IsOnline = true;
                        Program.MainForm = new ComputerExam.BusicWork.frmBusicWorkMain();
                        DialogResult = DialogResult.OK;
                    }
                }
                else
                {
                    result = IsShowFormOffLine();
                    if (result)
                    {
                        Program.IsOnline = false;
                        Globals.IsOnline = false;
                        Program.MainFormOffLine = new ComputerExam.BusicWorkOffLine.frmBusicWorkMain();
                        DialogResult = DialogResult.OK;
                    }
                }
            }, null);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = PublicClass.ShowMessageOKCancel("确定要退出吗？");
            if (result == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                txtAccount.Focus();
            }
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            DialogResult result = PublicClass.ShowMessageOKCancel("确定要退出吗？");
            if (result == DialogResult.OK)
            {
                this.Close();
            }
            else
            {
                txtAccount.Focus();
            }
        }

        private void lblSetService_Click(object sender, EventArgs e)
        {
            frmSetServiceAddress setServiceAddress = new frmSetServiceAddress();
            setServiceAddress.ShowDialog();
        }

        private void cbAccount_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAccount.Checked)
            {
                UserConfigSettings.Instance.WriteSetting("记住账号", "是");
            }
            else
            {
                UserConfigSettings.Instance.WriteSetting("记住账号", "否");
            }
        }

        private void lblSetService_MouseHover(object sender, EventArgs e)
        {
            lblSetService.ForeColor = Color.Blue;
            lblSetService.Font = new Font("宋体", 9, FontStyle.Underline);
        }

        private void lblSetService_MouseLeave(object sender, EventArgs e)
        {
            lblSetService.ForeColor = Color.Black;
            lblSetService.Font = new Font("宋体", 9, FontStyle.Regular);
        }

        private void frmLogin_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
    }
}
