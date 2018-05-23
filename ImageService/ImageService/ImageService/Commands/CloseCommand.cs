using ImageService.Commands;
using ImageService.Infrastructure.Enums;
using ImageService.Modal;
using ImageService.Server;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.ImageService.Commands {
    class CloseCommand : ICommand {
        private ImageServer m_server;

        public CloseCommand(ImageServer server) {
            m_server = server;
        }

        public string Execute(string[] args, out bool result) {
            if (args.Length != 1)
                throw new Exception("Too few/mant args for the remove func");
            try {
                string[] handlers = ConfigurationManager.AppSettings.Get("Handler").Split(';');
                StringBuilder newHandlers = new StringBuilder();
                foreach (string dir in handlers) {
                    if (String.Compare(args[0], dir) != 0) {
                        newHandlers.Append(dir + ";");
                    }
                }
                ConfigurationManager.AppSettings.Set("Handler", newHandlers.ToString().TrimEnd(';'));
                m_server.removeHandlerCommand(args[0]);
                CommandRecievedEventArgs commandArgs = new CommandRecievedEventArgs((int)CommandEnum.CloseCommand, args, "");
                result = true;
                return "";
            } catch (Exception e) {
                result = false;
                return e.ToString();
            }
        }
    }
}
