using ImageWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageWebApp.Controllers {
    public class HomeController : Controller {
        static string selected_dir = "";
        static string selected_pic = "";

        static ConfigModel config_model = new ConfigModel();
        static readonly LogsModel logs_model = new LogsModel();
        static readonly HomeModel home_model = new HomeModel();
        static PhotosModel Photos_model = new PhotosModel(config_model.OutputDir, config_model.ThumbnailSize);

        /// <summary>
        /// count the current number of photos and view the Web Image page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index() {
            home_model.PicsCount(config_model.OutputDir);

            return View(home_model);
        }

        /// <summary>
        /// display the config page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Config() {
            ViewBag.Message = "Service Config.";

            return View(config_model);
        }

        /// <summary>
        /// display the photos page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Photos() {
            ViewBag.Message = "Service Photos.";

            return View(new PhotosModel(config_model.OutputDir, config_model.ThumbnailSize));
        }

        /// <summary>
        /// display the photo.
        /// </summary>
        /// <param name="picture">The picture path.</param>
        /// <param name="name">The picture name.</param>
        /// <param name="date">The picture date.</param>
        /// <returns></returns>
        public ActionResult ViewPhoto(string picture, string name, string date) {
            ViewBag.picture = picture;
            ViewBag.name = name;
            ViewBag.date = date;
            return View();
        }

        /// <summary>
        /// Delete the photo.
        /// </summary>
        /// <param name="picture">The thumbnail picture path.</param>
        /// <param name="name">The picture name.</param>
        /// <param name="date">The picture date.</param>
        /// <returns></returns>
        public ActionResult DeletePhoto(string picture, string name, string date) {
            selected_pic = picture;
            ViewBag.picture = picture;
            ViewBag.name = name;
            ViewBag.date = date;
            return View();
        }

        /// <summary>
        /// Delete confirmation.
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteOK() {
            Photos_model.DeleteImage(selected_pic);

            return RedirectToAction("Photos", "Home");
        }

        /// <summary>
        /// display the logs page.
        /// </summary>
        /// <returns></returns>
        public ActionResult Logs() {
            ViewBag.Message = "Service Logs.";
            logs_model.NewLogsRequest();

            return View(logs_model);
        }

        /// <summary>
        /// display the remove handler page.
        /// </summary>
        /// <param name="dir">The dir.</param>
        /// <returns></returns>
        public ActionResult RemoveHandler(string dir) {
            ViewBag.Message = "Remove Handler.";
            ViewBag.Dir = dir;
            selected_dir = dir;

            return View(this);
        }

        /// <summary>
        /// Remove handler confirmation.
        /// </summary>
        /// <returns></returns>
        public ActionResult RemoveOK() {
            ViewBag.Message = "Remove The Handler.";
            config_model.OnRemove(selected_dir);

            return RedirectToAction("Config", "Home");
        }
    }
}