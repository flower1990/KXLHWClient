using ComputerExam.BLL;
using ComputerExam.Model;
using ComputerExam.Properties;
using ComputerExam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace ComputerExam.BusicWork
{
    public partial class frmAddTopicDB : Form
    {
        SqliteKey3 key3 = new SqliteKey3();
        PublicClass publicClass = new PublicClass();
        B_SubjectProp bSubjectProp = new B_SubjectProp();
        B_Service bService = new B_Service();
        BindingList<M_ExerciseSubjectDetail> listSubject = new BindingList<M_ExerciseSubjectDetail>();
        BindingList<M_ExerciseFile> listFile = new BindingList<M_ExerciseFile>();
        BindingList<M_ExerciseFile> listVideoFile = new BindingList<M_ExerciseFile>();
        /// <summary>
        /// 加载题库文件
        /// </summary>
        private void LoadTopicDB()
        {
            try
            {
                listSubject.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\data");
                FileInfo[] fileInfo = directoryInfo.GetFiles("*.sdbt"); //只取.sdbt文件
                foreach (FileInfo item in fileInfo)
                {
                    if (item.Extension.ToLower() == ".sdbt")
                    {
                        string subjectId = Path.GetFileNameWithoutExtension(item.Name);
                        string[] temp = subjectId.Split('_');
                        if (temp.Length == 0) continue;
                        if (temp[0] != PublicClass.StudentCode) continue;
                        M_SubjectProp mSubjectProp = bSubjectProp.GetSubjectProp(Path.GetFileName(item.Name));
                        M_ExerciseSubjectDetail subject = new M_ExerciseSubjectDetail();
                        subject.TopicDBCode = mSubjectProp.TopicDBCode;
                        subject.SubjectName = mSubjectProp.SubjectName;
                        subject.TopicDBVersion = mSubjectProp.TopicDBVersion;
                        subject.FileName = mSubjectProp.EnvFileName;
                        subject.TopicFilePath = item.FullName;
                        subject.VideoFileName = Path.GetFileNameWithoutExtension(mSubjectProp.MediaPackageFileName);
                        listSubject.Add(subject);
                    }
                }
                DataBindTopicDB();
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmExercise), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 绑定题库列表
        /// </summary>
        private void DataBindTopicDB()
        {
            dgvTopicDB.AutoGenerateColumns = false;
            dgvTopicDB.DataSource = listSubject;
        }
        /// <summary>
        /// 加载账套文件
        /// </summary>
        private void LoadFiles()
        {
            try
            {
                listFile.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Paper\Account\");
                FileInfo[] fileInfo = directoryInfo.GetFiles("*.casf");//只取文本文档
                foreach (FileInfo item in fileInfo)
                {
                    if (item.Extension.ToLower() == ".casf")
                    {
                        M_ExerciseFile file = new M_ExerciseFile();
                        file.FileName = Path.GetFileName(item.Name);
                        file.FileVersion = item.LastWriteTime.ToString();
                        file.Description = item.Name;
                        file.FilePath = item.FullName;
                        listFile.Add(file);
                    }
                }
                dgvFiles.AutoGenerateColumns = false;
                dgvFiles.DataSource = listFile;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmExercise), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 加载视频文件
        /// </summary>
        private void LoadVideoFiles()
        {
            try
            {
                listVideoFile.Clear();
                DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Video\");
                DirectoryInfo[] dirList = directoryInfo.GetDirectories();
                foreach (DirectoryInfo item in dirList)
                {
                    string[] nameList = item.Name.Split('_');
                    if (nameList.Length <= 1) continue;
                    if (nameList[0] != PublicClass.StudentCode) continue;
                    M_ExerciseFile file = new M_ExerciseFile();
                    file.FileName = nameList[1];
                    file.FileVersion = item.LastWriteTime.ToString();
                    file.Description = item.Name;
                    file.FilePath = item.FullName;
                    listVideoFile.Add(file);
                }
                dgvVideoFiles.AutoGenerateColumns = false;
                dgvVideoFiles.DataSource = listVideoFile;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmExercise), ex);
                CommonUtil.WriteLog(ex);
            }
        }

        public frmAddTopicDB()
        {
            InitializeComponent();
        }

        private void frmAddTopicDB_Load(object sender, EventArgs e)
        {
            LoadTopicDB();
            LoadFiles();
            LoadVideoFiles();
        }
        /// <summary>
        /// 添加题库文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddTopicDB_Click(object sender, EventArgs e)
        {
            DialogResult result = ofdOpenTopicDB.ShowDialog();
            if (result == DialogResult.OK)
            {
                string existsResult = string.Empty;
                string filePath = ofdOpenTopicDB.FileName;
                string fileTPath = ofdOpenTopicDB.FileName + "t";
                string fileName = Path.GetFileName(filePath);
                string fileExt = Path.GetExtension(filePath).ToLower();
                string copyPath = string.Format(@"{0}\data\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName.Replace(".srk", ".sdb"));
                string copyTPath = string.Format(@"{0}\data\{1}_{2}t", Application.StartupPath, PublicClass.StudentCode, fileName.Replace(".srk", ".sdb"));
                string connection = string.Format(@"data source={0};password={1};polling=false;failifmissing=true", filePath, PublicClass.PasswordTopicDB);
                string connectionT = string.Format(@"data source={0};polling=false;failifmissing=true", fileTPath);
                M_SubjectProp mSubjectProp = new M_SubjectProp();
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
                                conn.Close();
                                DirFileHelper.CopyFile(filePath, copyPath);
                                DirFileHelper.CopyFile(fileTPath, copyTPath);
                                mSubjectProp = bSubjectProp.GetSubjectProp(Path.GetFileName(copyTPath));
                                existsResult = bService.ExistsTopicDB(mSubjectProp.TopicDBCode, mSubjectProp.TopicDBVersion);
                                if (existsResult == "-1")
                                {
                                    DirFileHelper.DeleteFile(copyPath);
                                    DirFileHelper.DeleteFile(copyTPath);
                                }
                            }
                            conn.Dispose();
                            conn = null;
                            DirFileHelper.DeleteFile(fileTPath);
                        }
                        else
                        {
                            PublicClass.ShowMessageOk("无法打开题库文件，该题库不是有效的题库文件！");
                        }
                        key3.Dispose();
                    }, null);
                    if (existsResult == "-1")
                    {
                        Msg.ShowInformation("在作业中心->练习题库列表中没有添加该文件，请联系授课老师进行添加。");
                    }
                }
                catch (SQLiteException)
                {
                    PublicClass.ShowMessageOk("无法打开题库文件，该题库不是有效的题库文件！");
                    DirFileHelper.DeleteFile(copyPath);
                    DirFileHelper.DeleteFile(copyTPath);
                }
                catch (AggregateException)
                {
                    PublicClass.ShowMessageOk("无法打开题库文件，该题库不是有效的题库文件！");
                    DirFileHelper.DeleteFile(copyPath);
                    DirFileHelper.DeleteFile(copyTPath);
                }
                catch (Exception ex)
                {
                    PublicClass.ShowMessageOk(ex.Message);
                    DirFileHelper.DeleteFile(copyPath);
                    DirFileHelper.DeleteFile(copyTPath);
                }
                finally
                {
                    LoadTopicDB();
                }
            }
        }
        /// <summary>
        /// 删除题库文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteTopicDB_Click(object sender, EventArgs e)
        {
            string sdbFile = "";
            string sdbtFile = "";

            if (dgvTopicDB.SelectedRows.Count <= 0) return;

            DialogResult dialogResult = PublicClass.ShowMessageOKCancel("您确定要删除吗？");
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    M_ExerciseSubjectDetail subject = dgvTopicDB.SelectedRows[0].DataBoundItem as M_ExerciseSubjectDetail;
                    sdbFile = subject.TopicFilePath;
                    sdbtFile = subject.TopicFilePath.Substring(0, subject.TopicFilePath.Length - 1);
                    DirFileHelper.DeleteFile(sdbFile);
                    DirFileHelper.DeleteFile(sdbtFile);
                }
                catch (IOException)
                {
                    string errorMessage = string.Format("无法删除该文件，文件正在被另一个人或程序使用。\n关闭任何可能使用这个文件的程序，重新试一次。");
                    PublicClass.ShowErrorMessageOk(errorMessage);
                }
                catch (Exception ex)
                {
                    PublicClass.ShowMessageOk(ex.Message);
                }
                finally
                {
                    //刷新列表
                    LoadTopicDB();
                }
            }
        }
        /// <summary>
        /// 添加账套文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            ofdOpenFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = ofdOpenFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = ofdOpenFile.FileName;
                string fileName = Path.GetFileName(filePath);
                string fileExt = Path.GetExtension(filePath).ToLower();
                string copyPath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, fileName);
                try
                {
                    if (fileExt != ".casf")
                    {
                        PublicClass.ShowMessageOk("该文件不是有效的账套文件，请重新添加！");
                        return;
                    }
                    if (File.Exists(copyPath))
                    {
                        DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该账套文件已经存在，确定要覆盖吗？");
                        if (dialogResult == DialogResult.Cancel) return;
                    }
                    CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                    {
                        DirFileHelper.CopyFile(filePath, copyPath);
                    }, null);
                }
                catch (Exception ex)
                {
                    PublicClass.ShowMessageOk(ex.Message);
                }
                finally
                {
                    LoadFiles();
                }
            }
        }
        /// <summary>
        /// 删除账套文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteFile_Click(object sender, EventArgs e)
        {
            string casfFile = "";
            if (dgvFiles.SelectedRows.Count == 0) return;
            DialogResult dialogResult = PublicClass.ShowMessageOKCancel("您确定要删除吗？");
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    M_ExerciseFile files = dgvFiles.SelectedRows[0].DataBoundItem as M_ExerciseFile;
                    casfFile = files.FilePath;
                    listFile.Remove(files);
                    //删除.sdb文件
                    DirFileHelper.DeleteFile(casfFile);
                }
                catch (IOException)
                {
                    string errorMessage = string.Format("无法删除该文件，文件正在被另一个人或程序使用。\n关闭任何可能使用这个文件的程序，重新试一次。");
                    PublicClass.ShowErrorMessageOk(errorMessage);
                }
                catch (Exception ex)
                {
                    PublicClass.ShowMessageOk(ex.Message);
                }
                finally
                {
                    //刷新列表
                    LoadFiles();
                }
            }
        }
        /// <summary>
        /// 添加视频资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddVideoFile_Click(object sender, EventArgs e)
        {
            ofdOpenVideoFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            DialogResult result = ofdOpenVideoFile.ShowDialog();
            if (result == DialogResult.OK)
            {
                string filePath = ofdOpenVideoFile.FileName;
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                string fileExt = Path.GetExtension(filePath).ToLower();
                string copyPath = string.Format(@"{0}\SowerTestClient\Video\{1}_{2}", Application.StartupPath, PublicClass.StudentCode, fileName);
                try
                {
                    if (fileExt != ".zip")
                    {
                        PublicClass.ShowMessageOk("该文件不是有效的视频资源包，请重新添加！");
                        return;
                    }
                    if (Directory.Exists(copyPath))
                    {
                        DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该视频资源包已经存在，确定要覆盖吗？");
                        if (dialogResult == DialogResult.Cancel) return;
                    }
                    CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                    {
                        ZipFileTools.UnZipSZL(filePath, copyPath);
                    }, null);
                }
                catch (Exception ex)
                {
                    PublicClass.ShowMessageOk(ex.Message);
                }
                finally
                {
                    LoadVideoFiles();
                }
            }
        }
        /// <summary>
        /// 删除视频资源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelVideoFile_Click(object sender, EventArgs e)
        {
            string videoFile = "";
            if (dgvVideoFiles.SelectedRows.Count == 0) return;
            DialogResult dialogResult = PublicClass.ShowMessageOKCancel("您确定要删除吗？");
            if (dialogResult == DialogResult.OK)
            {
                try
                {
                    M_ExerciseFile files = dgvVideoFiles.SelectedRows[0].DataBoundItem as M_ExerciseFile;
                    videoFile = files.FilePath;
                    listFile.Remove(files);
                    //删除视频资源包
                    DirFileHelper.DeleteDirectory(videoFile);
                }
                catch (IOException)
                {
                    string errorMessage = string.Format("无法删除该文件，文件正在被另一个人或程序使用。\n关闭任何可能使用这个文件的程序，重新试一次。");
                    PublicClass.ShowErrorMessageOk(errorMessage);
                }
                catch (Exception ex)
                {
                    PublicClass.ShowMessageOk(ex.Message);
                }
                finally
                {
                    //刷新列表
                    LoadVideoFiles();
                }
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //获取TabControl主控件的工作区域 
            Rectangle rec = tabControl1.ClientRectangle;
            //获取背景图片，我的背景图片在项目资源文件中。 
            Image backImage = Resources.背景2;
            //新建一个StringFormat对象，用于对标签文字的布局设置 
            StringFormat strFormat = new StringFormat();
            strFormat.LineAlignment = StringAlignment.Center;
            strFormat.Alignment = StringAlignment.Center;
            //标签背景填充颜色，也可以是图片 
            SolidBrush brush = new SolidBrush(Color.White);
            SolidBrush brush1 = new SolidBrush(Color.FromArgb(35, 135, 194));
            //标签字体颜色 
            SolidBrush bruFont = new SolidBrush(Color.White);
            SolidBrush bruFont1 = new SolidBrush(Color.FromArgb(35, 135, 194));
            //设置标签字体样式 
            Font font = new System.Drawing.Font("微软雅黑", 9F);
            //绘制主控件的背景 
            e.Graphics.DrawImage(backImage, 0, 0, tabControl1.Width, tabControl1.Height);
            //绘制标签样式 
            for (int i = 0; i < tabControl1.TabPages.Count; i++)
            {
                //获取标签头的工作区域
                Rectangle recChild = tabControl1.GetTabRect(i);
                //绘制标签头背景颜色 
                e.Graphics.FillRectangle(brush1, recChild);
                //绘制标签头的文字 
                e.Graphics.DrawString(tabControl1.TabPages[i].Text, font, bruFont, recChild, strFormat);
            }
            if (e.Index == tabControl1.SelectedIndex)
            {
                //获取标签头的工作区域
                Rectangle recChild = tabControl1.GetTabRect(e.Index);
                //绘制标签头背景颜色 
                e.Graphics.FillRectangle(brush, recChild);
                //绘制标签头的文字 
                e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text, font, bruFont1, recChild, strFormat);
            }
        }


    }
}
