using ImageService.Infrastructure;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Commands {
    public class NewFileCommand : ICommand {
        private IImageServiceModal m_modal;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewFileCommand"/> class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public NewFileCommand(IImageServiceModal modal) {
            m_modal = modal;            // Storing the Modal
        }

        /// <summary>
        /// Executes the new file command with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <param name="result">if set to <c>true</c> [result].</param>
        /// <returns></returns>
        /// <exception cref="Exception">No args received!</exception>
        public string Execute(string[] args, out bool result) {
            try {
                if (args.Length == 0) {
                    throw new Exception("No args received!");
                } else if (File.Exists(args[0])) {
                    return m_modal.AddFile(args[0], out result);
                }
                result = true;
                return " ";
            } catch (Exception exeption) {
                result = false;
                return exeption.ToString();
            }
        }
    }
}