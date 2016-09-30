using ComputerExam.Model;
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

namespace ComputerExam.BusicWork
{
    public partial class frmWorkScoreDetail : Form
    {
        M_JobScore jobScore = new M_JobScore();
        B_Service bService = new B_Service();
        PublicClass publicClass = new PublicClass();

        public frmWorkScoreDetail()
        {
            InitializeComponent();
        }
        public frmWorkScoreDetail(M_JobScore score)
        {
            InitializeComponent();
            jobScore = score;
            CommonUtil.InitialBackgroundImage(Globals.BGScoreDetail, pnlBackground);
        }

        private void frmWorkScoreDetail_Load(object sender, EventArgs e)
        {
            lblSubject.Text = string.Format("作业名称：{0}  作业成绩：{1}", jobScore.HWName, jobScore.TotalScore);
            string result = bService.GetJobDetailScore(PublicClass.StudentCode, jobScore.HWID, 1);
            //List<M_PaperTopic> listPaperTopic = XmlHelper.XmlToObjList<M_PaperTopic>(jobScore.ScoreDetail, "PaperTopicType");
            
            //publicClass.InitialPaperScore(listPaperTopic, txtScore);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
