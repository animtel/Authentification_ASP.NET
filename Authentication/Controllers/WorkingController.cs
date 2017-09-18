using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Authentication.Controllers
{
    public class WorkingController : Controller
    {
        [Authorize(Roles = "user")]
        public ActionResult test()
        {
            var all_items = new List<string>();
            all_items.Add("Journals");
            all_items.Add("Books");
            all_items.Add("All");

            var all_creates = new List<string>();
            all_creates.Add("Journals");
            all_creates.Add("Books");

            ViewBag.AllCreates = all_creates;
            ViewBag.AllItems = all_items;

            return View("test");
        }

        public ActionResult CreateItem()
        {
            string some = Request.Form["DropCreate"].ToString();

            if (some == "Books") return Redirect("~/Books/Create");
            if (some == "Journals") return Redirect("~/Journals/Create");

            return View("test");
        }
        
        
        [Authorize]
        public ActionResult Drop()
        {
            string some = Request.Form["DropTypes"].ToString();

            if (some == "Books") return Redirect("~/Books/Index");
            if (some == "Journals") return Redirect("~/Journals/Index");
            if (some == "All") return Redirect("~/Items/Index");

            return View("test");
        }

    }
}