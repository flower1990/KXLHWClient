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
using ComputerExam.Util;
using ComputerExam.Common;
using System.Threading;

namespace ComputerExam.BusicWork
{
    public partial class frmWorkBrowse : Form
    {
        B_Service bService = new B_Service();
        List<M_JobScore> listJobScore = new List<M_JobScore>();
        ListUtil listUtil = new ListUtil();

        /// <summary>
        /// 设置作业序号
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetJobNo(List<M_JobScore> listJobScore)
        {
            for (int i = 0; i < listJobScore.Count; i++)
            {
                listJobScore[i].JobScoreNo = (i + 1).ToString("00");
            }
        }
        /// <summary>
        /// 设置作业未上交成绩
        /// </summary>
        private void SetJobScoreTotalScore(List<M_JobScore> jobScore)
        {
            foreach (M_JobScore item in jobScore)
            {
                if (item.TotalScore == 0m)
                {
                    item.TotalScore = 0;
                }
            }
        }
        //排序
        private int SortMyJob(M_JobScore job1, M_JobScore job2)
        {
            if (job1.CourseName.CompareTo(job2.CourseName) != 0)
                return job1.CourseName.CompareTo(job2.CourseName);
            else if (job1.ChapterName.CompareTo(job2.ChapterName) != 0)
                return job1.ChapterName.CompareTo(job2.ChapterName);
            else
                return job1.HWName.CompareTo(job2.HWName);
        }
        public frmWorkBrowse()
        {
            InitializeComponent();
        }

        private void frmBrowse_Load(object sender, EventArgs e)
        {
            try
            {
                CommonUtil.BindComboBox(cboSubject, "CourseID", "CourseName", CommonUtil.listSubject);
                if (CommonUtil.listSubject.Count == 2) cboSubject.SelectedIndex = 1; 
                else cboSubject.SelectedIndex = 0;

                cboSubject.SelectedValueChanged += cboSubject_SelectedValueChanged;
                cboSubject_SelectedValueChanged(this, e);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex.Message);
            }
        }

        private void cboSubject_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                string courseId = cboSubject.SelectedValue.ToString();
                List<M_MyJob> listMyJob = CommonUtil.listMyJob.Where(l => l.CourseID == courseId).ToList();
                listMyJob.Insert(0, new M_MyJob() { HWID = "0", HWName = "---请选择---" });
                CommonUtil.BindComboBox(cboJob, "HWID", "HWName", listMyJob);
            }
            catch (Exception ex)
            {
                PublicClass.ShowErrorMessageOk(ex.Message);
                LogHelper.WriteLog(typeof(frmHomeWork), ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string subjectValue = cboSubject.SelectedValue.ToString();
                string jobValue = cboJob.SelectedValue.ToString();
                
                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    Thread.Sleep(1000);
                    listJobScore = bService.GetJobScore(PublicClass.StudentCode, "1900-1-1", "2099-1-1", 2);
                    //根据科目查询作业
                    if (subjectValue != "0") listJobScore = listJobScore.FindAll(s => s.CourseID == subjectValue);
                    //根据实验查询成绩
                    if (jobValue != "0") listJobScore = listJobScore.FindAll(s => s.HWID == jobValue);
                    //排序
                    listJobScore.Sort(SortMyJob);
                    //设置作业序号
                    SetJobNo(listJobScore);
                }, null);

                //绑定作业到列表
                dgvResult.AutoGenerateColumns = false;
                dgvResult.DataSource = new BindingCollection<M_JobScore>(listJobScore);

                int trainingCount = listJobScore.Count;
                decimal totalScore = listJobScore.Sum(j => j.TotalScore);
                decimal average = trainingCount == 0 ? 0m : totalScore / trainingCount;
                lblTrainingCount.Text = trainingCount.ToString() + "次";
                lblAverage.Text = average.ToString("0.00") + "分";
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnScoreDetail_Click(object sender, EventArgs e)
        {
            if (dgvResult.SelectedRows.Count == 0) return;

            M_JobScore jobScore = dgvResult.SelectedRows[0].DataBoundItem as M_JobScore;

            frmWorkScoreDetail workScoreDetail = new frmWorkScoreDetail(jobScore);
            workScoreDetail.ShowDialog();
        }

        private void dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e == null || e.Value == null || e.Value.ToString() == "" || !(sender is DataGridView))
            {
                return;
            }

            DataGridView dgv = (DataGridView)sender;
            object originalValue = e.Value;

            if (e.ColumnIndex == dgv.Columns["ScoreSubmitTime"].Index)   //格式化日期
            {
                if (e.Value != null && e.Value.ToString() != "")
                {
                    e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-M-d");
                }
            }
        }
    }
}
