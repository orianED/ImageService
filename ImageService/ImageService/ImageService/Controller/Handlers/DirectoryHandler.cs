using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ImageService.Controller.Handlers {
    public class DirectoryHandler : IDirectoryHandler {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private string[] extensions = { ".png", ".jpg", ".bmp", ".gif" };
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoyHandler"/> class.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="logging">The logging.</param>
        public DirectoryHandler(IImageController controller, ILoggingService logging) {
            m_controller = controller;
            m_logging = logging;
        }

        /// <summary>
        /// Called when [command recieved] and call to the execute function.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="CommandRecievedEventArgs"/> instance containing the event data.</param>
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e) {
            bool result;

            string msg = m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            if (result) {
                m_logging.Log(msg, MessageTypeEnum.INFO);
            } else {
                m_logging.Log(msg, MessageTypeEnum.FAIL);
            }
        }

        /// <summary>
        /// Starts the handle directory (initializes the path for the handler and start).
        /// </summary>
        /// <param name="dirPath">The directory path.</param>
        public void StartHandleDirectory(string dirPath) {
            m_path = dirPath;
            m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.Path = m_path;
            m_dirWatcher.Created += new FileSystemEventHandler(NewEvent);
            m_dirWatcher.EnableRaisingEvents = true;
            m_logging.Log("Start handle directory: " + m_path, MessageTypeEnum.INFO);
        }

        /// <summary>
        /// Check if the new event is a picture and if it is, operate the command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="FileSystemEventArgs"/> instance containing the event data.</param>
        public void NewEvent(object sender, FileSystemEventArgs e) {
            string[] args = new string[] { e.FullPath };
            if (extensions.Contains(Path.GetExtension(e.FullPath).ToLower())) {
                CommandRecievedEventArgs commandEventArgs = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, args, m_path);
                OnCommandRecieved(this, commandEventArgs);
            }
        }

        /// <summary>
        /// Closes the handler and commit to logger.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="DirectoryCloseEventArgs"/> instance containing the event data.</param>
        public void CloseHandler(object sender, DirectoryCloseEventArgs e) {
            try {
                m_dirWatcher.EnableRaisingEvents = false;
                DirectoryCloseEventArgs directoryCloseArgs = new DirectoryCloseEventArgs(m_path, "Closing handler for: " + m_path);
                DirectoryClose?.Invoke(this, directoryCloseArgs);
                m_logging.Log("Closing handler for: " + m_path, MessageTypeEnum.INFO);
            } catch (Exception e1) {
                m_logging.Log("Failed to closed handler for: " + m_path, MessageTypeEnum.INFO);
            } finally {
                m_dirWatcher.Created -= new FileSystemEventHandler(NewEvent);
            }
        }
    }
}
