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

        /// <summary>
        /// Initializes a new instance of the <see cref="PhotosModel"/> class and take all photos from the output dir.
        /// </summary>
        /// <param name="Output">The output.</param>
        /// <param name="ThumbnailSize">Size of the thumbnail.</param>
        public PhotosModel(string Output, string ThumbnailSize) {
            Pictures = new List<Dictionary<string, string>>();
            try {
                OutputDir = Output;
                ThumbnailsDir = Path.Combine(Output, "Thumbnails");
                Paths = Directory.GetFiles(ThumbnailsDir, "*", SearchOption.AllDirectories);
                string tempStr;
                foreach (string str in Paths) {
                    tempStr = str.Replace(ThumbnailsDir, "Images");
                    DateTime date = DateFromPath(str);
                    Pictures.Add(new Dictionary<string, string> {
                    {"thumb",str.Replace(OutputDir, "Images") }, {"picture", tempStr},
                        { "name",  Path.GetFileNameWithoutExtension(tempStr)}, {"date", Path.Combine(date.Month.ToString(),date.Year.ToString()) }
                    });
                }
            } catch { }
        }

        /// <summary>
        /// Generate creation/taken dates from file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the image from the given app.
        /// </summary>
        /// <param name="thumb_path">The thumb path.</param>
        public void DeleteImage(string thumb_path) {
            try {
                thumb_path = thumb_path.Replace("Images/", "");
                thumb_path = thumb_path.Replace("/", "\\");
                string path = thumb_path.Replace("Thumbnails\\", "");
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