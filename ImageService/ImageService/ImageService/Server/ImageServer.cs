using ImageService.Controller;
using ImageService.Controller.Handlers;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Modal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageService.Server {
    public class ImageServer {
        #region Members
        private IImageController m_controller;
        private ILoggingService m_logging;
        #endregion

        #region Properties
        public event EventHandler<CommandRecievedEventArgs> CommandRecieved;          // The event that notifies about a new Command being recieved
        public event EventHandler<DirectoryCloseEventArgs> CloseServer;
        public Dictionary<string, IDirectoryHandler> Handlers { get; set; }
        #endregion

        /// <summary>
        /// constructor, start the server and creath hanlers for the directory we need to watch
        /// </summary>
        /// <param name="m_controller">The controller.</param>
        /// <param name="m_logging">The logger.</param>
        public ImageServer(IImageController m_controller, ILoggingService m_logging) {
            this.m_controller = m_controller;
            this.m_logging = m_logging;
            Handlers = new Dictionary<string, IDirectoryHandler>();
            string[] dirs = ConfigurationManager.AppSettings.Get("Handler").Split(';');

            foreach (string dir in dirs) {
                IDirectoryHandler handler = new DirectoryHandler(m_controller, m_logging);
                Handlers[dir] = handler;
                this.m_logging.Log("handler created for " + dir + " directory", Logging.Modal.MessageTypeEnum.INFO);
                CommandRecieved += handler.OnCommandRecieved;
                CloseServer += handler.CloseHandler;
                handler.StartHandleDirectory(dir);
                this.m_logging.Log("handler started for " + dir + " directory", Logging.Modal.MessageTypeEnum.INFO);

            }
        }

        /// <summary>
        /// Called when the service closed and closing all the handlers.
        /// </summary>
        public void OnCloseServer() {
            try {
                CloseServer?.Invoke(this, null);
                this.m_logging.Log("The CloseServer function succeeded", Logging.Modal.MessageTypeEnum.INFO);
            } catch {
                this.m_logging.Log("There is a problem to close the server", Logging.Modal.MessageTypeEnum.FAIL);
            }
        }

        public void removeHandlerCommand(string dir) {
            if (Handlers.ContainsKey(dir)) {
                IDirectoryHandler handler = Handlers[dir];
                CloseServer -= handler.CloseHandler;
                handler.CloseHandler(this, null);
                Handlers.Remove(dir);
            }
        }
    }
}