using ImageService.Commands;
using ImageService.Infrastructure;
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

        public ImageController(IImageServiceModal modal) {
            m_modal = modal;                                    // Storing the Modal Of The System
            commands = new Dictionary<int, ICommand>();
            commands.Add((int)CommandEnum.NewFileCommand, new NewFileCommand(this.m_modal));
        }
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