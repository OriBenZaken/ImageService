using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller.Handlers
{
    /// <summary>
    /// Interface of object which responsible for listening to a directory and and do operations, such as backup
    /// opreation, when files in the directory is changing/creating.
    /// </summary>
    public interface IDirectoryHandler
    { 
        event EventHandler<DirectoryCloseEventArgs> DirectoryClose;               // The Event That Notifies that the Directory is being closed
        /// <summary>
        /// The Function Recieves the directory to Handle
        /// </summary>
        /// <param name="dirPath">Directory path</param>
        void StartHandleDirectory(string dirPath);

        /// <summary>
        ///  The meothod that will be activated upon new Command when the CommandRecived event will be invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">arguments of CommandRecieved event.</param>
        void OnCommandRecieved(object sender, CommandRecievedEventArgs e);

        /// <summary>
        /// The meothod that will be activated upon server closing, when the OnCloseServer event will be invoked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">arguments of DirectoryClose event.</param>
        void OnCloseHandler(object sender, DirectoryCloseEventArgs e);
    }
}
