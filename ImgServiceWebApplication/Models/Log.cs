using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ImgServiceWebApplication.Models
{
    public class Log
    {
       
      
        //members
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Enrty Type")]
        public string EntryType { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Message")]
        public string Message { get; set; }

       


    }
}