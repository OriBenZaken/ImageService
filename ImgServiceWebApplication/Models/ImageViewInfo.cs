using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImgServiceWebApplication.Models
{
    public class ImageViewInfo
    {

       

        [Required]
        [Display(Name = "Is Connected")]
        public bool IsConnected { get; set; }

      

        [Required]
        [Display(Name = "Num of Pics")]
        public int NumofPics { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Students")]
        public string Students { get; set; }
    }
}