using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Authentication.Controllers
{
    public class WorkingController : Controller
    {
        [Authorize(Roles = "user")]
        public ActionResult test()
        {
            List<string> all_items = new List<string>();
            all_items.Add("Journals");
            all_items.Add("Books");
            all_items.Add("All");

            List<string> all_creates = new List<string>();
            all_creates.Add("Journals");
            all_creates.Add("Books");

            ViewBag.AllCreates = all_creates;
            ViewBag.AllItems = all_items;

            return View("test");
        }

        public ActionResult CreateItem()
        {
            string some = Request.Form["DropCreate"].ToString();
            switch (some)
            {
                case "Books":
                    return Redirect("~/Books/Create");
                case "Journals":
                    return Redirect("~/Journals/Create");
            }
            return View("test");
        }

        public ActionResult Drop()
        {
            string some = Request.Form["DropTypes"].ToString();

            switch (some)
            {
                case "Books":
                    return Redirect("~/Books/Index");
                case "Journals":
                    return Redirect("~/Journals/Index");
                case "All":
                    return Redirect("~/Items/Index");
            }
            return View("test");
        }

    }
}