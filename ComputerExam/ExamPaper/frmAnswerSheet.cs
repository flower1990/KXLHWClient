using AxSowerRichText;
using ComputerExam.BLL;
using ComputerExam.Common;
using ComputerExam.Model;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ComputerExam.BusicWork;
using System.Diagnostics;
using ComputerExam.ExamPaper.TopicType;

namespace ComputerExam.ExamPaper
{
    public partial class frmAnswerSheet : Form
    {
        #region 公用变量
        public int Index = 1;
        public string TopicTypeId = "";
        string sysMessage = "";
        public int LastIndex = 0;
        private int iResidualExamTime;
        int totalUserTime;
        private int topicFaceWidth;
        int topicHeight = 0;
        /// <summary>
        /// //0：放大 1：右下角缩小 2：上方缩小
        /// </summary>
        int formState = 0;
        public int FFontSize = 12;

        List<string> panelList = new List<string>();
        Size topicFaceSize = new Size();
        Point topicFacePoint = new Point();
        Point topicLocation = new Point();
        Point topicTypeLocation = new Point();

        PublicClass publicClass = new PublicClass();
        XmlUnit xml = new XmlUnit();
        OpaqueCommand cmd = new OpaqueCommand();

        frmDanXuanTi danXuan = null;
        frmDuoXuanTi duoXuan = null;
        frmPeiWuTi peiWu = null;
        frmPanDuanTi panDuan = null;
        frmTianKongTi tianKong = null;
        frmTyping typing = null;
        frmAnLiFenXi anLiFenXi = null;
        frmJiSuanFenXi jiSuanFenXi = null;
        frmZhuGuanTi zhuguan = null;

        B_ExamPaperScore bExamPaperScore = new B_ExamPaperScore();
        B_Service bService = new B_Service();

        public M_Topic oCurrTopic = new M_Topic();
        M_TopicType topicType = new M_TopicType();
        M_Topic topic = new M_Topic();
        M_SubTopic subTopic = new M_SubTopic();
        #endregion

        #region 公用方法
        /// <summary>
        /// 初始化窗体位置
        /// </summary>
        public void InitialForm()
        {
            formState = 0;
            SetFormSize();

            topicFaceWidth = txtTopicFace.Width;
            topicFaceSize = pnlTopicFace.Size;
            topicFacePoint = pnlTopicFace.Location;
            topicHeight = pnlTopic.Height;
            topicLocation = pnlTopic.Location;
            topicTypeLocation = pnlTopicType.Location;

            txtAnalysis.FontSize = FFontSize;
            txtTopicFace.FontSize = FFontSize;
            txtTyping.Font = new Font("宋体", FFontSize);

            publicClass.DisableRightClickMenu(txtTyping);
            publicClass.DisableCopying(txtTyping);
        }
        /// <summary>
        /// 显示窗体信息
        /// </summary>
        public void FormShow()
        {
            try
            {
                int iHour;
                int iSecond;

                if (PublicClass.JobType == JobType.TiKu)
                {
                    this.Text = string.Format("考学练 作业客户端_{0}--第{1}套试题", PublicClass.oSubjectProp.SubjectName, PublicClass.oSubjectProp.PresetPaperID);
                    string examTime = PublicClass.oSubjectProp.ExamMode == "1" ? "不计时" : PublicClass.oSubjectProp.TotalExamTime.ToString() + "分钟";
                    lblExamSubject.Text = string.Format("考试科目：{0}，考试时长：{1}，试卷总分：{2}", PublicClass.oSubjectProp.SubjectName, examTime, PublicClass.oPaperInfo.PaperMark);
                    lblExamName.Text = string.Format("考生姓名：{0}，准考证号：{1}，考试用机：{2}[{3}]", PublicClass.oSubjectProp.StudentName, PublicClass.StudentCode, Environment.MachineName, PublicClass.LANIP);
                }

                if (PublicClass.JobType == JobType.ShiJuan)
                {
                    this.Text = string.Format("考学练 作业客户端_{0}", PublicClass.oMyJob.HWName);
                    string examTime = string.IsNullOrEmpty(PublicClass.oMyJob.TotalExamTime) == true ? "不计时" : PublicClass.oMyJob.TotalExamTime.ToString() + "分钟";
                    lblExamSubject.Text = string.Format("作业名称：{0}，考试时长：{1}，试卷总分：{2}", PublicClass.oMyJob.HWName, examTime, PublicClass.oPaperInfo.PaperMark);
                    lblExamName.Text = string.Format("考生姓名：{0}，准考证号：{1}，考试用机：{2}[{3}]", PublicClass.oMyJob.RealName, PublicClass.StudentCode, Environment.MachineName, PublicClass.LANIP);
                }

                //加载试题
                LoadExamineeInfo();
                //加载第一题
                Index = 1;
                TopicTypeId = PublicClass.oPaperInfo.TopicTypes[0].Id;
                tmrShowTopic.Start();

                #region 题库
                if (PublicClass.JobType == JobType.TiKu)
                {
                    if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                    {
                        iResidualExamTime = PublicClass.UseExamMin;
                        iHour = iResidualExamTime / 60;
                        iSecond = iResidualExamTime % 60;
                        lblTime.Text = string.Format("{0}:{1}", iHour, iSecond);

                        tmrExamTimeCountDownTimer.Enabled = true;
                        tsbDisplayAnalysis.Visible = false;
                        tsbSingleQuesAnswer.Visible = false;
                        tsbPaperScore.Visible = false;
                        scTopic.Panel2Collapsed = true;

                        lblTime.Visible = true;
                        tsbHandPaper.Visible = true;
                    }
                    else
                    {
                        tmrExamTimeCountDownTimer.Enabled = false;
                        tsbDisplayAnalysis.Visible = true;
                        tsbSingleQuesAnswer.Visible = true;
                        tsbPaperScore.Visible = true;
                        scTopic.Panel2Collapsed = true;
                        lblTime.Visible = false;
                        tsbHandPaper.Visible = true;
                    }
                }
                #endregion

                #region 作业
                if (PublicClass.JobType == JobType.ShiJuan)
                {
                    if (PublicClass.oMyJob.IsCaculateTime.ToLower() == "true")
                    {
                        iResidualExamTime = PublicClass.UseExamMin;
                        iHour = iResidualExamTime / 60;
                        iSecond = iResidualExamTime % 60;
                        lblTime.Text = string.Format("{0}:{1}", iHour, iSecond);

                        tmrExamTimeCountDownTimer.Start();
                        tsbDisplayAnalysis.Visible = false;
                        tsbSingleQuesAnswer.Visible = false;
                        tsbPaperScore.Visible = false;
                        scTopic.Panel2Collapsed = true;
                        scSubTopic.Panel2Collapsed = true;
                        lblTime.Visible = true;
                    }
                    else
                    {
                        tmrExamTimeCountDownTimer.Start();
                        tsbDisplayAnalysis.Visible = false;
                        tsbSingleQuesAnswer.Visible = false;
                        tsbPaperScore.Visible = false;
                        scTopic.Panel2Collapsed = true;
                        scSubTopic.Panel2Collapsed = true;
                        lblTime.Visible = false;
                    }

                    //是否显示评析
                    if (PublicClass.oMyJob.ShowAnalysis.ToLower() == "false" || PublicClass.oMyJob.ShowAnalysis == null)
                    {
                        tsbDisplayAnalysis.Visible = false;
                    }
                    else
                    {
                        tsbDisplayAnalysis.Visible = true;
                    }

                    //是否显示单题评分
                    if (PublicClass.oMyJob.IsSingleGrade.ToLower() == "false" || PublicClass.oMyJob.IsSingleGrade == null)
                    {
                        tsbSingleQuesAnswer.Visible = false;
                    }
                    else
                    {
                        tsbSingleQuesAnswer.Visible = true;
                    }
                    if (Globals.IsOnline) bService.RecordingTime(1, PublicClass.StudentCode, PublicClass.oMyJob.ID, PublicClass.oMyJob.HWName);
                }
                #endregion

                //记录考试开始时间
                if (PublicClass.KaiShiFangShi == KaiShiFangShi.XinDeKaiShi || PublicClass.KaiShiFangShi == KaiShiFangShi.XinDeZuoYe)
                {
                    PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考试开始时间", DateTime.Now.ToString());
                    PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考试答题时间", "0");
                }

                totalUserTime = publicClass.IntParse(PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "考生信息", "考试答题时间"));
                tmrTotalUserTime.Start();
                //离线模式隐藏交卷退出
                if (!Globals.IsOnline)
                {
                    tsbHandPaper.Visible = false;
                    tsbBackupJob.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmAnswerSheet), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 绑定答题窗体
        /// </summary>
        /// <param name="form"></param>
        private void FormBind(Form form)
        {
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            form.Show();
            pnlTopicTypeContainer.Controls.Clear();
            pnlTopicTypeContainer.Controls.Add(form);
        }
        /// <summary>
        /// 根据题型ID，获取指定题型对象
        /// </summary>
        /// <param name="TopicTypeId"></param>
        /// <returns></returns>
        public M_TopicType GetTopicTypeObject(string TopicTypeId)
        {
            M_TopicType aTopicType = new M_TopicType();

            for (int i = 0; i < PublicClass.oPaperInfo.TopicTypes.Count; i++)
            {
                aTopicType = PublicClass.oPaperInfo.TopicTypes[i];
                if (aTopicType.Id == TopicTypeId)
                {
                    return aTopicType;
                }
            }
            return null;
        }
        /// <summary>
        /// 通过题型ID与试题ID，获取指定试题对象
        /// </summary>
        /// <param name="TopicTypeId"></param>
        /// <param name="TopicId"></param>
        /// <returns></returns>
        public M_Topic GetTopicObject(string TopicTypeId, string TopicId, string SubTopicId)
        {
            M_TopicType aTopicType = new M_TopicType();
            M_Topic aTopic = new M_Topic();
            aTopicType = GetTopicTypeObject(TopicTypeId);
            if (aTopicType.Topics.Count > 0)
            {
                for (int i = 0; i < aTopicType.Topics.Count; i++)
                {
                    aTopic = aTopicType.Topics[i];
                    if (aTopic.TopicId == TopicId && aTopic.TopicTypeId == TopicTypeId && aTopic.SubTopicId == SubTopicId)
                    {
                        return aTopic;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// //通过当前顺序ID，获取当前试题
        /// </summary>
        /// <param name="TypeOrderID"></param>
        /// <param name="TopicOrderID"></param>
        /// <param name="PfEngineId"></param>
        /// <returns></returns>
        public M_Topic GetCurrTopicObject(int TypeOrderID, int TopicOrderID, int PfEngineId)
        {
            M_TopicType aTopicType = new M_TopicType();
            M_Topic aTopic = new M_Topic();

            if (TypeOrderID > PublicClass.oPaperInfo.TopicTypes.Count)
            {
                return null;
            }

            aTopicType = PublicClass.oPaperInfo.TopicTypes[TypeOrderID];

            for (int i = 0; i < aTopicType.Topics.Count; i++)
            {
                if (aTopicType.Topics[i].TopicNo == TopicOrderID.ToString() && aTopicType.Topics[i].JudgeEngineId == PfEngineId.ToString())
                {
                    aTopic = aTopicType.Topics[i];
                    ShowTopic(aTopic.TopicTypeId, aTopic.TopicId, aTopic.SubTopicId);
                }
            }

            return aTopic;
        }
        /// <summary>
        /// 获取多选题用户答案
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public string GetDuoXuanAnswer(Panel panel)
        {
            StringBuilder sb = new StringBuilder();
            List<string> list = new List<string>();

            foreach (var item in panel.Controls)
            {
                if (item is CheckBox)
                {
                    CheckBox checkBox = item as CheckBox;
                    if (checkBox.Checked)
                    {
                        list.Add(checkBox.Text);
                    }
                }
            }

            list.Sort();

            foreach (var item in list)
            {
                sb.Append(item);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 获取配伍题用户答案
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public string GetPeiWuAnswer(Panel panel)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in panelList)
            {
                Panel subPanel = panel.Controls[item] as Panel;
                foreach (var subItem in subPanel.Controls)
                {
                    if (subItem is ComboBox)
                    {
                        if ((subItem as ComboBox).SelectedIndex != 0)
                        {
                            sb.AppendFormat("{0}", (subItem as ComboBox).Text.Trim());
                        }
                        else
                        {
                            sb.AppendFormat("{0}", " ");
                        }
                    }
                }
                //sb.AppendFormat("{0}", "♂");
            }
            string result = sb.ToString();
            return result;
        }
        /// <summary>
        /// 获取案例分析题用户答案
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public string GetAnliFenXiAnswer(Panel panel)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string item in panelList)
            {
                Panel subPanel = panel.Controls[item] as Panel;
                foreach (var subItem in subPanel.Controls)
                {
                    if (subItem is CheckBox && (subItem as CheckBox).Checked)
                    {
                        sb.AppendFormat("{0}", (subItem as CheckBox).Text);
                    }
                }
                sb.AppendFormat("{0}", "♂");
            }

            string result = sb.ToString();

            return result.Remove(result.Length - 1);
        }
        /// <summary>
        /// 获取计算分析题用户答案
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public string GetJiSuanFenXiAnswer(Panel panel)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in panelList)
            {
                Panel subPanel = panel.Controls[item] as Panel;
                foreach (var subItem in subPanel.Controls)
                {
                    if (subItem is TextBox && (subItem as TextBox).Text.Trim() != string.Empty)
                    {
                        sb.AppendFormat("{0}", (subItem as TextBox).Text.Trim());
                    }
                }
                sb.AppendFormat("{0}", "♂");
            }

            string result = sb.ToString();

            return result.Remove(result.Length - 1);
        }
        /// <summary>
        /// 获取填空题用户答案
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public string GetTianKongAnswer(Panel panel)
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in panelList)
            {
                Panel subPanel = panel.Controls[item] as Panel;
                foreach (var subItem in subPanel.Controls)
                {
                    if (subItem is TextBox && (subItem as TextBox).Text.Trim() != string.Empty)
                    {
                        sb.AppendFormat("{0}", (subItem as TextBox).Text.Trim());
                    }
                }
                sb.AppendFormat("{0}", "♂");
            }

            string result = sb.ToString();

            return result.Remove(result.Length - 1);
        }
        /// <summary>
        /// 通过题型ID与试题ID在试卷对象中更新对应试题对象
        /// </summary>
        /// <param name="TopicTypeId">题型ID</param>
        /// <param name="TopicId">试题ID</param>
        /// <param name="NewTopic"></param>
        public bool UpdateTopicObject(string TopicTypeId, string TopicId, string SubTopicID, M_Topic NewTopic)
        {
            var sCurrTopicTypeId = string.Empty;
            var sCurrTopicId = string.Empty;
            var sCurrSubTopicId = string.Empty;
            var result = false;
            var topicTypes = PublicClass.oPaperInfo.TopicTypes;
            //查询题型内试题结点并进行更新
            for (int i = 0; i < topicTypes.Count; i++)
            {
                for (int j = 0; j < topicTypes[i].Topics.Count; j++)
                {
                    sCurrTopicTypeId = topicTypes[i].Topics[j].TopicTypeId;
                    sCurrTopicId = topicTypes[i].Topics[j].TopicId;
                    sCurrSubTopicId = topicTypes[i].Topics[j].SubTopicId;
                    if (sCurrTopicTypeId == TopicTypeId && sCurrTopicId == TopicId && sCurrSubTopicId == SubTopicID)
                    {
                        PublicClass.oPaperInfo.TopicTypes[i].Topics[j] = NewTopic;
                        result = true;
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 显示试题
        /// </summary>
        /// <param name="TopicTypeId"></param>
        /// <param name="TopicId"></param>
        /// <param name="SubTopicId"></param>
        public void ShowTopic(string TopicTypeId, string TopicId, string SubTopicId)
        {
            M_TopicType oTopicType = new M_TopicType();
            M_Topic oTopic = new M_Topic();
            string sTopicInfo = string.Empty;
            string sTopicFace = string.Empty;
            string sSubTopicFace = string.Empty;
            string sStandardAnswer = string.Empty;
            string sAppPath = string.Empty;
            string sSampleDoc = string.Empty;
            string sEditorType = string.Empty;
            string sEditorProp = string.Empty;
            string sCurrTopicFlag = string.Empty;
            string sT3Type = string.Empty;
            string sCallT3Para = string.Empty;
            bool bTopicTypeChanged;
            int iDisplayMode = 2;

            //如果是当前试题的话，退出不处理
            if (oCurrTopic.TopicTypeId == TopicTypeId && oCurrTopic.TopicId == TopicId && oCurrTopic.SubTopicId == SubTopicId) return;
            //判断是否需要切换题型
            bTopicTypeChanged = (oCurrTopic.TopicTypeId != TopicTypeId);
            //如果当前原试题已被修改，则进行答案保存
            if (oCurrTopic.Changed) SaveUserAnswer();
            //获取指定题型及试题对象
            oTopicType = GetTopicTypeObject(TopicTypeId);
            oTopic = GetTopicObject(TopicTypeId, TopicId, SubTopicId);
            //安全检测
            if (oTopicType.Id == "" || oTopic.TopicId == "")
            {
                MessageBox.Show("指定题型或试题对象不存在，无法显示指定试题内容，按【确定】退出。");
                if (Globals.IsOnline) bService.ExitSystem(PublicClass.StudentCode);
                Environment.Exit(0);
            }
            //设置当前试题为指定试题，并获得该试题的附加信息
            oCurrTopic = oTopic;
            //获取应用程序路径与试题题面
            sTopicFace = PublicClass.SowerExamPlugn.GetTopicItem(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), 1);
            if (oCurrTopic.AllowSubTopic) sSubTopicFace = PublicClass.SowerExamPlugn.GetSubTopicItem(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), int.Parse(oCurrTopic.SubTopicId), 1);
            sTopicInfo = PublicClass.SowerExamPlugn.GetTopicInfo(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), int.Parse(oCurrTopic.SubTopicId), iDisplayMode);
            //安全检测
            if (sTopicInfo == "")
            {
                MessageBox.Show("无法获取指定试题的相关信息，按【确定】退出。");
                if (Globals.IsOnline) bService.ExitSystem(PublicClass.StudentCode);
                Environment.Exit(0);
            }
            sSampleDoc = xml.GetXmlNodeValue(sTopicInfo, "样张");
            sAppPath = xml.GetXmlNodeValue(sTopicInfo, "路径");
            sEditorType = xml.GetXmlNodeValue(sTopicInfo, "编辑类型");
            sEditorProp = xml.GetXmlNodeValue(sTopicInfo, "调用参数");
            sStandardAnswer = xml.GetXmlNodeValue(sTopicInfo, "答案");
            sT3Type = xml.GetXmlNodeValue(sTopicInfo, "编辑类型");
            sCallT3Para = xml.GetXmlNodeValue(sTopicInfo, "调用参数");
            //更新当前试题对象、卷内试题对象与考生试卷中的“当前试题信息”值  
            oCurrTopic.AppPath = sAppPath;
            oCurrTopic.SampleDoc = sSampleDoc;
            oCurrTopic.TopicFace = sTopicFace;
            oCurrTopic.SubTopicFace = sSubTopicFace;
            oCurrTopic.StandardAnswer = sStandardAnswer;
            oCurrTopic.T3Type = sT3Type;
            oCurrTopic.CallT3Para = sCallT3Para;
            oCurrTopic.EditorType = sEditorType;
            oCurrTopic.EditorProp = sEditorProp;
            UpdateTopicObject(oCurrTopic.TopicTypeId, oCurrTopic.TopicId, oCurrTopic.SubTopicId, oCurrTopic);
            sCurrTopicFlag = string.Format("{0}|{1}|{2}|{3}|{4}", oCurrTopic.TopicTypeId, oCurrTopic.TopicId, oCurrTopic.SubTopicId, iDisplayMode, oCurrTopic.TopicNo);
            PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "当前试题信息", sCurrTopicFlag);
            //加载题面
            if (oCurrTopic.AllowSubTopic)
            {
                scTopic.Visible = false;
                scSubTopic.Visible = true;
                if (publicClass.TopicIsTyping(oCurrTopic) == false)
                {
                    CommonUtil.LoadSowerText(txtSubTopicTop, oCurrTopic.TopicFace, oCurrTopic);
                    CommonUtil.LoadSowerTextSub(txtSubTopicBottom, oCurrTopic.SubTopicFace, oCurrTopic);
                }
                else
                {
                    txtTyping.Text = oCurrTopic.TopicFace;
                }
            }
            else
            {
                scTopic.Visible = true;
                scSubTopic.Visible = false;
                if (publicClass.TopicIsTyping(oCurrTopic) == false)
                {
                    CommonUtil.LoadSowerText(txtTopicFace, oCurrTopic.TopicFace, oCurrTopic);
                }
                else
                {
                    txtTyping.Text = oCurrTopic.TopicFace;
                }
            }
        }
        /// <summary>
        /// 设置上方工具栏，显示和隐藏
        /// </summary>
        /// <param name="tssbAddFontSize">增大字号</param>
        /// <param name="tssbReduceFontSize">减小字号</param>
        /// <param name="tssbDifficultMark">疑难标识</param>
        /// <param name="tssbResetTopic">重做本题</param>
        /// <param name="tssbDisplayAnalysis">显示评析</param>
        /// <param name="tssbSingleQuesAnswer">单题评分</param>
        /// <param name="tssbPaperScore">试卷评分</param>
        /// <param name="tssbReturnBigTopic">返回大题板</param>
        /// <param name="tssbReset">复位题板位置</param>
        /// <param name="tssbLessenTopicFace">缩小题版--计算机--实操题</param>
        /// <param name="tssbLessenTopicFace2">缩小题版--会计--实务仿真题</param>
        /// <param name="tssbExit">退出练习</param>
        /// <param name="tssbHandPaper">交卷退出</param>
        private void ShowTopTsbButton(bool tssbAddFontSize, bool tssbReduceFontSize, bool tssbDifficultMark, bool tssbResetTopic, bool tssbDisplayAnalysis, bool tssbSingleQuesAnswer, bool tssbPaperScore, bool tssbReturnBigTopic, bool tssbReset, bool tssbLessenTopicFace, bool tssbLessenTopicFace2, bool tssbExit, bool tssbHandPaper)
        {
            if (tssbAddFontSize) tsbAddFontSize.Visible = true; else tsbAddFontSize.Visible = false;
            if (tssbReduceFontSize) tsbReduceFontSize.Visible = true; else tsbReduceFontSize.Visible = false;
            if (tssbDifficultMark) tsbDifficultMark.Visible = true; else tsbDifficultMark.Visible = false;
            if (tssbResetTopic) tsbResetTopic.Visible = true; else tsbResetTopic.Visible = false;
            if (tssbDisplayAnalysis) tsbDisplayAnalysis.Visible = true; else tsbDisplayAnalysis.Visible = false;
            if (tssbSingleQuesAnswer) tsbSingleQuesAnswer.Visible = true; else tsbSingleQuesAnswer.Visible = false;
            if (tssbPaperScore) tsbPaperScore.Visible = true; else tsbPaperScore.Visible = false;
            if (tssbReturnBigTopic) tsbReturnBigTopic.Visible = true; else tsbReturnBigTopic.Visible = false;
            if (tssbReset) tsbReset.Visible = true; else tsbReset.Visible = false;
            if (tssbLessenTopicFace) tsbLessenTopicFace.Visible = true; else tsbLessenTopicFace.Visible = false;
            if (tssbLessenTopicFace2) tsbLessenTopicFace2.Visible = true; else tsbLessenTopicFace2.Visible = false;
            if (tssbExit) tsbExit.Visible = true; else tsbExit.Visible = false;
            if (tssbHandPaper) tsbHandPaper.Visible = true; else tsbHandPaper.Visible = false;

            #region 作业特殊情况
            if (PublicClass.JobType == JobType.ShiJuan)
            {
                if (PublicClass.oMyJob.ShowAnalysis == null || PublicClass.oMyJob.ShowAnalysis.ToLower() == "false")
                {
                    tsbDisplayAnalysis.Visible = false;
                }
                else
                {
                    tsbDisplayAnalysis.Visible = true;
                }

                if (PublicClass.oMyJob.IsSingleGrade == null || PublicClass.oMyJob.IsSingleGrade.ToLower() == "false")
                {
                    tsbSingleQuesAnswer.Visible = false;
                }
                else
                {
                    tsbSingleQuesAnswer.Visible = true;
                }
            }
            #endregion

            //离线模式隐藏交卷退出
            if (!Globals.IsOnline)
            {
                tsbHandPaper.Visible = false;
                tsbBackupJob.Visible = false;
                tsbExit.Visible = true;
            }
        }
        /// <summary>
        /// 设置下方工具栏，显示和隐藏
        /// </summary>
        /// <param name="tssbFirst">首题</param>
        /// <param name="tssbLastTopic">上一题</param>
        /// <param name="tssbNextTopic">下一题</param>
        /// <param name="tssbEndTopic">末题</param>
        /// <param name="tssbExamFolder">考生文件夹</param>
        /// <param name="tssbOpenApplication">打开应用程序</param>
        /// <param name="tssbSave">保存</param>
        /// <param name="tssbDaTi">答题</param>
        /// <param name="tssbResetTopic2">重做本题</param>
        /// <param name="tssbReturnBigTopic2">返回大题板</param>
        /// <param name="tssbResetUI">还原答题界面</param>
        private void ShowButtomTsbButton(bool tssbFirst, bool tssbLastTopic, bool tssbNextTopic, bool tssbEndTopic, bool tssbExamFolder, bool tssbOpenApplication, bool tssbSave, bool tssbDaTi, bool tssbResetTopic2, bool tssbReturnBigTopic2, bool tssbResetUI)
        {
            if (tssbFirst) tsbFirst.Visible = true; else tsbFirst.Visible = false;
            if (tssbLastTopic) tsbLastTopic.Visible = true; else tsbLastTopic.Visible = false;
            if (tssbNextTopic) tsbNextTopic.Visible = true; else tsbNextTopic.Visible = false;
            if (tssbEndTopic) tsbEndTopic.Visible = true; else tsbEndTopic.Visible = false;
            if (tssbExamFolder) tsbExamFolder.Visible = true; else tsbExamFolder.Visible = false;
            if (tssbOpenApplication) tsbOpenApplication.Visible = true; else tsbOpenApplication.Visible = false;
            if (tssbSave) tsbSave.Visible = true; else tsbSave.Visible = false;
            if (tssbDaTi) tsbDaTi.Visible = true; else tsbDaTi.Visible = false;
            if (tssbResetTopic2) tsbResetTopic2.Visible = true; else tsbResetTopic2.Visible = false;
            if (tssbReturnBigTopic2) tsbReturnBigTopic2.Visible = true; else tsbReturnBigTopic2.Visible = false;
            if (tssbResetUI) tsbResetUI.Visible = true; else tsbResetUI.Visible = false;
        }
        /// <summary>
        /// 界面显示和隐藏
        /// </summary>
        /// <param name="pnnlTitle">系统登陆信息</param>
        /// <param name="pnnlNavigation">试题导航信息</param>
        /// <param name="pnnlCompany">公司信息</param>
        /// <param name="pnnlTopTool">工具栏--上</param>
        /// <param name="pnnlTopicType">题型下试题数量</param>
        /// <param name="pnnlTopic">题面信息</param>
        /// <param name="pnnlOpation">试题选项</param>
        /// <param name="pnnlPageIndex">工具栏--下</param>
        private void ShowPanel(bool pnnlTitle, bool pnnlNavigation, bool pnnlCompany, bool pnnlTopTool, bool pnnlTopicType, bool pnnlTopic, bool pnnlOpation, bool pnnlPageIndex)
        {
            if (pnnlTitle) pnlTitle.Visible = true; else pnlTitle.Visible = false;
            if (pnnlNavigation) pnlNavigation.Visible = true; else pnlNavigation.Visible = false;
            if (pnnlCompany) pnlCompany.Visible = true; else pnlCompany.Visible = false;
            if (pnnlTopTool) pnlTopTool.Visible = true; else pnlTopTool.Visible = false;
            if (pnnlTopicType) pnlTopicType.Visible = true; else pnlTopicType.Visible = false;
            if (pnnlTopic) pnlTopic.Visible = true; else pnlTopic.Visible = false;
            if (pnnlOpation) pnlOpation.Visible = true; else pnlOpation.Visible = false;
            if (pnnlPageIndex) pnlPageIndex.Visible = true; else pnlPageIndex.Visible = false;
        }
        /// <summary>
        /// 保存用户答案
        /// </summary>
        public void SaveUserAnswer()
        {
            int iUseTime = 0;
            int iCurrTopicTypeId = 0;
            int iCurrTopicId = 0;
            int iCurrSubTopicId = 0;
            bool bTopicSaveOk = false;
            var sUserAnswer = string.Empty;
            var sDisplayValue = string.Empty;
            var sDiffString = string.Empty;
            var DisplayId = string.Empty;
            var isCaculateTime = string.Empty;
            Enum_TTopicRealType CurrTopicRealType;
            TreeNode CurrTopicNode;
            try
            {
                //安全检查
                CurrTopicRealType = publicClass.GetTopicRealType(oCurrTopic);
                if (CurrTopicRealType == Enum_TTopicRealType.trtUnknown) return;
                if (oCurrTopic.TopicId != "" && oCurrTopic.Changed == false) return;

                //正式模式下保存当前剩余时间
                isCaculateTime = string.IsNullOrEmpty(PublicClass.oMyJob.IsCaculateTime) == true ? "false" : PublicClass.oMyJob.IsCaculateTime.ToLower();
                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2" || isCaculateTime == "true") SaveResidualExamTime();

                #region 初始化用户答案
                //单选题
                if (CurrTopicRealType == Enum_TTopicRealType.trtDanXuan)
                {
                    if (danXuan.rdoA.Checked) sUserAnswer = "A";
                    if (danXuan.rdoB.Checked) sUserAnswer = "B";
                    if (danXuan.rdoC.Checked) sUserAnswer = "C";
                    if (danXuan.rdoD.Checked) sUserAnswer = "D";
                    if (danXuan.rdoE.Checked) sUserAnswer = "E";
                    if (danXuan.rdoF.Checked) sUserAnswer = "F";
                }

                //多选
                if (CurrTopicRealType == Enum_TTopicRealType.trtDuoXuan)
                {
                    if (oCurrTopic.AllowPeiWu)
                    {
                        sUserAnswer = GetPeiWuAnswer(peiWu.pnlPeiWu);
                    }
                    else
                    {
                        sUserAnswer = GetDuoXuanAnswer(duoXuan.pnlDuoXuanTi);
                    }
                }

                //判断
                if (CurrTopicRealType == Enum_TTopicRealType.trtPanDuan)
                {
                    if (panDuan.rdoRight.Checked) sUserAnswer = "true";
                    if (panDuan.rdoError.Checked) sUserAnswer = "false";
                }

                //填空
                if (CurrTopicRealType == Enum_TTopicRealType.trtTianKong)
                {
                    sUserAnswer = GetTianKongAnswer(tianKong.pnlContainer);
                }

                //主观题
                if (CurrTopicRealType == Enum_TTopicRealType.trtZhuGuan)
                {
                    if (zhuguan.txtZhuGuanTi.Text.Trim() != string.Empty)
                        sUserAnswer = zhuguan.txtZhuGuanTi.Text.Trim();
                }

                //案例分析
                if (CurrTopicRealType == Enum_TTopicRealType.trtAnLiFenXi)
                {
                    sUserAnswer = GetAnliFenXiAnswer(anLiFenXi.pnlAnLiFenXi);
                }

                //计算分析
                if (CurrTopicRealType == Enum_TTopicRealType.trtJiSuanFenXi)
                {
                    sUserAnswer = GetJiSuanFenXiAnswer(jiSuanFenXi.pnlContainer);
                }

                //打字题
                if (CurrTopicRealType == Enum_TTopicRealType.trtTyping)
                {
                    sUserAnswer = typing.txtTyping.Text.Trim();
                }

                //实操题
                if (publicClass.TopicIsCaoZuoTi(oCurrTopic))
                {
                    sUserAnswer = "已答";
                }
                #endregion

                //初始化变量
                iCurrTopicTypeId = int.Parse(oCurrTopic.TopicTypeId);
                iCurrTopicId = int.Parse(oCurrTopic.TopicId);
                iCurrSubTopicId = int.Parse(oCurrTopic.SubTopicId);
                iUseTime = 0;

                if (CurrTopicRealType == Enum_TTopicRealType.trtTyping)
                {
                    iUseTime = typing.iResidualTypingTime;
                }

                //保存用户答案到考生试卷中
                bTopicSaveOk = PublicClass.SowerExamPlugn.SetSaveAnswer(PublicClass.StudentDir, iCurrTopicTypeId, iCurrTopicId, iCurrSubTopicId, sUserAnswer, iUseTime, 0);

                //保存成功后更新数据
                if (bTopicSaveOk)
                {
                    oCurrTopic.HaveUserAnswer = true;
                    oCurrTopic.UserAnswer = sUserAnswer;
                    oCurrTopic.Changed = false;
                    UpdateTopicObject(oCurrTopic.TopicTypeId, oCurrTopic.TopicId, oCurrTopic.SubTopicId, oCurrTopic);
                }

                #region 更新试题导航树的节点显示
                CurrTopicNode = GetCurrTopicNode();
                if (CurrTopicNode != null)
                {
                    sDisplayValue = "（已答）";

                    if (CurrTopicRealType == Enum_TTopicRealType.trtPanDuan)
                    {
                        sDisplayValue = sUserAnswer == "true" ? "(√)" : "(×)";
                    }

                    if (CurrTopicRealType == Enum_TTopicRealType.trtDanXuan)
                    {
                        sDisplayValue = string.Format("（{0}）", sUserAnswer);
                    }

                    if (CurrTopicRealType == Enum_TTopicRealType.trtDuoXuan)
                    {
                        if (oCurrTopic.AllowPeiWu)
                        {
                            if (sUserAnswer == "Δ" || sUserAnswer == "")
                            {
                                sDisplayValue = "（未答）";
                            }
                            else
                            {
                                sDisplayValue = string.Format("（{0}）", string.Join("", sUserAnswer.Split(' ').ToArray()));
                            }
                        }
                        else
                        {
                            sDisplayValue = string.Format("（{0}）", sUserAnswer);
                            if (sUserAnswer == "Δ" || sUserAnswer == "")
                            {
                                sDisplayValue = "（未答）";
                            }

                            sDisplayValue = string.Format("（{0}）", sUserAnswer);
                        }
                    }

                    if (publicClass.TopicIsCaoZuoTi(oCurrTopic))
                    {
                        sDisplayValue = string.Format("（{0}）", sUserAnswer);

                        if (sUserAnswer == "Δ" || sUserAnswer == "")
                        {
                            sDisplayValue = "（已答）";
                        }
                    }

                    sDiffString = oCurrTopic.Difficult == true ? "（存疑）" : "";
                    DisplayId = oCurrTopic.DisplayId < 10 ? string.Format("0{0}", oCurrTopic.DisplayId) : string.Format("{0}", oCurrTopic.DisplayId);
                    CurrTopicNode.Text = string.Format("第{0}题{1}{2}", DisplayId, sDisplayValue, sDiffString);
                }
                #endregion

                if (publicClass.TopicIsKeGuanTi(oCurrTopic))
                {
                    pnlTopicFace.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("{0}目前无法保存考生答案。", ex.Message);
                throw;
            }
        }
        /// <summary>
        /// 保存考试时间
        /// </summary>
        private void SaveResidualExamTime()
        {
            #region 题库
            if (PublicClass.JobType == JobType.TiKu)
            {
                if (PublicClass.oPaperInfo.ExamInfo.ExamMode != "2")
                {
                    return;
                }
            }
            #endregion

            #region 作业
            if (PublicClass.JobType == JobType.ShiJuan)
            {
                if (PublicClass.oMyJob.IsCaculateTime.ToLower() != "true")
                {
                    return;
                }
            }
            #endregion

            PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考试剩余时间", iResidualExamTime.ToString());
        }
        /// <summary>
        /// 设置导航文字
        /// </summary>
        /// <param name="answer"></param>
        public void SetTreeViewText(string answer)
        {
            #region old
            for (int i = 0; i < tvMenu.Nodes.Count; i++)
            {
                string parentId = tvMenu.Nodes[i].Tag.ToString();

                if (oCurrTopic.TopicTypeId != parentId)
                    continue;

                for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                {
                    string childNo = tvMenu.Nodes[i].Nodes[j].Tag.ToString();
                    if (oCurrTopic.TopicId == childNo)
                    {
                        string nodeText = tvMenu.Nodes[i].Nodes[j].Text;
                        if (int.Parse(oCurrTopic.TopicNo) < 10)
                        {
                            if (nodeText.IndexOf("（存疑）") > 0)
                            {
                                tvMenu.Nodes[i].Nodes[j].Text =
                                    string.Format("第0{0}题（{1}）（存疑）", oCurrTopic.TopicNo, answer);
                            }
                            else
                            {
                                tvMenu.Nodes[i].Nodes[j].Text =
                                    string.Format("第0{0}题（{1}）", oCurrTopic.TopicNo, answer);
                            }
                        }
                        else
                        {
                            if (nodeText.IndexOf("（存疑）") > 0)
                            {
                                tvMenu.Nodes[i].Nodes[j].Text =
                                    string.Format("第{0}题（{1}）（存疑）", oCurrTopic.TopicNo, answer);
                            }
                            else
                            {
                                tvMenu.Nodes[i].Nodes[j].Text =
                                    string.Format("第{0}题（{1}）", oCurrTopic.TopicNo, answer);
                            }
                        }
                    }
                    else
                    {
                        tvMenu.Nodes[i].Nodes[j].ForeColor = Color.White;
                    }
                }
            }
            #endregion

            //TreeNode CurrTopicNode;
            //string sUserAnswer, sDiffString, DisplayId;

            //CurrTopicNode = GetCurrTopicNode();

            ////更新导航栏中的试题节点信息
            //sUserAnswer = GetUserAnswerDisplayValue(oCurrTopic);
            //sDiffString = oCurrTopic.Difficult == true ? "(存疑)" : "";
            //DisplayId = oCurrTopic.DisplayId < 10 ? string.Format("0{0}", oCurrTopic.DisplayId) : string.Format("{0}", oCurrTopic.DisplayId);

            //CurrTopicNode.Text = string.Format("第{0}题 {1} {2}", DisplayId, sUserAnswer, sDiffString);
        }
        /// <summary>
        /// 设置窗体位置
        /// </summary>
        private void SetFormLocation()
        {
            try
            {
                int height = System.Windows.Forms.SystemInformation.WorkingArea.Height;
                int width = System.Windows.Forms.SystemInformation.WorkingArea.Width;

                int formheight = this.Size.Height;
                int formwidth = this.Size.Width;

                int newformx = width / 2 - formwidth / 2;
                int newformy = 0;

                this.SetDesktopLocation(newformx, newformy);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmAnswerSheet), ex);
                CommonUtil.WriteLog(ex);
            };
        }
        /// <summary>
        /// 加载导航
        /// </summary>
        /// <returns></returns>
        public void LoadTreeView()
        {
            var subTopicCount = 0;
            var sUserAnswer = string.Empty;
            var sDiffString = string.Empty;
            var DisplayId = string.Empty;
            var DisplayText = string.Empty;
            var topicTypes = PublicClass.oPaperInfo.TopicTypes;
            foreach (M_TopicType topicType in topicTypes)
            {
                TreeNode parentNode = new TreeNode();
                parentNode.Text = topicType.Name;
                parentNode.Tag = topicType.Id;
                tvMenu.Nodes.Add(parentNode);

                if (topicType.AllowSubTopic)
                {
                    var topicidList = topicType.Topics.GroupBy(f => f.TopicId).Select(f => f.Key).ToList();
                    for (int i = 0; i < topicidList.Count; i++)
                    {
                        DisplayId = i + 1 < 10 ? string.Format("0{0}", i + 1) : string.Format("{0}", i + 1);
                        DisplayText = string.Format("第{0}题", DisplayId);
                        TreeNode childNode = new TreeNode();
                        childNode.Text = DisplayText;
                        childNode.Tag = topicidList[i];
                        parentNode.Nodes.Add(childNode);

                        var topicList = topicType.Topics.Where(f => f.TopicId == topicidList[i]).ToList();
                        subTopicCount = 0;
                        foreach (var subTopic in topicList[0].SubTopics)
                        {
                            subTopicCount++;
                            //更新导航栏中的试题节点信息
                            var topicinfo = topicList.Where(f => f.TopicId == subTopic.TopicID &&
                                                                 f.SubTopicId == subTopic.TopicFaceID)
                                                     .FirstOrDefault();
                            sUserAnswer = GetUserAnswerDisplayValue(topicinfo);
                            sDiffString = topicinfo.Difficult == true ? "（存疑）" : "";
                            DisplayId = subTopicCount < 10 ? string.Format("0{0}", subTopicCount) : subTopicCount.ToString();
                            DisplayText = string.Format("第{0}题{1}{2}", DisplayId, sUserAnswer, sDiffString);

                            TreeNode subTopicNode = new TreeNode();
                            subTopicNode.Text = DisplayText;
                            subTopicNode.Tag = topicinfo.TopicNo;
                            childNode.Nodes.Add(subTopicNode);
                        }
                    }
                }
                else
                {
                    foreach (M_Topic topic in topicType.Topics)
                    {
                        //更新导航栏中的试题节点信息
                        sUserAnswer = GetUserAnswerDisplayValue(topic);
                        sDiffString = topic.Difficult == true ? "（存疑）" : "";
                        DisplayId = topic.DisplayId < 10 ? string.Format("0{0}", topic.DisplayId) : topic.DisplayId.ToString();
                        DisplayText = string.Format("第{0}题{1}{2}", DisplayId, sUserAnswer, sDiffString);

                        TreeNode childNode = new TreeNode();
                        childNode.Text = DisplayText;
                        childNode.Tag = topic.TopicId;
                        parentNode.Nodes.Add(childNode);
                    }
                }
            }
            if (topicTypes[0].AllowSubTopic)
            {
                tvMenu.Nodes[0].Nodes[0].Nodes[0].ForeColor = Color.Yellow;
                tvMenu.Nodes[0].Expand();
                tvMenu.Nodes[0].Nodes[0].Expand();
            }
            else
            {
                tvMenu.Nodes[0].Nodes[0].ForeColor = Color.Yellow;
                tvMenu.Nodes[0].Expand();
            }
        }
        /// <summary>
        /// 加载题面
        /// </summary>
        /// <param name="AView"></param>
        /// <param name="DocText"></param>
        public void LoadSowerText(AxSowerRichTextBox AView, string DocText)
        {
            AView.Clear();

            if (oCurrTopic.TopicFace.Length > 0)
            {
                AView.DocType = PublicClass.SowerExamPlugn.GetTopicDocType(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), 0);
                AView.SowerText = DocText;
            }
        }
        /// <summary>
        /// 加载考试信息
        /// </summary>
        /// <returns></returns>
        public void LoadExamineeInfo()
        {
            string sHint = "";
            string sErrorInfo = "";

            //获取试卷信息
            PublicClass.oPaperInfo = publicClass.GetStudentPaperInfo(PublicClass.StudentDir, ref sErrorInfo);

            if (PublicClass.oPaperInfo.Inited == false)
            {
                sHint = string.Format("无法获取试卷信息，原因：{0}，按【确定】退出系统。", sErrorInfo);
                return;
            }

            //检测考生试卷完整性
            if (PublicClass.SowerExamPlugn.ValidateExamPaper(PublicClass.StudentDir) == false)
            {
                MessageBox.Show("当前试卷信息不完整，按【确定】返回后，请重新登录。");
                Environment.Exit(0);
            }

            //检测考生是否合法
            if (PublicClass.SowerExamPlugn.ValidateExamPaper(PublicClass.StudentDir) == false)
            {
                MessageBox.Show("考生与考卷不匹配，试卷或被外部修改，按【确定】退出系统。");
                Environment.Exit(0);
            }

            //判断试卷是否绑定考生
            if (PublicClass.oPaperInfo.ExamInfo.PaperName == "" || PublicClass.oPaperInfo.ExamInfo.TopicDBCode == "" || PublicClass.oPaperInfo.ExamInfo.ExamNumber == "")
            {
                MessageBox.Show("当前试卷信息不完整，尚未指定试卷归属，按【确定】返回后，请重新登录。");
                Environment.Exit(0);
            }

            PublicClass.oPaperInfo.ExamInfo.ExamMode = PublicClass.oSubjectProp.ExamMode;
            PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考生状态", "正在考试");
        }
        /// <summary>
        /// 打开应用程序
        /// </summary>
        /// <param name="judgeEngine">评分引擎</param>
        private void OpenApplication(int judgeEngine)
        {
            PublicClass.SowerExamPlugn.OpenApp(PublicClass.StudentDir, PublicClass.ExamSysDir, oCurrTopic.AppPath, judgeEngine, "", "");
        }

        /// <summary>
        /// 改变字体大小
        /// </summary>
        /// <param name="FontSizeOffset"></param>
        private void ChangeFontSize(int FontSizeOffset)
        {
            int iNewFontSize;

            if (FontSizeOffset >= 10)
            {
                FontSizeOffset = 10;
            }

            iNewFontSize = FFontSize + FontSizeOffset;
            tsbAddFontSize.Enabled = !(iNewFontSize >= 48);
            tsbReduceFontSize.Enabled = !(iNewFontSize <= 9);
            FFontSize = iNewFontSize;

            txtTopicFace.FontSize = FFontSize;
            txtAnalysis.FontSize = FFontSize;
            txtSubTopicTop.FontSize = FFontSize;
            txtSubTopicBottom.FontSize = FFontSize;
            txtSubAnalysis.FontSize = FFontSize;
            txtTyping.Font = new Font("宋体", FFontSize);
            if (typing != null) typing.txtTyping.Font = new Font("宋体", FFontSize);
        }
        /// <summary>
        /// 获取当前节点答案显示
        /// </summary>
        /// <param name="ATopic"></param>
        /// <returns></returns>
        private string GetUserAnswerDisplayValue(M_Topic ATopic)
        {
            string sDisplayValue, sUserAnswer;
            Enum_TTopicRealType CurrTopicRealType;

            sDisplayValue = "（未答）";
            if (ATopic.HaveUserAnswer)
            {
                CurrTopicRealType = publicClass.GetTopicRealType(ATopic);
                sUserAnswer = ATopic.UserAnswer;

                if (CurrTopicRealType == Enum_TTopicRealType.trtAnLiFenXi)
                {
                    sDisplayValue = "（已答）";
                }

                if (CurrTopicRealType == Enum_TTopicRealType.trtJiSuanFenXi)
                {
                    sDisplayValue = "（已答）";
                }

                if (CurrTopicRealType == Enum_TTopicRealType.trtZhuGuan)
                {
                    sDisplayValue = "（已答）";
                }

                if (CurrTopicRealType == Enum_TTopicRealType.trtTianKong)
                {
                    sDisplayValue = "（已答）";
                }

                if (publicClass.TopicIsCaoZuoTi(ATopic))
                {
                    sDisplayValue = "（已答）";
                }

                if (CurrTopicRealType == Enum_TTopicRealType.trtPanDuan)
                {
                    sDisplayValue = sUserAnswer == "true" ? "(√)" : "(×)";
                }

                if (CurrTopicRealType == Enum_TTopicRealType.trtDanXuan)
                {
                    sDisplayValue = string.Format("（{0}）", sUserAnswer);
                }

                if (CurrTopicRealType == Enum_TTopicRealType.trtDuoXuan)
                {
                    if (ATopic.AllowPeiWu)
                    {
                        if (sUserAnswer == "Δ" || sUserAnswer == "")
                        {
                            sDisplayValue = "（未答）";
                        }
                        else
                        {
                            sDisplayValue = string.Format("（{0}）", string.Join("", sUserAnswer.Split(' ').ToArray()));
                        }
                    }
                    else
                    {
                        sDisplayValue = string.Format("（{0}）", sUserAnswer);
                        if (sUserAnswer == "Δ" || sUserAnswer == "")
                        {
                            sDisplayValue = "（未答）";
                        }
                    }
                }
            }

            return sDisplayValue;
        }
        /// <summary>
        /// 获取当前节点
        /// </summary>
        /// <returns></returns>
        private TreeNode GetCurrTopicNode()
        {
            var childNo = string.Empty;
            var subChildNo = string.Empty;
            var CurrTopicNode = new TreeNode();
            if (oCurrTopic.AllowSubTopic)
            {
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    string parentId = tvMenu.Nodes[i].Tag.ToString();
                    if (oCurrTopic.TopicTypeId != parentId) continue;
                    for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                    {
                        childNo = tvMenu.Nodes[i].Nodes[j].Tag.ToString();
                        for (int k = 0; k < tvMenu.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            subChildNo = tvMenu.Nodes[i].Nodes[j].Nodes[k].Tag.ToString();
                            if (oCurrTopic.TopicId == childNo && oCurrTopic.SubTopicId == subChildNo)
                            {
                                CurrTopicNode = tvMenu.Nodes[i].Nodes[j].Nodes[k];
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    string parentId = tvMenu.Nodes[i].Tag.ToString();
                    if (oCurrTopic.TopicTypeId != parentId) continue;
                    for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                    {
                        childNo = tvMenu.Nodes[i].Nodes[j].Tag.ToString();
                        if (oCurrTopic.TopicId == childNo)
                        {
                            CurrTopicNode = tvMenu.Nodes[i].Nodes[j];
                        }
                    }
                }
            }
            return CurrTopicNode;
        }
        /// <summary>
        /// 试卷评分
        /// </summary>
        /// <returns></returns>
        private string PaperScore()
        {
            #region 变量声明
            M_Topic aTopic;
            int iTopicTypeId;
            int iTopicId;
            string iSubTopicId;
            int iQuestionNumber;
            int PaperTopicCount = 0;
            int TopicIndex = 0;
            double fTopicScore, fTopicTypeScore, fPaperScore, fTopicMark, topicTypePoint, topicTypeScore;
            string iTopicNo = "";
            string sHint = "";
            string sUserAnswer = "";
            string sStandardAnswer = "";
            string sErrorHint = "";
            string sJudgeLevel = "";
            string sGradeResult = "";
            string sErrorInfo = "";
            string XiaHuaXian = "\n -------------------------------------------------------------------------------------------------- \n";
            string updateScoreResult = string.Empty;
            string updateScoreMessage = string.Empty;
            string paperScoreDetail = string.Empty;
            string topicScoreDetail = string.Empty;
            var backdir = string.Empty;
            var dataDir = string.Empty;
            var examDir = string.Empty;
            var studentDir = string.Empty;
            List<string> slUserAnswer = new List<string>();
            List<string> slStandardAnswer = new List<string>();
            List<string> slErrorHint = new List<string>();

            frmScoreDetail scoreDetail = new frmScoreDetail();
            frmHandPager handPaper = null;

            StringBuilder sb = new StringBuilder();
            #endregion
            try
            {
                #region 显示评阅提示
                handPaper = new frmHandPager();
                handPaper.lblTitle.Text = "处理考试数据中，请稍后...";
                handPaper.lblTitle.Refresh();
                handPaper.lblHint.Text = "准备对当前考生选择题进行评分，请稍候...";
                handPaper.lblHint.Refresh();
                handPaper.TopMost = true;
                handPaper.Show();
                Application.DoEvents();
                #endregion

                #region 获取试卷信息
                PublicClass.oPaperInfo = publicClass.GetStudentPaperInfo(PublicClass.StudentDir, ref sErrorInfo);
                if (PublicClass.oPaperInfo.Inited == false)
                {
                    sHint = string.Format("无法获取试卷信息，原因：“{0}”，按【确定】退出系统。", sErrorInfo);
                    MessageBox.Show(sHint);
                    Environment.Exit(0);
                }
                #endregion

                #region 进行整卷评分
                fPaperScore = 0;
                for (int i = 0; i < PublicClass.oPaperInfo.TopicTypes.Count; i++)
                {
                    fTopicTypeScore = 0;

                    for (int j = 0; j < PublicClass.oPaperInfo.TopicTypes[i].Topics.Count; j++)
                    {
                        if (PublicClass.oPaperInfo.TopicTypes[i].JudgeEngineId !=
                            PublicClass.oPaperInfo.TopicTypes[i].Topics[j].JudgeEngineId)
                        {
                            continue;
                        }

                        sHint = string.Format("正在对 {0} 中的第 {1} 题进行分析...", PublicClass.oPaperInfo.TopicTypes[i].Name, j + 1);
                        handPaper.lblHint.Text = sHint;
                        handPaper.lblHint.Refresh();

                        iTopicTypeId = int.Parse(PublicClass.oPaperInfo.TopicTypes[i].Topics[j].TopicTypeId);
                        iTopicId = int.Parse(PublicClass.oPaperInfo.TopicTypes[i].Topics[j].TopicId);
                        iSubTopicId = PublicClass.oPaperInfo.TopicTypes[i].Topics[j].SubTopicId;
                        iTopicNo = PublicClass.oPaperInfo.TopicTypes[i].Topics[j].TopicNo;
                        sGradeResult = PublicClass.SowerExamPlugn.ExamGrade(PublicClass.StudentDir, PublicClass.ExamSysDir, PublicClass.StudentCode, iTopicTypeId, iTopicId, iSubTopicId);
                        fTopicScore = Math.Round(double.Parse(xml.GetXmlNodeValue(sGradeResult, "得分")), 2);
                        fTopicMark = PublicClass.oPaperInfo.TopicTypes[i].Topics[j].Mark;
                        sStandardAnswer = xml.GetXmlNodeValue(sGradeResult, "标准答案");
                        sErrorHint = xml.GetXmlNodeValue(sGradeResult, "错误报告");
                        sJudgeLevel = xml.GetXmlNodeValue(sGradeResult, "得分评价");
                        PublicClass.oPaperInfo.TopicTypes[i].Topics[j].Score = fTopicScore;
                        PublicClass.oPaperInfo.TopicTypes[i].Topics[j].ErrorHint = sErrorHint;
                        PublicClass.oPaperInfo.TopicTypes[i].Topics[j].JudgeLevel = sJudgeLevel;
                        PublicClass.oPaperInfo.TopicTypes[i].Topics[j].StandardAnswer = sStandardAnswer;
                        PublicClass.oPaperInfo.TopicTypes[i].Topics[j].TopicRightLevel = publicClass.GetTopicRightLevel(fTopicMark, fTopicScore);
                        fTopicTypeScore = fTopicTypeScore + fTopicScore;
                    }
                    PublicClass.oPaperInfo.TopicTypes[i].Score = fTopicTypeScore;
                    fPaperScore = fPaperScore + fTopicTypeScore;
                    PaperTopicCount += PublicClass.oPaperInfo.TopicTypes[i].Topics.Count;
                }
                PublicClass.oPaperInfo.PaperScore = fPaperScore;
                #endregion

                #region 构建得分明细内容
                scoreDetail.txtScore.Clear();

                if (PublicClass.JobType == JobType.TiKu)
                {
                    scoreDetail.lblSubject.Text = string.Format("考试科目：{0}  考生姓名：{1}  考试得分：{2}",
                        PublicClass.oSubjectProp.SubjectName,
                        PublicClass.oSubjectProp.StudentName,
                        PublicClass.oPaperInfo.PaperScore);
                }

                if (PublicClass.JobType == JobType.ShiJuan)
                {
                    scoreDetail.lblSubject.Text = string.Format("作业名称：{0}  考生姓名：{1}  作业成绩：{2}",
                     PublicClass.oMyJob.HWName,
                     PublicClass.ExamineeName,
                     PublicClass.oPaperInfo.PaperScore);
                }
                #endregion

                #region 题库得分明细
                if (PublicClass.JobType == JobType.TiKu)
                {
                    #region PaperScore
                    //sb.AppendFormat("<?xml version='1.0' encoding='utf-8' ?>");
                    sb.AppendFormat("<PaperScore>");
                    sb.AppendFormat("<HWInfo>");
                    sb.AppendFormat("<TopicDBCode>{0}</TopicDBCode>", PublicClass.oSubjectProp.TopicDBCode);
                    sb.AppendFormat("<TopicDBVersion>{0}</TopicDBVersion>", PublicClass.oSubjectProp.TopicDBVersion);
                    sb.AppendFormat("<PaperId>{0}</PaperId>", PublicClass.oSubjectProp.PresetPaperID);
                    sb.AppendFormat("<PaperName>{0}</PaperName>", GetPaperName());
                    sb.AppendFormat("<StudentCode>{0}</StudentCode>", PublicClass.StudentCode);
                    sb.AppendFormat("<PaperTopicCount>{0}</PaperTopicCount>", PaperTopicCount);
                    sb.AppendFormat("<TotalPoint>{0}</TotalPoint>", PublicClass.oPaperInfo.PaperMark);
                    sb.AppendFormat("<TotalScore>{0}</TotalScore>", PublicClass.oPaperInfo.PaperScore);
                    sb.AppendFormat("<TotalUserTime>{0}</TotalUserTime>", totalUserTime);
                    sb.AppendFormat("</HWInfo>");
                    #endregion
                }
                #endregion

                #region 作业得分明细
                if (PublicClass.JobType == JobType.ShiJuan)
                {
                    #region PaperScore
                    //sb.AppendFormat("<?xml version='1.0' encoding='utf-8' ?>");
                    sb.AppendFormat("<PaperScore>");
                    sb.AppendFormat("<HWInfo>");
                    sb.AppendFormat("<HWID>{0}</HWID>", PublicClass.oMyJob.ID);
                    sb.AppendFormat("<StudentCode>{0}</StudentCode>", PublicClass.StudentCode);
                    sb.AppendFormat("<PaperTopicCount>{0}</PaperTopicCount>", PaperTopicCount);
                    sb.AppendFormat("<TotalPoint>{0}</TotalPoint>", PublicClass.oPaperInfo.PaperMark);
                    sb.AppendFormat("<TotalScore>{0}</TotalScore>", PublicClass.oPaperInfo.PaperScore);
                    sb.AppendFormat("<TotalUserTime>{0}</TotalUserTime>", totalUserTime);
                    sb.AppendFormat("</HWInfo>");
                    #endregion
                }
                #endregion

                #region 构建得分明细
                for (int i = 0; i < PublicClass.oPaperInfo.TopicTypes.Count; i++)
                {
                    if (i > 0)
                    {
                        scoreDetail.txtScore.SelectedText = "";
                    }

                    if (PublicClass.oPaperInfo.TopicTypes[i].JudgeEngineId == JudgeEngine.ZhuGuan)
                    {
                        continue;
                    }

                    sHint = string.Format("【{0}】 题型分值：{1}， 得分小计：{2}", PublicClass.oPaperInfo.TopicTypes[i].Name, PublicClass.oPaperInfo.TopicTypes[i].Mark, PublicClass.oPaperInfo.TopicTypes[i].Score);
                    scoreDetail.txtScore.SelectionColor = Color.CornflowerBlue;
                    scoreDetail.txtScore.SelectionFont = new Font("宋体", 10F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(134)));
                    scoreDetail.txtScore.SelectedText = sHint;
                    scoreDetail.txtScore.SelectedText = "\n ------------------------------------------------------------------------- \n";

                    #region PaperTopicType
                    sb.AppendFormat("<PaperTopicType>");
                    sb.AppendFormat("<TopicType>");
                    sb.AppendFormat("<TopicTypeID>{0}</TopicTypeID>", PublicClass.oPaperInfo.TopicTypes[i].Id);
                    sb.AppendFormat("<TopicTypeName>{0}</TopicTypeName>", PublicClass.oPaperInfo.TopicTypes[i].Name);
                    sb.AppendFormat("<JudgeEngineId>{0}</JudgeEngineId>", PublicClass.oPaperInfo.TopicTypes[i].JudgeEngineId);
                    sb.AppendFormat("<TopicTypePoint>{0}</TopicTypePoint>", PublicClass.oPaperInfo.TopicTypes[i].Mark);
                    sb.AppendFormat("<TopicTypeScore>{0}</TopicTypeScore>", PublicClass.oPaperInfo.TopicTypes[i].Score);
                    sb.AppendFormat("</TopicType>");
                    #endregion

                    for (int j = 0; j < PublicClass.oPaperInfo.TopicTypes[i].Topics.Count; j++)
                    {
                        if (PublicClass.oPaperInfo.TopicTypes[i].JudgeEngineId !=
                            PublicClass.oPaperInfo.TopicTypes[i].Topics[j].JudgeEngineId)
                        {
                            continue;
                        }

                        aTopic = PublicClass.oPaperInfo.TopicTypes[i].Topics[j];
                        TopicIndex++;

                        #region 客观题（单选、多选、判断）
                        if (publicClass.TopicIsKeGuanTi(aTopic))
                        {
                            sUserAnswer = aTopic.UserAnswer;
                            sStandardAnswer = aTopic.StandardAnswer;

                            if (sUserAnswer == "Δ" || sUserAnswer == "") { sUserAnswer = "(未答)"; }
                            if (sUserAnswer == "true") sUserAnswer = "正确";
                            if (sUserAnswer == "false") sUserAnswer = "错误";
                            if (sStandardAnswer.ToLower() == "true") sStandardAnswer = "正确";
                            if (sStandardAnswer.ToLower() == "false") sStandardAnswer = "错误";
                            if (sUserAnswer != "Δ" || sUserAnswer != "" && aTopic.AllowPeiWu == true)
                            {
                                sUserAnswer = string.Join("", sUserAnswer.Split(' ').ToArray());
                            }

                            sHint = string.Format(" 第{0}题： {1}， 用户答案：{2}， 标准答案：{3}， 本题分值：{4}， 考生得分：{5}",
                                j + 1,
                                aTopic.ErrorHint,
                                sUserAnswer,
                                sStandardAnswer,
                                aTopic.Mark,
                                aTopic.Score);

                            scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                            scoreDetail.txtScore.SelectedText = sHint;
                        }
                        #endregion

                        #region 复合客观题（案例分析、计算分析）
                        if (publicClass.TopicIsComplexKeGuanTi(aTopic))
                        {
                            try
                            {
                                slUserAnswer = aTopic.UserAnswer.Split('♂').ToList();
                                slStandardAnswer = aTopic.StandardAnswer.Split('♂').ToList();
                                iQuestionNumber = slStandardAnswer.Count;

                                if (aTopic.UserAnswer == "Δ" || aTopic.UserAnswer == "")
                                {
                                    slUserAnswer.Clear();
                                    for (int k = 0; k < iQuestionNumber; k++)
                                    {
                                        slUserAnswer.Add("未答");
                                    }
                                }

                                if (iQuestionNumber == slUserAnswer.Count)
                                {
                                    sHint = string.Format(" 第{0}题： {1}，本题分值：{2}， 考生得分：{3}",
                                        j + 1,
                                        aTopic.JudgeLevel,
                                        aTopic.Mark,
                                        aTopic.Score);

                                    scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                                    scoreDetail.txtScore.SelectedText = sHint;

                                    #region 案例分析题
                                    if (publicClass.GetTopicRealType(aTopic) == Enum_TTopicRealType.trtAnLiFenXi)
                                    {
                                        for (int k = 0; k < iQuestionNumber; k++)
                                        {
                                            sUserAnswer = slUserAnswer[k].ToString();

                                            if (sUserAnswer == "Δ" || sUserAnswer == "")
                                            {
                                                sUserAnswer = "(未答)";
                                            }

                                            sHint = string.Format("\n{0}[第{1}空]：用户答案：{2}，标准答案：{3}",
                                                " 　　　　",
                                                k + 1,
                                                sUserAnswer,
                                                slStandardAnswer[k].ToString());

                                            scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                                            scoreDetail.txtScore.SelectedText = sHint;
                                        }
                                    }
                                    #endregion

                                    #region 计算分析题
                                    if (publicClass.GetTopicRealType(aTopic) == Enum_TTopicRealType.trtJiSuanFenXi)
                                    {
                                        for (int k = 0; k < iQuestionNumber; k++)
                                        {
                                            sHint = string.Format("\n{0}[第{1}空]：", "     ", k + 1);

                                            //拼接用户答案
                                            sHint = sHint + "用户答案：";
                                            List<string> UserAnswer = slUserAnswer[k].Split(';').ToList();
                                            for (int m = 0; m < UserAnswer.Count; m++)
                                            {
                                                if (m > 0)
                                                {
                                                    sHint = sHint + "\n" + "                        ";
                                                }
                                                string answers = UserAnswer[m] == "" ? "(未答)" : UserAnswer[m];
                                                sHint = sHint + answers;
                                            }

                                            //拼接标准答案
                                            sHint = sHint + "\n" + "              " + "标准答案：";
                                            List<string> StandardAnswer = slStandardAnswer[k].Split(';').ToList();
                                            for (int m = 0; m < StandardAnswer.Count; m++)
                                            {
                                                if (m > 0)
                                                {
                                                    sHint = sHint + "\n" + "                       ";
                                                }
                                                sHint = sHint + StandardAnswer[m];
                                            }

                                            scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                                            scoreDetail.txtScore.SelectedText = sHint;
                                        }
                                    }
                                    #endregion

                                    #region 填空题
                                    if (publicClass.GetTopicRealType(aTopic) == Enum_TTopicRealType.trtTianKong)
                                    {
                                        for (int k = 0; k < iQuestionNumber; k++)
                                        {
                                            sHint = string.Format("\n{0}[第{1}空]：", "     ", k + 1);

                                            //拼接用户答案
                                            sHint = sHint + "用户答案：";
                                            List<string> UserAnswer = slUserAnswer[k].Split(';').ToList();
                                            for (int m = 0; m < UserAnswer.Count; m++)
                                            {
                                                if (m > 0)
                                                {
                                                    sHint = sHint + "\n" + "                        ";
                                                }
                                                string answers = UserAnswer[m] == "" ? "(未答)" : UserAnswer[m];
                                                sHint = sHint + answers;
                                            }

                                            //拼接标准答案
                                            sHint = sHint + "\n" + "              " + "标准答案：";
                                            List<string> StandardAnswer = slStandardAnswer[k].Split(';').ToList();
                                            for (int m = 0; m < StandardAnswer.Count; m++)
                                            {
                                                if (m > 0)
                                                {
                                                    sHint = sHint + "\n" + "                       ";
                                                }
                                                sHint = sHint + StandardAnswer[m];
                                            }

                                            scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                                            scoreDetail.txtScore.SelectedText = sHint;
                                        }
                                    }
                                    #endregion
                                }
                            }
                            catch (Exception)
                            {

                                throw;
                            }
                        }
                        #endregion

                        #region 打字题
                        if (publicClass.TopicIsTyping(aTopic))
                        {
                            sHint = string.Format(" 第{0}题： 本题分值：{1}， 考生得分：{2}",
                                j + 1,
                                aTopic.Mark,
                                aTopic.Score);

                            sHint = sHint + "\n" + "         " + "评分信息：";
                            List<string> ErrorHint = aTopic.ErrorHint.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).ToList();

                            for (int k = 0; k < ErrorHint.Count; k++)
                            {
                                if (k > 0)
                                {
                                    sHint = sHint + "\n" + "                   ";
                                }
                                sHint = sHint + ErrorHint[k];
                            }

                            scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                            scoreDetail.txtScore.SelectedText = sHint;
                        }
                        #endregion

                        #region 实操题
                        if (publicClass.TopicIsCaoZuoTi(aTopic))
                        {
                            sHint = string.Format(" 第{0}题： 本题分值：{1}， 考生得分：{2}",
                                j + 1,
                                aTopic.Mark,
                                aTopic.Score);


                            sHint = sHint + "\n" + "         " + "评分信息：";
                            List<string> ErrorHint = aTopic.ErrorHint.Split('$').ToList();

                            for (int k = 0; k < ErrorHint.Count; k++)
                            {
                                if (k > 0)
                                {
                                    sHint = sHint + "\n" + "                   ";
                                }
                                sHint = sHint + ErrorHint[k];
                            }

                            scoreDetail.txtScore.SelectionColor = Color.DarkRed;
                            scoreDetail.txtScore.SelectedText = sHint;
                        }
                        #endregion

                        #region 填空题

                        #endregion

                        //试题间增加分隔虚线
                        scoreDetail.txtScore.SelectionColor = Color.DarkGray;
                        scoreDetail.txtScore.SelectedText = XiaHuaXian;

                        #region Topic
                        topicTypePoint = PublicClass.oPaperInfo.TopicTypes[i].Mark;
                        topicTypeScore = PublicClass.oPaperInfo.TopicTypes[i].Score;
                        topicScoreDetail = GetTopicScoreDetail(aTopic, TopicIndex, topicTypePoint, topicTypeScore);

                        sb.AppendFormat("<Topic>");
                        sb.AppendFormat("<TopicID>{0}</TopicID>", aTopic.TopicId);
                        sb.AppendFormat("<GUID>{0}</GUID>", aTopic.GUID);
                        sb.AppendFormat("<TopicCode>{0}</TopicCode>", "");
                        sb.AppendFormat("<TopicIndex>{0}</TopicIndex>", TopicIndex);
                        sb.AppendFormat("<TopicNo>{0}</TopicNo>", aTopic.TopicNo);
                        sb.AppendFormat("<ErrorHint>{0}</ErrorHint>", aTopic.ErrorHint);
                        sb.AppendFormat("<BasicTypeId>{0}</BasicTypeId>", aTopic.BasicTypeId);
                        sb.AppendFormat("<JudgeLevel>{0}</JudgeLevel>", aTopic.JudgeLevel);
                        sb.AppendFormat("<OrdFormatName>{0}</OrdFormatName>", "");
                        sb.AppendFormat("<OrdName>{0}</OrdName>", "");
                        sb.AppendFormat("<DifficultID>{0}</DifficultID>", aTopic.DifficultyID);
                        sb.AppendFormat("<DifficultName>{0}</DifficultName>", aTopic.DifficultyName);
                        sb.AppendFormat("<TopicPoint>{0}</TopicPoint>", aTopic.Mark);
                        sb.AppendFormat("<TopicScore>{0}</TopicScore>", aTopic.Score);
                        sb.AppendFormat("<StandardAnswer>{0}</StandardAnswer>", aTopic.StandardAnswer);
                        sb.AppendFormat("<UserAnswer>{0}</UserAnswer>", aTopic.UserAnswer);
                        sb.AppendFormat("<UseTime>{0}</UseTime>", 0);
                        sb.AppendFormat("<TopicRightLevel>{0}</TopicRightLevel>", aTopic.TopicRightLevel);
                        sb.AppendFormat("<TopicTypeID>{0}</TopicTypeID>", aTopic.TopicTypeId);
                        sb.AppendFormat("<TopicTypeName>{0}</TopicTypeName>", aTopic.TopicTypeName);
                        sb.AppendFormat("<JudgeEngineId>{0}</JudgeEngineId>", aTopic.JudgeEngineId);
                        sb.AppendFormat("<TopicTypePoint>{0}</TopicTypePoint>", topicTypePoint);
                        sb.AppendFormat("<TopicTypeScore>{0}</TopicTypeScore>", topicTypeScore);
                        sb.AppendFormat("<ScoreDetail>{0}</ScoreDetail>", topicScoreDetail);
                        sb.AppendFormat("</Topic>");
                        #endregion
                    }
                    sb.AppendFormat("</PaperTopicType>");
                }
                sb.AppendFormat("</PaperScore>");
                #endregion

                #region 备份试卷
                handPaper.lblHint.Text = "正在备份试卷，请稍候...";
                handPaper.lblHint.Refresh();
                if (PublicClass.JobType == JobType.ShiJuan)
                {
                    studentDir = Path.GetFileName(PublicClass.StudentDir);
                    backdir = string.Format("{0}\\{1}", Globals.BackupDir, studentDir);
                    dataDir = string.Format("{0}\\Data", backdir);
                    examDir = string.Format("{0}\\CbtesExam", backdir);
                    DirFileHelper.CopyFolder(PublicClass.StudentDirData, dataDir);
                    DirFileHelper.CopyFolder(PublicClass.StudentDirCbtesExam, examDir);
                    ZipFileTools.ZipFileFromDirectorySZL(backdir, backdir + ".zip", 6, "");
                }
                #endregion

                #region 交卷退出
                handPaper.lblHint.Text = "正在上传成绩，请稍候...";
                handPaper.lblHint.Refresh();
                Application.DoEvents();
                if (PublicClass.handPaper == "2")
                {
                    #region 上传题库成绩
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        //获取评分明细
                        paperScoreDetail = sb.ToString();
                        //上传成绩
                        updateScoreResult = bService.UploadExerciseScore(PublicClass.StudentCode, paperScoreDetail, out updateScoreMessage);
                        if (updateScoreResult == "1")
                        {
                            #region //还原用友账套
                            if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtCaoZuoUFIDA)
                            {
                                handPaper.lblHint.Text = "正在还原账套，请稍后...";
                                handPaper.lblHint.Refresh();
                                RedoEnvFile();
                            }
                            #endregion

                            #region //删除考生试卷
                            if (Directory.Exists(PublicClass.StudentDir))
                            {
                                handPaper.lblHint.Text = "正在删除试卷，请稍后...";
                                handPaper.lblHint.Refresh();
                                PublicClass.SowerExamPlugn.foDeleteFolder(PublicClass.StudentDir);
                            }
                            #endregion

                            string message = "成绩已顺利提交";
                            handPaper.lblTitle.Text = message;
                            handPaper.lblTitle.Refresh();
                            handPaper.lblHint.Text = message;
                            handPaper.lblHint.Refresh();
                            handPaper.ShowMessage(message);
                        }
                        else
                        {
                            string message = updateScoreMessage;
                            handPaper.lblTitle.Text = updateScoreMessage;
                            handPaper.lblTitle.Refresh();
                            handPaper.lblHint.Text = message;
                            handPaper.lblHint.Refresh();
                            handPaper.ShowMessage(message);
                        }
                    }
                    #endregion

                    #region 上传作业成绩
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        //当前作业是否提交到服务器
                        if (PublicClass.oMyJob.IsScoreToCenter.ToLower() == "false")      //true：提交，false：不提交
                        {
                            PublicClass.ShowMessageOk("当前成绩不允许提交到服务器，请联系管理员。");
                            return "";
                        }
                        //获取评分明细
                        paperScoreDetail = sb.ToString();
                        //上传成绩
                        updateScoreResult = bService.UploadJobScore(PublicClass.StudentCode, PublicClass.oMyJob, paperScoreDetail, out updateScoreMessage);
                        if (PublicClass.oMyJob.IsUploadAnswerFile.ToLower() == "true")
                        {
                            //上传试卷
                            bService.UploadExamFile(PublicClass.StudentCode, PublicClass.oMyJob, backdir + ".zip");
                        }
                        if (updateScoreResult == "1")
                        {
                            if (Globals.IsOnline)
                            {
                                string recPractiseResult = bService.RecordingTime(2, PublicClass.StudentCode, PublicClass.oMyJob.ID, PublicClass.oMyJob.HWName);
                            }
                            string message = string.Format("作业已顺利提交");
                            handPaper.lblTitle.Text = message;
                            handPaper.lblTitle.Refresh();
                            handPaper.lblHint.Text = message;
                            handPaper.lblHint.Refresh();
                            handPaper.ShowMessage(message);

                            #region //还原用友账套
                            if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtCaoZuoUFIDA)
                            {
                                handPaper.lblHint.Text = "正在还原账套，请稍后...";
                                handPaper.lblHint.Refresh();
                                RedoEnvFile();
                            }
                            #endregion

                            #region //删除考生试卷
                            if (Directory.Exists(PublicClass.StudentDir))
                                PublicClass.SowerExamPlugn.foDeleteFolder(PublicClass.StudentDir);
                            #endregion
                        }
                        else
                        {
                            string message = string.Format("{0},请您返回主界面，进入系统提交成绩。", updateScoreMessage);
                            handPaper.lblTitle.Text = updateScoreMessage;
                            handPaper.lblTitle.Refresh();
                            handPaper.lblHint.Text = message;
                            handPaper.lblHint.Refresh();
                            handPaper.ShowMessage(message);
                        }
                    }
                    #endregion
                }
                #endregion

                //关闭考试数据处理窗体
                handPaper.Close();

                #region 题库得分详情
                if (PublicClass.JobType == JobType.TiKu)
                {
                    //关闭评阅提示后显示得分详情
                    scoreDetail.Tag = 100;
                    scoreDetail.TopMost = true;
                    scoreDetail.ShowDialog();
                }
                #endregion

                #region 作业得分详情
                if (PublicClass.JobType == JobType.ShiJuan && PublicClass.oMyJob.ShowScore.ToLower() == "true")
                {
                    //关闭评阅提示后显示得分详情
                    scoreDetail.Tag = 100;
                    scoreDetail.TopMost = true;
                    scoreDetail.ShowDialog();
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmAnswerSheet), ex);
                CommonUtil.WriteLog(ex);
            }

            return sb.ToString();
        }
        /// <summary>
        /// 获取试题得分详情
        /// </summary>
        /// <param name="aTopic"></param>
        /// <param name="topicIndex"></param>
        /// <param name="topicTypePoint"></param>
        /// <param name="topicTypeScore"></param>
        /// <returns></returns>
        public string GetTopicScoreDetail(M_Topic aTopic, int topicIndex, double topicTypePoint, double topicTypeScore)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<Topic>");
            sb.AppendFormat("<TopicID>{0}</TopicID>", aTopic.TopicId);
            sb.AppendFormat("<GUID>{0}</GUID>", aTopic.GUID);
            sb.AppendFormat("<TopicCode>{0}</TopicCode>", "");
            sb.AppendFormat("<TopicIndex>{0}</TopicIndex>", topicIndex);
            sb.AppendFormat("<TopicNo>{0}</TopicNo>", aTopic.TopicNo);
            sb.AppendFormat("<ErrorHint>{0}</ErrorHint>", aTopic.ErrorHint);
            sb.AppendFormat("<BasicTypeId>{0}</BasicTypeId>", aTopic.BasicTypeId);
            sb.AppendFormat("<JudgeLevel>{0}</JudgeLevel>", aTopic.JudgeLevel);
            sb.AppendFormat("<OrdFormatName>{0}</OrdFormatName>", "");
            sb.AppendFormat("<OrdName>{0}</OrdName>", "");
            sb.AppendFormat("<DifficultID>{0}</DifficultID>", aTopic.DifficultyID);
            sb.AppendFormat("<DifficultName>{0}</DifficultName>", aTopic.DifficultyName);
            sb.AppendFormat("<TopicPoint>{0}</TopicPoint>", aTopic.Mark);
            sb.AppendFormat("<TopicScore>{0}</TopicScore>", aTopic.Score);
            sb.AppendFormat("<StandardAnswer>{0}</StandardAnswer>", aTopic.StandardAnswer);
            sb.AppendFormat("<UserAnswer>{0}</UserAnswer>", aTopic.UserAnswer);
            sb.AppendFormat("<UseTime>{0}</UseTime>", 0);
            sb.AppendFormat("<TopicRightLevel>{0}</TopicRightLevel>", aTopic.TopicRightLevel);
            sb.AppendFormat("<TopicTypeID>{0}</TopicTypeID>", aTopic.TopicTypeId);
            sb.AppendFormat("<TopicTypeName>{0}</TopicTypeName>", aTopic.TopicTypeName);
            sb.AppendFormat("<JudgeEngineId>{0}</JudgeEngineId>", aTopic.JudgeEngineId);
            sb.AppendFormat("<TopicTypePoint>{0}</TopicTypePoint>", topicTypePoint);
            sb.AppendFormat("<TopicTypeScore>{0}</TopicTypeScore>", topicTypeScore);
            sb.AppendFormat("</Topic>");

            return sb.ToString();
        }
        /// <summary>
        /// 设置窗体大小
        /// </summary>
        private void SetFormSize()
        {
            int height = Screen.PrimaryScreen.WorkingArea.Height;
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Size = new Size(width, height);
            this.Location = new Point(0, 0);
        }
        /// <summary>
        /// 快捷键设置
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int WM_KEYDOWN = 256;
            int WM_SYSKEYDOWN = 260;

            if (msg.Msg == WM_KEYDOWN | msg.Msg == WM_SYSKEYDOWN)
            {
                #region 单选
                if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtDanXuan)
                {
                    switch (keyData)
                    {
                        case Keys.A:
                            if (danXuan.rdoA.Visible) danXuan.rdoA.Checked = true;
                            break;
                        case Keys.B:
                            if (danXuan.rdoB.Visible) danXuan.rdoB.Checked = true;
                            break;
                        case Keys.C:
                            if (danXuan.rdoC.Visible) danXuan.rdoC.Checked = true;
                            break;
                        case Keys.D:
                            if (danXuan.rdoD.Visible) danXuan.rdoD.Checked = true;
                            break;
                        case Keys.E:
                            if (danXuan.rdoE.Visible) danXuan.rdoE.Checked = true;
                            break;
                        case Keys.F:
                            if (danXuan.rdoF.Visible) danXuan.rdoF.Checked = true;
                            break;
                    }
                }
                #endregion

                #region 多选
                if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtDuoXuan)
                {
                    switch (keyData)
                    {
                        case Keys.A:
                            if (duoXuan.cbA.Visible)
                            {
                                if (duoXuan.cbA.CheckState == CheckState.Checked)
                                {
                                    duoXuan.cbA.Checked = false;
                                    break;
                                }

                                if (duoXuan.cbA.CheckState == CheckState.Unchecked)
                                {
                                    duoXuan.cbA.Checked = true;
                                    break;
                                }
                            }
                            break;
                        case Keys.B:
                            if (duoXuan.cbB.Visible)
                            {
                                if (duoXuan.cbB.CheckState == CheckState.Checked)
                                {
                                    duoXuan.cbB.Checked = false;
                                    break;
                                }

                                if (duoXuan.cbB.CheckState == CheckState.Unchecked)
                                {
                                    duoXuan.cbB.Checked = true;
                                    break;
                                }
                            }
                            break;
                        case Keys.C:
                            if (duoXuan.cbC.Visible)
                            {
                                if (duoXuan.cbC.CheckState == CheckState.Checked)
                                {
                                    duoXuan.cbC.Checked = false;
                                    break;
                                }

                                if (duoXuan.cbC.CheckState == CheckState.Unchecked)
                                {
                                    duoXuan.cbC.Checked = true;
                                    break;
                                }
                            }
                            break;
                        case Keys.D:
                            if (duoXuan.cbD.Visible)
                            {
                                if (duoXuan.cbD.CheckState == CheckState.Checked)
                                {
                                    duoXuan.cbD.Checked = false;
                                    break;
                                }

                                if (duoXuan.cbD.CheckState == CheckState.Unchecked)
                                {
                                    duoXuan.cbD.Checked = true;
                                    break;
                                }
                            }
                            break;
                        case Keys.E:
                            if (duoXuan.cbE.Visible)
                            {
                                if (duoXuan.cbE.CheckState == CheckState.Checked)
                                {
                                    duoXuan.cbE.Checked = false;
                                    break;
                                }

                                if (duoXuan.cbE.CheckState == CheckState.Unchecked)
                                {
                                    duoXuan.cbE.Checked = true;
                                    break;
                                }
                            }
                            break;
                        case Keys.F:
                            if (duoXuan.cbF.Visible)
                            {
                                if (duoXuan.cbF.CheckState == CheckState.Checked)
                                {
                                    duoXuan.cbF.Checked = false;
                                    break;
                                }

                                if (duoXuan.cbF.CheckState == CheckState.Unchecked)
                                {
                                    duoXuan.cbF.Checked = true;
                                    break;
                                }
                            }
                            break;
                    }
                }
                #endregion

                #region 判断
                if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtPanDuan)
                {
                    switch (keyData)
                    {
                        case Keys.R:
                            if (panDuan.rdoRight.Visible) panDuan.rdoRight.Checked = true;
                            break;
                        case Keys.W:
                            if (panDuan.rdoError.Visible) panDuan.rdoError.Checked = true;
                            break;
                    }
                }
                #endregion

                #region 上一题--下一题
                switch (keyData)
                {
                    case Keys.PageUp:
                        if (tsbLastTopic.Enabled)
                        {
                            tsbLastTopic_Click(this, new EventArgs());
                        }
                        break;
                    case Keys.PageDown:
                        if (tsbNextTopic.Enabled)
                        {
                            tsbNextTopic_Click(this, new EventArgs());
                        }
                        break;
                }
                #endregion
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        /// <summary>
        /// 重新还原用友账套
        /// </summary>
        private void RedoEnvFile()
        {
            try
            {
                //判断是否需要进行原始账套复位
                if (oCurrTopic.JudgeEngineId == "20001")
                {
                    string sAccountDir = PublicClass.StudentDir + "\\Account";

                    //如果考生文件夹中的账套目录不为空，应还原原有用友系统账套，并分离考生账套
                    if (PublicClass.SowerExamPlugn.foFolderIsEmpty(sAccountDir) == 0)
                    {
                        PublicClass.SowerExamPlugn.ResetERPAccount(PublicClass.StudentDir, PublicClass.ExamSysDir, 2);
                        PublicClass.SowerExamPlugn.FreeAccount(sAccountDir, PublicClass.ExamSysDir, true);
                        PublicClass.SowerExamPlugn.StartUFNETService("UFNET");
                    }

                    //清空考生文件夹中的账套目录
                    if (PublicClass.SowerExamPlugn.foEmptyFolder(sAccountDir) == 0)
                    {
                        PublicClass.ShowMessageOk("清空考生文件夹 Account 目录失败！请检查磁盘读写权限是否足够。");
                        return;
                    }

                    //重启用友相关服务
                    if (!PublicClass.SowerExamPlugn.StartUFNETService("UFNET"))
                    {
                        PublicClass.ShowMessageOk("启动用友服务失败！按【确定】退出系统！");
                        Environment.Exit(0);
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk("“" + ex.Message + "”\r\n\r\n[RedoTopic] 无法重新加载账套。");
            }
        }
        /// <summary>
        /// 获取用户考试时间
        /// </summary>
        /// <returns></returns>
        private string GetTotalUserTime()
        {
            if (PublicClass.handPaper == "1") return "";

            DateTime ExamStartingTime = DateTime.Parse(PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "考生信息", "考试开始时间"));
            DateTime ExamBlockingTime = DateTime.Parse(PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "考生信息", "考试结束时间"));
            string result = ExamBlockingTime.Subtract(ExamStartingTime).TotalMinutes.ToString("0.0");


            return result;
        }
        private string GetPaperName()
        {
            B_TaoJuanXinXi bTaoJuanXinXi = new B_TaoJuanXinXi();

            M_TaoJuanXinXi taojuanxinxi = bTaoJuanXinXi.GetTaoJuanXinXiById(PublicClass.TopicDBFileName_SDBT, PublicClass.oSubjectProp.PresetPaperID.ToString());

            return taojuanxinxi.TaoJuanMingCheng;
        }
        /// <summary>
        /// 初始化界面组件
        /// </summary>
        public frmAnswerSheet()
        {
            InitializeComponent();
            this.Text = UserConfigSettings.Instance.ReadSetting("系统标题");
        }
        #endregion

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAnswerSheet_Load(object sender, EventArgs e)
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);



        }
        /// <summary>
        /// 选中当前树节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvMenu_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var parentId = string.Empty;
            var childId = string.Empty;
            var subChildId = string.Empty;
            var treeNode = tvMenu.SelectedNode;
            var topicTypes = PublicClass.oPaperInfo.TopicTypes;
            if (treeNode.Level == 0)
            {
                parentId = treeNode.Tag.ToString();
                topicType = topicTypes.Find(t => t.Id == parentId);
                lblTopicType.Text = string.Format("{0}：第 {1}/{2} 题", topicType.Name, topicType.Topics[0].TopicNo, topicType.Topics.Count);
                e.Node.Expand();
            }
            else if (treeNode.Level == 1)
            {
                parentId = treeNode.Parent.Tag.ToString();
                childId = treeNode.Tag.ToString();
                topicType = topicTypes.Find(t => t.Id == parentId);
                topic = topicType.Topics.Find(t => t.TopicId == childId);
                if (topic.AllowSubTopic)
                {
                    subTopic = topic.SubTopics.Find(t => t.TopicID == topic.TopicId && t.TopicFaceID == topic.SubTopicId);
                    lblTopicType.Text = string.Format("{0}：第 {1}/{2} 题", topicType.Name, subTopic.TopicFaceID, topic.SubTopics.Count);
                    lblSubTopicTitle.Text = string.Format("第 {0} 小题", topic.SubTopicId);
                    tvMenu.CollapseAll();
                    treeNode.Parent.Expand();
                    treeNode.Expand();
                    Index = int.Parse(topic.TopicNo);
                    TopicTypeId = topicType.Id;
                }
                else
                {
                    lblTopicType.Text = string.Format("{0}：第 {1}/{2} 题", topicType.Name, topic.TopicNo, topicType.Topics.Count);
                    tvMenu.CollapseAll();
                    treeNode.Parent.Expand();
                    Index = int.Parse(topic.TopicNo);
                    TopicTypeId = topicType.Id;
                }
                tmrShowTopic.Start();
            }
            else if (treeNode.Level == 2)
            {
                subTopic = topic.SubTopics.Find(t => t.TopicID == topic.TopicId && t.TopicFaceID == topic.SubTopicId);
                parentId = treeNode.Parent.Parent.Tag.ToString();
                childId = treeNode.Parent.Tag.ToString();
                subChildId = treeNode.Tag.ToString();
                topicType = topicTypes.Find(t => t.Id == parentId);
                //topic = topicType.Topics.Find(t => t.TopicId == childId && t.SubTopicId == subChildId);
                topic = topicType.Topics[int.Parse(subChildId) - 1];
                lblTopicType.Text = string.Format("{0}：第 {1}/{2} 题", topicType.Name, subTopic.TopicFaceID, topic.SubTopics.Count);
                lblSubTopicTitle.Text = string.Format("第 {0} 小题", topic.SubTopicId);
                tvMenu.CollapseAll();
                treeNode.Parent.Expand();
                treeNode.Parent.Parent.Expand();
                Index = int.Parse(subChildId);
                TopicTypeId = topicType.Id;
                tmrShowTopic.Start();
            }
        }
        /// <summary>
        /// 计时模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrExamTimeCountDownTimer_Tick(object sender, EventArgs e)
        {
            string str1 = "";
            string str2 = "";
            int iHour, iSecond;

            #region 题库
            if (PublicClass.JobType == JobType.TiKu)
            {
                if (PublicClass.oPaperInfo.ExamInfo.ExamMode != "2")
                {
                    return;
                }
            }
            #endregion

            #region 作业
            if (PublicClass.JobType == JobType.ShiJuan)
            {
                if (PublicClass.oMyJob.IsCaculateTime.ToLower() != "true")
                {
                    return;
                }
            }
            #endregion

            try
            {
                #region //倒计时显示
                if (iResidualExamTime > 0)
                {
                    iResidualExamTime--;

                    iHour = iResidualExamTime / 60;
                    iSecond = iResidualExamTime % 60;

                    str1 = iHour.ToString();
                    if (str1.Length == 1)
                    {
                        str1 = "0" + str1;
                    }

                    str2 = iSecond.ToString();
                    if (str2.Length == 1)
                    {
                        str2 = "0" + str2;
                    }

                    lblTime.Text = string.Format("答题剩余时间：{0}分 {1}秒", str1, str2);

                    if (iResidualExamTime == 300)
                    {
                        //MessageBox.Show("注意：考试时间只剩下 5 分钟！");
                        frmTimingMessageBox message = new frmTimingMessageBox("注意：考试时间只剩下 5 分钟！", 5);
                        message.Show();
                    }
                }
                #endregion

                #region //保存考试时间
                if (PublicClass.oPaperInfo.ExamInfo.AutoSaveInterval > 0)
                {
                    SaveResidualExamTime();
                }
                #endregion

                //考试时间到自动交卷
                if (iResidualExamTime <= 0)
                {
                    //保存用户答案
                    SaveUserAnswer();
                    tmrExamTimeCountDownTimer.Stop();
                    PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考生状态", "正在交卷");
                    PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考试结束时间", DateTime.Now.ToString());
                    //停止考试，转向评分页面
                    publicClass.KillTask();
                    PublicClass.handPaper = "2";
                    //试卷评分并提交成绩
                    PaperScore();
                    //返回主窗体
                    frmBusicWorkMain busicWorkMain = new frmBusicWorkMain();
                    busicWorkMain.Show();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmAnswerSheet), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 实操题10秒后自动保存答案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmrSetAnswer_Tick(object sender, EventArgs e)
        {
            tmrSetAnswer.Stop();

            if (oCurrTopic.HaveUserAnswer == true) return;

            if (publicClass.TopicIsCaoZuoTi(oCurrTopic))
            {
                oCurrTopic.Changed = true;
                SaveUserAnswer();
            }
        }

        private void tmrTotalUserTime_Tick(object sender, EventArgs e)
        {
            totalUserTime++;
            PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考试答题时间", totalUserTime.ToString());
        }

        private void tmrShowTopic_Tick(object sender, EventArgs e)
        {
            M_TopicType aTopicType = new M_TopicType();
            M_Topic oNextTopic = new M_Topic();
            string sTopicFace = string.Empty;
            string sSubTopicFace = string.Empty;
            string panelName = string.Empty;
            int iQuestionNumber;
            List<string> slEditorType = new List<string>();
            Panel panelGroup = null;
            int height = 0;
            int width = 0;
            Point pointTopic = new Point();
            Point pointPage = new Point();
            List<string> listUserAnswer = new List<string>();
            List<char> listAnswerPeiwu = new List<char>();
            tmrShowTopic.Stop();

            int TopicNo = Index;
            string topicTypeId = TopicTypeId;

            #region 首题-上一题--下一题-尾题
            for (int i = 0; i < PublicClass.oPaperInfo.TopicTypes.Count; i++)
            {
                aTopicType = PublicClass.oPaperInfo.TopicTypes[i];
                if (aTopicType.Id != topicTypeId)
                {
                    continue;
                }
                oNextTopic = aTopicType.Topics[TopicNo - 1];
                if (oNextTopic.AllowSubTopic)
                {
                    if (aTopicType.Topics.Count < TopicNo)
                    {
                        tsbNextTopic.Enabled = false;
                        tsbEndTopic.Enabled = false;
                    }

                    if (TopicNo <= 1)
                    {
                        tsbFirst.Enabled = false;
                        tsbLastTopic.Enabled = false;
                        tsbNextTopic.Enabled = true;
                        tsbEndTopic.Enabled = true;
                    }

                    if (TopicNo >= aTopicType.Topics.Count)
                    {
                        tsbFirst.Enabled = true;
                        tsbLastTopic.Enabled = true;
                        tsbNextTopic.Enabled = false;
                        tsbEndTopic.Enabled = false;
                    }

                    if (TopicNo > 1 && TopicNo < aTopicType.Topics.Count)
                    {
                        tsbLastTopic.Enabled = true;
                        tsbNextTopic.Enabled = true;
                        tsbFirst.Enabled = true;
                        tsbEndTopic.Enabled = true;
                    }

                    if (TopicNo == 1)
                    {
                        tsbFirst.Enabled = false;
                        tsbLastTopic.Enabled = false;
                    }

                    if (aTopicType.Topics.Count < TopicNo - 1)
                    {
                        MessageBox.Show("加载试题失败，缺少试题数。");
                    }
                }
                else
                {
                    if (aTopicType.TopicNumber < TopicNo)
                    {
                        tsbNextTopic.Enabled = false;
                        tsbEndTopic.Enabled = false;
                    }

                    if (TopicNo <= 1)
                    {
                        tsbFirst.Enabled = false;
                        tsbLastTopic.Enabled = false;
                        tsbNextTopic.Enabled = true;
                        tsbEndTopic.Enabled = true;
                    }

                    if (TopicNo >= aTopicType.TopicNumber)
                    {
                        tsbFirst.Enabled = true;
                        tsbLastTopic.Enabled = true;
                        tsbNextTopic.Enabled = false;
                        tsbEndTopic.Enabled = false;
                    }

                    if (TopicNo > 1 && TopicNo < aTopicType.TopicNumber)
                    {
                        tsbLastTopic.Enabled = true;
                        tsbNextTopic.Enabled = true;
                        tsbFirst.Enabled = true;
                        tsbEndTopic.Enabled = true;
                    }

                    if (TopicNo == 1)
                    {
                        tsbFirst.Enabled = false;
                        tsbLastTopic.Enabled = false;
                    }

                    if (aTopicType.Topics.Count < TopicNo - 1)
                    {
                        MessageBox.Show("加载试题失败，缺少试题数。");
                    }
                }
                break;
            }
            #endregion

            ShowTopic(oNextTopic.TopicTypeId, oNextTopic.TopicId, oNextTopic.SubTopicId);

            #region 加载颜色
            if (oCurrTopic.AllowSubTopic)
            {
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                    {
                        tvMenu.Nodes[i].Nodes[j].ForeColor = Color.White;
                        for (int k = 0; k < tvMenu.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            tvMenu.Nodes[i].Nodes[j].Nodes[k].ForeColor = Color.White;
                        }
                    }
                }
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    string parentId = tvMenu.Nodes[i].Tag.ToString();
                    if (topicTypeId != parentId) continue;
                    for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                    {
                        for (int k = 0; k < tvMenu.Nodes[i].Nodes[j].Nodes.Count; k++)
                        {
                            string childNo = tvMenu.Nodes[i].Nodes[j].Nodes[k].Tag.ToString();
                            if (oCurrTopic.TopicNo == childNo)
                            {
                                tvMenu.CollapseAll();
                                tvMenu.Nodes[i].Expand();
                                tvMenu.Nodes[i].Nodes[j].Expand();
                                tvMenu.Nodes[i].Nodes[j].Nodes[k].ForeColor = Color.Yellow;
                            }
                            else
                            {
                                tvMenu.Nodes[i].Nodes[j].Nodes[k].ForeColor = Color.White;
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                    {
                        tvMenu.Nodes[i].Nodes[j].ForeColor = Color.White;
                    }
                }
                for (int i = 0; i < tvMenu.Nodes.Count; i++)
                {
                    string parentId = tvMenu.Nodes[i].Tag.ToString();
                    if (topicTypeId != parentId) continue;
                    for (int j = 0; j < tvMenu.Nodes[i].Nodes.Count; j++)
                    {
                        string childNo = tvMenu.Nodes[i].Nodes[j].Tag.ToString();
                        if (oCurrTopic.TopicId == childNo)
                        {
                            tvMenu.Nodes[i].Nodes[j].ForeColor = Color.Yellow;
                        }
                        else
                        {
                            tvMenu.Nodes[i].Nodes[j].ForeColor = Color.White;
                        }
                    }
                }
            }
            #endregion

            #region 加载评析
            sTopicFace = PublicClass.SowerExamPlugn.GetTopicItem(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), 2);
            sSubTopicFace = PublicClass.SowerExamPlugn.GetSubTopicItem(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), int.Parse(oCurrTopic.SubTopicId), 2);
            if (oCurrTopic.AllowSubTopic)
            {
                //加载评析
                CommonUtil.LoadSowerTextSub(txtAnalysis, sSubTopicFace, oCurrTopic);
                if (string.IsNullOrEmpty(sSubTopicFace) || sSubTopicFace.Length <= 664)
                {
                    if (oCurrTopic.StandardAnswer == "Δ" || oCurrTopic.StandardAnswer == "")
                    {
                        txtSubAnalysis.DocType = "txt";
                        txtSubAnalysis.SowerText = "无评析";
                    }
                    else
                    {
                        txtSubAnalysis.DocType = "txt";
                        txtSubAnalysis.SowerText = "正确答案：" + oCurrTopic.StandardAnswer;
                    }
                }
            }
            else
            {
                //加载评析
                CommonUtil.LoadSowerText(txtAnalysis, sTopicFace, oCurrTopic);
                if (string.IsNullOrEmpty(sTopicFace) || sTopicFace.Length <= 264)
                {
                    if (oCurrTopic.StandardAnswer == "Δ" || oCurrTopic.StandardAnswer == "")
                    {
                        txtAnalysis.DocType = "txt";
                        txtAnalysis.SowerText = "无评析";
                    }
                    else
                    {
                        txtAnalysis.DocType = "txt";
                        txtAnalysis.SowerText = "正确答案：" + oCurrTopic.StandardAnswer;
                    }
                }
            }
            #endregion

            #region 加载选项
            switch (oCurrTopic.JudgeEngineId)
            {
                case JudgeEngine.DanXuan:
                    #region 单选题
                    danXuan = new frmDanXuanTi();
                    FormBind(danXuan);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);

                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);

                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);

                        ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                    }
                    #endregion

                    height = pnlTopicFace.Height - 140;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));

                    switch (oCurrTopic.OptionNumber)
                    {
                        case 1:
                            danXuan.rdoA.Visible = true;
                            danXuan.rdoB.Visible = false;
                            danXuan.rdoC.Visible = false;
                            danXuan.rdoD.Visible = false;
                            danXuan.rdoE.Visible = false;
                            danXuan.rdoF.Visible = false;
                            break;
                        case 2:
                            danXuan.rdoA.Visible = true;
                            danXuan.rdoB.Visible = true;
                            danXuan.rdoC.Visible = false;
                            danXuan.rdoD.Visible = false;
                            danXuan.rdoE.Visible = false;
                            danXuan.rdoF.Visible = false;
                            break;
                        case 3:
                            danXuan.rdoA.Visible = true;
                            danXuan.rdoB.Visible = true;
                            danXuan.rdoC.Visible = true;
                            danXuan.rdoD.Visible = false;
                            danXuan.rdoE.Visible = false;
                            danXuan.rdoF.Visible = false;
                            break;
                        case 4:
                            danXuan.rdoA.Visible = true;
                            danXuan.rdoB.Visible = true;
                            danXuan.rdoC.Visible = true;
                            danXuan.rdoD.Visible = true;
                            danXuan.rdoE.Visible = false;
                            danXuan.rdoF.Visible = false;
                            break;
                        case 5:
                            danXuan.rdoA.Visible = true;
                            danXuan.rdoB.Visible = true;
                            danXuan.rdoC.Visible = true;
                            danXuan.rdoD.Visible = true;
                            danXuan.rdoE.Visible = true;
                            danXuan.rdoF.Visible = false;
                            break;
                        case 6:
                            danXuan.rdoA.Visible = true;
                            danXuan.rdoB.Visible = true;
                            danXuan.rdoC.Visible = true;
                            danXuan.rdoD.Visible = true;
                            danXuan.rdoE.Visible = true;
                            danXuan.rdoF.Visible = true;
                            break;
                        default:
                            break;
                    }
                    #endregion
                    break;
                case JudgeEngine.DuoXuan:
                    if (oCurrTopic.AllowPeiWu)
                    {
                        #region 配伍题
                        peiWu = new frmPeiWuTi();
                        FormBind(peiWu);

                        pnlOpation.Visible = true;
                        txtTopicFace.Visible = true;
                        txtTyping.Visible = false;

                        #region 题库
                        if (PublicClass.JobType == JobType.TiKu)
                        {
                            if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                            {
                                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                                ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                            }
                            else
                            {
                                ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);
                                ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                            }
                        }
                        #endregion

                        #region 作业
                        if (PublicClass.JobType == JobType.ShiJuan)
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        #endregion

                        height = pnlTopicFace.Height - 170;
                        width = pnlTopicFace.Width;
                        pnlTopic.Size = new Size(width, height);
                        pointPage = pnlPageIndex.Location;
                        pnlOpation.Size = new Size(width, 63);
                        pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 70));

                        panelList.Clear();
                        for (int i = 0; i < oCurrTopic.OptionNumber; i++)
                        {
                            string pnlName = string.Format("panel{0}", i + 1);
                            panelList.Add(pnlName);
                            Panel panel = peiWu.pnlPeiWu.Controls[pnlName] as Panel;
                            panel.Visible = true;
                            foreach (var item in panel.Controls)
                            {
                                if (item is Label)
                                {
                                    Label label = item as Label;
                                    label.Visible = true;
                                }
                                if (item is ComboBox)
                                {
                                    ComboBox comboBox = item as ComboBox;
                                    comboBox.Visible = true;
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 多选题
                        duoXuan = new frmDuoXuanTi();
                        FormBind(duoXuan);

                        pnlOpation.Visible = true;
                        txtTopicFace.Visible = true;
                        txtTyping.Visible = false;

                        #region 题库
                        if (PublicClass.JobType == JobType.TiKu)
                        {
                            if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                            {
                                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                                ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                            }
                            else
                            {
                                ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);
                                ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                            }
                        }
                        #endregion

                        #region 作业
                        if (PublicClass.JobType == JobType.ShiJuan)
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        #endregion

                        height = pnlTopicFace.Height - 140;
                        width = pnlTopicFace.Width;
                        pnlTopic.Size = new Size(width, height);
                        pointPage = pnlPageIndex.Location;
                        pnlOpation.Size = new Size(width, 33);
                        pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));

                        switch (oCurrTopic.OptionNumber)
                        {
                            case 1:
                                duoXuan.cbA.Visible = true;
                                duoXuan.cbB.Visible = false;
                                duoXuan.cbC.Visible = false;
                                duoXuan.cbD.Visible = false;
                                duoXuan.cbE.Visible = false;
                                duoXuan.cbF.Visible = false;
                                break;
                            case 2:
                                duoXuan.cbA.Visible = true;
                                duoXuan.cbB.Visible = true;
                                duoXuan.cbC.Visible = false;
                                duoXuan.cbD.Visible = false;
                                duoXuan.cbE.Visible = false;
                                duoXuan.cbF.Visible = false;
                                break;
                            case 3:
                                duoXuan.cbA.Visible = true;
                                duoXuan.cbB.Visible = true;
                                duoXuan.cbC.Visible = true;
                                duoXuan.cbD.Visible = false;
                                duoXuan.cbE.Visible = false;
                                duoXuan.cbF.Visible = false;
                                break;
                            case 4:
                                duoXuan.cbA.Visible = true;
                                duoXuan.cbB.Visible = true;
                                duoXuan.cbC.Visible = true;
                                duoXuan.cbD.Visible = true;
                                duoXuan.cbE.Visible = false;
                                duoXuan.cbF.Visible = false;
                                break;
                            case 5:
                                duoXuan.cbA.Visible = true;
                                duoXuan.cbB.Visible = true;
                                duoXuan.cbC.Visible = true;
                                duoXuan.cbD.Visible = true;
                                duoXuan.cbE.Visible = true;
                                duoXuan.cbF.Visible = false;
                                break;
                            case 6:
                                duoXuan.cbA.Visible = true;
                                duoXuan.cbB.Visible = true;
                                duoXuan.cbC.Visible = true;
                                duoXuan.cbD.Visible = true;
                                duoXuan.cbE.Visible = true;
                                duoXuan.cbF.Visible = true;
                                break;
                        }
                        #endregion
                    }
                    break;
                case JudgeEngine.PanDuan:
                    #region 判断题
                    panDuan = new frmPanDuanTi();
                    FormBind(panDuan);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                    }
                    #endregion

                    height = pnlTopicFace.Height - 140;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));
                    #endregion
                    break;
                case JudgeEngine.TianKong:
                    #region 填空题
                    tianKong = new frmTianKongTi();
                    FormBind(tianKong);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);

                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);

                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);

                        ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                    }
                    #endregion

                    int optionCount = oCurrTopic.OptionNumber / 2;
                    height = pnlTopicFace.Height - 140 - (32 * optionCount);
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33 + (32 * optionCount));
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40 - (32 * optionCount)));

                    panelList.Clear();
                    tianKong.pnlContainer.RowCount = optionCount;
                    for (int i = 0; i < oCurrTopic.OptionNumber; i++)
                    {
                        string pnlName = string.Format("panel{0}", i + 1);
                        panelList.Add(pnlName);
                        Panel panel = tianKong.pnlContainer.Controls[pnlName] as Panel;
                        panel.Visible = true;

                        foreach (var item in panel.Controls)
                        {
                            if (item is Label)
                            {
                                Label label = item as Label;
                                label.Visible = true;
                            }
                            if (item is TextBox)
                            {
                                TextBox textBox = item as TextBox;
                                textBox.Visible = true;
                            }
                        }
                    }
                    #endregion
                    break;
                case JudgeEngine.ZhuGuan:
                    #region 主观题
                    zhuguan = new frmZhuGuanTi();
                    FormBind(zhuguan);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, true, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, false, false, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, true, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, false, false, true, false, false, false, false);
                    }
                    #endregion

                    height = (topicFaceSize.Height - 63) / 2;
                    width = topicFaceSize.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointTopic = pnlTopic.Location;

                    pnlOpation.Size = new Size(width, height);
                    pnlOpation.Location = new Point(pointTopic.X, (pointTopic.Y - 72 + height + 25));
                    #endregion
                    break;
                case JudgeEngine.Windows:
                    #region 基本操作题
                    pnlOpation.Visible = false;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    tmrSetAnswer.Start();

                    switch (formState)
                    {
                        case 0:
                            #region 题库
                            if (PublicClass.JobType == JobType.TiKu)
                            {
                                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                                {
                                    ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                                    ShowButtomTsbButton(true, true, true, true, true, false, false, false, true, false, false);
                                }
                                else
                                {
                                    ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                                    ShowButtomTsbButton(true, true, true, true, true, false, false, false, true, false, false);
                                }
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                                ShowButtomTsbButton(true, true, true, true, true, false, false, false, true, false, false);
                            }
                            #endregion
                            break;
                        case 1:
                            #region 题库
                            if (PublicClass.JobType == JobType.TiKu)
                            {
                                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                                {
                                    ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, true);
                                    ShowButtomTsbButton(false, true, true, false, true, false, false, false, true, false, false);
                                }
                                else
                                {
                                    ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, true, true);
                                    ShowButtomTsbButton(false, true, true, false, true, false, false, false, true, false, false);
                                }
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, true);

                                ShowButtomTsbButton(false, true, true, false, true, false, false, false, true, false, false);
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }

                    height = pnlTopicFace.Height - 140;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height + 35);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));
                    #endregion
                    break;
                case JudgeEngine.Typing:
                    #region 打字题
                    typing = new frmTyping();
                    typing.txtTyping.Font = new Font("宋体", FFontSize);
                    FormBind(typing);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = false;
                    txtTyping.Visible = true;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, true, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, true, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, false, false, true, false, false, false, false);
                    }
                    #endregion

                    height = (topicFaceSize.Height - 63) / 2;
                    width = topicFaceSize.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointTopic = pnlTopic.Location;

                    pnlOpation.Size = new Size(width, height);
                    pnlOpation.Location = new Point(pointTopic.X, (pointTopic.Y - 72 + height + 25));
                    #endregion
                    break;
                case JudgeEngine.AnLiFenXi:
                    #region 案例分析题
                    anLiFenXi = new frmAnLiFenXi();
                    FormBind(anLiFenXi);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                    }
                    #endregion

                    height = pnlTopicFace.Height - 170;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 63);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 70));

                    panelList.Clear();
                    panelList.Add("panel1");
                    panelList.Add("panel2");
                    panelList.Add("panel3");
                    panelList.Add("panel4");
                    panelList.Add("panel5");
                    #endregion
                    break;
                case JudgeEngine.JiSuanFenXi:
                    #region 计算分析题
                    jiSuanFenXi = new frmJiSuanFenXi();
                    FormBind(jiSuanFenXi);

                    pnlOpation.Visible = true;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, false, false, false, false, false, false, false);
                    }
                    #endregion

                    height = pnlTopicFace.Height - 196;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 89);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 96));

                    slEditorType = oCurrTopic.EditorType.Split('♂').ToList();
                    iQuestionNumber = slEditorType.Count;
                    panelList.Clear();
                    for (int i = 0; i < slEditorType.Count; i++)
                    {
                        string pnlName = string.Format("panel{0}", i + 1);
                        panelList.Add(pnlName);
                        Panel panel = jiSuanFenXi.pnlContainer.Controls[pnlName] as Panel;

                        #region 分录编辑
                        if (slEditorType[i] == "1")
                        {
                            foreach (var item in panel.Controls)
                            {
                                if (item is Label)
                                {
                                    Label label = item as Label;
                                    label.Visible = true;
                                }
                                if (item is TextBox)
                                {
                                    TextBox textBox = item as TextBox;
                                    textBox.ReadOnly = true;
                                    textBox.Visible = true;
                                    textBox.BackColor = Color.White;
                                }
                                if (item is Button)
                                {
                                    Button button = item as Button;
                                    button.Visible = true;
                                }
                            }
                        }
                        #endregion

                        #region 计算金额
                        if (slEditorType[i] == "0")
                        {
                            foreach (var item in panel.Controls)
                            {
                                if (item is Label)
                                {
                                    Label label = item as Label;
                                    label.Visible = true;
                                }
                                if (item is TextBox)
                                {
                                    TextBox textBox = item as TextBox;
                                    Size size = textBox.Size;
                                    size.Width += 81;
                                    textBox.Size = new Size(size.Width, size.Height);
                                    textBox.ReadOnly = false;
                                    textBox.Visible = true;
                                }
                            }
                        }
                        #endregion
                    }
                    #endregion
                    break;
                case JudgeEngine.ShiWuFangZhen:
                    #region 实务仿真
                    pnlOpation.Visible = false;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    tmrSetAnswer.Start();

                    tsbLessenTopicFace2_Click(this, new EventArgs());

                    PublicClass.SowerExamPlugn.OpenApp(
                        PublicClass.StudentDir, PublicClass.ExamSysDir,
                        oCurrTopic.AppPath, int.Parse(JudgeEngine.ShiWuFangZhen), oCurrTopic.T3Type, oCurrTopic.CallT3Para);
                    #endregion
                    break;
                case JudgeEngine.UFIDA:
                    #region 用友操作题
                    pnlOpation.Visible = false;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    tmrSetAnswer.Start();

                    switch (formState)
                    {
                        case 0:
                            #region 题库
                            if (PublicClass.JobType == JobType.TiKu)
                            {
                                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                                {
                                    ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                                    ShowButtomTsbButton(true, true, true, true, true, false, false, false, false, false, false);
                                }
                                else
                                {
                                    ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                                    ShowButtomTsbButton(true, true, true, true, true, false, false, false, false, false, false);
                                }
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                                ShowButtomTsbButton(true, true, true, true, true, false, false, false, false, false, false);
                            }

                            height = pnlTopicFace.Height - 140;
                            width = pnlTopicFace.Width;
                            pnlTopic.Size = new Size(width, height + 35);
                            pointPage = pnlPageIndex.Location;
                            pnlOpation.Size = new Size(width, 33);
                            pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));
                            #endregion
                            break;
                        case 1:
                            #region 题库
                            if (PublicClass.JobType == JobType.TiKu)
                            {
                                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                                {
                                    ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);
                                    ShowButtomTsbButton(false, true, true, false, true, false, false, false, false, false, false);
                                }
                                else
                                {
                                    ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);
                                    ShowButtomTsbButton(false, true, true, false, true, false, false, false, false, false, false);
                                }
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);
                                ShowButtomTsbButton(false, true, true, false, true, false, false, false, false, false, false);
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }

                    height = pnlTopicFace.Height - 140;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height + 35);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));
                    #endregion
                    break;
                case JudgeEngine.OutLook:
                    #region 电子邮件
                    pnlOpation.Visible = false;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    tmrSetAnswer.Start();

                    switch (formState)
                    {
                        case 0:
                            #region 题库
                            if (PublicClass.JobType == JobType.TiKu)
                            {
                                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                                {
                                    ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                                    ShowButtomTsbButton(true, true, true, true, false, true, false, false, true, false, false);
                                }
                                else
                                {
                                    ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                                    ShowButtomTsbButton(true, true, true, true, false, true, false, false, true, false, false);
                                }
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                                ShowButtomTsbButton(true, true, true, true, false, true, false, false, true, false, false);
                            }
                            #endregion
                            break;
                        case 1:
                            #region 题库
                            if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                            {
                                ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                                ShowButtomTsbButton(false, true, true, false, false, true, false, false, true, false, false);
                            }
                            else
                            {
                                ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);

                                ShowButtomTsbButton(false, true, true, false, false, true, false, false, true, false, false);
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                                ShowButtomTsbButton(false, true, true, false, false, true, false, false, true, false, false);
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }

                    height = pnlTopicFace.Height - 140;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height + 35);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));
                    #endregion
                    break;
                default:
                    #region 默认题
                    pnlOpation.Visible = false;
                    txtTopicFace.Visible = true;
                    txtTyping.Visible = false;

                    tmrSetAnswer.Start();

                    switch (formState)
                    {
                        case 0:
                            #region 题库
                            if (PublicClass.JobType == JobType.TiKu)
                            {
                                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                                {
                                    ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                                    ShowButtomTsbButton(true, true, true, true, true, true, false, false, true, false, false);
                                }
                                else
                                {
                                    ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                                    ShowButtomTsbButton(true, true, true, true, true, true, false, false, true, false, false);
                                }
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                                ShowButtomTsbButton(true, true, true, true, true, true, false, false, true, false, false);
                            }
                            #endregion
                            break;
                        case 1:
                            #region 题库
                            if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                            {
                                ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                                ShowButtomTsbButton(false, true, true, false, true, true, false, false, true, false, false);
                            }
                            else
                            {
                                ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);

                                ShowButtomTsbButton(false, true, true, false, true, true, false, false, true, false, false);
                            }
                            #endregion

                            #region 作业
                            if (PublicClass.JobType == JobType.ShiJuan)
                            {
                                ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                                ShowButtomTsbButton(false, true, true, false, true, true, false, false, true, false, false);
                            }
                            #endregion
                            break;
                        default:
                            break;
                    }

                    height = pnlTopicFace.Height - 140;
                    width = pnlTopicFace.Width;
                    pnlTopic.Size = new Size(width, height + 35);
                    pointPage = pnlPageIndex.Location;
                    pnlOpation.Size = new Size(width, 33);
                    pnlOpation.Location = new Point(pointPage.X, (pointPage.Y - 40));
                    #endregion
                    break;
            }
            #endregion

            #region 加载标题
            if (oCurrTopic.AllowSubTopic)
            {

                lblTopicType.Text = string.Format("{0}：第 {1}/{2} 题", oCurrTopic.TopicTypeName, oCurrTopic.SubTopicId, oCurrTopic.SubTopics.Count);
                lblSubTopicTitle.Text = string.Format("第 {0} 小题", oCurrTopic.SubTopicId);
            }
            else
            {
                lblTopicType.Text = string.Format("{0}：第 {1}/{2} 题", aTopicType.Name, oCurrTopic.TopicNo, aTopicType.Topics.Count);
            }
            #endregion

            #region 答案选中
            switch (oCurrTopic.JudgeEngineId)
            {
                case JudgeEngine.DanXuan:
                    #region 单选题
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        danXuan.rdoA.Checked = false;
                        danXuan.rdoB.Checked = false;
                        danXuan.rdoC.Checked = false;
                        danXuan.rdoD.Checked = false;
                        danXuan.rdoE.Checked = false;
                        danXuan.rdoF.Checked = false;
                    }

                    switch (oCurrTopic.UserAnswer)
                    {
                        case "A":
                            danXuan.rdoA.Checked = true;
                            break;
                        case "B":
                            danXuan.rdoB.Checked = true;
                            break;
                        case "C":
                            danXuan.rdoC.Checked = true;
                            break;
                        case "D":
                            danXuan.rdoD.Checked = true;
                            break;
                        case "E":
                            danXuan.rdoE.Checked = true;
                            break;
                        case "F":
                            danXuan.rdoF.Checked = true;
                            break;
                        default:
                            break;
                    }
                    #endregion
                    break;
                case JudgeEngine.DuoXuan:
                    if (oCurrTopic.AllowPeiWu)
                    {
                        #region 配伍题
                        if (oCurrTopic.HaveUserAnswer == false)
                        {
                            foreach (var item in peiWu.pnlPeiWu.Controls)
                            {
                                if (item is Panel)
                                {
                                    Panel subPanel = item as Panel;
                                    foreach (var subItem in subPanel.Controls)
                                    {
                                        if (subItem is ComboBox)
                                        {
                                            ComboBox comboBox = subItem as ComboBox;
                                            comboBox.SelectedIndex = 0;
                                        }
                                    }
                                }
                            }
                        }
                        listAnswerPeiwu = oCurrTopic.UserAnswer.ToArray().ToList();
                        for (int i = 0; i < listAnswerPeiwu.Count; i++)
                        {
                            if (listAnswerPeiwu[i] == 'Δ') continue;
                            if (listAnswerPeiwu[i] == ' ') continue;

                            panelName = string.Format("panel{0}", i + 1);
                            panelGroup = peiWu.pnlPeiWu.Controls[panelName] as Panel;
                            if (panelGroup != null)
                            {
                                foreach (var item in panelGroup.Controls)
                                {
                                    if (item is ComboBox)
                                    {
                                        ComboBox comboBox = item as ComboBox;
                                        comboBox.Text = listAnswerPeiwu[i].ToString();
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        #region 多选
                        if (oCurrTopic.HaveUserAnswer == false)
                        {
                            duoXuan.cbA.Checked = false;
                            duoXuan.cbB.Checked = false;
                            duoXuan.cbC.Checked = false;
                            duoXuan.cbD.Checked = false;
                            duoXuan.cbE.Checked = false;
                            duoXuan.cbF.Checked = false;
                        }

                        switch (oCurrTopic.UserAnswer)
                        {
                            case "A":
                                duoXuan.cbA.Checked = true;
                                break;
                            case "AB":
                                duoXuan.cbA.Checked = true;
                                duoXuan.cbB.Checked = true;
                                break;
                            case "ABC":
                                duoXuan.cbA.Checked = true;
                                duoXuan.cbB.Checked = true;
                                duoXuan.cbC.Checked = true;
                                break;
                            case "ABCD":
                                duoXuan.cbA.Checked = true;
                                duoXuan.cbB.Checked = true;
                                duoXuan.cbC.Checked = true;
                                duoXuan.cbD.Checked = true;
                                break;
                            case "ABCDE":
                                duoXuan.cbA.Checked = true;
                                duoXuan.cbB.Checked = true;
                                duoXuan.cbC.Checked = true;
                                duoXuan.cbD.Checked = true;
                                duoXuan.cbE.Checked = true;
                                break;
                            case "ABCDEF":
                                duoXuan.cbA.Checked = true;
                                duoXuan.cbB.Checked = true;
                                duoXuan.cbC.Checked = true;
                                duoXuan.cbD.Checked = true;
                                duoXuan.cbE.Checked = true;
                                duoXuan.cbF.Checked = true;
                                break;
                            default:
                                break;
                        }
                        #endregion
                    }
                    break;
                case JudgeEngine.PanDuan:
                    #region 判断题
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        panDuan.rdoRight.Checked = false;
                        panDuan.rdoError.Checked = false;
                    }

                    switch (oCurrTopic.UserAnswer.ToLower())
                    {
                        case "true":
                            panDuan.rdoRight.Checked = true;
                            break;
                        case "false":
                            panDuan.rdoError.Checked = true;
                            break;
                        default:
                            break;
                    }
                    #endregion
                    break;
                case JudgeEngine.TianKong:
                    #region 填空题
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        foreach (var item in tianKong.pnlContainer.Controls)
                        {
                            if (item is Panel)
                            {
                                Panel subPanel = item as Panel;
                                foreach (var subItem in subPanel.Controls)
                                {
                                    if (subItem is TextBox)
                                    {
                                        TextBox textBox = subItem as TextBox;
                                        textBox.Clear();
                                    }
                                }
                            }
                        }
                    }

                    listUserAnswer = oCurrTopic.UserAnswer.Split('♂').ToList();

                    for (int i = 0; i < listUserAnswer.Count; i++)
                    {
                        if (listUserAnswer[i] == "Δ") continue;
                        if (listUserAnswer[i] == "") continue;

                        panelName = string.Format("panel{0}", i + 1);
                        panelGroup = tianKong.pnlContainer.Controls[panelName] as Panel;
                        if (panelGroup != null)
                        {
                            foreach (var item in panelGroup.Controls)
                            {
                                if (item is TextBox)
                                {
                                    TextBox textBox = item as TextBox;
                                    textBox.Text = listUserAnswer[i];
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case JudgeEngine.ZhuGuan:
                    #region 主观题
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        zhuguan.txtZhuGuanTi.Clear();
                    }
                    else
                    {
                        zhuguan.txtZhuGuanTi.Text = oCurrTopic.UserAnswer;
                    }
                    #endregion
                    break;
                case JudgeEngine.AnLiFenXi:
                    #region 案例分析
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        foreach (var item in anLiFenXi.pnlAnLiFenXi.Controls)
                        {
                            if (item is CheckBox)
                            {
                                CheckBox checkBox = item as CheckBox;
                                checkBox.Checked = false;
                            }
                        }
                    }

                    listUserAnswer = oCurrTopic.UserAnswer.Split('♂').ToList();

                    for (int i = 0; i < listUserAnswer.Count; i++)
                    {
                        if (listUserAnswer[i] != "" || listUserAnswer[i] != "Δ")
                        {
                            panelName = string.Format("panel{0}", i + 1);
                            panelGroup = anLiFenXi.pnlAnLiFenXi.Controls[panelName] as Panel;
                            if (panelGroup != null)
                            {
                                foreach (var item in panelGroup.Controls)
                                {
                                    if (item is CheckBox)
                                    {
                                        CheckBox checkBox = item as CheckBox;
                                        bool result = listUserAnswer[i].Contains(checkBox.Text);
                                        if (result)
                                        {
                                            checkBox.Checked = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case JudgeEngine.JiSuanFenXi:
                    #region 计算分析
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        foreach (var item in jiSuanFenXi.pnlContainer.Controls)
                        {
                            if (item is Panel)
                            {
                                Panel subPanel = item as Panel;
                                foreach (var subItem in subPanel.Controls)
                                {
                                    if (subItem is TextBox)
                                    {
                                        TextBox textBox = subItem as TextBox;
                                        textBox.Clear();
                                    }
                                }
                            }
                        }
                    }

                    listUserAnswer = oCurrTopic.UserAnswer.Split('♂').ToList();

                    for (int i = 0; i < listUserAnswer.Count; i++)
                    {
                        if (listUserAnswer[i] == "Δ") continue;
                        if (listUserAnswer[i] == "") continue;

                        panelName = string.Format("panel{0}", i + 1);
                        panelGroup = jiSuanFenXi.pnlContainer.Controls[panelName] as Panel;
                        if (panelGroup != null)
                        {
                            foreach (var item in panelGroup.Controls)
                            {
                                if (item is TextBox)
                                {
                                    TextBox textBox = item as TextBox;
                                    textBox.Text = listUserAnswer[i];
                                }
                            }
                        }
                    }
                    #endregion
                    break;
                case JudgeEngine.Typing:
                    #region 打字题
                    if (oCurrTopic.HaveUserAnswer == false)
                    {
                        txtTyping.Font = new System.Drawing.Font("宋体", FFontSize, FontStyle.Regular);
                        txtTyping.Select(0, txtTyping.Text.Length);
                        txtTyping.SelectionColor = Color.Black;
                        typing.txtTyping.Clear();
                    }
                    else
                    {
                        typing.txtTyping.Text = oCurrTopic.UserAnswer;
                    }
                    #endregion
                    break;
                default:
                    break;
            }
            #endregion

            #region 显示样张
            if (oCurrTopic.SampleDoc != "Δ")
            {
                tsbSimpleDox.Visible = true;
            }
            else
            {
                tsbSimpleDox.Visible = false;
            }
            #endregion

            txtAnalysis.FontSize = FFontSize;
            txtTopicFace.FontSize = FFontSize;
        }

        #region 界面特效
        private void pnlTitle_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                this.pnlTitle.ClientRectangle,
                                Color.FromArgb(85, 136, 187),//7f9db9
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid);

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                this.pnlCompany.ClientRectangle,
                                Color.FromArgb(85, 136, 187),//7f9db9
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid);
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                this.pnlTopTool.ClientRectangle,
                                Color.FromArgb(85, 136, 187),//7f9db9
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid);
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                                this.pnlPageIndex.ClientRectangle,
                                Color.FromArgb(85, 136, 187),//7f9db9
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid,
                                Color.FromArgb(85, 136, 187),
                                1,
                                ButtonBorderStyle.Solid);
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                               this.pnlTopicType.ClientRectangle,
                               Color.FromArgb(85, 136, 187),//7f9db9
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid);
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                               this.pnlOpation.ClientRectangle,
                               Color.FromArgb(85, 136, 187),//7f9db9
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid);
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
                               this.pnlTopic.ClientRectangle,
                               Color.FromArgb(85, 136, 187),//7f9db9
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               0,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               1,
                               ButtonBorderStyle.Solid,
                               Color.FromArgb(85, 136, 187),
                               0,
                               ButtonBorderStyle.Solid);
        }

        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width, this.toolStrip1.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }

        private void tsbCommon_MouseHover(object sender, EventArgs e)
        {
            //ToolStripButton toolStrip = (sender as ToolStripButton);
            //toolStrip.BackColor = Color.Orange;
        }

        private void tsbCommon_MouseLeave(object sender, EventArgs e)
        {
            //ToolStripButton toolStrip = (sender as ToolStripButton);
            //toolStrip.BackColor = Color.Transparent;
        }

        private void tsbCommon_Paint(object sender, PaintEventArgs e)
        {
            //ToolStripButton tsb = (ToolStripButton)sender;  
            //Rectangle rectButton = tsb.Bounds;
            //Point p = toolStrip1.PointToClient(Control.MousePosition);
            //if (rectButton.Contains(p))
            //{
            //    e.Graphics.Clear(Color.Orange);
            //    if (tsb.Image != null)
            //    {
            //        e.Graphics.DrawImage(tsb.Image, new Point((e.ClipRectangle.Width - tsb.Image.Width) / 2, (e.ClipRectangle.Height - tsb.Image.Height) / 2));
            //    }
            //}
        }
        #endregion

        #region 工具栏--上
        /// <summary>
        /// 增大字号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbAddFontSize_Click(object sender, EventArgs e)
        {
            ChangeFontSize(3);
        }
        /// <summary>
        /// 减小字号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbReduceFontSize_Click(object sender, EventArgs e)
        {
            ChangeFontSize(-3);
        }
        /// <summary>
        /// 疑难标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDifficultMark_Click(object sender, EventArgs e)
        {
            string sQuestion;
            TreeNode CurrTopicNode;
            string sUserAnswer, sDiffString, DisplayId;
            int iDiffFlag, iCurrTopicTypeId, iCurrTopicId, iCurrSubTopicId;

            CurrTopicNode = GetCurrTopicNode();

            sQuestion = string.Format("是否【{0}】当前试题的疑难标识？", oCurrTopic.Difficult == true ? "取消" : "设置");
            DialogResult = PublicClass.ShowMessageOKCancel(sQuestion);
            if (DialogResult == DialogResult.OK)
            {
                //设置疑难标识后，更新试卷对象对应试题数据
                oCurrTopic.Difficult = !oCurrTopic.Difficult;
                UpdateTopicObject(oCurrTopic.TopicTypeId, oCurrTopic.TopicId, oCurrTopic.SubTopicId, oCurrTopic);

                //更新考试试卷内试题疑难标识
                iCurrTopicTypeId = int.Parse(oCurrTopic.TopicTypeId);
                iCurrTopicId = int.Parse(oCurrTopic.TopicId);
                iCurrSubTopicId = int.Parse(oCurrTopic.SubTopicId);
                iDiffFlag = oCurrTopic.Difficult == true ? 1 : 0;
                PublicClass.SowerExamPlugn.SetTopicQuest(PublicClass.StudentDir, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), int.Parse(oCurrTopic.SubTopicId), iDiffFlag);

                //更新导航栏中的试题节点信息
                sUserAnswer = GetUserAnswerDisplayValue(oCurrTopic);
                sDiffString = oCurrTopic.Difficult == true ? "（存疑）" : "";
                DisplayId = oCurrTopic.DisplayId < 10 ? string.Format("0{0}", oCurrTopic.DisplayId) : string.Format("{0}", oCurrTopic.DisplayId);

                CurrTopicNode.Text = string.Format("第{0}题{1}{2}", DisplayId, sUserAnswer, sDiffString);
            }
        }
        /// <summary>
        /// 重做本题--上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbResetTopic_Click(object sender, EventArgs e)
        {
            string sInitResult, sDiffString, sNewNodeText, DisplayId;
            TreeNode CurrTopicNode;

            if (publicClass.TopicIsCaoZuoTi(oCurrTopic))
            {
                DialogResult dialogResult = PublicClass.ShowMessageOKCancel("您将放弃本题原有的答题结果，立即重做本题吗？");

                if (dialogResult == DialogResult.OK)
                {
                    publicClass.KillTask();
                    PublicClass.SowerExamPlugn.SetInitVar(PublicClass.StudentDir, false);
                    sInitResult = PublicClass.SowerExamPlugn.InitTopic(PublicClass.StudentDir, PublicClass.ExamSysDir, PublicClass.StudentCode, false, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId));

                    if (sInitResult != "")
                    {
                        CurrTopicNode = GetCurrTopicNode();
                        if (CurrTopicNode != null)
                        {
                            sDiffString = oCurrTopic.Difficult == true ? "（存疑）" : "";
                            DisplayId = oCurrTopic.DisplayId < 10 ? string.Format("0{0}", oCurrTopic.DisplayId) : string.Format("{0}", oCurrTopic.DisplayId);
                            sNewNodeText = string.Format("第{0}题（{1}）{2}", DisplayId, "未答", sDiffString);
                            //更新导航栏中的试题节点信息
                            CurrTopicNode.Text = sNewNodeText;
                            //设置新的定时保存
                            if (tmrSetAnswer.Enabled) tmrSetAnswer.Enabled = false;
                            tmrSetAnswer.Interval = PublicClass.oPaperInfo.ExamInfo.AutoSaveInterval * 60 * 2;
                            tmrSetAnswer.Enabled = true;
                        }

                        PublicClass.ShowMessageOk("当前试题初始化成功，请重新答题。");

                        if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtCaoZuo_ShiWuFangZhen)
                        {
                            PublicClass.SowerExamPlugn.OpenApp(
                                PublicClass.StudentDir,
                                PublicClass.ExamSysDir,
                                oCurrTopic.AppPath,
                                int.Parse(JudgeEngine.ShiWuFangZhen),
                                oCurrTopic.T3Type,
                                oCurrTopic.CallT3Para);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 显示评析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDisplayAnalysis_Click(object sender, EventArgs e)
        {
            if (oCurrTopic.AllowSubTopic)
            {
                if (scSubTopic.Panel2Collapsed == true)
                {
                    scSubTopic.Panel2Collapsed = false;
                    tsbDisplayAnalysis.Text = "隐藏评析";
                }
                else
                {
                    scSubTopic.Panel2Collapsed = true;
                    tsbDisplayAnalysis.Text = "显示评析";
                }
            }
            else
            {
                if (scTopic.Panel2Collapsed == true)
                {
                    scTopic.Panel2Collapsed = false;
                    tsbDisplayAnalysis.Text = "隐藏评析";
                }
                else
                {
                    scTopic.Panel2Collapsed = true;
                    tsbDisplayAnalysis.Text = "显示评析";
                }
            }
        }
        /// <summary>
        /// 单题评分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSingleQuesAnswer_Click(object sender, EventArgs e)
        {
            M_TopicType oTopicType;
            double fTopicScore;
            double fTopicMark;
            string sReturnValue = "";
            string sStandardAnswer = "";
            string sErrorHint = "";
            string sJudgeLevel = "";
            string sTopicTitle = "";
            string sHint = "";
            string sUserAnswer = "";
            Enum_TTopicRealType CurrTopicRealType;
            List<string> listUserAnswer = new List<string>();
            List<string> listStandAnswer = new List<string>();
            StringBuilder sbUserAnswer = new StringBuilder();

            try
            {
                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    #region 加载数据
                    //安全检查
                    CurrTopicRealType = publicClass.GetTopicRealType(oCurrTopic);
                    if (CurrTopicRealType == Enum_TTopicRealType.trtUnknown)
                    {
                        MessageBox.Show("请选择一道操作题。");
                        return;
                    }
                    //获取当前题型
                    oTopicType = GetTopicTypeObject(oCurrTopic.TopicTypeId);
                    //单题评分
                    sReturnValue = PublicClass.SowerExamPlugn.ExamGrade(PublicClass.StudentDir, PublicClass.ExamSysDir, PublicClass.StudentCode, int.Parse(oCurrTopic.TopicTypeId), int.Parse(oCurrTopic.TopicId), oCurrTopic.SubTopicId);
                    fTopicScore = Math.Round(double.Parse(xml.GetXmlNodeValue(sReturnValue, "得分")), 2);
                    fTopicMark = double.Parse(xml.GetXmlNodeValue(sReturnValue, "小题分值"));
                    sStandardAnswer = xml.GetXmlNodeValue(sReturnValue, "标准答案");
                    sErrorHint = xml.GetXmlNodeValue(sReturnValue, "错误报告");
                    sJudgeLevel = xml.GetXmlNodeValue(sReturnValue, "得分评价");
                    //试题标题
                    sTopicTitle = string.Format("【{0}】 第{1}题", oCurrTopic.TopicTypeName, oCurrTopic.TopicNo);
                    if (oTopicType.AllowSubTopic)
                    {
                        sTopicTitle = sTopicTitle + string.Format(" （第{0}小题）", oCurrTopic.SubTopicId);
                    }

                    //评分信息
                    sHint = sTopicTitle + "\n";
                    sHint = sHint + "──────────────────" + "\n";
                    sHint = sHint + string.Format("试题分值：{0} \n", fTopicMark);
                    sHint = sHint + string.Format("本题得分：{0} \n", fTopicScore);
                    sHint = sHint + "──────────────────" + "\n";

                    #region 标准客观题的答案信息
                    if (publicClass.TopicIsKeGuanTi(oCurrTopic))
                    {
                        sUserAnswer = oCurrTopic.UserAnswer;
                        if (sUserAnswer == "Δ" || sUserAnswer == "") sUserAnswer = "(未答)";
                        if (sUserAnswer == "true") sUserAnswer = "正确";
                        if (sUserAnswer == "false") sUserAnswer = "错误";
                        if (sStandardAnswer.ToLower() == "true") sStandardAnswer = "正确";
                        if (sStandardAnswer.ToLower() == "false") sStandardAnswer = "错误";
                        if (sUserAnswer != "Δ" || sUserAnswer != "" && oCurrTopic.AllowPeiWu == true)
                        {
                            sUserAnswer = string.Join("", sUserAnswer.Split(' ').ToArray());
                        }
                        sHint = sHint + string.Format("标准答案：{0}\n用户答案：{1}\n", sStandardAnswer, sUserAnswer);
                    }
                    #endregion

                    #region 案例分析题
                    if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtAnLiFenXi)
                    {
                        sStandardAnswer = sStandardAnswer.Replace("♂", ", ");

                        if (oCurrTopic.UserAnswer.Trim() == "Δ" || oCurrTopic.UserAnswer.Trim() == "")
                        {
                            sbUserAnswer.Append("(未答)");
                        }
                        else
                        {
                            sbUserAnswer.Clear();

                            listUserAnswer = oCurrTopic.UserAnswer.Split('♂').ToList();
                            for (int i = 0; i < listUserAnswer.Count; i++)
                            {
                                if (i > 0) sbUserAnswer.Append(", ");

                                sbUserAnswer.AppendFormat(listUserAnswer[i] != "" ? listUserAnswer[i] : "（未答）");
                            }
                        }
                        sHint = sHint + string.Format("标准答案：{0}\n用户答案：{1}\n", sStandardAnswer, sbUserAnswer.ToString());
                    }
                    #endregion

                    #region 计算分析题
                    if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtJiSuanFenXi)
                    {
                        listStandAnswer = sStandardAnswer.Split('♂').ToList();
                        sStandardAnswer = "";
                        for (int i = 0; i < listStandAnswer.Count; i++)
                        {
                            if (i > 0) sStandardAnswer = sStandardAnswer + string.Format("\n　　　　　");
                            sStandardAnswer = sStandardAnswer + string.Format("（第{0}空） ", i + 1);
                            sStandardAnswer = sStandardAnswer + listStandAnswer[i];
                        }

                        if (oCurrTopic.UserAnswer.Trim() == "Δ" || oCurrTopic.UserAnswer.Trim() == "")
                        {
                            sbUserAnswer.Append("（未答）");
                        }
                        else
                        {
                            sbUserAnswer.Clear();

                            listUserAnswer = oCurrTopic.UserAnswer.Split('♂').ToList();
                            for (int i = 0; i < listUserAnswer.Count; i++)
                            {
                                if (i > 0) sbUserAnswer.Append("\n　　　　　");
                                sbUserAnswer.AppendFormat("（第{0}空） ", i + 1);
                                sbUserAnswer.AppendFormat(listUserAnswer[i] != "" ? listUserAnswer[i] : "（未答）");
                            }
                        }
                        sHint = sHint + string.Format("标准答案：{0}\n用户答案：{1}\n", sStandardAnswer, sbUserAnswer.ToString());
                    }
                    #endregion

                    #region 打字题
                    if (publicClass.TopicIsTyping(oCurrTopic))
                    {
                        if (sErrorHint != "")
                        {
                            sErrorHint = sErrorHint.Replace("$", "\n");
                            sHint = sHint + sErrorHint;
                        }
                    }
                    #endregion

                    #region 操作题
                    if (publicClass.TopicIsCaoZuoTi(oCurrTopic))
                    {
                        if (sErrorHint != "")
                        {
                            sErrorHint = sErrorHint.Replace("$", "\n");
                            sHint = sHint + sErrorHint;
                        }
                    }
                    #endregion

                    #region 填空题
                    if (publicClass.GetTopicRealType(oCurrTopic) == Enum_TTopicRealType.trtTianKong)
                    {
                        listStandAnswer = sStandardAnswer.Split('♂').ToList();
                        sStandardAnswer = "";
                        for (int i = 0; i < listStandAnswer.Count; i++)
                        {
                            if (i > 0) sStandardAnswer = sStandardAnswer + string.Format("\n　　　　　");
                            sStandardAnswer = sStandardAnswer + string.Format("（第{0}空） ", i + 1);
                            sStandardAnswer = sStandardAnswer + listStandAnswer[i];
                        }

                        if (oCurrTopic.UserAnswer.Trim() == "Δ" || oCurrTopic.UserAnswer.Trim() == "")
                        {
                            sbUserAnswer.Append("（未答）");
                        }
                        else
                        {
                            sbUserAnswer.Clear();

                            listUserAnswer = oCurrTopic.UserAnswer.Split('♂').ToList();
                            for (int i = 0; i < listUserAnswer.Count; i++)
                            {
                                if (i > 0) sbUserAnswer.Append("\n　　　　　");
                                sbUserAnswer.AppendFormat("（第{0}空） ", i + 1);
                                sbUserAnswer.AppendFormat(listUserAnswer[i] != "" ? listUserAnswer[i] : "（未答）");
                            }
                        }
                        sHint = sHint + string.Format("标准答案：{0}\n用户答案：{1}\n", sStandardAnswer, sbUserAnswer.ToString());
                    }
                    #endregion

                    #endregion
                }, null);

                MessageBoxEx.Show(this, sHint);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmAnswerSheet), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 试卷评分
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbPaperScore_Click(object sender, EventArgs e)
        {
            if (PublicClass.oPaperInfo.Inited == false)
            {
                return;
            }

            DialogResult resut = PublicClass.ShowMessageOKCancel("是否对当前考生答卷进行评分？？");

            if (resut == DialogResult.OK)
            {
                PublicClass.handPaper = "1";

                PaperScore();
            }
        }
        /// <summary>
        /// 复位题板位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbReset_Click(object sender, EventArgs e)
        {
            switch (formState)
            {
                case 0:
                    int height = Screen.PrimaryScreen.WorkingArea.Height;
                    int width = Screen.PrimaryScreen.WorkingArea.Width;
                    this.Size = new Size(width, height);
                    this.Location = new Point(0, 0);
                    break;
                case 1:
                    this.Size = new Size(500, 500);

                    int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
                    int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
                    this.Location = new Point(x, y);//设置窗体在屏幕右下角显示
                    break;
                case 2:
                    this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, 225);
                    SetFormLocation();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 返回大题版--上
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbReturnBigTopic_Click(object sender, EventArgs e)
        {
            formState = 0;

            switch (oCurrTopic.JudgeEngineId)
            {
                case JudgeEngine.Windows:
                    #region 基本操作题
                    pnlOpation.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, true, false, false, false, true, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, true, false, false, false, true, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, true, false, false, false, true, false, false);
                    }
                    #endregion
                    #endregion
                    break;
                case JudgeEngine.UFIDA:
                    #region 用友题
                    pnlOpation.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, true, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, true, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, true, false, false, false, false, false, false);
                    }
                    #endregion
                    #endregion
                    break;
                case JudgeEngine.OutLook:
                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, false, true, false, false, true, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, false, true, false, false, true, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, false, true, false, false, true, false, false);
                    }
                    #endregion
                    break;
                default:
                    #region 默认题
                    pnlOpation.Visible = false;

                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, false, true);
                            ShowButtomTsbButton(true, true, true, true, true, true, false, false, true, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(true, true, true, false, true, true, true, false, true, true, false, true, true);
                            ShowButtomTsbButton(true, true, true, true, true, true, false, false, true, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(true, true, true, false, false, false, false, false, true, true, false, true, true);
                        ShowButtomTsbButton(true, true, true, true, true, true, false, false, true, false, false);
                    }
                    #endregion
                    #endregion
                    break;
            }

            ShowPanel(true, true, true, true, true, true, false, true);

            pnlTitle.Visible = true;
            pnlNavigation.Visible = true;
            pnlCompany.Visible = true;
            pnlTopicFace.Dock = DockStyle.None;

            pnlTopicFace.BackColor = Color.Transparent;
            pnlContainer.BackColor = Color.FromArgb(225, 240, 255);
            pnlTopic.Height = pnlTopic.Height - 7;
            Point pointTopic = pnlTopic.Location;
            pnlTopic.Location = new Point(pointTopic.X, pointTopic.Y + 7);
            Point pointTopicType = pnlTopicType.Location;
            pnlTopicType.Location = new Point(pointTopicType.X, pointTopicType.Y + 7);

            pnlTopicFace.Size = topicFaceSize;
            pnlTopicFace.Location = topicFacePoint;

            int height = Screen.PrimaryScreen.WorkingArea.Height;
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Size = new Size(width, height);
            this.Location = new Point(0, 0);
            this.TopMost = false;
        }
        /// <summary>
        /// 缩小题版--计算机--实操题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbLessenTopicFace_Click(object sender, EventArgs e)
        {
            formState = 1;

            switch (oCurrTopic.JudgeEngineId)
            {
                case JudgeEngine.Windows:
                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);
                            ShowButtomTsbButton(false, true, true, false, true, false, false, false, true, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);
                            ShowButtomTsbButton(false, true, true, false, true, false, false, false, true, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, true);

                        ShowButtomTsbButton(false, true, true, false, true, false, false, false, true, false, false);
                    }
                    #endregion
                    break;
                case JudgeEngine.UFIDA:
                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);
                            ShowButtomTsbButton(false, true, true, false, true, false, false, false, false, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);
                            ShowButtomTsbButton(false, true, true, false, true, false, false, false, false, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);
                        ShowButtomTsbButton(false, true, true, false, true, false, false, false, false, false, false);
                    }
                    #endregion
                    break;
                case JudgeEngine.OutLook:
                    #region 题库
                    if (PublicClass.JobType == JobType.TiKu)
                    {
                        if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                        {
                            ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                            ShowButtomTsbButton(false, true, true, false, false, true, false, false, true, false, false);
                        }
                        else
                        {
                            ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);

                            ShowButtomTsbButton(false, true, true, false, false, true, false, false, true, false, false);
                        }
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                        ShowButtomTsbButton(false, true, true, false, false, true, false, false, true, false, false);
                    }
                    #endregion
                    break;
                default:
                    #region 题库
                    if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                    {
                        ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                        ShowButtomTsbButton(false, true, true, false, true, true, false, false, true, false, false);
                    }
                    else
                    {
                        ShowTopTsbButton(false, false, false, false, false, true, false, true, true, false, false, false, false);

                        ShowButtomTsbButton(false, true, true, false, true, true, false, false, true, false, false);
                    }
                    #endregion

                    #region 作业
                    if (PublicClass.JobType == JobType.ShiJuan)
                    {
                        ShowTopTsbButton(false, false, false, false, false, false, false, true, true, false, false, false, false);

                        ShowButtomTsbButton(false, true, true, false, true, true, false, false, true, false, false);
                    }
                    #endregion
                    break;
            }

            ShowPanel(false, false, false, true, true, true, false, true);

            pnlTopicFace.Dock = DockStyle.Fill;
            pnlTopicFace.BackColor = Color.FromArgb(225, 240, 255);
            pnlContainer.BackColor = Color.Transparent;
            pnlTopic.Height = pnlTopic.Height + 7;
            Point pointTopic = pnlTopic.Location;
            pnlTopic.Location = new Point(pointTopic.X, pointTopic.Y - 7);
            Point pointTopicType = pnlTopicType.Location;
            pnlTopicType.Location = new Point(pointTopicType.X, pointTopicType.Y - 7);

            this.Size = new Size(500, 500);
            int x = Screen.PrimaryScreen.WorkingArea.Right - this.Width;
            int y = Screen.PrimaryScreen.WorkingArea.Bottom - this.Height;
            this.Location = new Point(x, y);    //设置窗体在屏幕右下角显示
            this.TopMost = true;
        }
        /// <summary>
        /// 缩小题板--会计--实务仿真
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbLessenTopicFace2_Click(object sender, EventArgs e)
        {
            formState = 2;

            #region 题库
            if (PublicClass.JobType == JobType.TiKu)
            {
                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                {
                    ShowTopTsbButton(false, false, false, false, false, false, false, false, false, false, false, false, false);

                    ShowButtomTsbButton(false, true, true, false, true, false, false, true, false, true, true);
                }
                else
                {
                    ShowTopTsbButton(false, false, false, false, false, false, false, false, false, false, false, false, false);

                    ShowButtomTsbButton(false, true, true, false, true, false, false, true, true, true, true);
                }
            }
            #endregion

            #region 作业
            if (PublicClass.JobType == JobType.ShiJuan)
            {
                ShowTopTsbButton(false, false, false, false, false, false, false, false, false, false, false, false, false);

                ShowButtomTsbButton(false, true, true, false, true, false, false, true, true, true, true);
            }
            #endregion

            ShowPanel(false, false, false, false, true, true, false, true);

            pnlTopic.Location = new Point(topicLocation.X, topicLocation.Y - 40);
            pnlTopicType.Location = new Point(topicTypeLocation.X, topicTypeLocation.Y - 40);

            pnlTopicFace.Dock = DockStyle.Fill;
            pnlTopicFace.BackColor = Color.FromArgb(225, 240, 255);
            pnlContainer.BackColor = Color.Transparent;

            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, 225);
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;

            pnlTopic.Height = 100;

            SetFormLocation();
        }
        /// <summary>
        /// 备份作业
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbBackupJob_Click(object sender, EventArgs e)
        {
            var studentDir = Path.GetFileName(PublicClass.StudentDir);
            var backupFileName = studentDir;
            var dialogResult = DialogResult.OK;

            sfdExportPaper.FileName = string.Format("{0}.zip", backupFileName);
            dialogResult = sfdExportPaper.ShowDialog();
            if (dialogResult == DialogResult.Cancel) return;
            backupFileName = sfdExportPaper.FileName;
            CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
            {
                #region 初始化作业信息
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "RealName", PublicClass.oMyJob.RealName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ShowScore", PublicClass.oMyJob.ShowScore);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ShowAnalysis", PublicClass.oMyJob.ShowAnalysis);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ExamMode", PublicClass.oMyJob.ExamMode);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "CreateTime", PublicClass.oMyJob.CreateTime);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsAllowReSubmitScore", PublicClass.oMyJob.IsAllowReSubmitScore);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "AllowReSubmitScoreCount", PublicClass.oMyJob.AllowReSubmitScoreCount.ToString());
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "HWSubmitTimeType", PublicClass.oMyJob.HWSubmitTimeType);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "CourseName", PublicClass.oMyJob.CourseName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ChapterName", PublicClass.oMyJob.ChapterName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "HWName", PublicClass.oMyJob.HWName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "EnvFileName", PublicClass.oMyJob.EnvFileName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "FileName", PublicClass.oMyJob.FileName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "FilePath", PublicClass.oMyJob.FilePath);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsEnable", PublicClass.oMyJob.IsEnable);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsScoreToCenter", PublicClass.oMyJob.IsScoreToCenter);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ManagerName", PublicClass.oMyJob.ManagerName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsCaculateTime", PublicClass.oMyJob.IsCaculateTime);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ClassName", PublicClass.oMyJob.ClassName);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "SpecialtyID", PublicClass.oMyJob.SpecialtyID);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "CityID", PublicClass.oMyJob.CityID);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "PublicUserID", PublicClass.oMyJob.PublicUserID);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ExamStartDateTime", PublicClass.oMyJob.ExamStartDateTime);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ExamEndDateTime", PublicClass.oMyJob.ExamEndDateTime);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsPay", PublicClass.oMyJob.IsPay);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsSingleGrade", PublicClass.oMyJob.IsSingleGrade);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "JobDownLoadState", PublicClass.oMyJob.JobDownLoadState);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ScoreSubmitted", PublicClass.oMyJob.ScoreSubmitted);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "ClassID", PublicClass.oMyJob.ClassID.ToString());
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "AccountDownLoadState", PublicClass.oMyJob.AccountDownLoadState);
                PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "作业信息", "IsUploadAnswerFile", PublicClass.oMyJob.IsUploadAnswerFile);
                #endregion
                ZipFileTools.ZipFileFromDirectorySZL(PublicClass.StudentDir, backupFileName, 6, "");
            }, null);
        }
        /// <summary>
        /// 退出练习
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExit_Click(object sender, EventArgs e)
        {
            try
            {
                //题库退出
                if (PublicClass.JobType == JobType.TiKu)
                {
                    DialogResult result = PublicClass.ShowMessageOKCancel("确定要退出练习吗？");
                    if (result == DialogResult.OK)
                    {
                        tmrExamTimeCountDownTimer.Stop();
                        SaveResidualExamTime();
                        SaveUserAnswer();
                        //删除答案
                        publicClass.ClearDirectory(new DirectoryInfo(PublicClass.StudentDir));
                        if (Globals.IsOnline)
                        {
                            ComputerExam.BusicWork.frmBusicWorkMain busicWorkMain = new ComputerExam.BusicWork.frmBusicWorkMain();
                            busicWorkMain.Show();
                            this.Close();
                        }
                        else
                        {
                            ComputerExam.BusicWorkOffLine.frmBusicWorkMain busicWorkMain = new ComputerExam.BusicWorkOffLine.frmBusicWorkMain();
                            busicWorkMain.Show();
                            this.Close();
                        }
                    }
                }
                //作业退出
                if (PublicClass.JobType == JobType.ShiJuan)
                {
                    DialogResult result = PublicClass.ShowMessageOKCancel("确定要退出练习吗？");
                    if (result == DialogResult.OK)
                    {
                        tmrExamTimeCountDownTimer.Stop();
                        SaveResidualExamTime();
                        SaveUserAnswer();
                        //删除答案
                        publicClass.ClearDirectory(new DirectoryInfo(PublicClass.StudentDir));
                        if (Globals.IsOnline)
                        {
                            ComputerExam.BusicWork.frmBusicWorkMain busicWorkMain = new ComputerExam.BusicWork.frmBusicWorkMain();
                            busicWorkMain.Show();
                            this.Close();
                        }
                        else
                        {
                            ComputerExam.BusicWorkOffLine.frmBusicWorkMain busicWorkMain = new ComputerExam.BusicWorkOffLine.frmBusicWorkMain();
                            busicWorkMain.Show();
                            this.Close();
                        }
                    }
                }
                tmrTotalUserTime.Stop();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmAnswerSheet), ex.Message);
            }
        }
        /// <summary>
        /// 交卷退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbHandPaper_Click(object sender, EventArgs e)
        {
            string sHint = "";
            int iTotalNotAnswerTopic = 0;
            int iTotalTopicDifficult = 0;
            int iNotAnswerSum = 0;
            int iDifficultSum = 0;
            DialogResult dialogResult;
            M_Topic aTopic = new M_Topic();

            #region //统计未答试题总量、疑难试题总量与整卷试题总量
            for (int i = 0; i < PublicClass.oPaperInfo.TopicTypes.Count; i++)
            {
                iNotAnswerSum = 0;
                iDifficultSum = 0;
                for (int j = 0; j < PublicClass.oPaperInfo.TopicTypes[i].Topics.Count; j++)
                {
                    aTopic = PublicClass.oPaperInfo.TopicTypes[i].Topics[j];
                    if (!aTopic.HaveUserAnswer) iNotAnswerSum++;
                    if (aTopic.Difficult) iDifficultSum++;
                }
                iTotalNotAnswerTopic += iNotAnswerSum;
                iTotalTopicDifficult += iDifficultSum;
            }
            #endregion

            #region //如仍有未答试题进行提示
            if (iTotalNotAnswerTopic > 0)
            {
                sHint = string.Format("{0}，当前试卷中仍有 {1} 题未作答，您确认要交卷退场吗？", PublicClass.oSubjectProp.StudentName, iTotalNotAnswerTopic);
                dialogResult = PublicClass.ShowMessageOKCancel(sHint);
                if (dialogResult == DialogResult.Cancel) return;
            }
            #endregion

            #region //如仍有疑难试题进行提示
            if (iTotalTopicDifficult > 0)
            {
                sHint = string.Format("{0}，当前试卷中仍有 {1} 题标识为疑难，您确认要交卷退场吗？", PublicClass.oSubjectProp.StudentName, iTotalTopicDifficult);
                dialogResult = PublicClass.ShowMessageOKCancel(sHint);
                if (dialogResult == DialogResult.Cancel) return;
            }
            #endregion

            #region //退出提示
            if (PublicClass.JobType == JobType.TiKu)
            {
                sHint = "您确认要提交练习成绩吗？";
                dialogResult = PublicClass.ShowMessageOKCancel(sHint);
                if (dialogResult == DialogResult.Cancel) return;
            }

            if (PublicClass.JobType == JobType.ShiJuan)
            {
                sHint = "您确认要提交作业成绩吗？";
                dialogResult = PublicClass.ShowMessageOKCancel(sHint);
                if (dialogResult == DialogResult.Cancel) return;
            }
            #endregion

            #region //检测是否自己的试卷
            string studentCode = PublicClass.SowerExamPlugn.GetParaValue(PublicClass.StudentDir, "考生信息", "考生考号");
            if (studentCode != PublicClass.StudentCode)
            {
                PublicClass.ShowErrorMessageOk("请上交自己的试卷成绩！\n按【确定】退出系统。");
                //删除考生试卷
                PublicClass.SowerExamPlugn.foDeleteFolder(PublicClass.StudentDir);
                Environment.Exit(0);
                return;
            }
            #endregion

            //保存考试时间
            SaveResidualExamTime();
            //保存用户答案
            SaveUserAnswer();
            //停止计时
            tmrExamTimeCountDownTimer.Stop();
            tmrTotalUserTime.Stop();
            PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考生状态", "正在交卷");
            PublicClass.SowerExamPlugn.SetParaValue(PublicClass.StudentDir, "考生信息", "考试结束时间", DateTime.Now.ToString());
            //停止考试，转向评分页面
            publicClass.KillTask();
            PublicClass.handPaper = "2";
            //试卷评分并提交成绩
            PaperScore();
            //返回主窗体
            frmBusicWorkMain busicWorkMain = new frmBusicWorkMain();
            busicWorkMain.Show();
            this.Close();
        }
        #endregion

        #region 工具栏--下
        /// <summary>
        /// 上一题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbLastTopic_Click(object sender, EventArgs e)
        {
            Index = int.Parse(oCurrTopic.TopicNo);
            Index = Index - 1;
            TopicTypeId = oCurrTopic.TopicTypeId;
            tmrShowTopic.Start();
            //ShowTopicByTopicNo(Index, oCurrTopic.TopicTypeId);
        }
        /// <summary>
        /// 下一题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbNextTopic_Click(object sender, EventArgs e)
        {
            Index = int.Parse(oCurrTopic.TopicNo);
            Index = Index + 1;
            TopicTypeId = oCurrTopic.TopicTypeId;
            tmrShowTopic.Start();
            //ShowTopicByTopicNo(Index, oCurrTopic.TopicTypeId);
        }
        /// <summary>
        /// 第一题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbFirst_Click(object sender, EventArgs e)
        {
            string topicNo = PublicClass.oPaperInfo.TopicTypes.Find(t => t.Id == oCurrTopic.TopicTypeId).Topics[0].TopicNo;
            Index = int.Parse(topicNo);
            TopicTypeId = oCurrTopic.TopicTypeId;
            tmrShowTopic.Start();
        }
        /// <summary>
        /// 最后一题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbEndTopic_Click(object sender, EventArgs e)
        {
            if (oCurrTopic.AllowSubTopic)
            {
                int topicCount = PublicClass.oPaperInfo.TopicTypes.Find(t => t.Id == oCurrTopic.TopicTypeId).Topics.Count;
                string topicNo = PublicClass.oPaperInfo.TopicTypes.Find(t => t.Id == oCurrTopic.TopicTypeId).Topics[topicCount - 1].TopicNo;
                Index = int.Parse(topicNo);
                TopicTypeId = oCurrTopic.TopicTypeId;
                tmrShowTopic.Start();
            }
            else
            {
                int topicCount = PublicClass.oPaperInfo.TopicTypes.Find(t => t.Id == oCurrTopic.TopicTypeId).Topics.Count;
                string topicNo = PublicClass.oPaperInfo.TopicTypes.Find(t => t.Id == oCurrTopic.TopicTypeId).Topics[topicCount - 1].TopicNo;
                Index = int.Parse(topicNo);
                TopicTypeId = oCurrTopic.TopicTypeId;
                tmrShowTopic.Start();
            }
        }
        /// <summary>
        /// 考生文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbExamFolder_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start(oCurrTopic.AppPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 打开应用程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbOpenApplication_Click(object sender, EventArgs e)
        {
            #region 打开应用程序
            try
            {
                switch (oCurrTopic.JudgeEngineId)
                {
                    case JudgeEngine.Word:
                        OpenApplication(int.Parse(JudgeEngine.Word));
                        break;
                    case JudgeEngine.Excel:
                        OpenApplication(int.Parse(JudgeEngine.Excel));
                        break;
                    case JudgeEngine.PowerPoint:
                        OpenApplication(int.Parse(JudgeEngine.PowerPoint));
                        break;
                    case JudgeEngine.Internet:
                        OpenApplication(int.Parse(JudgeEngine.Internet));
                        break;
                    case JudgeEngine.OutLook:
                        //OpenApplication(int.Parse(JudgeEngine.Email));
                        //PublicClass.SowerExamPlugn.OpenApp(
                        //    PublicClass.StudentDir,
                        //    PublicClass.ExamSysDir,
                        //    string.Format(@"{0}System\OutlookExpress.exe", PublicClass.ExamSysDir),
                        //    int.Parse(oCurrTopic.JudgeEngineId), "", "");

                        PublicClass.SowerExamPlugn.OpenApp(
                            PublicClass.StudentDir,
                            PublicClass.ExamSysDir,
                            oCurrTopic.AppPath,
                            int.Parse(oCurrTopic.JudgeEngineId), "", "");
                        break;
                    case JudgeEngine.Access:
                        OpenApplication(int.Parse(JudgeEngine.Access));
                        break;
                    case JudgeEngine.VisualFoxPro:
                        OpenApplication(int.Parse(JudgeEngine.VisualFoxPro));
                        break;
                    case JudgeEngine.VisualBasic:
                        OpenApplication(int.Parse(JudgeEngine.VisualBasic));
                        break;
                    case JudgeEngine.C:
                        OpenApplication(int.Parse(JudgeEngine.C));
                        break;
                    case JudgeEngine.JAVA:
                        Process.Start(@"C:\netbeans-ncre2007\bin\nbncre.exe");
                        break;
                    default:
                        break;
                }
                SetTreeViewText("已答");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            #endregion
        }
        /// <summary>
        /// 保存打字题答案
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSave_Click(object sender, EventArgs e)
        {
            oCurrTopic.Changed = true;
            Index = int.Parse(oCurrTopic.TopicNo);

            SaveUserAnswer();
        }
        /// <summary>
        /// 答题--会计--实务仿真
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbDaTi_Click(object sender, EventArgs e)
        {
            PublicClass.SowerExamPlugn.OpenApp(
                        PublicClass.StudentDir, PublicClass.ExamSysDir,
                        oCurrTopic.AppPath, int.Parse(JudgeEngine.ShiWuFangZhen), oCurrTopic.T3Type, oCurrTopic.CallT3Para);
        }
        /// <summary>
        /// 重做本题--下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbReseTopic_Click(object sender, EventArgs e)
        {
            tsbResetTopic_Click(this, e);
        }
        /// <summary>
        /// 返回大题板--下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbReturnBigTopic2_Click(object sender, EventArgs e)
        {
            formState = 0;

            #region 题库
            if (PublicClass.JobType == JobType.TiKu)
            {
                if (PublicClass.oPaperInfo.ExamInfo.ExamMode == "2")
                {
                    ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, true, false, true);

                    ShowButtomTsbButton(false, true, true, false, true, false, false, true, false, true, true);
                }
                else
                {
                    ShowTopTsbButton(true, true, true, false, true, true, true, false, true, false, true, true, true);

                    ShowButtomTsbButton(true, true, true, true, true, false, false, true, true, false, false);
                }
            }
            #endregion

            #region 作业
            if (PublicClass.JobType == JobType.ShiJuan)
            {
                ShowTopTsbButton(true, true, true, false, false, false, false, false, true, false, true, true, true);

                ShowButtomTsbButton(false, true, true, false, true, false, false, true, false, true, true);
            }
            #endregion

            ShowPanel(true, true, true, true, true, true, false, true);

            pnlTopic.Height = topicHeight - 300;

            Point pointTopic = pnlTopic.Location;
            pnlTopic.Location = new Point(pointTopic.X, pointTopic.Y + 40);
            Point pointTopicType = pnlTopicType.Location;
            pnlTopicType.Location = new Point(pointTopicType.X, pointTopicType.Y + 40);

            pnlTopicFace.Size = topicFaceSize;
            pnlTopicFace.Location = topicFacePoint;

            pnlTopicFace.Dock = DockStyle.None;
            pnlTopicFace.BackColor = Color.Transparent;
            pnlContainer.BackColor = Color.FromArgb(225, 240, 255);

            int height = Screen.PrimaryScreen.WorkingArea.Height;
            int width = Screen.PrimaryScreen.WorkingArea.Width;
            this.Size = new Size(width, height);
            this.Location = new Point(0, 0);
            this.TopMost = false;
        }
        /// <summary>
        /// 还原答题界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbResetUI_Click(object sender, EventArgs e)
        {
            tsbDaTi_Click(this, e);
        }
        /// <summary>
        /// 查看样张
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsbSimpleDox_Click(object sender, EventArgs e)
        {
            string sSampleDoc = string.Format("{0}{1}", oCurrTopic.AppPath, oCurrTopic.SampleDoc);

            if (File.Exists(sSampleDoc))
            {
                Process.Start(sSampleDoc);
            }
        }
        #endregion
    }
}
