using ImageWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageWebApp.Controllers {
    public class HomeController : Controller {
        static string selected_dir = "";

        static ConfigModel config_model = new ConfigModel();
        static readonly LogsModel logs_model = new LogsModel();
        static readonly HomeModel home_model = new HomeModel();

        public ActionResult Index() {
            home_model.PicsCount(config_model.OutputDir);

            return View(home_model);
        }

        public ActionResult Config() {
            ViewBag.Message = "Service Config.";

            return View(config_model);
        }

        public ActionResult Photos() {
            ViewBag.Message = "Service Photos.";

            return View(new PhotosModel(config_model.OutputDir, config_model.ThumbnailSize));
        }

        public ActionResult ViewPhoto(string picture, string name, string date) {
            ViewBag.picture = picture;
            ViewBag.name = name;
            ViewBag.date = date;
            return View();
        }

        public ActionResult DeletePhoto(string picture, string name, string date) {
            selected_pic = picture;
            ViewBag.picture = picture;
            ViewBag.name = name;
            ViewBag.date = date;
            return View();
        }

        public ActionResult DeleteOK() {
            Photos_model.DeleteImage(selected_pic);

            return RedirectToAction("Photos", "Home");
        }

        public ActionResult Logs() {
            ViewBag.Message = "Service Logs.";
            logs_model.NewLogsRequest();

            return View(logs_model);
        }

        public ActionResult RemoveHandler(string dir) {
            ViewBag.Message = "Remove Handler.";
            ViewBag.Dir = dir;
            selected_dir = dir;

            return View(this);
        }

        public ActionResult RemoveOK() {
            ViewBag.Message = "Remove The Handler.";
            config_model.OnRemove(selected_dir);

            return RedirectToAction("Config", "Home");
        }
    }
}