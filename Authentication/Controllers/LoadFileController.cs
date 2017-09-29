using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Authentication.Controllers
{
    public class LoadFileController : Controller
    {
        // GET: LoadFile
        public ActionResult Index()
        {
            var all_choise = new List<string>();
            all_choise.Add("Book");
            all_choise.Add("Journal");
            all_choise.Add("Brochure");

            ViewBag.AllChoise = all_choise;

            return View("Index");
        }

        public ActionResult Drop()
        {
            string some = Request.Form["DropTypes"].ToString();

            if (some == "Book") return Redirect("~/Books/Load");
            if (some == "Journal") return Redirect("~/Journals/Load");
            if (some == "Brochure") return Redirect("~/Brochures/Load");

            return View("Index");
        }
    }
}