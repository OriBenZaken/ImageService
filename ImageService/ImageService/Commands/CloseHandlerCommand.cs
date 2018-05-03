using ImageService.Commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands
{
    class CloseHandlerCommand : ICommand
    {
        public string Execute(string[] args, out bool result)
        {
            try
            {
                result = true;
                if (args==null || args.Length == 0)
                {
                    throw new Exception("Invalid args for deleting handler");
                }
                string toBeDeletedHandler = args[0];
                string[] directories = (ConfigurationManager.AppSettings.Get("Handler").Split(';'));
                StringBuilder sbNewHandlers = new StringBuilder();
                for (int i = 0; i < directories.Length; i++)
                {
                    if (directories[i]!=toBeDeletedHandler)
                    {
                        sbNewHandlers.Append(directories[i] + ";");
                    }
                }
                string newHandlers = (sbNewHandlers.ToString()).TrimEnd(';');
                Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                config.AppSettings.Settings["Handler"].Value = newHandlers;
                config.Save(ConfigurationSaveMode.Modified);
                //todo: stop listen to this dir!!
                //todo: update other customers!!!!!!
                return string.Empty;
            }
            catch (Exception ex)
            {
                result = false;
                return ex.ToString();
            }
        }
    }
}
