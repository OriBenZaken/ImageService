using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    /// <summary>
    /// Command which adds a new Image file to the output directory (backup directory).
    /// </summary>
    public class NewFileCommand : ICommand
    {
        #region Members
        private IImageServiceModal m_modal;
        #endregion

        /// <summary>
        /// NewFileCommand constructor.
        /// </summary>
        /// <param name="modal">IImageModal</param>
        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }

        /// <summary>
        /// That function will execute the task of the command.
        /// </summary>
        /// <param name="args">arguments</param>
        /// <param name="result"> tells if the command succeded or not.</param>
        /// <returns>command return a string describes the operartion of the command.</returns>
        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            try
            {
                if (args.Length==0)
                {
                    throw new Exception("Ars len is 0!");
                }
                else if (File.Exists(args[0]))
                {
                    //calls to AddFile method to add the file in the given path to the backup directory.
                    return m_modal.AddFile(args[0], out result);
                }
                else
                {
                    throw new Exception ("NewFileCommand.Execute: path or params are not valid "+ args[0]);
                }
            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}
