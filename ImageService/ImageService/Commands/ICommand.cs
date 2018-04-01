using ImageService.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ImageService.Commands
{
    /// <summary>
    /// Command Interface. Represents an object which have a single task to perform.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// That function will execute the task of the command.
        /// </summary>
        /// <param name="args">arguments</param>
        /// <param name="result"> tells if the command succeded or not.</param>
        /// <returns>command return a string describes the operartion of the command.</returns>
        string Execute(string[] args, out bool result); 
    }
}
