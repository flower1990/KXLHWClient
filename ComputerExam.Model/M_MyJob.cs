using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerExam.Model
{
    public partial class M_MyJob
    {
        public string ID { get; set; }
        /// <summary>
        /// 作业编号
        /// </summary>
        public string HWID { get; set; }
        /// <summary>
        /// 科目编号
        /// </summary>
        public string CourseID { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 是否显示成绩
        /// </summary>
        public string ShowScore { get; set; }
        /// <summary>
        /// 是否显示评析
        /// </summary>
        public string ShowAnalysis { get; set; }
        /// <summary>
        /// 考试模式
        /// </summary>
        public string ExamMode { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 考试开始时间
        /// </summary>
        public string ExamStartDateTime { get; set; }
        /// <summary>
        /// 考试结束时间
        /// </summary>
        public string ExamEndDateTime { get; set; }
        /// <summary>
        /// 考试时间
        /// </summary>
        public string TotalExamTime { get; set; }
        /// <summary>
        /// 是否允许提交成绩
        /// </summary>
        public string IsAllowReSubmitScore { get; set; }
        /// <summary>
        /// 允许提交成绩次数
        /// </summary>
        public int AllowReSubmitScoreCount { get; set; }
        public int SubmittedCount { get; set; }
        /// <summary>
        /// 计时模式
        /// </summary>
        public string HWSubmitTimeType { get; set; }
        /// <summary>
        /// 科目名称
        /// </summary>
        public string CourseName { get; set; }
        /// <summary>
        /// 作业名称
        /// </summary>
        public string HWName { get; set; }
        /// <summary>
        /// 作业文件名称
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 作业路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public string IsEnable { get; set; }
        /// <summary>
        /// 是否上传成绩到中心
        /// </summary>
        public string IsScoreToCenter { get; set; }
        /// <summary>
        /// 管理员
        /// </summary>
        public string ManagerName { get; set; }
        /// <summary>
        /// 是否计时
        /// </summary>
        public string IsCaculateTime { get; set; }
        public int ChapterID { get; set; }
        public string ChapterName { get; set; }
        public int ClassID { get; set; }
        public string ClassName { get; set; }
        public string SpecialtyID { get; set; }
        public string CityID { get; set; }
        public string PublicUserID { get; set; }
        public string IsPay { get; set; }
        public string IsSingleGrade { get; set; }
        public string FileID { get; set; }
        public string ScoreSubmitted { get; set; }
        public string StudentName { get; set; }
        public string StudentCode { get; set; }
        public string JobDownLoadState { get; set; }
        public string AccountDownLoadState { get; set; }
        public string JobSubmitState { get; set; }
        public string JobNo { get; set; }
        public string EnvFileName { get; set; }
        public string HWIntro { get; set; }
        public int TermID { get; set; }
        public string TermName { get; set; }
        public string EnvFilePath { get; set; }
        public string IsUpload { get; set; }
        public string RequireEnvFile { get; set; }
        public string IsUploadAnswerFile { get; set; }
    }
}
