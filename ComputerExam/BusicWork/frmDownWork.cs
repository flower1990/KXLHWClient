using ComputerExam.BLL;
using ComputerExam.Common;
using ComputerExam.Model;
using ComputerExam.StepWizard;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace ComputerExam.BusicWork
{
    public partial class frmDownWork : Form
    {
        string fileHost = "";
        ListUtil listUtil = new ListUtil();
        PublicClass publicClass = new PublicClass();

        List<M_MyJob> serverMyJob = new List<M_MyJob>();
        List<M_MyJob> downMyJob = new List<M_MyJob>();

        B_Service bService = new B_Service();

        /// <summary>
        /// 初始化科目属性
        /// </summary>
        /// <param name="myJob"></param>
        private void InitialSubjectProp()
        {
            //初始化科目参数
            PublicClass.oSubjectProp.ExamNumber = PublicClass.StudentCode;
            PublicClass.oSubjectProp.Password = PublicClass.StudentPwd;
            PublicClass.oSubjectProp.StudentName = PublicClass.oMyJob.RealName;
            PublicClass.oSubjectProp.SubjectName = PublicClass.oMyJob.CourseName;
            //PublicClass.oSubjectProp.TopicDBCode = subjectClient.TopicDBCode;
            PublicClass.oSubjectProp.PaperName = string.Format("{0}_{1}", PublicClass.StudentCode, Path.GetFileName(PublicClass.oMyJob.FilePath));
            //PublicClass.oSubjectProp.TopicDBVersion = subjectClient.TopicDBVersion;
            //PublicClass.oSubjectProp.RequireEnvFile = subjectClient.RequireEnvFile;
            PublicClass.oSubjectProp.EnvFileName = PublicClass.oMyJob.EnvFileName;
            //PublicClass.oSubjectProp.CreatePaperMode = subjectClient.CreatePaperMode;
            //PublicClass.oSubjectProp.PresetPaperID = subjectClient.PresetPaperID;
            PublicClass.oSubjectProp.ShowScore = bool.Parse(PublicClass.oMyJob.ShowScore);
            PublicClass.oSubjectProp.ShowAnalysis = bool.Parse(PublicClass.oMyJob.ShowAnalysis);
            //PublicClass.oSubjectProp.CreateTopicSubDir = subjectClient.CreateTopicSubDir;
            //PublicClass.oSubjectProp.CheckOfficeVersion = subjectClient.CheckOfficeVersion;
            //PublicClass.oSubjectProp.UFTopicTypeExists = subjectClient.UFTopicTypeExists;
            //PublicClass.oSubjectProp.UseUFAccountInitDate = subjectClient.UseUFAccountInitDate;
            //PublicClass.oSubjectProp.UFAccountInitDate = subjectClient.UFAccountInitDate;
            PublicClass.oSubjectProp.ExamMode = PublicClass.oMyJob.ExamMode;
            //PublicClass.oSubjectProp.ShowReadme = subjectClient.ShowReadme;
            //PublicClass.oSubjectProp.ReadmeInOfficialExam = subjectClient.ReadmeInOfficialExam;
            //PublicClass.oSubjectProp.ReadmeInSimulativelExam = subjectClient.ReadmeInSimulativelExam;
            PublicClass.oSubjectProp.TotalExamTime = publicClass.IntParse(PublicClass.oMyJob.TotalExamTime);
            //PublicClass.oSubjectProp.TimeMode = subjectClient.TimeMode;
            //PublicClass.oSubjectProp.AllowHideNavBar = subjectClient.AllowHideNavBar;
            //PublicClass.oSubjectProp.IgnoreTopicTypeUseNavButton = subjectClient.IgnoreTopicTypeUseNavButton;
            //PublicClass.oSubjectProp.AutoSaveInterval = 0;
            //PublicClass.oSubjectProp.PaperType = subjectClient.PaperType;
        }
        /// <summary>
        /// 初始化学生目录
        /// </summary>
        private void InitialStudentDir()
        {
            string studentDir = UserConfigSettings.Instance.ReadSetting("学生目录");
            string filePath = PublicClass.oMyJob.FilePath;
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (string.IsNullOrEmpty(studentDir))
            {
                PublicClass.StudentDir = string.Format(@"{0}\K01\{1}_{2}", PublicClass.GetStudentDir(), PublicClass.StudentCode, fileName);
            }
            else
            {
                PublicClass.StudentDir = string.Format(@"{0}\K01\{1}_{2}", studentDir.Substring(0, 3), PublicClass.StudentCode, fileName);
            }
            UserConfigSettings.Instance.WriteSetting("学生目录", PublicClass.StudentDir);
        }
        /// <summary>
        /// 初始化考试方式
        /// </summary>
        private void InitialKaoShiFangShi()
        {
            publicClass.InitSystemProp();
            string paperPath = PublicClass.StudentDir + "\\Data\\ExamRec.dat";
            string studentCode = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "考生信息", "考生考号");
            string IsUTF = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "试卷参数", "用友实操题");
            if (File.Exists(paperPath) && studentCode == PublicClass.StudentCode)
            {
                if (IsUTF.ToLower() == "true")
                {
                    int checkUFResult = PublicClass.SowerExamPlugn.CheckUFData(PublicClass.StudentDir, PublicClass.ExamSysDir, 0);
                    if (checkUFResult == 1)
                    {
                        SetKaiShiFangShi();
                    }
                    else
                    {
                        PublicClass.KaiShiFangShi = KaiShiFangShi.XinDeKaiShi;
                    }
                }
                else
                {
                    SetKaiShiFangShi();
                }
            }
            else
            {
                PublicClass.KaiShiFangShi = KaiShiFangShi.XinDeZuoYe;
            }
        }
        /// <summary>
        /// 设置开始方式
        /// </summary>
        private void SetKaiShiFangShi()
        {
            DialogResult result = PublicClass.ShowMessageOKCancel("上次作业还没有答完\n【确定】进入上次作业练习\n【取消】进入新的作业练习。");
            if (result == DialogResult.OK)
            {
                PublicClass.KaiShiFangShi = KaiShiFangShi.JiXuShangCiZuoYe;
            }
            else
            {
                PublicClass.KaiShiFangShi = KaiShiFangShi.XinDeZuoYe;
            }
        }
        /// <summary>
        /// 设置作业下载状态
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetJobDownLoadState(List<M_MyJob> serverMyJob)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Paper\Download");
            List<FileInfo> fileInfo = directoryInfo.GetFiles("*.*").Where(file => file.Name.ToLower().EndsWith("rar") || file.Name.ToLower().EndsWith("zip")).ToList();
            downMyJob.Clear();
            foreach (var server in serverMyJob)
            {
                string serverName = string.Format("{0}_{1}", PublicClass.StudentCode, Path.GetFileNameWithoutExtension(server.FilePath));
                bool result = fileInfo.Exists(f => Path.GetFileNameWithoutExtension(f.Name) == serverName);
                if (result)
                {
                    server.JobDownLoadState = "已下载";
                    downMyJob.Add(server);
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
        /// 设置视频下载状态
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetVideoDownLoadState(List<M_MyJob> serverMyJob)
        {
            bool existsResult = false;
            string videoFileName = string.Empty;
            DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Video\");
            List<DirectoryInfo> fileInfo = directoryInfo.GetDirectories().ToList();
            foreach (var server in serverMyJob)
            {
                videoFileName = string.IsNullOrEmpty(server.VideoFileName) == true ? "" : Path.GetFileNameWithoutExtension(server.VideoFileName.ToLower());
                existsResult = fileInfo.Exists(f =>
                {
                    string[] nameList = f.Name.Split('_');
                    if (nameList.Length <= 1) return false;
                    if (nameList[0] != PublicClass.StudentCode) return false;
                    if (nameList[1] == videoFileName) return true;
                    else return false;
                });
                if (existsResult)
                {
                    server.VideoDownLoadState = "已下载";
                }
                else
                {
                    server.VideoDownLoadState = "未下载";
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
        /// 排序
        /// </summary>
        /// <param name="job1"></param>
        /// <param name="job2"></param>
        /// <returns></returns>
        private int SortMyJob(M_MyJob job1, M_MyJob job2)
        {
            if (job1.CourseName.CompareTo(job2.CourseName) != 0)
                return job1.CourseName.CompareTo(job2.CourseName);
            else if (job1.ChapterName.CompareTo(job2.ChapterName) != 0)
                return job1.ChapterName.CompareTo(job2.ChapterName);
            else
                return job1.HWName.CompareTo(job2.HWName);
        }
        /// <summary>
        /// 初始化系统组件
        /// </summary>
        public frmDownWork()
        {
            InitializeComponent();
        }

        private void frmDownWork_Load(object sender, EventArgs e)
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
                    //筛选出已下载作业
                    SetJobDownLoadState(serverMyJob);
                    //设置账套下载状态
                    SetAccountDownLoadState(serverMyJob);
                    //设置视频下载状态
                    SetVideoDownLoadState(serverMyJob);
                    //排序
                    downMyJob.Sort(SortMyJob);
                    //设置作业序号
                    SetJobNo(downMyJob);
                }, null);

                //绑定作业到列表
                dgvResult.AutoGenerateColumns = false;
                dgvResult.DataSource = new BindingCollection<M_MyJob>(downMyJob);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnDoJob_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count == 0) return;

                M_MyJob myJob = dgvResult.SelectedRows[0].DataBoundItem as M_MyJob;
                PublicClass.oMyJob = myJob;
                PublicClass.JobType = JobType.ShiJuan;
                PublicClass.VideoFilePath = string.Format(@"{0}\SowerTestClient\Video\{1}_{2}\",
                    Application.StartupPath, PublicClass.StudentCode, DirFileHelper.GetFileNameNoExtension(myJob.VideoFilePath));

                //作业限定时间&&当前时间小于作业发布开始时间
                if (myJob.HWSubmitTimeType.ToLower() == "true" &&       //true：限时，false：不限时
                    DateTime.Now <= DateTime.Parse(myJob.ExamStartDateTime))
                {
                    PublicClass.ShowMessageOk("还没有到做作业的时间，先休息休息吧！");
                    return;
                }

                //作业限定时间&&不允许补交作业&&当前时间大于作业提交截止时间
                if (myJob.HWSubmitTimeType.ToLower() == "true" &&       //true：限时，false：不限时
                    myJob.IsPay.ToLower() == "false" &&           //true：允许补交作业，false：不允许补交作业
                    DateTime.Now >= DateTime.Parse(myJob.ExamEndDateTime))
                {
                    PublicClass.ShowMessageOk("对不起，您已经过了交作业时间。\n请联系老师允许您补交作业！");
                    return;
                }

                //不允许重复提交&&作业已经提交
                if (myJob.IsAllowReSubmitScore.ToLower() == "false" &&
                    myJob.ScoreSubmitted == "1")
                {
                    PublicClass.ShowMessageOk("对不起，您已经提交过作业，不能重复提交！");
                    return;
                }

                //允许重复提交&&大于重复提交次数
                if (myJob.IsAllowReSubmitScore.ToLower() == "true" &&
                    myJob.AllowReSubmitScoreCount <= myJob.SubmittedCount &&
                    myJob.AllowReSubmitScoreCount != -1)
                {
                    PublicClass.ShowMessageOk(string.Format("对不起，您只能提交{0}次成绩！", myJob.AllowReSubmitScoreCount));
                    return;
                }

                string envFilePath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, myJob.EnvFileName);
                string requireEnvFile = string.IsNullOrEmpty(myJob.RequireEnvFile) == true ? "false" : myJob.RequireEnvFile.ToLower();
                if (requireEnvFile == "true" && !File.Exists(envFilePath))
                {
                    PublicClass.ShowMessageOk(string.Format("对不起，没有检测到账套文件{0}。\n请您进行手动添加！", myJob.EnvFileName));
                    return;
                }

                InitialStudentDir();

                InitialSubjectProp();

                InitialKaoShiFangShi();

                frmExamInfo examInfo = new frmExamInfo();
                examInfo.Show();

                frmBusicWorkMain busicWorkMain = this.ParentForm as frmBusicWorkMain;
                busicWorkMain.Hide();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmDownWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }

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
                    Msg.ShowInformation(string.Format("您添加的帐套文件无效，请添加名字为【{0}】的账套文件。", envFileName));
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

        private void btnAddVideoFile_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedRows.Count == 0) return;
            M_MyJob myJob = dgvResult.SelectedRows[0].DataBoundItem as M_MyJob;
            ofdOpenFile.Title = "视频资源";
            ofdOpenFile.FileName = "视频资源";
            ofdOpenFile.Filter = "视频资源|*.zip*";
            ofdOpenFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = ofdOpenFile.ShowDialog();

            if (result == DialogResult.OK)
            {
                string filePath = ofdOpenFile.FileName;
                string fileName = Path.GetFileName(filePath);
                string fileExt = Path.GetExtension(filePath).ToLower();
                string copyPath = string.Format(@"{0}\SowerTestClient\Video\{1}_{2}\",
                    Application.StartupPath, PublicClass.StudentCode, DirFileHelper.GetFileNameNoExtension(filePath));
                string videoFileName = string.IsNullOrEmpty(myJob.VideoFileName) == true ? "" : myJob.VideoFileName.ToLower();
                if (string.IsNullOrEmpty(videoFileName))
                {
                    Msg.ShowInformation(string.Format("作业“{0}”没有添加视频资源，请联系授课教师到作业中心->作业维护发布列表进行添加。", myJob.HWName));
                    return;
                }
                if (fileName.ToLower() != videoFileName)
                {
                    Msg.ShowInformation(string.Format("您添加的视频资源无效，请添加名字为【{0}】的视频资源。", videoFileName));
                    return;
                }
                try
                {
                    if (fileExt != ".zip")
                    {
                        PublicClass.ShowMessageOk("该文件不是有效的视频资源，请重新添加！");
                        return;
                    }
                    if (Directory.Exists(copyPath))
                    {
                        DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该视频资源包已经存在，确定要覆盖吗？");
                        if (dialogResult == DialogResult.Cancel) return;
                    }
                    CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                    {
                        ZipFileTools.UnZipSZL(filePath, copyPath);
                        dgvResult.SelectedRows[0].Cells["VideoDownLoadState"].Value = "已下载";
                    }, null);
                }
                catch (Exception ex)
                {
                    LogHelper.WriteLog(typeof(frmHomeWork), ex);
                    CommonUtil.WriteLog(ex);
                }
            }
        }
    }
}
