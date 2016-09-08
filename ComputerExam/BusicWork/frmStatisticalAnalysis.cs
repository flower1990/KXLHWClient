using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComputerExam.Model;
using ComputerExam.BLL;
using ComputerExam.Util;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;

namespace ComputerExam.BusicWork
{
    public partial class frmStatisticalAnalysis : Form
    {
        B_Service bService = new B_Service();
        ListUtil listUtil = new ListUtil();
        List<M_JobScore> listJobScore = new List<M_JobScore>();

        /// <summary>
        /// 设置作业成绩状态
        /// </summary>
        /// <param name="jobScore"></param>
        private void SetJobScoreState(List<M_JobScore> jobScore)
        {
            foreach (M_JobScore item in jobScore)
            {
                //item.HwNameState = string.Format("{0}\n\n{1}", item.Stat, EnterAutoly(item.HWName));
                item.HwNameState = string.Format("{0}\n\n{1}", item.Stat, item.HWName);
            }
        }
        private string EnterAutoly(string input)
        {
            string newstr = input;
            int length = input.Length;
            int count = length / 1;
            if (count >= 1)
            {
                newstr = input.Substring(0, 1) + "\n";
                for (int i = 1; i < count; i++)
                {
                    newstr += input.Substring(i * 1, 1) + "\n";
                }
                newstr += input.Substring(count * 1);
            }
            return newstr;
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
        /// <summary>
        /// 设置图表颜色
        /// </summary>
        private void SetJobScorePoints()
        {
            List<DataPoint> dataPoints = chartMyJob.Series[0].Points.ToList();
            for (int i = 0; i < dataPoints.Count; i++)
            {
                List<string> result = dataPoints[i].AxisLabel.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                if (result[0] == "未按时上交")
                {
                    chartMyJob.Series[0].Points[i].Color = Color.Red;
                }

                if (result[0] == "已上交")
                {
                    chartMyJob.Series[0].Points[i].Color = Color.DodgerBlue;
                }

                if (result[0] == "未上交")
                {
                    chartMyJob.Series[0].Points[i].LabelForeColor = Color.Red;
                }
            }
        }
        public void SetChartVisible(bool value)
        {
            chartLine.Visible = !value;
            chartMyJob.Visible = value;
        }
        /// <summary>
        /// 设置作业序号
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetJobNo(List<M_JobScore> listJobScore)
        {
            for (int i = 0; i < listJobScore.Count; i++)
            {
                listJobScore[i].JobScoreNo = (i + 1).ToString();
            }
        }
        /// <summary>
        /// 加载系统组件
        /// </summary>
        public frmStatisticalAnalysis()
        {
            InitializeComponent();
        }

        private void frmStatisticalAnalysis_Load(object sender, EventArgs e)
        {
            try
            {
                SetChartVisible(true);
                CommonUtil.BindComboBox(cboSubject, "CourseID", "CourseName", CommonUtil.listSubject);
                CommonUtil.BindComboBox(cboChartType, "Id", "Name", CommonUtil.listChartType);
                if (CommonUtil.listSubject.Count == 2) cboSubject.SelectedIndex = 1;
                else cboSubject.SelectedIndex = 0;

                cboSubject.SelectedValueChanged += cboSubject_SelectedValueChanged;
                cboSubject_SelectedValueChanged(this, e);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
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
                if (listMyJob.Count > 1) cboJob.SelectedIndex = 1;
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
                string chartValue = cboChartType.SelectedValue.ToString();

                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    Thread.Sleep(1000);
                    listJobScore = bService.GetJobScore(PublicClass.StudentCode, "1900-1-1", "2099-1-1", 2);
                    //根据科目查询作业
                    if (subjectValue != "0") listJobScore = listJobScore.FindAll(s => s.CourseID == subjectValue);
                    //根据实验查询成绩
                    if (jobValue != "0") listJobScore = listJobScore.FindAll(s => s.HWID == jobValue);
                    //合并作业名称和状态
                    SetJobScoreState(listJobScore);
                    //设置作业序号
                    SetJobNo(listJobScore);
                }, null);

                switch (chartValue)
                {
                    case "1":
                        SetChartVisible(true);
                        //数据绑定
                        chartMyJob.DataSource = listJobScore;
                        chartMyJob.DataBind();
                        //设置图表颜色
                        SetJobScorePoints();
                        break;
                    case "2":
                        SetChartVisible(false);
                        //数据绑定
                        chartLine.DataSource = listJobScore;
                        chartLine.DataBind();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }
    }
}
