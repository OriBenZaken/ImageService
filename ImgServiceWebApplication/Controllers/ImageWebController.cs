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
        public ImageWebController()
        {
            ImageViewInfo.NotifyEvent -= Notify;
            ImageViewInfo.NotifyEvent += Notify;

        }
        void Notify()
        {
            ImageWeb();
            //RedirectToAction("ImageWeb");

        }


        // GET: ImageView
        public ActionResult ImageWeb()
        {
            if (ImageViewInfo.NumofPics!=0)
            {
                
            }
            ViewBag.NumofPics = ImageViewInfo.NumofPics;
            ViewBag.IsConnected = ImageViewInfo.IsConnected;
            return View(ImageViewInfo);
        }
        
    }
}