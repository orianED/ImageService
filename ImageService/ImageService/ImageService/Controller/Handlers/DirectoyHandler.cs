using ImageService.Modal;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageService.Infrastructure;
using ImageService.Infrastructure.Enums;
using ImageService.Logging;
using ImageService.Logging.Modal;
using System.Text.RegularExpressions;
using System.Configuration;

namespace ImageService.Controller.Handlers {
    public class DirectoyHandler : IDirectoryHandler {
        #region Members
        private IImageController m_controller;              // The Image Processing Controller
        private ILoggingService m_logging;
        private FileSystemWatcher m_dirWatcher;             // The Watcher of the Dir
        private string m_path;                              // The Path of directory
        private string[] extensions = { ".png", ".jpg", ".bmp", ".gif" };
        #endregion

        public event EventHandler<DirectoryCloseEventArgs> DirectoryClose;              // The Event That Notifies that the Directory is being closed

        public DirectoyHandler(IImageController controller, ILoggingService logging) {
            m_controller = controller;
            m_logging = logging;
        }
        public void OnCommandRecieved(object sender, CommandRecievedEventArgs e) {
            bool result;

            string msg = m_controller.ExecuteCommand(e.CommandID, e.Args, out result);
            if (result) {
                m_logging.Log(msg, MessageTypeEnum.INFO);
            } else {
                m_logging.Log(msg, MessageTypeEnum.FAIL);
            }
        }

        public void StartHandleDirectory(string dirPath) {
            m_path = dirPath;
            m_dirWatcher = new FileSystemWatcher();
            m_dirWatcher.Path = m_path;
            m_dirWatcher.Created += new FileSystemEventHandler(NewEvent);
            m_dirWatcher.EnableRaisingEvents = true;
            m_logging.Log("Start handle directory: " + m_path, MessageTypeEnum.INFO);
        }

        public void NewEvent(object sender, FileSystemEventArgs e) {
            string[] args = new string[] { e.FullPath };
            if (extensions.Contains(Path.GetExtension(e.FullPath).ToLower())) {
                CommandRecievedEventArgs commandEventArgs = new CommandRecievedEventArgs((int)CommandEnum.NewFileCommand, args, m_path);
                OnCommandRecieved(this, commandEventArgs);
            }
        }

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
        // Implement Here!
    }
}
