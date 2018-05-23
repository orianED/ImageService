using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Commands {
    public class LogCommand : ICommand {

        private ILoggingService loggingService;

        public LogCommand(ILoggingService logger) {
            this.loggingService = logger;
        }

        public string Execute(string[] args, out bool result) {
            try {
                string[] log = new string[1];
                log[0] = JsonConvert.SerializeObject(this.loggingService.Logger);
                CommandRecievedEventArgs commandLog = new CommandRecievedEventArgs((int)CommandEnum.LogCommand, log, null);
                result = true;
                return JsonConvert.SerializeObject(commandLog);
            } catch {
                result = false;
                return "LogCommand execute Failed!";
            }
        }
    }
}