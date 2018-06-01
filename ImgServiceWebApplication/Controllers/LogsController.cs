using ImgServiceWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImgServiceWebApplication.Controllers
{
    public class LogsController : Controller
    {
        public static LogCollection log = new LogCollection();
        public LogsController()
        {
            log.Notify -= Notify;
            log.Notify += Notify;
        }



        public void Notify()
        {
            RedirectToAction("Logs");
        }


        // GET: Logs
        public ActionResult Logs()
        {
            return View(log.LogEntries);
        }
    }
}