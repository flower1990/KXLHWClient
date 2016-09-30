using ComputerExam.BLL;
using ComputerExam.Common;
using ComputerExam.Model;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace ComputerExam.BusicWork
{
    public partial class frmResource : Form
    {
        string fileHost = "";

        PublicClass publicClass = new PublicClass();
        SqliteKey3 key3 = new SqliteKey3();

        List<M_Resource> serverResource = new List<M_Resource>();

        B_Service bService = new B_Service();

        /// <summary>
        /// 下载资源文件
        /// </summary>
        /// <param name="copyPath"></param>
        /// <returns></returns>
        private void DownLoad(string copyPath)
        {
            bool downResult = false;
            string downLoadUrl = string.Empty;
            string filePath = string.Empty;
            string fileName = string.Empty;
            string savePath = string.Empty;
            string downPath = Globals.DownLoadDir;
            try
            {
                if (dgvResult.SelectedRows.Count == 0) return;
                M_Resource resource = dgvResult.SelectedRows[0].DataBoundItem as M_Resource;
                btnDownLoad.Enabled = false;
                //下载地址
                downLoadUrl = string.Format("{0}/{1}", fileHost, resource.RSPath.Replace(@"\", @"/"));
                //文件路径
                filePath = resource.RSPath;
                //文件名
                fileName = Path.GetFileName(filePath);
                //下载文件保存路径
                savePath = string.Format("{0}{1}", downPath, fileName);
                //下载作业
                downResult = CommonUtil.DownloadFile(downLoadUrl, savePath, tsbBar, tsbMessage, "下载进度：");
                if (downResult)
                {
                    switch (resource.RSType)
                    {
                        case MyResourceType.ZhangTao:
                            //解压资源到目录
                            ZipFileTools.UnZipSZL(savePath, copyPath);
                            break;
                        case MyResourceType.TiKU:
                            downPath = string.Format(@"{0}\{1}", downPath, Path.GetFileNameWithoutExtension(fileName));
                            //解压资源到目录
                            ZipFileTools.UnZipSZL(savePath, downPath);
                            DirectoryInfo directoryInfo = new DirectoryInfo(downPath);
                            FileInfo[] fileInfo = directoryInfo.GetFiles(); //只取.srk文件
                            if (fileInfo.Length > 0)
                                DownLoadTopicDB(fileInfo[0].FullName);
                            DirFileHelper.DeleteDirectory(downPath);
                            break;
                        case MyResourceType.ZiLiao:
                            //解压资源到目录
                            ZipFileTools.UnZipSZL(savePath, copyPath);
                            break;
                        default:
                            //解压资源到目录
                            ZipFileTools.UnZipSZL(savePath, copyPath);
                            break;
                    }
                    //删除下载文件 
                    DirFileHelper.DeleteFile(savePath);
                    //设置已下载状态
                    dgvResult.SelectedRows[0].Cells["DownLoadState"].Value = "已下载";
                }
            }
            catch (Exception ex)
            {
                Msg.ShowError("资源下载失败，详情请参考系统错误日志。");
                LogHelper.WriteLog(typeof(frmResource), ex);
                CommonUtil.WriteLog(ex);
            }
            finally
            {
                btnDownLoad.Enabled = true;
            }
        }

        private void DownLoadTopicDB(string fileFullName)
        {
            string filePath = fileFullName;
            string fileTPath = fileFullName + "t";
            string fileName = Path.GetFileName(filePath);
            string fileExt = Path.GetExtension(filePath).ToLower();
            string copyPath = string.Format(@"{0}\data\{1}", Application.StartupPath, fileName);
            string copyTPath = string.Format(@"{0}\data\{1}t", Application.StartupPath, fileName);
            string connection = string.Format(@"data source={0};password={1};polling=false;failifmissing=true", filePath, PublicClass.PasswordTopicDB);
            string connectionT = string.Format(@"data source={0};polling=false;failifmissing=true", fileTPath);
            try
            {
                if (fileExt != ".sdb" && fileExt != ".srk")
                {
                    PublicClass.ShowMessageOk("该文件不是有效的题库文件，请重新添加！");
                    return;
                }
                if (File.Exists(copyPath) && File.Exists(copyTPath))
                {
                    DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该题库文件已经存在，确定要覆盖吗？");
                    if (dialogResult == DialogResult.Cancel) return;
                }
                CommonUtil.ShowProcessing("正在验证题库，请稍候...", this, (obj) =>
                {
                    //复制一个.sdbt文件
                    DirFileHelper.CopyFile(filePath, fileTPath);
                    //修改.sdbt文件密码
                    bool updateResult = key3.ChangePassWordByGB2312(fileTPath, PublicClass.PassWordTopicDB_SDB, "");
                    if (updateResult)
                    {
                        SQLiteConnection conn = new SQLiteConnection(connectionT);
                        conn.Open();
                        if (ConnectionState.Open == conn.State)
                        {
                            conn.ChangePassword(PublicClass.PasswordTopicDB);
                            DirFileHelper.CopyFile(filePath, copyPath.Replace(".srk", ".sdb"));
                            DirFileHelper.CopyFile(fileTPath, copyTPath.Replace(".srk", ".sdb"));
                            conn.Close();
                        }
                        conn.Dispose();
                        conn = null;
                    }
                    else
                    {
                        PublicClass.ShowMessageOk("无法打开题库文件，该题库不是有效的题库文件！");
                    }
                    key3.Dispose();
                }, null);
            }
            catch (SQLiteException)
            {
                PublicClass.ShowMessageOk("无法打开题库文件，该题库不是有效的题库文件！");
            }
            catch (AggregateException)
            {
                PublicClass.ShowMessageOk("无法打开题库文件，该题库不是有效的题库文件！");
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk(ex.Message);
            }
        }

        private bool DownLoadFile(string copyPath)
        {
            bool downResult = false;
            bool downMyJobResult = false;

            string downLoadUrl = string.Empty;
            string filePath = string.Empty;
            string fileName = string.Empty;
            string savePath = string.Empty;

            string ftpFilePath = "C:\\data\\";
            string ftpServerIp = UserConfigSettings.Instance.ReadSetting("题库地址");
            string ftpRemotePath = UserConfigSettings.Instance.ReadSetting("题库目录");
            string ftpPort = UserConfigSettings.Instance.ReadSetting("端口号");
            string ftpUserId = UserConfigSettings.Instance.ReadSetting("ftp用户名");
            string ftpPassword = UserConfigSettings.Instance.ReadSetting("ftp密码");
            bool anonymous = bool.Parse(UserConfigSettings.Instance.ReadSetting("匿名"));
            FtpWeb ftpWeb = new FtpWeb(ftpServerIp, ftpRemotePath, ftpUserId, ftpPassword, ftpPort, 10000, false, anonymous);

            M_Resource resource = dgvResult.SelectedRows[0].DataBoundItem as M_Resource;

            try
            {
                btnDownLoad.Enabled = false;
                //下载地址
                downLoadUrl = fileHost.Replace(@"\", @"/") + resource.RSPath;
                //文件路径
                filePath = resource.RSPath;
                //文件名
                fileName = Path.GetFileName(filePath);
                //下载文件保存路径
                savePath = string.Format("C:\\{0}_{1}", PublicClass.StudentCode, fileName);
                //下载作业
                //downResult = CommonUtil.DownloadFile(downLoadUrl, savePath, tsbBar, tsbMessage, "下载进度：");
                ftpWeb.Download(ftpFilePath, resource.RSPath, "", tsbBar, tsbMessage, "下载进度：");

                if (downResult)
                {
                    //解压资源到目录
                    ZipFileTools.UnZipSZL(savePath, copyPath);
                    //删除下载文件 
                    File.Delete(savePath);
                    //设置已下载状态
                    dgvResult.SelectedRows[0].Cells["DownLoadState"].Value = "已下载";
                    downMyJobResult = true;
                }
            }
            catch (Exception ex)
            {
                PublicClass.ShowMessageOk(ex.Message);
                downMyJobResult = false;
            }
            finally
            {
                btnDownLoad.Enabled = true;
            }

            return downMyJobResult;
        }
        /// <summary>
        /// 设置下载状态
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetDownLoadState()
        {
            DirectoryInfo directoryInfo = null;
            List<FileInfo> fileInfo = null;

            string path = "";
            bool result = false;

            foreach (var resource in serverResource)
            {
                GetResourceDirectoryInfo(resource, ref directoryInfo, ref fileInfo, ref path, ref result);

                if (result)
                {
                    resource.DownLoadState = "已下载";
                }
                else
                {
                    resource.DownLoadState = "未下载";
                }
            }
        }
        /// <summary>
        /// 设置序号
        /// </summary>
        /// <param name="serverMyJob"></param>
        private void SetNumber()
        {
            for (int i = 0; i < serverResource.Count; i++)
            {
                serverResource[i].Number = (i + 1).ToString("00");
            }
        }
        /// <summary>
        /// 获取资源文件信息
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="directoryInfo"></param>
        /// <param name="fileInfo"></param>
        /// <param name="path"></param>
        /// <param name="result"></param>
        private static void GetResourceDirectoryInfo(M_Resource resource, ref DirectoryInfo directoryInfo, ref List<FileInfo> fileInfo, ref string path, ref bool result)
        {
            string serverName = string.Empty;
            switch (resource.RSType)
            {
                case MyResourceType.ZhangTao:
                    path = Application.StartupPath + @"\SowerTestClient\Paper\Account";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    directoryInfo = new DirectoryInfo(path);
                    fileInfo = directoryInfo.GetFiles("*.casf").ToList();
                    break;
                case MyResourceType.TiKU:
                    path = Application.StartupPath + @"\data";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    directoryInfo = new DirectoryInfo(path);
                    fileInfo = directoryInfo.GetFiles("*.sdb").ToList();
                    break;
                case MyResourceType.ZiLiao:
                    path = Application.StartupPath + @"\Common\Resource";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    directoryInfo = new DirectoryInfo(path);
                    fileInfo = directoryInfo.GetFiles().ToList();
                    break;
                default:
                    path = Application.StartupPath + @"\Common\Resource";
                    if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                    directoryInfo = new DirectoryInfo(path);
                    fileInfo = directoryInfo.GetFiles().ToList();
                    break;
            }

            if (fileInfo == null) return;
            if (string.IsNullOrEmpty(resource.RSPath)) return;

            serverName = Path.GetFileNameWithoutExtension(resource.RSPath);
            result = fileInfo.Exists(f => Path.GetFileNameWithoutExtension(f.Name) == serverName);
        }
        public frmResource()
        {
            InitializeComponent();
        }

        private void frmResource_Load(object sender, EventArgs e)
        {
            try
            {
                CommonUtil.BindComboBox(cboResourceType, "ID", "Name", CommonUtil.listResourceType);
                CommonUtil.BindComboBox(cboResourceModel, "Id", "Name", CommonUtil.listResourceModel);
                CommonUtil.BindComboBox(cboSubject, "CourseID", "CourseName", CommonUtil.listSubject);
                if (CommonUtil.listSubject.Count == 2) cboSubject.SelectedIndex = 1; else cboSubject.SelectedIndex = 0;
                publicClass.BindDataGridViewColumn(dgvResult.Columns["ResourceModel"] as DataGridViewComboBoxColumn, "Id", "Name", CommonUtil.listResourceModel);

                cboResourceModel.SelectedValueChanged += cboResourceModel_SelectedValueChanged;
                cboSubject.SelectedValueChanged += cboSubject_SelectedValueChanged;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string resourceName = txtResourceName.Text.Trim();
                string resourceType = cboResourceType.SelectedValue.ToString();
                string resourceModel = cboResourceModel.SelectedValue.ToString();
                string courseID = cboSubject.SelectedValue.ToString();

                CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                {
                    Thread.Sleep(1000);
                    //获取服务器资源列表
                    serverResource = bService.GetResource(resourceName, resourceType, resourceModel, "1900-01-01", "2099-12-31", out fileHost);
                    //获取可用的资源列表
                    serverResource = serverResource.Where(r => r.IsEnable == true).ToList();
                    //获取可用的资源列表
                    serverResource = serverResource.Where(f => f.CourseID == courseID).ToList();
                    //设置资源下载状态
                    SetDownLoadState();
                    //设置序号
                    SetNumber();
                }, null);

                //绑定作业到列表
                dgvResult.AutoGenerateColumns = false;
                dgvResult.DataSource = new BindingCollection<M_Resource>(serverResource);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmHomeWork), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count == 0) return;

                M_Resource resource = dgvResult.SelectedRows[0].DataBoundItem as M_Resource;
                DirectoryInfo directoryInfo = null;
                List<FileInfo> fileInfo = null;

                string path = string.Empty;
                string resourceModel = cboResourceModel.SelectedValue.ToString();
                bool result = false;
                GetResourceDirectoryInfo(resource, ref directoryInfo, ref fileInfo, ref path, ref result);
                this.ParentForm.Enabled = false;
                if (result)
                {
                    DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该资源文件已经存在，您确定继续下载并覆盖吗？");
                    if (dialogResult == DialogResult.OK)
                    {
                        DownLoad(directoryInfo.FullName);
                    }
                }
                else
                {
                    switch (resourceModel)
                    {
                        case "1":
                            DownLoad(directoryInfo.FullName);
                            break;
                        case "2":
                            Process.Start(resource.Url);
                            break;
                        default:
                            break;
                    }
                }
                this.ParentForm.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void dgvResult_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (e == null || e.Value == null || e.Value.ToString() == "" || !(sender is DataGridView))
                {
                    return;
                }
                DataGridView dgv = (DataGridView)sender;
                object originalValue = e.Value;
                if (e.ColumnIndex == dgv.Columns["CreateDate"].Index)   //格式化日期
                {
                    if (e.Value != null && e.Value.ToString() != "")
                    {
                        e.Value = Convert.ToDateTime(e.Value).ToString("yyyy-MM-dd");
                        if (e.Value.ToString() == "0001-01-01")
                        {
                            e.Value = "-";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void dgvResult_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count == 0) return;
                M_Resource resource = dgvResult.SelectedRows[0].DataBoundItem as M_Resource;
                if (resource.DownLoadState == "已下载")
                {
                    tsbBar.Value = 100;
                }
                else
                {
                    tsbBar.Value = 0;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void cboResourceModel_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                btnSearch_Click(this, e);

                string model = cboResourceModel.SelectedValue.ToString();
                switch (model)
                {
                    case "1":
                        dgvResult.Columns["TotalSize"].Visible = true;
                        dgvResult.Columns["DownLoadState"].Visible = true;
                        dgvResult.Columns["Url"].Visible = false;
                        break;
                    case "2":
                        dgvResult.Columns["TotalSize"].Visible = false;
                        dgvResult.Columns["DownLoadState"].Visible = false;
                        dgvResult.Columns["Url"].Visible = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        void cboSubject_SelectedValueChanged(object sender, EventArgs e)
        {
            btnSearch_Click(this, e);
        }

        private void dgvResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count == 0) return;
                M_Resource resource = dgvResult.SelectedRows[0].DataBoundItem as M_Resource;

                if (e.ColumnIndex == 13)
                {
                    Process.Start(resource.Url);
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        private void btnLook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvResult.SelectedRows.Count == 0) return;

                string path = string.Empty;
                M_Resource resource = dgvResult.SelectedRows[0].DataBoundItem as M_Resource;

                switch (resource.RSType)
                {
                    case MyResourceType.ZhangTao:
                        path = Application.StartupPath + @"\SowerTestClient\Paper\Account";
                        break;
                    case MyResourceType.TiKU:
                        path = Application.StartupPath + @"\data";
                        break;
                    case MyResourceType.ZiLiao:
                        path = Application.StartupPath + @"\Common\Resource";
                        break;
                    default:
                        path = Application.StartupPath + @"\Common\Resource";
                        break;
                }

                if (!Directory.Exists(path)) Directory.CreateDirectory(path);
                Process.Start(path);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmLogin), ex);
                CommonUtil.WriteLog(ex);
            }
        }
    }
}
