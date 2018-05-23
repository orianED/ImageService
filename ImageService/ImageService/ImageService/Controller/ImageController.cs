using ImageService.Commands;
using ImageService.ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Controller {
    public class ImageController : IImageController {
        private IImageServiceModal m_modal;                      // The Modal Object
        private Dictionary<int, ICommand> commands;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="modal">The modal.</param>
        public ImageController(IImageServiceModal modal) {
            m_modal = modal;                                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>();
            commands.Add((int)CommandEnum.NewFileCommand, new NewFileCommand(this.m_modal));
            commands.Add((int)CommandEnum.GetConfigCommand, new GetConfigCommand());
            //commands.Add((int)CommandEnum.CloseCommand, new CloseCommand());
        }

        /// <summary>
        /// Executes the command by the ID with the args in new task.
        /// </summary>
        /// <param name="commandID">The command identifier.</param>
        /// <param name="args">The arguments.</param>
        /// <param name="resultSuccesful">if set to <c>true</c> [result succesful].</param>
        /// <returns></returns>
        public string ExecuteCommand(int commandID, string[] args, out bool resultSuccesful) {
            Task<Tuple<string, bool>> task = new Task<Tuple<string, bool>>(() => {
                bool result;
                String str = this.commands[commandID].Execute(args, out result);
                return Tuple.Create(str, result);
            });
            task.Start();
            task.Wait();
            Tuple<string, bool> tupleResult = task.Result;
            resultSuccesful = tupleResult.Item2;
            return tupleResult.Item1;
        }
    }
}