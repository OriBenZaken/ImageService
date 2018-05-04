using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// Interface of Image Processing Controller.
    /// </summary>
    public interface IImageController
    {
        /// <summary>
        /// Executing the Command Requet
        /// </summary>
        /// <param name="commandID">Command ID</param>
        /// <param name="args">Arguments for the command</param>
        /// <param name="result">Tells is the command succeeded or not.</param>
        /// <returns></returns>
        string ExecuteCommand(int commandID, string[] args, out bool result);
        ImageServer ImageServer { get; set; }

    }
}
