using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace ImgServiceWebApplication.Models
{
    public class Photo
    {
        public Photo(string imageUrl)
        {
            ImageUrl = imageUrl;
            Name = Path.GetFileNameWithoutExtension(ImageUrl);
            Month = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(ImageUrl));
            Year = Path.GetFileNameWithoutExtension(Path.GetDirectoryName((Path.GetDirectoryName(ImageUrl))));
            ImageRelativePath = @"~\Images\Thumbnails\" + Year + @"\" + Month + @"\" + Path.GetFileName(ImageUrl);

        }


        //members
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Year")]
        public string Year { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Month")]
        public string Month { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImageUrl")]
        public string ImageUrl { get; set; }

        [Required]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "ImageRelativePath")]
        public string ImageRelativePath { get; set; }

    }
}