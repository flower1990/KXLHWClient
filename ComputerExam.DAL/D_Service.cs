using ComputerExam.Model;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ComputerExam.DAL
{
    public class D_Service
    {
        string errorMessage = "服务器验证不通过，请联系管理员！";
        XmlUnit xmlUnit = new XmlUnit();
        PublicClass publicClass = new PublicClass();

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="message">验证信息</param>
        /// <returns></returns>
        public string GetUserInfo(string userName, string password, out string message)
        {
            string result = string.Empty;
            string rXml = string.Empty;
            string realName = string.Empty;
            StringBuilder sb = new StringBuilder();

            sb.AppendFormat("<StudentCode>{0}</StudentCode>", userName);
            sb.AppendFormat("<Password>{0}</Password>", DES.EncryStrHexUTF8(password, userName));

            rXml = publicClass.ReturnRequest(sb.ToString(), Globals.CODE_Login);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_Login);

            if (publicClass.IsRight(result))
            {
                realName = xmlUnit.GetXmlNodeValue(result, "RealName");
                message = xmlUnit.GetXmlNodeValue(result, "exsm");
            }
            else
            {
                message = errorMessage;
            }
            return realName;
        }
        /// <summary>
        /// 获取学生科目数据
        /// </summary>
        /// <param name="studentCode">学生编号</param>
        /// <param name="message">验证信息</param>
        /// <returns></returns>
        public List<M_MyJobSubject> GetExamSubject(string studentCode, out string message)
        {
            string rXml = "";
            string result = "";
            StringBuilder sb = new StringBuilder();
            List<M_MyJobSubject> list = new List<M_MyJobSubject>();

            sb.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);

            rXml = publicClass.ReturnRequest(sb.ToString(), Globals.CODE_Subject);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_Subject);

            if (publicClass.IsRight(result))
            {
                list = XmlHelper.XmlToObjList<M_MyJobSubject>(result, "body");
                message = xmlUnit.GetXmlNodeValue(result, "exsm");
            }
            else
            {
                message = errorMessage;
            }

            return list;
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
            string result = string.Empty;
            string rXml = string.Empty;
            string message = string.Empty;
            List<M_MyJob> listHomeWork = new List<M_MyJob>();

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);
            sb.AppendFormat("<StartTime>{0}</StartTime>", startTime);
            sb.AppendFormat("<EndTime>{0}</EndTime>", endTime);
            sb.AppendFormat("<DataType>{0}</DataType>", dataType);

            rXml = publicClass.ReturnRequest(sb.ToString(), Globals.CODE_HomeWork);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_HomeWork);

            if (publicClass.IsRight(result))
            {
                listHomeWork = XmlHelper.XmlToObjList<M_MyJob>(result, "body");
                message = xmlUnit.GetXmlNodeValue(result, "exsm");
                fileHost = xmlUnit.GetXmlNodeValue(result, "FileHost");
            }
            else
            {
                message = errorMessage;
                fileHost = string.Empty;
            }

            return listHomeWork;
        }
        /// <summary>
        /// 上传作业成绩
        /// </summary>
        /// <param name="studentCode"></param>
        /// <param name="examSubjectID">科目编号</param>
        /// <param name="scoreDetail">成绩</param>
        /// <returns></returns>
        public string UploadJobScore(string studentCode, M_MyJob job, string scoreDetail, out string uploadResult)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();
            List<M_MyJob> listHomeWork = new List<M_MyJob>();

            sbParam.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);
            sbParam.AppendFormat("<ClassID>{0}</ClassID>", job.ClassID);
            sbParam.AppendFormat("<ClassName>{0}</ClassName>", job.ClassName);
            sbParam.AppendFormat("<FileID>{0}</FileID>", job.FileID);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), scoreDetail, Globals.CODE_UploadHWScore);
            result = Globals.SERVICE.examonline(DES.EncryStrHexUTF8(rXml, "sower"), Globals.CODE_UploadHWScore);

            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
                message = xmlUnit.GetXmlNodeValue(result, "exsm");
            }
            else
            {
                message = errorMessage;
            }

            uploadResult = message;
            return state;
        }
        /// <summary>
        /// 上传练习成绩
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="examSubjectID">科目编号</param>
        /// <param name="scoreDetail">成绩</param>
        /// <returns></returns>
        public string UploadExerciseScore(string studentCode, string scoreDetail, out string uploadResult)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();
            List<M_MyJob> listHomeWork = new List<M_MyJob>();

            sbParam.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), scoreDetail, Globals.CODE_UploadExScore);
            result = Globals.SERVICE.examonline(DES.EncryStrHexUTF8(rXml, "sower"), Globals.CODE_UploadExScore);

            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
                message = xmlUnit.GetXmlNodeValue(result, "exsm");
            }
            else
            {
                message = errorMessage;
            }
            uploadResult = message;
            return state;
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
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();
            List<M_JobScore> listJobScore = new List<M_JobScore>();

            sbParam.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);
            sbParam.AppendFormat("<StartTime>{0}</StartTime>", startTime);
            sbParam.AppendFormat("<EndTime>{0}</EndTime>", endTime);
            sbParam.AppendFormat("<DataType>{0}</DataType>", dataType);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_StudentScore);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_StudentScore);

            if (publicClass.IsRight(result))
            {
                listJobScore = XmlHelper.XmlToObjList<M_JobScore>(result, "body");
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }

            return listJobScore;
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
            //string result = PublicClass.rjdh.GetMyJobDetailScore(studentCode, hwId, dataType);

            //return result;
            return "";
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
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();
            List<M_TopicDB> listTopicDB = new List<M_TopicDB>();

            sbParam.AppendFormat("<StartTime>{0}</StartTime>", InStartTime);
            sbParam.AppendFormat("<EndTime>{0}</EndTime>", InEndTime);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_TopicDB);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_TopicDB);

            if (publicClass.IsRight(result))
            {
                listTopicDB = XmlHelper.XmlToObjList<M_TopicDB>(result, "body");
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }

            return listTopicDB;
        }
        /// <summary>
        /// 获取资源数据
        /// </summary>
        /// <param name="resourceName"></param>
        /// <param name="resourceType"></param>
        /// <param name="resourceModel"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="fileHost"></param>
        /// <returns></returns>
        public List<M_Resource> GetResource(string resourceName, string resourceType, string resourceModel, string startTime, string endTime, out string fileHost)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();
            List<M_Resource> listResourceData = new List<M_Resource>();

            sbParam.AppendFormat("<RSName>{0}</RSName>", resourceName);
            sbParam.AppendFormat("<RSType>{0}</RSType>", resourceType);
            sbParam.AppendFormat("<RSMode>{0}</RSMode>", resourceModel);
            sbParam.AppendFormat("<StartTime>{0}</StartTime>", startTime);
            sbParam.AppendFormat("<EndTime>{0}</EndTime>", endTime);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_Resource);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_Resource);

            if (publicClass.IsRight(result))
            {
                listResourceData = XmlHelper.XmlToObjList<M_Resource>(result, "body");
                state = xmlUnit.GetXmlNodeValue(result, "state");
                fileHost = xmlUnit.GetXmlNodeValue(result, "FileHost");
            }
            else
            {
                fileHost = "";
                message = errorMessage;
            }

            return listResourceData;
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
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();

            sbParam.AppendFormat("<RecordType>{0}</RecordType>", recType);
            sbParam.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);
            sbParam.AppendFormat("<HWID>{0}</HWID>", hwId);
            sbParam.AppendFormat("<HWName>{0}</HWName>", hwName);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_RecordingTime);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_RecordingTime);

            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }

            return state;
        }
        /// <summary>
        /// 记录练习开始、结束时间
        /// </summary>
        /// <param name="studentCode">学生编号</param>
        /// <returns></returns>
        public string ExitSystem(string studentCode)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();

            sbParam.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_ExitSystem);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_ExitSystem);

            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }

            return state;
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
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();

            sbParam.AppendFormat("<StudentCode>{0}</StudentCode>", studentCode);
            sbParam.AppendFormat("<logData>{0}</logData>", logData);

            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_Log);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_Log);

            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }

            return state;
        }
        /// <summary>
        /// 获取资源类型
        /// </summary>
        /// <returns></returns>
        public List<M_ResourceType> GetResourceType()
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            List<M_ResourceType> listResourceType = new List<M_ResourceType>();

            rXml = publicClass.ReturnRequest("", Globals.CODE_ResourceType);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_ResourceType);

            if (publicClass.IsRight(result))
            {
                listResourceType = XmlHelper.XmlToObjList<M_ResourceType>(result, "body");
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }

            return listResourceType;
        }
        /// <summary>
        /// 上传试卷
        /// </summary>
        /// <returns></returns>
        public void UploadExamFile(string studentCode, M_MyJob job, string filePath)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            string fileName = string.Empty;
            byte[] fileByte = null;
            StringBuilder sbParam = new StringBuilder();

            fileName = Path.GetFileName(filePath);
            fileByte = FileOperate.SetFileToByteArray(filePath);
            sbParam.AppendFormat("<Code>{0}</Code>", studentCode);
            sbParam.AppendFormat("<FileName>{0}</FileName>", fileName);
            sbParam.AppendFormat("<FileID>{0}</FileID>", job.FileID);
            sbParam.AppendFormat("<HWID>{0}</HWID>", job.HWID);
            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_UploadExamFile);
            result = Globals.SERVICE.examupload(rXml, Globals.CODE_UploadExamFile, fileByte);

            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }
        }
        /// <summary>
        /// 检查题库是否有效
        /// </summary>
        /// <returns></returns>
        public string ExistsTopicDB(string topicdbcode, string topicdbversion)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();

            sbParam.AppendFormat("<TopicDBCode>{0}</TopicDBCode>", topicdbcode);
            sbParam.AppendFormat("<TopicDBVersion>{0}</TopicDBVersion>", topicdbversion);
            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_ExistsTopicDB);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_ExistsTopicDB);
            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }
            return state;
        }
        /// <summary>
        /// 检查作业文件是否有效
        /// </summary>
        /// <returns></returns>
        public string ExistsHWFile(string hwid, string topicdbcode, string topicdbversion)
        {
            string state = string.Empty;
            string rXml = string.Empty;
            string result = string.Empty;
            string message = string.Empty;
            StringBuilder sbParam = new StringBuilder();

            sbParam.AppendFormat("<HWID>{0}</HWID>", hwid);
            sbParam.AppendFormat("<TopicDBCode>{0}</TopicDBCode>", topicdbcode);
            sbParam.AppendFormat("<TopicDBVersion>{0}</TopicDBVersion>", topicdbversion);
            rXml = publicClass.ReturnRequest(sbParam.ToString(), Globals.CODE_ExistsHWFile);
            result = Globals.SERVICE.examonline(rXml, Globals.CODE_ExistsHWFile);
            if (publicClass.IsRight(result))
            {
                state = xmlUnit.GetXmlNodeValue(result, "state");
            }
            else
            {
                message = errorMessage;
            }
            return state;
        }
    }
}
