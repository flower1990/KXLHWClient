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

namespace ComputerExam.BusicWorkOffLine
{
    public partial class frmHomeWork : Form
    {
        PublicClass publicClass = new PublicClass();

        /// <summary>
        /// 初始化学生目录
        /// </summary>
        private void InitialStudentDir(string fileName)
        {
            string studentDir = UserConfigSettings.Instance.ReadSetting("学生目录");
            if (string.IsNullOrEmpty(studentDir))
            {
                PublicClass.StudentDir = string.Format(@"{0}\\K01\{1}", PublicClass.GetStudentDir(), fileName);
            }
            else
            {
                PublicClass.StudentDir = string.Format(@"{0}\\K01\{1}", studentDir.Substring(0, 3), fileName);
            }
            UserConfigSettings.Instance.WriteSetting("学生目录", PublicClass.StudentDir);
        }
        /// <summary>
        /// 初始化科目属性
        /// </summary>
        /// <param name="myJob"></param>
        private void InitialSubjectProp()
        {
            //初始化科目参数
            PublicClass.oSubjectProp.ExamNumber = PublicClass.StudentCode;
            PublicClass.oSubjectProp.StudentName = PublicClass.oMyJob.RealName;
            PublicClass.oSubjectProp.SubjectName = PublicClass.oMyJob.CourseName;
            PublicClass.oSubjectProp.PaperName = string.Format("{0}_{1}", PublicClass.StudentCode, Path.GetFileName(PublicClass.oMyJob.FilePath));
            PublicClass.oSubjectProp.EnvFileName = PublicClass.oMyJob.EnvFileName;
            PublicClass.oSubjectProp.ShowScore = bool.Parse(PublicClass.oMyJob.ShowScore);
            PublicClass.oSubjectProp.ShowAnalysis = bool.Parse(PublicClass.oMyJob.ShowAnalysis);
            PublicClass.oSubjectProp.ExamMode = PublicClass.oMyJob.ExamMode;
            PublicClass.oSubjectProp.TotalExamTime = publicClass.IntParse(PublicClass.oMyJob.TotalExamTime);
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
                        PublicClass.KaiShiFangShi = KaiShiFangShi.JiXuShangCiZuoYe;
                    }
                    else
                    {
                        PublicClass.KaiShiFangShi = KaiShiFangShi.XinDeKaiShi;
                    }
                }
                else
                {
                    PublicClass.KaiShiFangShi = KaiShiFangShi.JiXuShangCiZuoYe;
                }
            }
            else
            {
                PublicClass.KaiShiFangShi = KaiShiFangShi.XinDeZuoYe;
            }
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
            if (string.IsNullOrEmpty(PublicClass.StudentDir))
            {
                btnDoJob.Enabled = false;
            }
            else
            {
                btnDoJob.Enabled = true;
            }
        }
        /// <summary>
        /// 还原作业文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestoreJob_Click(object sender, EventArgs e)
        {
            var fileName = string.Empty;
            var fileFullPath = string.Empty;
            var myJob = new M_MyJob();
            var listMyJob = new List<M_MyJob>();
            DialogResult dialogResult = ofdOpenFile.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;
            try
            {
                fileFullPath = ofdOpenFile.FileName;
                fileName = Path.GetFileNameWithoutExtension(fileFullPath);
                InitialStudentDir(fileName);
                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    ZipFileTools.UnZipSZL(fileFullPath, PublicClass.StudentDir);
                    #region 初始化作业信息
                    myJob.JobNo = "1";
                    myJob.RealName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "RealName");
                    myJob.ShowScore = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ShowScore");
                    myJob.ShowAnalysis = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ShowAnalysis");
                    myJob.ExamMode = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ExamMode");
                    myJob.CreateTime = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "CreateTime");
                    myJob.IsAllowReSubmitScore = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsAllowReSubmitScore");
                    myJob.AllowReSubmitScoreCount = publicClass.IntParse(PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "AllowReSubmitScoreCount"));
                    myJob.HWSubmitTimeType = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "HWSubmitTimeType");
                    myJob.CourseName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "CourseName");
                    myJob.ChapterName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ChapterName");
                    myJob.HWName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "HWName");
                    myJob.EnvFileName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "EnvFileName");
                    myJob.FileName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "FileName");
                    myJob.FilePath = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "FilePath");
                    myJob.IsEnable = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsEnable");
                    myJob.IsScoreToCenter = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsScoreToCenter");
                    myJob.ManagerName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ManagerName");
                    myJob.IsCaculateTime = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsCaculateTime");
                    myJob.ClassName = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ClassName");
                    myJob.SpecialtyID = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "SpecialtyID");
                    myJob.CityID = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "CityID");
                    myJob.PublicUserID = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "PublicUserID");
                    myJob.ExamStartDateTime = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ExamStartDateTime");
                    myJob.ExamEndDateTime = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ExamEndDateTime");
                    myJob.IsPay = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsPay");
                    myJob.IsSingleGrade = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsSingleGrade");
                    myJob.JobDownLoadState = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "JobDownLoadState");
                    myJob.ScoreSubmitted = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ScoreSubmitted");
                    myJob.ClassID = publicClass.IntParse(PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "ClassID"));
                    myJob.AccountDownLoadState = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "AccountDownLoadState");
                    myJob.IsUploadAnswerFile = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "作业信息", "IsUploadAnswerFile");
                    #endregion
                    listMyJob.Add(myJob);
                    PublicClass.oMyJob = myJob;
                    PublicClass.JobType = JobType.ShiJuan;
                }, null);
                dgvResult.AutoGenerateColumns = false;
                dgvResult.DataSource = listMyJob;
                btnDoJob.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
            }
        }
        /// <summary>
        /// 做作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDoJob_Click(object sender, EventArgs e)
        {
            try
            {
                //作业限定时间&&当前时间小于作业发布开始时间
                if (PublicClass.oMyJob.HWSubmitTimeType.ToLower() == "true" &&       //true：限时，false：不限时
                    DateTime.Now <= DateTime.Parse(PublicClass.oMyJob.ExamStartDateTime))
                {
                    PublicClass.ShowMessageOk("还没有到做作业的时间，先休息休息吧！");
                    return;
                }

                //作业限定时间&&不允许补交作业&&当前时间大于作业提交截止时间
                if (PublicClass.oMyJob.HWSubmitTimeType.ToLower() == "true" &&       //true：限时，false：不限时
                    PublicClass.oMyJob.IsPay.ToLower() == "false" &&           //true：允许补交作业，false：不允许补交作业
                    DateTime.Now >= DateTime.Parse(PublicClass.oMyJob.ExamEndDateTime))
                {
                    PublicClass.ShowMessageOk("对不起，您已经过了交作业时间。\n请联系老师允许您补交作业！");
                    return;
                }

                //不允许重复提交&&作业已经提交
                if (PublicClass.oMyJob.IsAllowReSubmitScore.ToLower() == "false" &&
                    PublicClass.oMyJob.ScoreSubmitted == "1")
                {
                    PublicClass.ShowMessageOk("对不起，您已经提交过作业，不能重复提交！");
                    return;
                }

                //允许重复提交&&大于重复提交次数
                if (PublicClass.oMyJob.IsAllowReSubmitScore.ToLower() == "true" &&
                    PublicClass.oMyJob.AllowReSubmitScoreCount <= PublicClass.oMyJob.SubmittedCount &&
                    PublicClass.oMyJob.AllowReSubmitScoreCount != -1)
                {
                    PublicClass.ShowMessageOk(string.Format("对不起，您只能提交{0}次成绩！", PublicClass.oMyJob.AllowReSubmitScoreCount));
                    return;
                }

                InitialSubjectProp();
                InitialKaoShiFangShi();

                frmExamInfo examInfo = new frmExamInfo();
                examInfo.Show();

                frmBusicWorkMain busicWorkMain = this.ParentForm as frmBusicWorkMain;
                busicWorkMain.Hide();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
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
