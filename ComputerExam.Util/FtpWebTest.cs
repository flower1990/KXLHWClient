using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ComputerExam.Util
{
    public class FtpWebTest
    {
        static Dictionary<string, string> StatusList;

        /// <summary>
        /// 检查FTP连接（带地址）
        /// </summary>
        /// <param name="ftpUserID">用户名</param>
        /// <param name="ftpPassword">密码</param>
        /// <param name="port">端口号</param>
        /// <param name="pattern">模式</param>
        /// <param name="ftpAnonymous">匿名</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public static bool CheckFtp(string ftpUserID, string ftpPassword, string port, bool pattern, bool ftpAnonymous, string dirName, out string message)
        {
            return CheckFtp("", ftpUserID, ftpPassword, port, pattern, ftpAnonymous, dirName, out message);
        }
        /// <summary>
        /// 检查FTP连接（不带地址）
        /// </summary>
        /// <param name="ip">地址</param>
        /// <param name="ftpUserID">用户名</param>
        /// <param name="ftpPassword">密码</param>
        /// <param name="port">端口号</param>
        /// <param name="pattern">模式</param>
        /// <param name="ftpAnonymous">匿名</param>
        /// <param name="message">返回信息</param>
        /// <returns></returns>
        public static bool CheckFtp(string ip, string ftpUserID, string ftpPassword, string port, bool pattern, bool ftpAnonymous, string dirName, out string message)
        {
            int statusCode = 0;
            bool checkResult = false;
            string serverIP = string.Empty;
            string ftpURL = string.Empty;
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;

            try
            {
                if (string.IsNullOrEmpty(ip)) serverIP = GetIPAddress(); else serverIP = ip;
                if (string.IsNullOrEmpty(serverIP)) throw new Exception("获取服务器IP地址失败");
                ftpURL = string.Format("ftp://{0}:{1}/{2}", serverIP, port, dirName);
                ftpRequest = (FtpWebRequest)FtpWebRequest.Create(new Uri(ftpURL));
                if (!ftpAnonymous)
                {
                    ftpRequest.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                }
                ftpRequest.UsePassive = pattern;
                ftpRequest.UseBinary = true;
                ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                ftpRequest.Timeout = 3000;
                ftpResponse = ftpRequest.GetResponse() as FtpWebResponse;
                if (ftpResponse.StatusCode == FtpStatusCode.OpeningData || ftpResponse.StatusCode == FtpStatusCode.DataAlreadyOpen)
                {
                    checkResult = true;
                }
                statusCode = (int)ftpResponse.StatusCode;
                message = GetStatusMessage(statusCode.ToString());
                ftpResponse.Close();
            }
            catch (WebException)
            {
                checkResult = false;
                message = "当前FTP地址不可用，请检查地址和目录是否配置正确。";
            }
            catch (Exception ex)
            {
                checkResult = false;
                message = ex.Message;
            }

            return checkResult;
        }
        private static string GetStatusMessage(string statusCode)
        {
            GetStatusName();

            if (StatusList.ContainsKey(statusCode))
            {
                return StatusList[statusCode];
            }
            else
            {
                return "检测失败";
            }
        }
        private static void GetStatusName()
        {
            StatusList = new Dictionary<string, string>();
            StatusList.Add("110", "响应包含一个重新启动标记回复");
            StatusList.Add("120", "此服务现在不可用,请稍后再试您的请求");
            StatusList.Add("125", "数据连接已打开并且请求的传输已开始");
            StatusList.Add("150", "服务器正在打开数据连接");
            StatusList.Add("200", "命令成功完成");
            StatusList.Add("202", "服务器未执行该命令");
            StatusList.Add("220", "服务器已能进行用户登录操作");
            StatusList.Add("221", "服务器正在关闭管理连接");
            StatusList.Add("226", "服务器正在关闭数据连接，并且请求的文件操作成功");
            StatusList.Add("250", "请求的文件操作成功完成");
            StatusList.Add("331", "服务器需要提供密码");
            StatusList.Add("332", "服务器需要提供登录帐户");
            StatusList.Add("421", "此服务不可用");
            StatusList.Add("425", "无法打开数据连接");
            StatusList.Add("426", "连接已关闭");
            StatusList.Add("451", "发生了阻止完成请求操作的错误");
            StatusList.Add("452", "不能执行请求的操作，因为服务器上没有足够的空间");
            StatusList.Add("500", "命令具有语法错误或不是服务器可识别的命令");
            StatusList.Add("501", "一个或多个命令参数具有语法错误");
            StatusList.Add("502", "FTP 服务器未执行该命令");
            StatusList.Add("530", "登录信息必须发送到服务器");
            StatusList.Add("532", "需要服务器上的用户帐户");
            StatusList.Add("550", "无法对指定文件执行请求的操作，原因是该文件不可用");
            StatusList.Add("551", "不能采取请求的操作，因为指定的页类型是未知的");
            StatusList.Add("552", "不能执行请求的操作");
            StatusList.Add("553", "无法对指定文件执行请求的操作");
        }
        private static string GetIPAddress()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return "";
        }
    }
}
