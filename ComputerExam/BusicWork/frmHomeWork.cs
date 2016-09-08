using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComputerExam.BLL;
using ComputerExam.Model;
using System.IO;
using ComputerExam.StepWizard;
using ComputerExam.Common;
using System.Threading;

namespace ComputerExam.BusicWork
{
    public partial class frmHomeWork : Form
    {
        string fileHost = "";
        B_Service bService = new B_Service();
        ListUtil listUtil = new ListUtil();
        PublicClass publicClass = new PublicClass();
        XmlUnit xmlUnit = new XmlUnit();
        DownLoadHelper downLoad = new DownLoadHelper();
        List<M_MyJob> serverMyJob = new List<M_MyJob>();

        /// <summary>
        /// 设置作业下载状态
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetJobDownLoadState(List<M_MyJob> serverMyJob)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Paper\Download");
            List<FileInfo> fileInfo = directoryInfo.GetFiles("*.*").Where(file => file.Name.ToLower().EndsWith("rar") || file.Name.ToLower().EndsWith("zip")).ToList();
            foreach (var server in serverMyJob)
            {
                string serverName = string.Format("{0}_{1}", PublicClass.StudentCode, Path.GetFileNameWithoutExtension(server.FilePath));
                bool result = fileInfo.Exists(f => Path.GetFileNameWithoutExtension(f.Name) == serverName);
                if (result)
                {
                    server.JobDownLoadState = "已下载";
                }
                else
                {
                    server.JobDownLoadState = "未下载";
                }
            }
        }
        /// <summary>
        /// 设置账套下载状态
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetAccountDownLoadState(List<M_MyJob> serverMyJob)
        {
            bool existsResult = false;
            string envFileName = string.Empty;
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Paper\Account");
            List<FileInfo> fileInfo = directoryInfo.GetFiles("*.*").Where(file => file.Name.ToLower().EndsWith("casf")).ToList();
            foreach (var server in serverMyJob)
            {
                envFileName = string.IsNullOrEmpty(server.EnvFileName) == true ? "" : server.EnvFileName.ToLower();
                existsResult = fileInfo.Exists(f => f.Name.ToLower() == envFileName);
                if (existsResult)
                {
                    server.AccountDownLoadState = "已下载";
                }
                else
                {
                    server.AccountDownLoadState = "未下载";
                }
            }
        }
        /// <summary>
        /// 设置作业序号
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetJobNo(List<M_MyJob> serverMyJob)
        {
            for (int i = 0; i < serverMyJob.Count; i++)
            {
                serverMyJob[i].JobNo = (i + 1).ToString("00");
            }
        }
        /// <summary>
        /// 作业排序
        /// </summary>
        /// <param name="job1"></param>
        /// <param name="job2"></param>
        /// <returns></returns>
        private int SortMyJob(M_MyJob job1, M_MyJob job2)
        {
            if (string.IsNullOrEmpty(job1.CourseName) || string.IsNullOrEmpty(job2.CourseName)) return 0;
            if (string.IsNullOrEmpty(job1.ChapterName) || string.IsNullOrEmpty(job2.ChapterName)) return 0;
            if (string.IsNullOrEmpty(job1.HWName) || string.IsNullOrEmpty(job2.HWName)) return 0;

            if (job1.CourseName.CompareTo(job2.CourseName) != 0)
                return job1.CourseName.CompareTo(job2.CourseName);
            else if (job1.ChapterName.CompareTo(job2.ChapterName) != 0)
                return job1.ChapterName.CompareTo(job2.ChapterName);
            else
                return job1.HWName.CompareTo(job2.HWName);
        }
        /// <summary>
        /// 初始化组件
        /// </summary>
        public frmHomeWork()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmHomeWork_Load(object sender, EventArgs e)
        {
            try
            {
                CommonUtil.BindComboBox(cboJobState, "Id", "Name", CommonUtil.listJobState);
                CommonUtil.BindComboBox(cboSubject, "CourseID", "CourseName", CommonUtil.listSubject);
                if (CommonUtil.listJobState.Count > 0) cboJobState.SelectedValue = 1;
                if (CommonUtil.listSubject.Count == 2) cboSubject.SelectedIndex = 1; else cboSubject.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 查询作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int dataType = Convert.ToInt32(cboJobState.SelectedValue);
                string subjectValue = cboSubject.SelectedValue.ToString();
                string subjectText = cboSubject.Text;

                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    Thread.Sleep(1000);
                    //获取服务器作业列表
                    serverMyJob = bService.GetMyJob(PublicClass.StudentCode, "1900-1-1", "2099-1-1", dataType, out fileHost);
                    //根据科目查询作业
                    if (subjectValue != "0") serverMyJob = serverMyJob.FindAll(s => s.CourseName == subjectText);
                    //设置作业下载状态
                    SetJobDownLoadState(serverMyJob);
                    //设置账套下载状态
                    SetAccountDownLoadState(serverMyJob);
                    //排序
                    serverMyJob.Sort(SortMyJob);
                    //设置作业序号
                    SetJobNo(serverMyJob);
                }, null);

                //绑定作业到列表
                dgvResult.AutoGenerateColumns = false;
                dgvResult.DataSource = new BindingCollection<M_MyJob>(serverMyJob);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// HTTP下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedRows.Count == 0) return;
            M_MyJob myJob = dgvResult.SelectedRows[0].DataBoundItem as M_MyJob;
            myJob.StudentCode = PublicClass.StudentCode;

            #region //作业限定时间&&当前时间小于作业发布开始时间
            if (myJob.HWSubmitTimeType.ToLower() == "true" &&       //1：限时，0：不限时
                DateTime.Now <= DateTime.Parse(myJob.ExamStartDateTime))
            {
                PublicClass.ShowMessageOk("还没有到作业时间，不允许下载作业。");
                return;
            }
            #endregion

            #region //作业限定时间&&不允许补交作业&&当前时间大于作业提交截止时间
            if (myJob.HWSubmitTimeType.ToLower() == "true" &&       //true：限时，false：不限时
                myJob.IsPay.ToLower() == "false" &&           //true：允许补交作业，false：不允许补交作业
                DateTime.Now >= DateTime.Parse(myJob.ExamEndDateTime))
            {
                PublicClass.ShowMessageOk("对不起，您已经过了交作业时间。\n请联系老师允许您补交作业！");
                return;
            }
            #endregion

            try
            {
                btnDownLoad.Enabled = false;
                this.ParentForm.Enabled = false;
                //下载地址
                string downLoadUrl = string.Format("{0}/{1}", fileHost, myJob.FilePath.Replace(@"\", @"/"));
                //文件路径
                string filePath = myJob.FilePath;
                //文件名
                string fileName = Path.GetFileName(filePath);
                //复制文件到系统路径
                string copyPath = string.Format(@"{0}\SowerTestClient\Paper\Download\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName);
                //下载文件保存路径
                string savePath = string.Format("{0}\\{1}_{2}", Globals.DownLoadDir, PublicClass.StudentCode, fileName);
                //下载作业
                bool downResult = CommonUtil.DownloadFile(downLoadUrl, savePath, tsbBar, tsbMessage, "下载进度：");
                if (downResult)
                {
                    //复制作业到系统目录
                    File.Copy(savePath, copyPath, true);
                    //删除下载文件
                    File.Delete(savePath);
                    //设置已下载状态
                    dgvResult.SelectedRows[0].Cells["JobDownLoadState"].Value = "已下载";
                    tsbMessage.Text = "当前作业下载进度：";
                    tsbBar.Value = 0;
                }
                if (myJob.RequireEnvFile.ToLower() == "true" && myJob.IsUpload.ToLower() == "true" && cbIsDownAccount.Checked == true)
                {
                    downLoadUrl = string.Format("{0}/{1}", fileHost, myJob.EnvFilePath.Replace(@"\", @"/"));
                    filePath = myJob.FilePath;
                    fileName = myJob.EnvFileName;
                    copyPath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, fileName);
                    savePath = string.Format("{0}\\{1}", Globals.DownLoadDir, fileName);
                    downResult = CommonUtil.DownloadFile(downLoadUrl, savePath, tsbBar, tsbMessage, "下载进度：");
                    if (downResult)
                    {
                        //复制作业到系统目录
                        File.Copy(savePath, copyPath, true);
                        //删除下载文件
                        File.Delete(savePath);
                        //设置已下载状态
                        dgvResult.SelectedRows[0].Cells["JobDownLoadState"].Value = "已下载";
                        tsbMessage.Text = "当前帐套下载进度：";
                        tsbBar.Value = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk(ex.Message);
            }
            finally
            {
                btnDownLoad.Enabled = true;
                this.ParentForm.Enabled = true;
            }
        }
        /// <summary>
        /// FTP下载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFtpDown_Click(object sender, EventArgs e)
        {
            FtpWeb ftpWeb = null;
            if (dgvResult.SelectedRows.Count == 0) return;
            M_MyJob myJob = dgvResult.SelectedRows[0].DataBoundItem as M_MyJob;
            myJob.StudentCode = PublicClass.StudentCode;

            #region //作业限定时间&&当前时间小于作业发布开始时间
            if (myJob.HWSubmitTimeType.ToLower() == "true" &&       //1：限时，0：不限时
                DateTime.Now <= DateTime.Parse(myJob.ExamStartDateTime))
            {
                PublicClass.ShowMessageOk("还没有到作业时间，不允许下载作业。");
                return;
            }
            #endregion

            #region //作业限定时间&&不允许补交作业&&当前时间大于作业提交截止时间
            if (myJob.HWSubmitTimeType.ToLower() == "true" &&       //true：限时，false：不限时
                myJob.IsPay.ToLower() == "false" &&           //true：允许补交作业，false：不允许补交作业
                DateTime.Now >= DateTime.Parse(myJob.ExamEndDateTime))
            {
                PublicClass.ShowMessageOk("对不起，您已经过了交作业时间。\n请联系老师允许您补交作业！");
                return;
            }
            #endregion

            try
            {
                btnDownLoad.Enabled = false;
                this.ParentForm.Enabled = false;
                //文件路径
                string filePath = Path.GetDirectoryName(myJob.FilePath).Replace("\\", "//");
                //文件名
                string fileName = Path.GetFileName(myJob.FilePath);
                //复制文件到系统路径
                string copyPath = string.Format(@"{0}\SowerTestClient\Paper\Download\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName);
                //下载文件保存路径
                string savePath = string.Format("{0}\\{1}", Globals.DownLoadDir, fileName);

                ftpWeb = CommonUtil.GetFtpWeb();
                if (ftpWeb != null)
                {
                    ftpWeb.Download(Globals.DownLoadDir, fileName, filePath, tsbBar, tsbMessage, "作业下载进度：");
                }

                //复制作业到系统目录
                File.Copy(savePath, copyPath, true);
                //删除下载文件
                File.Delete(savePath);
                //设置已下载状态
                dgvResult.SelectedRows[0].Cells["JobDownLoadState"].Value = "已下载";
                tsbMessage.Text = "作业下载进度：";
                tsbBar.Value = 0;
                if (ftpWeb != null && myJob.RequireEnvFile.ToLower() == "true" && myJob.IsUpload.ToLower() == "true" && cbIsDownAccount.Checked == true)
                {
                    filePath = Path.GetDirectoryName(myJob.EnvFilePath).Replace("\\", "//");
                    fileName = myJob.EnvFileName;
                    copyPath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, fileName);
                    savePath = string.Format("{0}\\{1}", Globals.DownLoadDir, fileName);
                    ftpWeb.Download(Globals.DownLoadDir, fileName, filePath, tsbBar, tsbMessage, "帐套下载进度：");
                    //复制作业到系统目录
                    File.Copy(savePath, copyPath, true);
                    //删除下载文件
                    File.Delete(savePath);
                    //设置已下载状态
                    dgvResult.SelectedRows[0].Cells["JobDownLoadState"].Value = "已下载";
                    tsbMessage.Text = "帐套下载进度：";
                    tsbBar.Value = 0;
                }
            }
            catch (Exception ex)
            {
                Msg.ShowError("下载作业出错，请联系管理员。");
                LogHelper.WriteLog(typeof(frmDownTopicDB), ex);
                CommonUtil.WriteLog(ex);
            }
            finally
            {
                btnDownLoad.Enabled = true;
                this.ParentForm.Enabled = true;
            }
        }
        /// <summary>
        /// 添加账套文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEnvfile_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedRows.Count == 0) return;
            M_MyJob myJob = dgvResult.SelectedRows[0].DataBoundItem as M_MyJob;
            ofdOpenFile.Title = "账套文件";
            ofdOpenFile.FileName = "账套文件";
            ofdOpenFile.Filter = "账套文件|*.casf*";
            ofdOpenFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = ofdOpenFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = ofdOpenFile.FileName;
                string fileName = Path.GetFileName(filePath);
                string fileExt = Path.GetExtension(filePath).ToLower();
                string copyPath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, fileName);
                string envFileName = string.IsNullOrEmpty(myJob.EnvFileName) == true ? "" : myJob.EnvFileName.ToLower();
                if (string.IsNullOrEmpty(envFileName))
                {
                    Msg.ShowInformation(string.Format("作业“{0}”没有添加账套文件，请联系授课教师到作业中心->作业维护发布列表进行添加。", myJob.HWName));
                    return;
                }
                if (fileName.ToLower() != envFileName)
                {
                    Msg.ShowInformation(string.Format("您添加的帐套文件无效，请添加名字为{0}的账套文件。", envFileName));
                    return;
                }
                try
                {
                    if (fileExt != ".casf")
                    {
                        PublicClass.ShowMessageOk("该文件不是有效的账套文件，请重新添加！");
                        return;
                    }

                    if (File.Exists(copyPath))
                    {
                        DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该账套文件已经存在，确定要覆盖吗？");
                        if (dialogResult == DialogResult.Cancel) return;
                    }

                    CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                    {
                        File.Copy(filePath, copyPath, true);
                        dgvResult.SelectedRows[0].Cells["AccountDownLoadState"].Value = "已下载";
                    }, null);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(frmHomeWork), ex);
                    CommonUtil.WriteLog(ex);
                }
            }
        }
        /// <summary>
        /// 添加作业文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddHWFile_Click(object sender, EventArgs e)
        {
            var filePath = string.Empty;
            var fileName = string.Empty;
            var copyPath = string.Empty;
            var unFilePath = string.Empty;
            var contentPath = string.Empty;
            var topicDBCode = string.Empty;
            var topicDBVersion = string.Empty;
            var sExistsHWFile = string.Empty;

            if (dgvResult.SelectedRows.Count == 0) return;
            M_MyJob myJob = dgvResult.SelectedRows[0].DataBoundItem as M_MyJob;
            ofdOpenFile.Title = "作业文件";
            ofdOpenFile.FileName = "作业文件";
            ofdOpenFile.Filter = "作业文件|*.zip*";
            ofdOpenFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = ofdOpenFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                filePath = ofdOpenFile.FileName;
                fileName = Path.GetFileName(myJob.FilePath);
                copyPath = string.Format(@"{0}\SowerTestClient\Paper\Download\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName);
                try
                {
                    //解压作业文件
                    unFilePath = string.Format("{0}\\{1}\\data", Path.GetDirectoryName(filePath), Path.GetFileNameWithoutExtension(filePath));
                    contentPath = Directory.GetParent(unFilePath).FullName;
                    PublicClass.SowerExamPlugn.foUnZipFiles(filePath, unFilePath, PublicClass.PasswordUserPaper);
                    topicDBCode = PublicClass.SowerExamPlugn.GetParaValue(contentPath, "试卷参数", "TopicDBCode");
                    topicDBVersion = PublicClass.SowerExamPlugn.GetParaValue(contentPath, "试卷参数", "TopicDBVersion");
                    sExistsHWFile = bService.ExistsHWFile(myJob.HWID, topicDBCode, topicDBVersion);
                    if (sExistsHWFile == "-1")
                    {
                        Msg.ShowInformation("该作业文件无效！");
                        return;
                    }
                    if (File.Exists(copyPath))
                    {
                        DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该作业文件已经存在，确定要覆盖吗？");
                        if (dialogResult == DialogResult.Cancel) return;
                    }
                    CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                    {
                        File.Copy(filePath, copyPath, true);
                        dgvResult.SelectedRows[0].Cells["JobDownLoadState"].Value = "已下载";
                    }, null);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(frmHomeWork), ex);
                    CommonUtil.WriteLog(ex);
                }
            }
        }
        /// <summary>
        /// 格式化单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || e.Value.ToString() == "" || !(sender is DataGridView))
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            object originalValue = e.Value;

            if (e.ColumnIndex == dgv.Columns["ExamStartDateTime"].Index)   //格式化日期
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd");
                    if (e.Value.ToString() == "0001-01-01")
                    {
                        e.Value = "-";
                    }
                }
            }

            if (e.ColumnIndex == dgv.Columns["ExamEndDateTime"].Index)   //格式化日期
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd");
                    if (e.Value.ToString() == "0001-01-01")
                    {
                        e.Value = "-";
                    }
                }
            }

            if (e.ColumnIndex == dgv.Columns["ScoreSubmitted"].Index)   //格式化上交作业状态
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    if (e.Value.ToString() == "1")
                    {
                        e.Value = "已上交";
                    }
                    else
                    {
                        e.Value = "未上交";
                    }
                }
            }
        }   
    }
}
