using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ImageWebApp.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Config() {
            ViewBag.Message = "Service Config.";

            return View();
        }

        public ActionResult Photos() {
            ViewBag.Message = "Service Photos.";

            return View();
        }

        public ActionResult Logs() {
            ViewBag.Message = "Service Logs.";

            return View();
        }
    }
}