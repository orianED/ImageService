using ImageCommunication.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Web;

namespace ImageWebApp.Models {
    public class HomeModel {
        private List<string[]> students;
        private ServiceController m_sc;

        public HomeModel() {
            students = new List<string[]>();
            this.ReadStudents();
        }

        public List<string[]> Students { get; set; }

        public string ServiceStatus {
            get {
                m_sc = new ServiceController("ImageService");
                if (m_sc.Status == ServiceControllerStatus.Running)
                    return "Running";
                return "Stopped";
            }
            set { }
        }

        public string StatusColor {
            get {
                m_sc = new ServiceController("ImageService");
                if (m_sc.Status == ServiceControllerStatus.Running)
                    return "Blue";
                return "Red";
            }
            set { }
        }

        public int NumOfTransfered {
            get; set;
        }

        public void PicsCount(string outputDirectory) {
            NumOfTransfered = 0;
            if (outputDirectory == null || outputDirectory == "")
                return;
            else {
                outputDirectory = outputDirectory + "\\Thumbnails";
                DirectoryInfo directoryName = new DirectoryInfo(outputDirectory);
                NumOfTransfered += directoryName.GetFiles("*.jpg", SearchOption.AllDirectories).Length;
                NumOfTransfered += directoryName.GetFiles("*.png", SearchOption.AllDirectories).Length;
                NumOfTransfered += directoryName.GetFiles("*.bmp", SearchOption.AllDirectories).Length;
                NumOfTransfered += directoryName.GetFiles("*.gif", SearchOption.AllDirectories).Length;
            }
        }

        public void ReadStudents() {
            try {
                string students_path = Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "App_Data", "StudentsDetails.txt");
                using (StreamReader sr = new StreamReader(students_path)) {
                    String line;

                    while ((line = sr.ReadLine()) != null) {
                        students.Add(line.Split(' '));
                    }
                }
            } catch (Exception e) {
                Debug.WriteLine("The file could not be read:");
                Debug.WriteLine(e.Message);
            }
            Students = students;
        }
    }
}