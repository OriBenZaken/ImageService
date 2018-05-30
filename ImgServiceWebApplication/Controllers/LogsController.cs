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
        static List<Log> logsEntries = new List<Log>()
        {
          new Log  { EntryType = "Info", Message = "Hi" },
          new Log  { EntryType = "Info", Message = "Bye" }
        };

        // GET: Logs
        public ActionResult Logs()
        {
            return View(logsEntries);
        }
    }
}