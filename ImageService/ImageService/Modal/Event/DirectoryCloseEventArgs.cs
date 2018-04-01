using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Modal
{
    /// <summary>
    /// Argments for the DirectoryCloseEvent.
    /// </summary>
    public class DirectoryCloseEventArgs : EventArgs
    {
        #region Members
        public string DirectoryPath { get; set; }       // Path of the directory to be closed.
        public string Message { get; set; }             // The Message That goes to the logger.
        #endregion

        /// <summary>
        /// DirectoryCloseEventArgs constructor.
        /// </summary>
        /// <param name="dirPath">Path of the directory to be closed.</param>
        /// <param name="message">The Message That goes to the logger.</param>
        public DirectoryCloseEventArgs(string dirPath, string message)
        {
            DirectoryPath = dirPath;                    // Setting the Directory Name
            Message = message;                          // Storing the String
        }

    }
}