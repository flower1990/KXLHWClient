using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComputerExam.StepWizard;
using ComputerExam.ExamPaper;
using System.Threading;
using ComputerExam.Util;
using System.Diagnostics;
using ComputerExam.BLL;

namespace ComputerExam
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //设置应用程序处理异常方式：ThreadException处理
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            //处理非UI线程异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            //检查程序是否运行多实例
            Program.CheckInstance();
            //注册类库
            Program.RegisterCom();

            if (frmLogin.Login())
            {
                if (IsOnline)
                {
                    Program.MainForm.Show();
                }
                else
                {
                    Program.MainFormOffLine.Show();
                }
                Application.Run();
            }
            else
            {
                //登录失败,退出程序
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// 处理UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogHelper.WriteLog(e.GetType(), e.Exception);
            CommonUtil.WriteLog(e.Exception);
            Msg.ShowError(e.Exception.Message);
        }
        /// <summary>
        /// 处理非UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            LogHelper.WriteLog(e.GetType(), ex);
            CommonUtil.WriteLog(ex);
            Msg.ShowError(ex.Message);
        }
        private static string ComPath = string.Format("{0}\\Common\\Sower\\RegisterCom.bat", Application.StartupPath);
        private static ComputerExam.BusicWork.frmBusicWorkMain _mainForm = null;
        private static ComputerExam.BusicWorkOffLine.frmBusicWorkMain _mainFormOffLine = null;
        private static B_Service bService = new B_Service();
        public static bool IsOnline = false;
        /// <summary>
        /// MDI主窗体
        /// </summary>        
        public static ComputerExam.BusicWork.frmBusicWorkMain MainForm { get { return _mainForm; } set { _mainForm = value; } }
        public static ComputerExam.BusicWorkOffLine.frmBusicWorkMain MainFormOffLine { get { return _mainFormOffLine; } set { _mainFormOffLine = value; } }
        /// <summary>
        ///检查程序是否运行多实例
        /// </summary>
        public static void CheckInstance()
        {
            Boolean createdNew; //返回是否赋予了使用线程的互斥体初始所属权
            Mutex instance = new Mutex(true, "朔日（云+）作业客户端", out createdNew); //同步基元变量
            if (createdNew) //首次使用互斥体
            {
                instance.ReleaseMutex();
            }
            else
            {
                Msg.Warning("已经启动了一个程序，请先退出！");
                Environment.Exit(0);
                return;
            }
        }
        /// <summary>
        /// 注册类库
        /// </summary>
        public static void RegisterCom()
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = ComPath;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(processStartInfo);
        }
    }
}
