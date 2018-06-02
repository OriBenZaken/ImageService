using ImgServiceWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgServiceWebApplication.Controllers
{
    public class PhotosController : Controller
    {
        public static PhotosCollection photos = new PhotosCollection();
        public PhotosController()
        {
            photos.NotifyEvent -= Notify;
            photos.NotifyEvent += Notify;

        }
        void Notify()
        {
            Photos();
            //RedirectToAction("ImageWeb");

        }
        // GET: Photos
        public ActionResult Photos()
        {
            return View(photos.PhotosList);
        }
    }
}