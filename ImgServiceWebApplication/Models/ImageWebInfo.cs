using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using static ImgServiceWebApplication.Models.Config;

namespace ImgServiceWebApplication.Models
{
    public class ImageWebInfo
    {
        private static Communication.IImageServiceClient GuiClient { get; set; }
        public event NotifyAboutChange NotifyEvent;
        private Config m_config;

        public ImageWebInfo()
        {
            GuiClient = Communication.ImageServiceClient.Instance;
            IsConnected = GuiClient.IsConnected;
            m_config = new Config();
            m_config.Notify += Notify;
            NumofPics = 0;
            Students = GetStudents();
        }
        void Notify()
        {
            NumofPics = GetNumOfPics(m_config.OutputDirectory);
            NotifyEvent?.Invoke();
        }

        public static int GetNumOfPics(string outputDir)
        {
            if (outputDir == null || outputDir == "")
            {
                return 0;
            }
            int counter = 0;
            while (outputDir == null && (counter < 2)) { System.Threading.Thread.Sleep(1000); counter++; }
            int sum = 0;
            DirectoryInfo di = new DirectoryInfo(outputDir);
            sum += di.GetFiles("*.PNG", SearchOption.AllDirectories).Length;
            sum += di.GetFiles("*.BMP", SearchOption.AllDirectories).Length;
            sum += di.GetFiles("*.JPG", SearchOption.AllDirectories).Length;
            sum += di.GetFiles("*.GIF", SearchOption.AllDirectories).Length;
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

        [Required]
        [Display(Name = "Is Connected")]
        public bool IsConnected { get; set; }

        [Required]
        [Display(Name = "Num of Pics")]
        public int NumofPics { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Students")]
        public List<Student> Students { get; set; }


        public class Student
        {
            [Required]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }

            [Required]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }

            [Required]
            [Display(Name = "ID")]
            public string ID { get; set; }
        }
    }
}