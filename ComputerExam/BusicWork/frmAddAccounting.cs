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

namespace ComputerExam.BusicWork
{
    public partial class frmAddAccounting : Form
    {
        List<M_ExerciseFile> listFile = new List<M_ExerciseFile>();

        /// <summary>
        /// 加载账套文件
        /// </summary>
        private void LoadFiles()
        {
            try
            {
                listFile.Clear();

                DirectoryInfo directoryInfo = new DirectoryInfo(Application.StartupPath + @"\SowerTestClient\Paper\Account");
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
                dgvFiles.DataSource = null;
                dgvFiles.DataSource = listFile;
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(typeof(frmExercise), ex);
                CommonUtil.WriteLog(ex);
            }
        }
        /// <summary>
        /// 加载组件
        /// </summary>
        public frmAddAccounting()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmAddAccounting_Load(object sender, EventArgs e)
        {
            LoadFiles();
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
                string casfPath = string.Format(@"{0}\SowerTestClient\Paper\Account\{1}", Application.StartupPath, fileName);
                string zipPath = string.Format(@"{0}\SowerTestClient\System\{1}", Application.StartupPath, fileName);

                try
                {
                    if (fileExt != ".casf" && fileExt != ".zip")
                    {
                        PublicClass.ShowMessageOk("该文件不是有效的账套文件，请重新添加！");
                        return;
                    }

                    if (File.Exists(casfPath) || File.Exists(zipPath))
                    {
                        DialogResult dialogResult = PublicClass.ShowMessageOKCancel("该账套文件已经存在，确定要覆盖吗？");
                        if (dialogResult == DialogResult.Cancel) return;
                    }

                    CommonUtil.ShowProcessing("正在处理中，请稍候...", this, (obj) =>
                    {
                        if (fileExt == ".casf")
                        {
                            File.Copy(filePath, casfPath, true);
                        }
                        else
                        {
                            File.Copy(filePath, zipPath, true);
                        }
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
                    //删除.sdb文件
                    if (File.Exists(casfFile))
                    {
                        File.Delete(casfFile);
                    }
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
