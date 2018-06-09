using ImageWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageWebApp.Controllers {
    public class HomeController : Controller {

        static readonly ConfigModel config_model = new ConfigModel();
        static readonly LogsModel logs_model = new LogsModel();
        static readonly HomeModel home_model = new HomeModel();
        static readonly PhotosModel photos_model = new PhotosModel();

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

            return View(photos_model);
        }

        public ActionResult Logs() {
            ViewBag.Message = "Service Logs.";

            return View(logs_model);
        }

        public ActionResult RemoveHandler(string dir) {
            ViewBag.Message = "Remove Handler.";
            ViewBag.Dir = dir;
            return View(this);
        }

        public ActionResult RemoveOK(string dir) {
            ViewBag.Message = "Remove The Handler.";
            config_model.OnRemove(dir);

            return RedirectToAction("Config", "Home");
        }
    }
}