﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ImageService {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main() {
            //ImageService1 myService = new ImageService1();
            //myService.OnDebug();
            //System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new ImageService1()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
