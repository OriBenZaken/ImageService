using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ImgServiceWebApplication.Models
{
    public class Config
    {

        [Required]
        [Display(Name = "Output Directory")]
        public string OutputDirectory { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Source Name")]
        public string SourceName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Log Name")]
        public string LogName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Thumbnail Size")]
        public int ThumbnailSize { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Handlers")]
        public ObservableCollection<string> Handlers { get; set; }

    }
}