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
    public class ImageServiceModal : IImageServiceModal
    {
        #region Members
        public string OutputFolder
        {
            // The Output Folder
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

        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size
        //todo: Hidden directories
        public string AddFile(string path, out bool result)
        {
            try
            {
                string year = String.Empty;
                string month = String.Empty;
                string returnMsg = string.Empty;
                if (File.Exists(path))
                {
                    DateTime date = File.GetCreationTime(path);
                    year = date.Year.ToString();
                    month = date.Month.ToString();
                    DirectoryInfo dirOutput = Directory.CreateDirectory(m_OutputFolder);
                    // make the output directory hidden
                    dirOutput.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    Directory.CreateDirectory(m_OutputFolder + "\\" + "Thumbnails");
                    string createFoldersMsg = this.CreateFolders(m_OutputFolder, year, month);
                    string createThumbsMsg = this.CreateFolders(m_OutputFolder + "\\" + "Thumbnails", year, month);
                    if (createFoldersMsg != string.Empty || createThumbsMsg != string.Empty)
                    {
                        throw new Exception("Error while creating folders");
                    }
                    string pathTargetFolder = m_OutputFolder + "\\" + year + "\\" + month + "\\";
                    if (!File.Exists(pathTargetFolder + Path.GetFileName(path)))
                    {
                        File.Copy(path, pathTargetFolder + Path.GetFileName(path));
                        returnMsg = "Added " + Path.GetFileName(path) + " to " + pathTargetFolder;
                    }
                    if (!File.Exists((m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\" + Path.GetFileName(path))))
                    {
                        Image thumb = Image.FromFile(path);
                        thumb = (Image)(new Bitmap(thumb, new Size(this.m_thumbnailSize, this.m_thumbnailSize)));
                        thumb.Save(m_OutputFolder + "\\" + "Thumbnails" + "\\" + year + "\\" + month + "\\" + Path.GetFileName(path));
                        returnMsg += " and added thumb " + Path.GetFileName(path);
                    }
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

        private string CreateFolders(string dirPath, string year, string month)
        {
            try
            {
                Directory.CreateDirectory(dirPath + "\\" + year);
                Directory.CreateDirectory(dirPath + "\\" + year + "\\" + month);
                return string.Empty;
            } catch (Exception ex)
            {
                return ex.ToString();
            }

        
        }

        #endregion
    }


}
