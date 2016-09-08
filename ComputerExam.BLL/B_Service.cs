using ComputerExam.DAL;
using ComputerExam.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ComputerExam.BLL
{
    public class B_Service
    {
        D_Service dal = new D_Service();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="message">验证信息</param>
        /// <returns></returns>
        public string GetUserInfo(string userName, string password, out string message)
        {
            return dal.GetUserInfo(userName, password, out message);
        }
        /// <summary>
        /// 获取学生科目数据
        /// </summary>
        /// <param name="studentCode">学生编号</param>
        /// <param name="message">验证信息</param>
        /// <returns></returns>
        public List<M_MyJobSubject> GetExamSubject(string studentCode, out string message)
        {
            return dal.GetExamSubject(studentCode, out message);
        }
        /// <summary>
        /// 获取学生作业数据
        /// </summary>
        /// <param name="studentCode"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="dataType"></param>
        /// <param name="fileHost"></param>
        /// <returns></returns>
        public List<M_MyJob> GetMyJob(string studentCode, string startTime, string endTime, int dataType, out string fileHost)
        {
            return dal.GetMyJob(studentCode, startTime, endTime, dataType, out fileHost);
        }
        /// <summary>
        /// 上传作业成绩
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="examSubjectID">科目编号</param>
        /// <param name="scoreDetail">成绩</param>
        /// <returns></returns>
        public string UploadJobScore(string userID, M_MyJob job, string scoreDetail, out string uploadResult)
        {
            return dal.UploadJobScore(userID, job, scoreDetail, out uploadResult);
        }
        /// <summary>
        /// 上传练习成绩
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="examSubjectID">科目编号</param>
        /// <param name="scoreDetail">成绩</param>
        /// <returns></returns>
        public string UploadExerciseScore(string userID, string scoreDetail, out string uploadResult)
        {
            return dal.UploadExerciseScore(userID, scoreDetail, out uploadResult);
        }
        /// <summary>
        /// 获取作业成绩
        /// </summary>
        /// <param name="studentCode">学生编号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="dataType">1.查询作业成绩 2.查询历史成绩</param>
        /// <returns></returns>
        public List<M_JobScore> GetJobScore(string studentCode, string startTime, string endTime, int dataType)
        {
            return dal.GetJobScore(studentCode, startTime, endTime, dataType);
        }
        /// <summary>
        /// 作业得分详情
        /// </summary>
        /// <param name="studentCode"></param>
        /// <param name="hwId"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public string GetJobDetailScore(string studentCode, string hwId, int dataType)
        {
            return dal.GetJobDetailScore(studentCode, hwId, dataType);
        }
        /// <summary>
        /// 获取题库列表
        /// </summary>
        /// <param name="studentCode">题库名称</param>
        /// <param name="startTime">题库编码</param>
        /// <param name="endTime">开始时间</param>
        /// <param name="dataType">结束时间</param>
        /// <returns>题库列表</returns>
        public List<M_TopicDB> GetTopicDBList(string TopicDBName, string TopicDBCode, string InStartTime, string InEndTime)
        {
            return dal.GetTopicDBList(TopicDBName, TopicDBCode, InStartTime, InEndTime);
        }
        /// <summary>
        /// 获取资源数据
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="resourceType"></param>
        /// <param name="resourceModel"></param>
        /// <param name="InStartTime"></param>
        /// <param name="InEndTime"></param>
        /// <param name="fileHost"></param>
        /// <returns></returns>
        public List<M_Resource> GetResource(string resourceName, string resourceType, string resourceModel, string InStartTime, string InEndTime, out string fileHost)
        {
            return dal.GetResource(resourceName, resourceType, resourceModel, InStartTime, InEndTime, out fileHost);
        }
        /// <summary>
        /// 记录练习开始、结束时间
        /// </summary>
        /// <param name="recType">1为开始答题记录 2为交卷记录</param>
        /// <param name="studentCode">学生编号</param>
        /// <param name="hwId">作业编号</param>
        /// <param name="hwName">作业名称</param>
        /// <returns></returns>
        public string RecordingTime(int recType, string studentCode, string hwId, string hwName)
        {
            return dal.RecordingTime(recType, studentCode, hwId, hwName);
        }
        /// <summary>
        /// 记录练习开始、结束时间
        /// </summary>
        /// <param name="studentCode">学生编号</param>
        /// <returns></returns>
        public string ExitSystem(string studentCode)
        {
            return dal.ExitSystem(studentCode);
        }
        /// <summary>
        /// 记录练习开始、结束时间
        /// </summary>
        /// <param name="recType">1为开始答题记录 2为交卷记录</param>
        /// <param name="studentCode">学生编号</param>
        /// <param name="hwId">作业编号</param>
        /// <param name="hwName">作业名称</param>
        /// <returns></returns>
        public string WriteLog(string studentCode, string logData)
        {
            return dal.WriteLog(studentCode, logData);
        }
        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <returns></returns>
        public List<M_ResourceType> GetResourceType()
        {
            return dal.GetResourceType();
        }
        /// <summary>
        /// 上传试卷
        /// </summary>
        /// <returns></returns>
        public void UploadExamFile(string studentCode, M_MyJob job, string filePath)
        {
            dal.UploadExamFile(studentCode, job, filePath);
        }
         /// <summary>
        /// 检查题库是否有效
        /// </summary>
        /// <returns></returns>
        public string ExistsTopicDB(string topicdbcode, string topicdbversion)
        {
            return dal.ExistsTopicDB(topicdbcode, topicdbversion);
        }
        /// <summary>
        /// 检查作业文件是否有效
        /// </summary>
        /// <returns></returns>
        public string ExistsHWFile(string hwid, string topicdbcode, string topicdbversion)
        {
            return dal.ExistsHWFile(hwid, topicdbcode, topicdbversion);
        }
    }
}
