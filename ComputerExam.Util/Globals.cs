using ComputerExam.Util.WebReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ComputerExam.Util
{
    public class Globals
    {
        #region 服务代码
        public static JobDataHandler SERVICE = new JobDataHandler();
        /// <summary>
        /// 登录，参数StudentCode和Password
        /// </summary>
        public const string CODE_Login = "1001";
        /// <summary>
        /// 获取学生科目数据，参数StudentCode
        /// </summary>
        public const string CODE_Subject = "1002";
        /// <summary>
        /// 获取学生作业数据，参数StudentCode，StartTime，EndTime，DataType
        /// 其中DataType=1为未完成作业，DataType=2为已完成作业
        /// </summary>
        public const string CODE_HomeWork = "1003";
        /// <summary>
        /// 获取学生成绩，参数StudentCode，StartTime，EndTime，DataType
        /// 其中DataType=1为最好成绩，DataType=2为历史成绩
        /// </summary>
        public const string CODE_StudentScore = "1004";
        /// <summary>
        /// 获取题库数据，参数StartTime，EndTime
        /// </summary>
        public const string CODE_TopicDB = "1005";
        /// <summary>
        /// 获取资源数据，RSName资源名称，RSType资源类型，RSMode资源模式：1上传文件,2外部文件")，StartTime，EndTime
        /// </summary>
        public const string CODE_Resource = "1006";
        /// <summary>
        /// 上传作业成绩，跟在线练习系统文件格式相同
        /// </summary>
        public const string CODE_UploadHWScore = "1007";
        /// <summary>
        /// 上传日志，参数StudentCode，logData
        /// </summary>
        public const string CODE_Log = "1008";
        /// <summary>
        /// 上传练习成绩，在原来XMl基础上添加TopicDBCode和PaperName
        /// </summary>
        public const string CODE_UploadExScore = "1009";
        /// <summary>
        /// 记录练习开始、结束时间，参数RecordType（1为开始答题记录 2为交卷记录），StudentCode，HWID，HWName
        /// </summary>
        public const string CODE_RecordingTime = "1010";
        /// <summary>
        /// 退出系统,参数StudentCode
        /// </summary>
        public const string CODE_ExitSystem = "1011";
        /// <summary>
        /// 资源类型
        /// </summary>
        public const string CODE_ResourceType = "1012";
        /// <summary>
        /// 上传试卷
        /// </summary>
        public const string CODE_UploadExamFile = "1013";
        /// <summary>
        /// 检查题库是否有效
        /// </summary>
        public const string CODE_ExistsTopicDB = "1014";
        /// <summary>
        /// 检查作业文件是否有效
        /// </summary>
        public const string CODE_ExistsHWFile = "1015";
        #endregion

        /// <summary>
        /// 下载临时目录
        /// </summary>
        public static string DownLoadDir
        {
            get
            {
                var tempPath = string.Format("{0}\\Common\\Temp\\", Application.StartupPath);
                if (!Directory.Exists(tempPath)) Directory.CreateDirectory(tempPath);
                return tempPath;
            }
        }
        public static string BackupDir
        {
            get
            {
                var backupPath = string.Format("{0}\\Backup\\", Application.StartupPath);
                if (!Directory.Exists(backupPath)) Directory.CreateDirectory(backupPath);
                return backupPath;
            }
        }
        public static string ImageDir
        {
            get
            {
                var imagePath = string.Format("{0}\\Common\\Images\\", Application.StartupPath);
                if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);
                return imagePath;
            }
        }
        public static bool IsOnline = false;
    }
}
