using ComputerExam.BLL;
using ComputerExam.Model;
using ComputerExam.Properties;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ComputerExam.BusicWork
{
    public partial class frmDownTopicDB : Form
    {
        SqliteKey3 key3 = new SqliteKey3();
        B_Service bService = new B_Service();
        B_SubjectProp bSubjectProp = new B_SubjectProp();

        /// <summary>
        /// 加载题库列表
        /// </summary>
        private void LoadDownTopicDB()
        {
            try
            {
                var courseID = cboSubject.SelectedValue.ToString();
                var topicDBList = new List<M_TopicDB>();
                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    Thread.Sleep(1000);
                    topicDBList = bService.GetTopicDBList("", "", "1900-1-1", "2099-12-31");
                    topicDBList = topicDBList.Where(f => f.CourseID == courseID).ToList();
                }, null);
                dgvDownTopicDB.AutoGenerateColumns = false;
                dgvDownTopicDB.DataSource = topicDBList;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmDownTopicDB), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 初始化组件
        /// </summary>
        public frmDownTopicDB()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载题库列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmDownTopicDB_Load(object sender, EventArgs e)
        {
            CommonUtil.BindComboBox(cboSubject, "CourseID", "CourseName", CommonUtil.listSubject);
            if (CommonUtil.listSubject.Count == 2) cboSubject.SelectedIndex = 1; else cboSubject.SelectedIndex = 0;
            cboSubject.SelectedValueChanged += cboSubject_SelectedValueChanged;
        }

        void cboSubject_SelectedValueChanged(object sender, EventArgs e)
        {
            LoadDownTopicDB();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadDownTopicDB();
        }
        /// <summary>
        /// 下载题库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, EventArgs e)
        {
            #region 局部变量
            string topicFileName = string.Empty;
            string topicFilePath = string.Empty;
            string topicEnvFilePath = string.Empty;
            string topicVideoFilePath = string.Empty;
            string requireEnvFile = string.Empty;
            string envFileName = string.Empty;
            string envFilePath = string.Empty;
            string videoFileName = string.Empty;
            string videoFilePath = string.Empty;
            string envFileExt = string.Empty;
            string copyEnvPath = string.Empty;
            string copyVideoPath = string.Empty;
            string filePath = string.Empty;
            string fileTPath = string.Empty;
            string fileExt = string.Empty;
            string fileName = string.Empty;
            string copyPath = string.Empty;
            string copyTPath = string.Empty;
            string connection = string.Empty;
            string connectionT = string.Empty;
            string errorMessage = string.Empty;
            FtpWeb ftpWeb = null;
            #endregion
            try
            {
                btnDown.Enabled = false;
                if (dgvDownTopicDB.SelectedRows.Count == 0) return;
                M_TopicDB topicDB = dgvDownTopicDB.SelectedRows[0].DataBoundItem as M_TopicDB;
                #region 检测本地是否存在题库文件
                string path = string.Format(@"{0}\data\{1}_{2}.sdbt", Application.StartupPath, PublicClass.StudentCode, topicDB.TopicDBName);
                if (File.Exists(path))
                {
                    M_SubjectProp subject = bSubjectProp.GetSubjectProp(string.Format("{0}_{1}.sdbt", PublicClass.StudentCode, topicDB.TopicDBName));
                    if (subject.TopicDBVersion == topicDB.TopicDBVersion && subject.TopicDBCode == topicDB.TopicDBCode)
                    {
                        PublicClass.ShowErrorMessageOk("您已经下载过这个题库文件，不能重复下载。");
                        return;
                    }
                }
                #endregion
                #region 初始化变量
                topicFileName = Path.GetFileName(topicDB.TopicDBPath);
                topicFilePath = Path.GetDirectoryName(topicDB.TopicDBPath).Replace("\\", "//");
                requireEnvFile = string.IsNullOrEmpty(topicDB.RequireEnvFile) == true ? "false" : topicDB.RequireEnvFile.ToLower();
                filePath = string.Format("{0}\\{1}", Globals.DownLoadDir, topicFileName);
                fileTPath = string.Format("{0}\\{1}t", Globals.DownLoadDir, topicFileName);
                fileExt = string.IsNullOrEmpty(topicDB.TopicDBPath) == true ? "" : Path.GetExtension(topicDB.TopicDBPath).ToLower();
                fileName = string.Format("{0}{1}", topicDB.TopicDBName, fileExt);
                copyPath = string.Format(@"{0}\data\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName.Replace(".srk", ".sdb"));
                copyTPath = string.Format(@"{0}\data\{1}_{2}t", Application.StartupPath, PublicClass.StudentCode, fileName.Replace(".srk", ".sdb"));
                connection = string.Format(@"data source={0};password={1};polling=false;failifmissing=true", filePath, PublicClass.PasswordTopicDB);
                connectionT = string.Format(@"data source={0};polling=false;failifmissing=true", fileTPath);
                #endregion
                //下载题库文件
                ftpWeb = CommonUtil.GetFtpWeb();
                if (ftpWeb != null)
                {
                    ftpWeb.Download(Globals.DownLoadDir, topicFileName, topicFilePath, proDown, lblDown, "题库下载进度：");
                }
                else
                {
                    Msg.ShowInformation("FTP地址不可用，请返回登陆界面进行配置。");
                    return;
                }
                //下载账套文件
                if (ftpWeb != null && requireEnvFile == "true" && topicDB.IsUploadEnvFile == true && !string.IsNullOrEmpty(topicDB.EnvFilePath))
                {
                    envFileName = Path.GetFileName(topicDB.EnvFilePath);
                    envFilePath = string.Format("{0}\\{1}", Globals.DownLoadDir, envFileName);
                    copyEnvPath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, envFileName);
                    topicEnvFilePath = Path.GetDirectoryName(topicDB.EnvFilePath).Replace("\\", "//");
                    ftpWeb.Download(Globals.DownLoadDir, envFileName, topicEnvFilePath, proDown, lblDown, "账套下载进度：");
                }
                //下载视频文件
                if (ftpWeb != null && topicDB.IsUploadVideoFile == true && !string.IsNullOrEmpty(topicDB.VideoFilePath))
                {
                    videoFileName = Path.GetFileName(topicDB.VideoFilePath);
                    videoFilePath = string.Format("{0}\\{1}", Globals.DownLoadDir, videoFileName);
                    copyVideoPath = string.Format(@"{0}\SowerTestClient\Video\{1}_{2}\", Application.StartupPath, PublicClass.StudentCode, DirFileHelper.GetFileNameNoExtension(videoFileName));
                    topicVideoFilePath = Path.GetDirectoryName(topicDB.VideoFilePath).Replace("\\", "//");
                    ftpWeb.Download(Globals.DownLoadDir, videoFileName, topicVideoFilePath, proDown, lblDown, "视频下载进度：");
                }
                #region 验证题库文件
                if (fileExt != ".sdb" && fileExt != ".srk")
                {
                    Msg.ShowError("该文件不是有效的题库文件，请重新添加！");
                    return;
                }
                if (File.Exists(copyTPath) && File.Exists(copyPath))
                {
                    if (!Msg.AskQuestion("该题库文件已经存在，确定要覆盖吗？")) return;
                }
                if (File.Exists(filePath))
                {
                    CommonUtil.ShowProcessing("正在验证题库，请稍候...", this, (obj) =>
                    {
                        //复制一个.sdbt文件
                        DirFileHelper.CopyFile(filePath, fileTPath);
                        //修改.sdbt文件密码
                        bool updateResult = key3.ChangePassWordByGB2312(fileTPath, PublicClass.PassWordTopicDB_SDB, "");
                        if (updateResult)
                        {
                            SQLiteConnection conn = new SQLiteConnection(connectionT);
                            conn.Open();
                            if (ConnectionState.Open == conn.State)
                            {
                                //更改题库密码
                                conn.ChangePassword(PublicClass.PasswordTopicDB);
                                //复制题库到系统目录
                                DirFileHelper.CopyFile(filePath, copyPath);
                                DirFileHelper.CopyFile(fileTPath, copyTPath);
                                //复制账套到系统目录
                                if (requireEnvFile == "true" && topicDB.IsUploadEnvFile == true && !string.IsNullOrEmpty(topicDB.EnvFilePath))
                                {
                                    DirFileHelper.CopyFile(envFilePath, copyEnvPath);
                                }
                                //复制视频到系统目录
                                if (topicDB.IsUploadVideoFile == true && !string.IsNullOrEmpty(topicDB.VideoFilePath))
                                {
                                    ZipFileTools.UnZipSZL(videoFilePath, copyVideoPath);
                                }
                                conn.Close();
                            }
                            conn.Dispose();
                            conn = null;
                            DirFileHelper.DeleteFile(filePath);
                            DirFileHelper.DeleteFile(fileTPath);
                            DirFileHelper.DeleteFile(envFilePath);
                            DirFileHelper.DeleteFile(videoFilePath);
                        }
                        else
                        {
                            Msg.ShowError("该题库不是有效的题库文件！");
                        }
                        key3.Dispose();
                    }, null);
                }
                #endregion
            }
            catch (SQLiteException se)
            {
                Msg.ShowError("无法打开题库文件，该题库不是有效的题库文件！");
                LogHelper.WriteLog(typeof(frmDownTopicDB), se);
                CommonUtil.WriteLog(se);
            }
            catch (WebException we)
            {
                Msg.ShowError("该题库文件不存在，请联系管理员重新上传。");
                LogHelper.WriteLog(typeof(frmDownTopicDB), we);
                CommonUtil.WriteLog(we);
            }
            catch (AggregateException ae)
            {
                Msg.ShowError("无法打开题库文件，该题库不是有效的题库文件！");
                LogHelper.WriteLog(typeof(frmDownTopicDB), ae);
                CommonUtil.WriteLog(ae);
            }
            catch (Exception ex)
            {
                PublicClass.ShowErrorMessageOk(ex.Message);
                LogHelper.WriteLog(typeof(frmDownTopicDB), ex);
                CommonUtil.WriteLog(ex);
            }
            finally
            {
                btnDown.Enabled = true;
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvDownTopicDB_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || e.Value.ToString() == "" || !(sender is DataGridView))
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            object originalValue = e.Value;

            if (e.ColumnIndex == dgv.Columns["CreateTime"].Index)   //格式化日期
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-M-d");
                }
            }
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //获取TabControl主控件的工作区域 
            Rectangle rec = tabControl1.ClientRectangle;
            //获取背景图片，我的背景图片在项目资源文件中。 
            Image backImage = Resources.背景2;
            //新建一个StringFormat对象，用于对标签文字的布局设置 
            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = StringAlignment.Center;
            //标签背景填充颜色，也可以是图片 
            SolidBrush brush = new SolidBrush(Color.White);
            SolidBrush brush1 = new SolidBrush(Color.FromArgb(35, 135, 194));
            //标签字体颜色 
            SolidBrush bruFont = new SolidBrush(Color.White);
            SolidBrush bruFont1 = new SolidBrush(Color.FromArgb(35, 135, 194));
            //设置标签字体样式 
            Font font = new System.Drawing.Font("微软雅黑", 9F);
            //绘制主控件的背景 
            e.Graphics.DrawImage(backImage, 0, 0, tabControl1.Width, tabControl1.Height);
            //绘制标签样式 
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //获取标签头的工作区域
                Rectangle recChild = tabControl1.GetTabRect(i);
                //绘制标签头背景颜色 
                e.Graphics.FillRectangle(brush1, recChild);
                //绘制标签头的文字 
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, font, bruFont, recChild, strFormat);
            }
            if (e.Index == tabControl1.SelectedIndex)
            {
                //获取标签头的工作区域
                Rectangle recChild = tabControl1.GetTabRect(e.Index);
                //绘制标签头背景颜色 
                e.Graphics.FillRectangle(brush, recChild);
                //绘制标签头的文字 
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, font, bruFont1, recChild, strFormat);
            }
        }
    }
}
