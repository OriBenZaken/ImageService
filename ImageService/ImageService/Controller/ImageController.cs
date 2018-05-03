using ImageService.Commands;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller
{
    /// <summary>
    /// ImageController. Implements the IImageController Interface.
    /// </summary>
    public class ImageController : IImageController
    {
        #region Members
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;             // Commands dictionary : [Command ID, Command]
        #endregion

        /// <summary>
        /// ImageController Constructor.
        /// </summary>
        /// <param name="modal">Modal of the system</param>
        public ImageController(IImageServiceModal modal)
        {
            m_modal = modal;                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>();

            // For Now will contain NEW_FILE_COMMAND
            this.commands[((int)CommandEnum.NewFileCommand)] = new NewFileCommand(this.m_modal);
            this.commands[((int)CommandEnum.GetConfigCommand)] = new GetConfigCommand();
            this.commands[((int)CommandEnum.CloseHandler)] = new CloseHandlerCommand();
        //    this.commands[((int)CommandEnum.GetConfigCommand)] = new GetConfigCommand();


        }

        /// <summary>
        /// Executing the Command Requet
        /// </summary>
        /// <param name="commandID">Command ID</param>
        /// <param name="args">Arguments for the command</param>
        /// <param name="result">Tells is the command succeeded or not.</param>
        /// <returns></returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful)
        {
            Task<Tuple<string, bool>> task = new Task<Tuple<string, bool>>(() => {
                bool resultSuccesfulTemp;
                string message = this.commands[commandID].Execute(args, out resultSuccesfulTemp);
                return Tuple.Create(message, resultSuccesfulTemp);
            });
            task.Start();
            task.Wait();
            Tuple<string, bool> result = task.Result;
            resultSuccesful = result.Item2;
            return result.Item1;
        }
    }
}
