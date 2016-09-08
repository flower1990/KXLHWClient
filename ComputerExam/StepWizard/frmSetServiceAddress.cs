using ComputerExam.Util;
using ComputerExam.Util.WebReference;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ComputerExam.StepWizard
{
    public partial class frmSetServiceAddress : Form
    {
        public delegate bool CheckServiceDelegate();
        ServiceUtil serviceUtil = new ServiceUtil();
        OpaqueCommand cmd = new OpaqueCommand();
        PublicClass publicClass = new PublicClass();
        XmlUnit xmlUnit = new XmlUnit();

        private bool ValidateServiceEmpty()
        {
            if (txtIP.Text.Trim() == string.Empty)
            {
                PublicClass.ShowMessageOk("请输入IP地址！");
                txtIP.Focus();
                return false;
            }

            return true;
        }

        private bool ValidateFtpEmpty()
        {
            if (txtFtp.Text.Trim() == string.Empty)
            {
                PublicClass.ShowMessageOk("请输入Ftp地址！");
                txtFtp.Focus();
                return false;
            }

            if (txtPort.Text.Trim() == string.Empty)
            {
                PublicClass.ShowMessageOk("请输入端口号！");
                txtPort.Focus();
                return false;
            }

            if (!cbAnonymous.Checked)
            {
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    PublicClass.ShowMessageOk("请输入用户名！");
                    txtUserName.Focus();
                    return false;
                }

                if (txtPassword.Text.Trim() == string.Empty)
                {
                    PublicClass.ShowMessageOk("请输入密码！");
                    txtPassword.Focus();
                    return false;
                }
            }

            return true;
        }

        private bool CheckService()
        {
            bool service = false;
            string ip = "http://" + txtIP.Text.Trim();
            string serviceDir = UserConfigSettings.Instance.ReadSetting("虚拟目录");

            try
            {
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(ip + serviceDir);
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    service = true;
                }
            }
            catch (WebException)
            {
                service = false;
            }
            catch (Exception)
            {
                service = false;
            }

            return service;
        }

        private bool CheckFtp()
        {
            bool service = false;
            string ftpServerIP = txtFtp.Text.Trim();
            string ftpPort = txtPort.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            bool anonymous = cbAnonymous.Checked;
            string ftpRemotePath = UserConfigSettings.Instance.ReadSetting("题库目录");

            try
            {
                service = CommonUtil.CheckFTP(ftpServerIP, ftpPort, userName, password, false, anonymous);
            }
            catch (WebException)
            {
                service = false;
            }
            catch (Exception)
            {
                service = false;
            }

            return service;
        }

        private void WriterAUList()
        {
            #region [写AutoUpdaterlist]

            string strEntryPoint = "ComputerExam.exe";
            string strFilePath = string.Format("{0}\\AutoUpdaterList.xml", Application.StartupPath);
            string appFilePath = string.Format("{0}\\ComputerExam.exe", Application.StartupPath);
            string url = string.Format("http://{0}/{1}/", txtIP.Text.Trim(), UserConfigSettings.Instance.ReadSetting("更新目录"));
            FileStream fs = new FileStream(strFilePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            sw.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            sw.Write("\r\n<AutoUpdater>\r\n");

            #region[description]

            sw.Write("\t<Description>");
            sw.Write(strEntryPoint.Substring(0, strEntryPoint.LastIndexOf(".")) + " autoUpdate");
            sw.Write("</Description>\r\n");

            #endregion[description]

            #region [Updater]

            sw.Write("\t<Updater>\r\n");

            sw.Write("\t\t<Url>");
            sw.Write(url);
            sw.Write("</Url>\r\n");

            sw.Write("\t\t<LastUpdateTime>");
            sw.Write(DateTime.Now.AddDays(-15).ToString("yyyy-MM-dd"));
            sw.Write("</LastUpdateTime>\r\n");

            sw.Write("\t</Updater>\r\n");

            #endregion [Updater]

            #region [application]

            sw.Write("\t<Application applicationId = \"" + strEntryPoint.Substring(0, strEntryPoint.LastIndexOf(".")) + "\">\r\n");

            sw.Write("\t\t<EntryPoint>");
            sw.Write(strEntryPoint);
            sw.Write("</EntryPoint>\r\n");

            sw.Write("\t\t<Location>");
            sw.Write(".");
            sw.Write("</Location>\r\n");

            FileVersionInfo _lcObjFVI = FileVersionInfo.GetVersionInfo(appFilePath);
            
            sw.Write("\t\t<Version>");
            sw.Write(string.Format("{0}.{1}.{2}.{3}", _lcObjFVI.FileMajorPart, _lcObjFVI.FileMinorPart, _lcObjFVI.FileBuildPart, _lcObjFVI.FilePrivatePart));
            sw.Write("</Version>\r\n");


            sw.Write("\t</Application>\r\n");


            #endregion [application]

            #region [Files]

            sw.Write("\t<Files>\r\n");

            sw.Write("\t</Files>\r\n");

            #endregion [Files]

            sw.Write("</AutoUpdater>");
            sw.Close();
            fs.Close();

            #endregion [写AutoUpdaterlist]
        }

        /// <summary>
        /// 修改自动更新地址
        /// </summary>
        private void ChangeUpdateUrl()
        {
            string path = string.Format("{0}\\AutoUpdaterList.xml", Application.StartupPath);
            if (File.Exists(path))
            {
                string url = string.Format("http://{0}/{1}/", txtIP.Text.Trim(),
                    UserConfigSettings.Instance.ReadSetting("更新目录"));
                bool result = xmlUnit.UpdateNodeInnerText(path, "AutoUpdater/Updater/Url", url);
            }
            else
            {
                WriterAUList();
            }
        }

        public frmSetServiceAddress()
        {
            InitializeComponent();
            CommonUtil.InitialBackgroundImage("bg_setupwizard.jpg", this);
        }

        private void frmSetServiceAddress_Load(object sender, EventArgs e)
        {
            bool result = false;
            string anonymous = "";

            txtIP.Text = UserConfigSettings.Instance.ReadSetting("服务地址");
            txtFtp.Text = UserConfigSettings.Instance.ReadSetting("题库地址");
            txtPort.Text = UserConfigSettings.Instance.ReadSetting("端口号");
            txtUserName.Text = UserConfigSettings.Instance.ReadSetting("ftp用户名");
            txtPassword.Text = UserConfigSettings.Instance.ReadSetting("ftp密码");
            anonymous = UserConfigSettings.Instance.ReadSetting("匿名");
            cbAnonymous.Checked = bool.TryParse(anonymous, out result);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateServiceEmpty()) return;

                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    bool result = CheckService();
                    if (result)
                    {
                        MessageBox.Show("当前服务可用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("当前服务不可用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk(ex.Message);
                LogHelper.WriteLog(typeof(ServiceUtil), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnFtpTest_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateFtpEmpty()) return;

                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    bool result = CheckFtp();
                    if (result)
                    {
                        MessageBox.Show("当前FTP地址可用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("当前FTP地址不可用！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }, null);
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk(ex.Message);
                LogHelper.WriteLog(typeof(ServiceUtil), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateServiceEmpty()) return;

            //修改自动更新地址
            ChangeUpdateUrl();
            UserConfigSettings.Instance.WriteSetting("服务地址", txtIP.Text.Trim());
            UserConfigSettings.Instance.WriteSetting("更新地址", txtIP.Text.Trim());
            UserConfigSettings.Instance.WriteSetting("题库地址", txtFtp.Text.Trim());
            UserConfigSettings.Instance.WriteSetting("端口号", txtPort.Text.Trim());
            UserConfigSettings.Instance.WriteSetting("ftp用户名", txtUserName.Text.Trim());
            UserConfigSettings.Instance.WriteSetting("ftp密码", txtPassword.Text.Trim());
            UserConfigSettings.Instance.WriteSetting("匿名", cbAnonymous.Checked.ToString());
            DialogResult = DialogResult.OK;
        }

        private void picClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
