using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Services.Description;

namespace ComputerExam.Util
{
    /// <summary>
    /// 压缩文件工具类 
    /// </summary>
    public class ZipFileTools
    {
        /// <summary>  
        /// 所有文件缓存  
        /// </summary>  
        static List<string> files = new List<string>();

        /// <summary>  
        /// 所有空目录缓存  
        /// </summary>  
        static List<string> paths = new List<string>();
        /// <summary>  
        /// 压缩单个文件  
        /// </summary>  
        /// <param name="fileToZip">要压缩的文件</param>  
        /// <param name="zipedFile">压缩后的文件全名</param>  
        /// <param name="compressionLevel">压缩程度，范围0-9，数值越大，压缩程序越高</param>  
        /// <param name="blockSize">分块大小</param>  
        public static void ZipFileSZL(string fileToZip, string zipedFile, int compressionLevel, int blockSize)
        {
            bool strResult = false;
            if (!System.IO.File.Exists(fileToZip))//如果文件没有找到，则报错  
            {
                throw new FileNotFoundException("The specified file " + fileToZip + " could not be found. Zipping aborderd");
            }
            string rootMark = Path.GetDirectoryName(fileToZip);
            FileStream streamToZip = null;
            ZipOutputStream zipStream = null;
            try
            {
                streamToZip = new FileStream(fileToZip, FileMode.Open, FileAccess.Read);
                FileStream zipFile = File.Create(zipedFile);
                zipStream = new ZipOutputStream(zipFile);
                ZipEntry zipEntry = new ZipEntry(fileToZip.Replace(rootMark, string.Empty));
                zipStream.PutNextEntry(zipEntry);
                zipStream.SetLevel(compressionLevel);
                byte[] buffer = new byte[blockSize];
                int size = streamToZip.Read(buffer, 0, buffer.Length);
                zipStream.Write(buffer, 0, size);


                while (size < streamToZip.Length)
                {
                    int sizeRead = streamToZip.Read(buffer, 0, buffer.Length);
                    zipStream.Write(buffer, 0, sizeRead);
                    size += sizeRead;
                }
                strResult = true;
            }
            catch (Exception ex)
            {
                strResult = false;
                throw ex;

            }
            finally
            {

                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }
                if (streamToZip != null)
                {
                    streamToZip.Close();
                }
            }
        }

        /// <summary>
        /// 压缩目录（包括子目录及所有文件）  
        /// </summary>
        /// <param name="rootPath">要压缩的根目录</param>
        /// <param name="destinationPath">保存路径</param>
        /// <param name="compressLevel">压缩程度，范围0-9，数值越大，压缩程序越高</param>
        /// <param name="zippassword">压缩密码</param>
        public static void ZipFileFromDirectorySZL(string rootPath, string destinationPath, int compressLevel, string zippassword)
        {
            GetAllDirectories(rootPath);
            //rootPath = rootPath.TrimEnd(new char[] { '\\' }); 

            string rootMark = rootPath;
            if (rootMark[rootMark.Length - 1] != Path.DirectorySeparatorChar)
            {
                rootMark = rootMark + Path.DirectorySeparatorChar;
                //得到当前路径的位置，以备压缩时将所压缩内容转变成相对路径。  
            }
            ZipOutputStream outPutStream = null;
            try
            {
                Crc32 crc = new Crc32();
                outPutStream = new ZipOutputStream(File.Create(destinationPath));
                //压缩密码
                if (zippassword.Length > 0)
                {
                    outPutStream.Password = zippassword;
                }
                outPutStream.SetLevel(compressLevel); // 0 - store only to 9 - means best compression  
                foreach (string file in files)
                {
                    FileStream fileStream = File.OpenRead(file);//打开压缩文件  
                    byte[] buffer = new byte[fileStream.Length];
                    fileStream.Read(buffer, 0, buffer.Length);
                    ZipEntry entry = new ZipEntry(file.Replace(rootMark, string.Empty));
                    //ZipEntry entry = new ZipEntry(file);
                    entry.DateTime = DateTime.Now;
                    // set Size and the crc, because the information  
                    // about the size and crc should be stored in the header  
                    // if it is not set it is automatically written in the footer.  
                    // (in this case size == crc == -1 in the header)  
                    // Some ZIP programs have problems with zip files that don't store  
                    // the size and crc in the header.  
                    entry.Size = fileStream.Length;
                    fileStream.Close();
                    crc.Reset();
                    crc.Update(buffer);
                    entry.Crc = crc.Value;
                    outPutStream.PutNextEntry(entry);
                    outPutStream.Write(buffer, 0, buffer.Length);
                }

                files.Clear();

                foreach (string emptyPath in paths)
                {
                    ZipEntry entry = new ZipEntry(emptyPath.Replace(rootMark, string.Empty) + "/");
                    outPutStream.PutNextEntry(entry);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                paths.Clear();
                if (outPutStream != null)
                {
                    outPutStream.Finish();
                    outPutStream.Close();
                }
            }
        }
        /// <summary>  
        /// 压缩目录（包括子目录及所有文件）  
        /// </summary>  
        /// <param name="rootPath">要压缩的根目录</param>  
        /// <param name="destinationPath">保存路径</param>  
        /// <param name="compressLevel">压缩程度，范围0-9，数值越大，压缩程序越高</param>  
        public static void ZipFileFromDirectorySZL(string rootPath, string destinationPath, int compressLevel)
        {
            ZipFileFromDirectorySZL(rootPath, destinationPath, compressLevel, "");
        }

        /// <summary>  
        /// 取得目录下所有文件及文件夹，分别存入files及paths  
        /// </summary>  
        /// <param name="rootPath">根目录</param>  
        private static void GetAllDirectories(string rootPath)
        {
            string[] subPaths = Directory.GetDirectories(rootPath);//得到所有子目录  
            foreach (string path in subPaths)
            {
                GetAllDirectories(path);//对每一个字目录做与根目录相同的操作：即找到子目录并将当前目录的文件名存入List  
            }
            string[] tmpFiles = Directory.GetFiles(rootPath);
            foreach (string file in tmpFiles)
            {
                files.Add(file);//将当前目录中的所有文件全名存入文件List  
            }
            if (subPaths.Length == tmpFiles.Length && tmpFiles.Length == 0)//如果是空目录  
            {
                paths.Add(rootPath);//记录空目录  
            }
        }

        /// <summary>
        /// 解压缩文件(压缩文件中含有子目录)  
        /// </summary>
        /// <param name="zipfilepath">待解压缩的文件路径</param>
        /// <param name="unzippath">解压缩到指定目录  如果解压路径为空则解压到要解压的路径下</param>
        /// <param name="unzippassword">解压密码</param>
        public static void UnZipSZL(string zipfilepath, string unzippath, string unzippassword)
        {
            //解压出来的文件列表  
            //List<string> unzipFiles = new List<string>();

            if (unzippath[unzippath.Length - 1] != Path.DirectorySeparatorChar)
            {
                unzippath = unzippath + Path.DirectorySeparatorChar;
            }
            string directoryName = Path.GetDirectoryName(unzippath);
            //生成解压目录【用户解压到硬盘根目录时，不需要创建】  
            //if (!string.IsNullOrEmpty(directoryName))
            //{
            //    //如果目录存在则删除
            //    if (Directory.Exists(directoryName))
            //    {
            //        DeleteDirectoryFile(directoryName);
            //        Directory.Delete(directoryName);
            //    }
            //    Directory.CreateDirectory(directoryName);
            //}
            //else
            //{
            //    Directory.CreateDirectory(Path.GetDirectoryName(zipfilepath));
            //}
            ZipInputStream s = null;
            try
            {
                s = new ZipInputStream(File.OpenRead(zipfilepath));
                //解压密码
                if (unzippassword.Length > 0)
                {
                    s.Password = unzippassword;
                }
                ZipEntry theEntry;
                string tmpFullFile = "";
                string tmpUnZipDir = "";
                while ((theEntry = s.GetNextEntry()) != null)
                {

                    string fileName = Path.GetFileName(theEntry.Name);

                    if (fileName != String.Empty)
                    {
                        tmpFullFile = unzippath + theEntry.Name;
                        //如果文件的压缩后大小为0那么说明这个文件是空的,因此不需要进行读出写入  
                        if (theEntry.CompressedSize == 0)
                            break;
                        //解压文件到指定的目录  
                        tmpUnZipDir = Path.GetDirectoryName(tmpFullFile);
                        //建立下面的目录和子目录  
                        if (!Directory.Exists(tmpUnZipDir))
                        {
                            Directory.CreateDirectory(tmpUnZipDir);
                        }
                        //记录导出的文件  
                        //unzipFiles.Add(unzippath + theEntry.Name);

                        FileStream streamWriter = File.Create(tmpFullFile);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        streamWriter.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                    s.Dispose();
                }
            }
        }
        /// <summary>  
        /// 解压缩文件(压缩文件中含有子目录)  
        /// </summary>  
        /// <param name="zipfilepath">待解压缩的文件路径</param>  
        /// <param name="unzippath">解压缩到指定目录  如果解压路径为空则解压到要解压的路径下</param>  
        /// <returns>解压后的文件列表</returns>  
        public static void UnZipSZL(string zipfilepath, string unzippath)
        {
            UnZipSZL(zipfilepath, unzippath, "");
        }

        /// <summary>
        /// 删除指定文件夹里的所有文件
        /// </summary>
        /// <param name="pDirPath"></param>
        public static void DeleteDirectoryFile(string pDirPath)
        {
            DirectoryInfo strDirInfo = new DirectoryInfo(pDirPath);
            foreach (System.IO.DirectoryInfo d in strDirInfo.GetDirectories())
            {
                DeleteDirectoryFile(d.FullName);
                d.Attributes = FileAttributes.Normal;
                d.Delete();
            }
            foreach (System.IO.FileInfo f in strDirInfo.GetFiles())
            {
                f.Attributes = FileAttributes.Normal;
                f.Delete();
            }
        }
    }
}
