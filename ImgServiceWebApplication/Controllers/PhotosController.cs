using ImgServiceWebApplication.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;

namespace ImgServiceWebApplication.Controllers
{
    public class PhotosController : Controller
    {
        public static PhotosCollection photos = new PhotosCollection();
        private static Photo m_currentPhoto;
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
            photos.PhotosList.Clear();
            photos.GetPhotos();

            return View(photos.PhotosList);
        }


        //public ActionResult PhotosViewer(string photoPath, string photoThumbnailPath)
        //{
        //    m_photoPath = photoPath;
        //    m_photoThumbnailPath = photoThumbnailPath;
        //    ViewBag.Photo = photoPath;
        //    return View(new Photo(""));
        //}

        public ActionResult PhotosViewer(string photoRelPath)
        {
            UpdateCurrentPhotoFromRelPath(photoRelPath);
            return View(m_currentPhoto);
        }

        public ActionResult DeletePhoto(string photoRelPath)
        {
            UpdateCurrentPhotoFromRelPath(photoRelPath);
            return View(m_currentPhoto);
        }

        public ActionResult DeleteYes(string photoRelPath)
        {
            try
            {
                System.IO.File.Delete(m_currentPhoto.ImageUrl);
                System.IO.File.Delete(m_currentPhoto.ImageFullUrl);
                photos.PhotosList.Remove(m_currentPhoto);
            } catch (Exception ex)
            {
            }


            return RedirectToAction("Photos");
        }

        private void UpdateCurrentPhotoFromRelPath(string photoRelPath)
        {
            foreach (Photo photo in photos.PhotosList)
            {
                if (photo.ImageRelativePath == photoRelPath)
                {
                    m_currentPhoto = photo;
                    break;
                }
            }
        }
    }
}