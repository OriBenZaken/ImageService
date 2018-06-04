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
        static ImageWebInfo ImageViewInfo = new ImageWebInfo();
        /// <summary>
        /// constructor.
        /// </summary>
        public ImageWebController()
        {
            ImageViewInfo.NotifyEvent -= Notify;
            ImageViewInfo.NotifyEvent += Notify;

        }
        /// <summary>
        /// Notify function.
        /// notify view about update.
        /// </summary>
        void Notify()
        {
            ImageWeb();
        }

        // GET: ImageView
        public ActionResult ImageWeb()
        {
            ViewBag.NumofPics = ImageViewInfo.NumofPics;
            ViewBag.IsConnected = ImageViewInfo.IsConnected;
            return View(ImageViewInfo);
        }
        
    }
}