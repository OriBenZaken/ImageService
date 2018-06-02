using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImgServiceWebApplication.Communication;
using ImgServiceWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgServiceWebApplication.Controllers
{
    public class ConfigController : Controller
    {
        static Config config = new Config();

        public ConfigController()
        {
            config.Notify -= Notify;
            config.Notify += Notify;
        }

       

        public void Notify()
        {
            Config();
           // RedirectToAction("Config");
        }
        private static string m_toBeDeletedHandler;
      

        // GET: Config/DeleteHandler/
        public ActionResult DeleteHandler(string toBeDeletedHandler)
        {
            //config.Handlers.Remove(toBeDeletedHandler);
            //ask user if he is sure he wants to delete the handler
            //return View("Confirm");
            m_toBeDeletedHandler = toBeDeletedHandler;
            return RedirectToAction("Confirm");

        }
        // GET: Confirm
        public ActionResult Confirm()
        {
            return View(config);
        }
        // GET: Config
        public ActionResult Config()
        {
            return View(config);
        }
        // GET: Config/DeleteOK/
        public ActionResult DeleteOK()
        {
           
            config.DeleteHandler(m_toBeDeletedHandler);
            //delete the handler
            //config.Handlers.Remove(m_toBeDeletedHandler);
            //go back to config page
            return RedirectToAction("Config");

        }
        // GET: Config/DeleteCancel/
        public ActionResult DeleteCancel()
        {
            //go back to config page
            return RedirectToAction("Config");

        }
    }
}