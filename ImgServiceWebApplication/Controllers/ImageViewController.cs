using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgServiceWebApplication.Models
{
    public class ImageViewController : Controller
    {

        // GET: ImageView
        public ActionResult Index()
        {
            return View();
        }
    }
}