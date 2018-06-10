using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ImageWebApp.Models {
    public class PhotosModel {
        private static Regex r = new Regex(":");

        public string OutputDir { get; set; }
        public string ThumbnailsDir { get; set; }
        public string[] Paths { get; set; }
        public List<Dictionary<string, string>> Pictures;

        public PhotosModel(string Output, string ThumbnailSize) {
            Pictures = new List<Dictionary<string, string>>();
            try {
                OutputDir = Output;
                ThumbnailsDir = Path.Combine(Output, "Thumbnails");
                Paths = Directory.GetFiles(ThumbnailsDir, "*", SearchOption.AllDirectories);
                string tempStr;
                foreach (string str in Paths) {
                    tempStr = str.Replace(ThumbnailsDir, "Images");
                    DateTime date = DateFromPath(tempStr);
                    Pictures.Add(new Dictionary<string, string> {
                    {"thumb",str.Replace(OutputDir, "Images") }, {"picture", tempStr},
                        { "name",  Path.GetFileNameWithoutExtension(tempStr)}, {"date", Path.Combine(date.Month.ToString(),date.Year.ToString()) }
                    });
                }
            } catch { }
        }

        public DateTime DateFromPath(string path) {
            try {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image myImage = Image.FromStream(fs, false, false)) {
                    PropertyItem propItem = myImage.GetPropertyItem(36867);
                    string dateTaken = r.Replace(Encoding.UTF8.GetString(propItem.Value), "-", 2);
                    return DateTime.Parse(dateTaken);
                }
            } catch (Exception e) {
                return File.GetCreationTime(path);
            }
        }

        public void DeleteImage(string thumb_path) {
            try {
                string path = thumb_path.Replace("Thumbnails/", "");
                thumb_path = thumb_path.Replace("Images/", "");
                path = path.Replace("Images/", "");
                path = path.Replace("/", "\\");
                thumb_path = thumb_path.Replace("/", "\\");
                path = Path.Combine(OutputDir, path);
                thumb_path = Path.Combine(OutputDir, thumb_path);
                File.Delete(path);
                File.Delete(thumb_path);
            } catch (Exception e) {
                Debug.WriteLine(e.ToString());
            }
        }
    }
}