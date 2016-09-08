using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web;
using System.Net;
using System.Data;
using System.Reflection;
using System.Net.Sockets;
using CBTESv7HWRichClientAddin;
using MakePaperEXWithPaperCode;
using ComputerExam.Util;
using ComputerExam.Model;
using ComputerExam.ExamPaper;
using AxSowerRichText;
using System.Threading;
using ComputerExam.Common;
using System.Dynamic;
using ComputerExam.BusicWork;
using ComputerExam.BLL;
using System.Drawing;

namespace ComputerExam
{
    public class CommonUtil
    {
        public CommonUtil()
        {

        }

        #region 变量
        public static frmAnswerSheet answerSheet = new frmAnswerSheet();
        public static frmFenLuBianJi fenLuBianJi = new frmFenLuBianJi();
        public static frmLogin login = new frmLogin();

        public static List<M_Accounting> listAccounting = new List<M_Accounting>();
        public static List<M_MyJobSubject> listSubject = new List<M_MyJobSubject>();
        public static List<JobState> listJobState = new List<JobState>();
        public static List<M_MyJob> listMyJob = new List<M_MyJob>();
        public static List<M_ResourceType> listResourceType = new List<M_ResourceType>();
        public static List<ResourceModel> listResourceModel = new List<ResourceModel>();
        public static List<ChartType> listChartType = new List<ChartType>();

        public static int subjectIndex = 0;
        private MyOpaqueLayer m_OpaqueLayer = null;//半透明蒙板层
        private static B_Service bService = new B_Service();
        #endregion

        public static void LoadSowerText(AxSowerRichTextBox AView, string DocText, M_Topic oCurrTopic)
        {
            AView.Clear();
            if (oCurrTopic.TopicFace.Length > 0)
            {
                AView.DocType = PublicClass.SowerExamPlugn.GetTopicDocType(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), 0);
                AView.SowerText = DocText;
            }
        }
        public static void LoadSowerTextSub(AxSowerRichTextBox AView, string DocText, M_Topic oCurrTopic)
        {
            AView.Clear();
            if (oCurrTopic.TopicFace.Length > 0)
            {
                AView.DocType = PublicClass.SowerExamPlugn.GetSubTopicDocType(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), int.Parse(oCurrTopic.SubTopicId), 0);
                AView.SowerText = DocText;
            }
        }

        public static void ShowProcessing(string msg, Form owner, ParameterizedThreadStart work, object workArg = null)
        {
            FrmProcessing processingForm = new FrmProcessing(msg);
            dynamic expObj = new ExpandoObject();
            expObj.Form = processingForm;
            expObj.WorkArg = workArg;
            processingForm.SetWorkAction(work, expObj);
            processingForm.ShowDialog(owner);
            if (processingForm.WorkException != null)
            {
                throw processingForm.WorkException;
            }
        }

        /// <summary>
        /// 显示遮罩层
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="alpha">透明度</param>
        /// <param name="isShowLoadingImage">是否显示图标</param>
        public void ShowOpaqueLayer(Control control, int alpha, bool isShowLoadingImage)
        {
            try
            {
                if (this.m_OpaqueLayer == null)
                {
                    this.m_OpaqueLayer = new MyOpaqueLayer(alpha, isShowLoadingImage);
                    control.Controls.Add(this.m_OpaqueLayer);
                    this.m_OpaqueLayer.Dock = DockStyle.Fill;
                    this.m_OpaqueLayer.BringToFront();
                }
                this.m_OpaqueLayer.Enabled = true;
                this.m_OpaqueLayer.Visible = true;
            }
            catch { }
        }

        /// <summary>
        /// 隐藏遮罩层
        /// </summary>
        public void HideOpaqueLayer()
        {
            try
            {
                if (this.m_OpaqueLayer != null)
                {
                    this.m_OpaqueLayer.Visible = false;
                    this.m_OpaqueLayer.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>        
        /// c#,.net 下载文件        
        /// </summary>        
        /// <param name="URL">下载文件地址</param>       
        /// 
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public static void DownloadFile(string URL, string filename, ProgressBar prog, Label label1)
        {
            float percent = 0;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    label1.Text = "当前补丁下载进度" + percent.ToString() + "%";
                    System.Windows.Forms.Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        /// <summary>        
        /// c#,.net 下载文件        
        /// </summary>        
        /// <param name="URL">下载文件地址</param>       
        /// 
        /// <param name="Filename">下载后的存放地址</param>        
        /// <param name="Prog">用于显示的进度条</param>        
        /// 
        public static bool DownloadFile(string URL, string filename, ToolStripProgressBar prog, ToolStripStatusLabel label1, string message)
        {
            float percent = 0;
            bool result = false;
            try
            {
                System.Net.HttpWebRequest Myrq = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(URL);
                System.Net.HttpWebResponse myrp = (System.Net.HttpWebResponse)Myrq.GetResponse();
                long totalBytes = myrp.ContentLength;
                if (prog != null)
                {
                    prog.Maximum = (int)totalBytes;
                }
                System.IO.Stream st = myrp.GetResponseStream();
                System.IO.Stream so = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                long totalDownloadedByte = 0;
                byte[] by = new byte[1024];
                int osize = st.Read(by, 0, (int)by.Length);
                while (osize > 0)
                {
                    totalDownloadedByte = osize + totalDownloadedByte;
                    System.Windows.Forms.Application.DoEvents();
                    so.Write(by, 0, osize);
                    if (prog != null)
                    {
                        prog.Value = (int)totalDownloadedByte;
                    }
                    osize = st.Read(by, 0, (int)by.Length);

                    percent = (float)totalDownloadedByte / (float)totalBytes * 100;
                    label1.Text = message + percent.ToString("0.0") + "%";
                    Application.DoEvents(); //必须加注这句代码，否则label1将因为循环执行太快而来不及显示信息
                }
                so.Close();
                st.Close();
                result = true;
            }
            catch (WebException)
            {
                PublicClass.ShowMessageOk("没有找到服务器上对应的作业文件，请联系管理员！");
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk(ex.Message);
            }
            return result;
        }

        public static void SetDateTimePicker(DateTimePicker dtpStart, DateTimePicker dtpEnd)
        {
            DateTime now = DateTime.Now;
            DateTime FirstDay = new DateTime(now.Year, now.Month, 1);
            DateTime LastDay = FirstDay.AddMonths(1).AddDays(-1);
            dtpStart.Value = FirstDay;
            dtpEnd.Value = LastDay;
        }

        /// <summary>
        /// 绑定DropDownList
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="valueMember"></param>
        /// <param name="displayMember"></param>
        /// <param name="result"></param>
        public static void BindComboBox(ComboBox comboBox, string valueMember, string displayMember, object result)
        {
            comboBox.DataSource = result;
            comboBox.ValueMember = valueMember;
            comboBox.DisplayMember = displayMember;
            comboBox.SelectedIndex = 0;
        }

        public static void BindDataGridView(DataGridView dataGridView, object result)
        {
            dataGridView.AutoGenerateColumns = false;
            dataGridView.DataSource = null;
            dataGridView.DataSource = result;
        }

        public static void WriteLog(Exception ex)
        {
            bService.WriteLog(PublicClass.StudentCode, LogHelper.GetExceptionMsg(ex, ex.Message));
        }

        public static void WriteLog(string message)
        {
            bService.WriteLog(PublicClass.StudentCode, message);
        }

        public static bool CheckFTP(string ftpServerIp, string ftpPort, string userName, string password, bool pattern, bool anonymous)
        {
            var checkResult = false;
            var message = string.Empty;
            string ftpRemotePath = string.Empty;
            try
            {
                ftpRemotePath = UserConfigSettings.Instance.ReadSetting("题库目录");
                checkResult = FtpWebTest.CheckFtp(ftpServerIp, userName, password, ftpPort, pattern, anonymous, ftpRemotePath, out message);
                if (checkResult)
                {
                    message = "FTP连接成功！";
                }
            }
            catch (Exception ex)
            {
                message = "测试FTP失败，原因：" + ex.Message;
            }
            return checkResult;
        }

        public static FtpWeb GetFtpWeb()
        {
            var checkResult = false;
            var pattern = false;
            var message = string.Empty;
            var ftpServerIp = UserConfigSettings.Instance.ReadSetting("题库地址");
            var ftpRemotePath = UserConfigSettings.Instance.ReadSetting("题库目录");
            var ftpPort = UserConfigSettings.Instance.ReadSetting("端口号");
            var ftpUserId = UserConfigSettings.Instance.ReadSetting("ftp用户名");
            var ftpPassword = UserConfigSettings.Instance.ReadSetting("ftp密码");
            var anonymous = bool.Parse(UserConfigSettings.Instance.ReadSetting("匿名"));
            FtpWeb ftpWeb = null;

            checkResult = FtpWebTest.CheckFtp(ftpServerIp, ftpUserId, ftpPassword, ftpPort, pattern, anonymous, ftpRemotePath, out message);
            if (checkResult == true)
            {
                ftpWeb = new FtpWeb(ftpServerIp, ftpRemotePath, ftpUserId, ftpPassword, ftpPort, 10000, pattern, anonymous);
            }
            return ftpWeb;
        }

        public static void InitialBackgroundImage(string imageName, Panel panel)
        {
            string fileName = Globals.ImageDir + imageName;
            if (File.Exists(fileName)) panel.BackgroundImage = Image.FromFile(fileName);
        }
        public static void InitialBackgroundImage(string imageName, Form form)
        {
            string fileName = Globals.ImageDir + imageName;
            if (File.Exists(fileName)) form.BackgroundImage = Image.FromFile(fileName);
        }
    }
}
