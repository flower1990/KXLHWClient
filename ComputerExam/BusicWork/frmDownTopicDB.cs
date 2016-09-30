using ComputerExam.BLL;
using ComputerExam.Model;
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
                copyPath = string.Format(@"{0}\data\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName);
                copyTPath = string.Format(@"{0}\data\{1}_{2}t", Application.StartupPath, PublicClass.StudentCode, fileName);
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
                if (File.Exists(copyTPath) && File.Exists(filePath))
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
                                DirFileHelper.CopyFile(filePath, copyPath.Replace(".srk", ".sdb"));
                                DirFileHelper.CopyFile(fileTPath, copyTPath.Replace(".srk", ".sdb"));
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
    }
}
