using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using static ImgServiceWebApplication.Models.Config;

namespace ImgServiceWebApplication.Models
{
    public class PhotosCollection
    {
        public event NotifyAboutChange NotifyEvent;
        private static Config m_config;
        private string m_outputDir;
        public List<Photo> PhotosList = new List<Photo>();

        public PhotosCollection()
        {
            m_config = new Config();
            m_config.Notify += Notify;
        }
        void Notify()
        {
            if (m_config.OutputDirectory != "")
            {
                //TODO:
                m_outputDir = m_config.OutputDirectory;
                GetPhotos();
                NotifyEvent?.Invoke();
            }
        }

        private void GetPhotos()
        {
            string thumbnailDir = m_outputDir + "\\Thumbnails";
            if (!Directory.Exists(thumbnailDir))
            {
                return;
            }
            DirectoryInfo di = new DirectoryInfo(thumbnailDir);
            //The only file types are relevant.
            string[] validExtensions = { ".jpg", ".png", ".gif", ".bmp" };
            foreach (DirectoryInfo yearDirInfo in di.GetDirectories())
            {
                if (!Path.GetDirectoryName(yearDirInfo.FullName).EndsWith("Thumbnails"))
                {
                    continue;
                }
                foreach (DirectoryInfo monthDirInfo in yearDirInfo.GetDirectories())
                {
                   

                    foreach (FileInfo fileInfo in monthDirInfo.GetFiles())
                    {
                        if (validExtensions.Contains(fileInfo.Extension.ToLower()))
                        {
                            PhotosList.Add(new Photo(fileInfo.FullName));
                        }
                    }
                }
            }
        }

    }
}