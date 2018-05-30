using ImgServiceWebApplication.Models;
using System;
using System.Collections.Generic;
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
            Handlers = new List<string>() { "handler1", "handler2", "handler3" }
        };
        // GET: Config
        public ActionResult Config()
        {
            return View(config);
        }
    }
}