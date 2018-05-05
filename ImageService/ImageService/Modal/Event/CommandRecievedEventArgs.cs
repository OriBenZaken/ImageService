using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>
    /// arguments for ICommand object.
    /// </summary>
    public class CommandRecievedEventArgs : EventArgs
    {
        #region Members
        public int CommandID { get; set; }      // The Command ID
        public string[] Args { get; set; }
        public string RequestDirPath { get; set; }  // The Request Directory
        #endregion

        /// <summary>
        /// CommandRecievedEventArgs constructor.
        /// </summary>
        /// <param name="id">Command ID</param>
        /// <param name="args">Arguments for the command.</param>
        /// <param name="path">path of the file relevant to the command.</param>
        public CommandRecievedEventArgs(int id, string[] args, string path)
        {
            CommandID = id;
            Args = args;
            RequestDirPath = path;
        }
    }
}