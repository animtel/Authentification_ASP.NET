using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Authentication.Models;
using Authentication.Services;
using Newtonsoft.Json;

namespace Authentication.Controllers
{
    public class BrochuresController : Controller
    {
        private BrochureService _brochureService;

        public BrochuresController()
        {
            _brochureService = new BrochureService();
        }

        public ActionResult Index()
        {
            ViewBag.DataTable = _brochureService.GetBrochureList().ToList();

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            foreach (var item in _brochureService.GetBrochureList())
            {
                _brochureService.Add(item);
            }
            Brochure brochure = _brochureService.GetBrochure(id);
            if (brochure != null)
            {
                return PartialView("Details", brochure);
            }
            return View("Index");
        }

        [Authorize(Roles = "user")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Brochure brochure)
        {
            if (ModelState.IsValid)
            {
                _brochureService.Add(brochure);
                _brochureService.Save();
                return RedirectToAction("Index");
            }
            return View(brochure);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Brochure brochure = _brochureService.GetBrochure(id);
            if (brochure != null)
            {
                return PartialView("Edit", brochure);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Brochure brochure)
        {
            _brochureService.Update(brochure);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Brochure brochure = _brochureService.GetBrochure(id);
            if (brochure != null)
            {
                return PartialView("Delete", brochure);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteRecord(int id)
        {
            Brochure comp = _brochureService.GetBrochure(id);

            if (comp != null)
            {
                _brochureService.Delete(id);
                _brochureService.Save();
            }
            else
            {
                return Content("<h2>Такого объекта е существует!</h2>");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Save(int id)
        {

            Brochure brochure = _brochureService.GetBrochure(id);

            string serialized = JsonConvert.SerializeObject(brochure);

            XmlSerializer formatter = new XmlSerializer(typeof(Brochure));

            StringWriter stringWriter = new StringWriter();
            formatter.Serialize(stringWriter, brochure);

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath($"~/Entities/{User.Identity.Name}"));
            dir.Create();


            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_brochure.json"), serialized);
            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_brochure.xml"), stringWriter.ToString());

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Load()
        {
            return View("Load");
        }
        [HttpPost]

        public ActionResult Load(HttpPostedFileBase load)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Brochure));
            string fileName = System.IO.Path.GetFileName(load.FileName);

            if (load != null)
            {
                // получаем имя файла
                load.SaveAs(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
            }
            char some = Convert.ToChar(fileName[fileName.Length - 1]);
            if (some == 'n') DeserializeJSON(fileName); // I don`t know, how it fix
            if (some == 'l') DeserializeXML(fileName);

            return RedirectToAction("Index");
        }

        public void DeserializeJSON(string fileName)
        {
            string deserialize = System.IO.File.ReadAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));

            Brochure test = JsonConvert.DeserializeObject<Brochure>(deserialize);
            _brochureService.Add(test);
            _brochureService.Save();


        }

        public void DeserializeXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Brochure));

            StreamReader reader = new StreamReader(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
            Brochure test = (Brochure)serializer.Deserialize(reader);
            reader.Close();
            _brochureService.Add(test);
            _brochureService.Save();
        }
        
    }
}
