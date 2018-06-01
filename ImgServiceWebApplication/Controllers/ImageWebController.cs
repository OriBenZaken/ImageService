using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static ImgServiceWebApplication.Models.ImageWebInfo;

namespace ImgServiceWebApplication.Models
{
    public class ImageWebController : Controller
    {

        // GET: ImageView
        public ActionResult ImageWeb()
        {
            ViewBag.NumofPics = ImageViewInfo.NumofPics;
            ViewBag.IsConnected = ImageViewInfo.IsConnected;
            return View(ImageViewInfo);
        }
        static ImageWebInfo ImageViewInfo = new ImageWebInfo()
        {
            IsConnected = GetServiceStatus(),
            NumofPics = GetNumOfPics(),
            Students = GetStudents()
        };
       

        public static Boolean GetServiceStatus()
        {
            return true;
        }

        public static int GetNumOfPics()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Users\lizah\Desktop\output");
            int sum = dir.GetFiles().Length;
            return sum;
        }

        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            StreamReader file = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/StudentsDetails.txt"));
            string line;

            while ((line = file.ReadLine()) != null)
            {
                string[] param = line.Split(',');
                students.Add(new Student() { FirstName = param[0], LastName = param[1], ID = param[2] });
            }
            file.Close();
            return students;
        }
    }
}