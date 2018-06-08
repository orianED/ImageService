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
        private IClient m_client;
        private List<string[]> students;
        private ServiceController m_sc;

        public HomeModel() {
            try {
                m_client = Client.GetInstance;
                m_sc = new ServiceController("ImageService");
                students = new List<string[]>();
                this.ReadStudents();
            } catch (Exception e) {
                Debug.WriteLine("Connection Failure");
            }
        }

        public List<string[]> Students { get; set; }
        
        public string ServiceStatus {
            get {
                if (m_sc.Status == ServiceControllerStatus.Running)
                    return "Running";
                return "Stopped";
            }
            set { }
        }

        public string StatusColor {
            get {
                if (m_sc.Status == ServiceControllerStatus.Running)
                    return "Blue";
                return "Red";
            }
            set { }
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