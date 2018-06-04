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
        Photo m_currentPhoto;
        string m_photoPath;
        string m_photoThumbnailPath;
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
      
            
        public ActionResult PhotosViewer(string photoPath, string photoThumbnailPath)
        {
            m_photoPath = photoPath;
            m_photoThumbnailPath = photoThumbnailPath;
            ViewBag.Photo = photoPath;
            return View(new Photo(""));
        }
    }
}