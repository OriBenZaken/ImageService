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
        private IImageServiceClient GuiClient;

        public ConfigController()
        {
            //GuiClient = ImageServiceClient.Instance;
            //this.GuiClient.RecieveCommand();
            //this.GuiClient.UpdateResponse += UpdateResponse;
            //config = new Config()
            //{
            //    SourceName = "",
            //    LogName = "",
            //    OutputDirectory = "",
            //    ThumbnailSize = 1,
            //    Handlers = new ObservableCollection<string>() { "LALA", "LOLO"},
            //    Enabled = false
            //};
            //string[] arr = new string[5];
            //CommandRecievedEventArgs request = new CommandRecievedEventArgs((int)CommandEnum.GetConfigCommand, arr, "");
            //this.GuiClient.SendCommand(request);

        }
        /// <summary>
        /// UpdateResponse function.
        /// updates the model when message recieved from srv.
        /// </summary>
        /// <param name="responseObj">the info came from srv</param>
        private void UpdateResponse(CommandRecievedEventArgs responseObj)
        {
            try
            {
                if (responseObj != null)
                {
                    switch (responseObj.CommandID)
                    {
                        case (int)CommandEnum.GetConfigCommand:
                            //UpdateConfigurations(responseObj);
                            break;
                        case (int)CommandEnum.CloseHandler:
                            //CloseHandler(responseObj);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        static Config config = new Config()
        {
            SourceName = "mysource",
            LogName = "mylog",
            OutputDirectory = "myoutputdir",
            ThumbnailSize = 1,
            Handlers = new ObservableCollection<string>() { "LALA", "LOLO" },
            Enabled = false
        };
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
            //delete the handler
            config.Handlers.Remove(m_toBeDeletedHandler);
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