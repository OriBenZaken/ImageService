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
        static Config config = new Config()
        {
            SourceName = "mysourcename",
            LogName = "mylogname",
            OutputDirectory = "myoutputdirectory",
            ThumbnailSize = 2,
            Handlers = new ObservableCollection<string>() { "handler1", "handler2", "handler3" }
        };
        // GET: Config
        public ActionResult Config()
        {
            return View(config);
        }

        // GET: Config/DeleteHandler/
        public ActionResult DeleteHandler(string toBeDeletedHandler)
        {
            //config.Handlers.Remove(toBeDeletedHandler);
            //ask user if he is sure he wants to delete the handler
            //return View("Confirm");
            return RedirectToAction("Confirm");

        }
        // GET: Confirm
        public ActionResult Confirm()
        {
            return View(config);
        }
        // GET: Config/DeleteOK/
        public ActionResult DeleteOK(string toBeDeletedHandler)
        {
            //delete the handler
            config.Handlers.Remove(toBeDeletedHandler);
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