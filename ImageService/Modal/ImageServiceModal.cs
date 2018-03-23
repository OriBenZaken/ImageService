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
        private string m_OutputFolder;            // The Output Folder
        private int m_thumbnailSize;              // The Size Of The Thumbnail Size

        public string AddFile(string path, out bool result)
        {
            try
            {
                string year = String.Empty;
                string month = String.Empty;

                if (File.Exists(path))
                {
                    DateTime date = File.GetCreationTime(path);
                    year = date.Year.ToString();
                    month = date.Month.ToString();
                    Directory.CreateDirectory(m_OutputFolder);
                    Directory.CreateDirectory(m_OutputFolder + "\\" + year);
                    //create folders for months
                    for (int j = 1; j <= 12; j++)
                    {
                        Directory.CreateDirectory(m_OutputFolder + "\\" + year + "\\" + j.ToString());
                    }
                    File.Copy(path, m_OutputFolder + "\\" + year + "\\" + month);
                    result = true;
                    return string.Empty;
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

        #endregion

    }
}
