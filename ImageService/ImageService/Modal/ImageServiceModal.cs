//using ImageService.Infrastructure;
using System;
using System.Collections.Generic;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>
    /// Implementation of IImageServiceModal Interface.
    /// </summary>
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        #endregion

        #region Properties
        // The Output Folder
        public string OutputFolder
        {
            get
            {
                return this.m_OutputFolder;
            }
            set
            {
                this.m_OutputFolder = value;
            }
        }

        // The Size Of The Thumbnail Size
        public int ThumbnailSize
        {
            get
            {
                return this.m_thumbnailSize;
            }
            set
            {
                this.m_thumbnailSize = value;
            }
        }
        #endregion

        public string AddFile(string path, out bool result)
        {
            try
            {
                string year = String.Empty;
                string month = String.Empty;
                string returnMsg = string.Empty;
                if (File.Exists(path))
                {
                    // Get file creation time : year and month
                    DateTime date = GetExplorerFileDate(path);
                    year = date.Year.ToString();
                    month = date.Month.ToString();
                    DirectoryInfo dirOutput = Directory.CreateDirectory(m_OutputFolder);
                    // make the output directory hidden
                    dirOutput.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    // Create the suitables folders for the file according to it's creation time
                    Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails");
                    string createFoldersMsg = this.CreateFolders(m_OutputFolder, year, month);
                    string createThumbsMsg = this.CreateFolders(m_OutputFolder + "\\" + "Thumbnails", year, month);
                    if (createFoldersMsg != string.Empty || createThumbsMsg != string.Empty)
                    {
                        throw new Exception("Error while creating folders");
                    }
                    string pathTargetFolder = m_OutputFolder + "\\" + year + "\\" + month + "\\";
                    string newPath = pathTargetFolder + Path.GetFileName(path);
                    
                    if (File.Exists(newPath))
                    {
                        newPath = this.GetAvailablePath(newPath, pathTargetFolder);
                    }
                    File.Move(path, newPath);
                    returnMsg = "Added " + Path.GetFileName(newPath) + " to " + pathTargetFolder;
                    // create thumbnail photo.
                    string thumbsNewPath = m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\" + Path.GetFileName(path);
                    if (File.Exists(thumbsNewPath))
                    {
                        thumbsNewPath = this.GetAvailablePath(thumbsNewPath, m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\");
                    }
                    
                    Image thumb = Image.FromFile(newPath);
                    thumb = (Image)(new Bitmap(thumb, new Size(this.m_thumbnailSize, this.m_thumbnailSize)));
                    thumb.Save(thumbsNewPath);
                    returnMsg += " and added thumb " + Path.GetFileName(path);
                    result = true;
                    return returnMsg;
                }
                else
                {
                    throw new Exception("File doesn't exists");
                }

            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }

        static DateTime GetExplorerFileDate(string filename)
        {
            DateTime now = DateTime.Now;
            TimeSpan localOffset = now - now.ToUniversalTime();
            return File.GetLastWriteTimeUtc(filename) + localOffset;
        }
        private string GetAvailablePath(string newPath, string pathTargetFolder)
        {
            int i = 1;
            string fileName = Path.GetFileNameWithoutExtension(newPath);
            string extension = Path.GetExtension(newPath);
            while (File.Exists((newPath = pathTargetFolder + fileName +" ("+ i.ToString()+")" + extension)))
            {
                i++;
            }
            return newPath;
        }

        /// <summary>
        /// Creates a year folder, and inside it a month folder in dirPath directory.
        /// </summary>
        /// <param name="dirPath">Directory path.</param>
        /// <param name="year">Year</param>
        /// <param name="month">Month</param>
        /// <returns></returns>
        private string CreateFolders(string dirPath, string year, string month)
        {
            try
            {
                Directory.CreateDirectory(dirPath + "\\" + year);
                Directory.CreateDirectory(dirPath + "\\" + year + "\\" + month);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}
