﻿using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    public class NewFileCommand : ICommand
    {
        private IImageServiceModal m_modal;

        public NewFileCommand(IImageServiceModal modal)
        {
            m_modal = modal;            // Storing the Modal
        }

        public string Execute(string[] args, out bool result)
        {
            // The String Will Return the New Path if result = true, and will return the error message
            try
            {
                if (args.Length>0 && File.Exists(args[0]))
                {
                    return m_modal.AddFile(args[0], out result);
                }
                else
                {
                    throw new Exception ("NewFileCommand.Execute: path or params are not valid");
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
