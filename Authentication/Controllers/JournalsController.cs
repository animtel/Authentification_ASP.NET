using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Authentication.Models;
using Authentication.Services;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Authentication.Controllers
{
    public class JournalsController : Controller
    {
        private JournalService _journalService;


        public JournalsController()
        {
            _journalService = new JournalService();
        }

        //[Authorize]
        public ActionResult Index()
        {
            ViewBag.DataTable = _journalService.GetJournalsList().ToList();

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            Journal it = _journalService.GetJournal(id);
            if (it != null)
            {
                return PartialView("Details", it);
            }
            return View("Index");
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Journal journal)
        {
            if (ModelState.IsValid)
            {
                _journalService.Add(journal);
                _journalService.Save();
                return RedirectToAction("Index");
            }
            return View(journal);
        }

        public ActionResult Edit(int id)
        {
            Journal comp = _journalService.GetJournal(id);
            if (comp != null)
            {
                return PartialView("Edit", comp);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Journal journal)
        {
            _journalService.Add(journal);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Journal comp = _journalService.GetJournal(id);
            if (comp != null)
            {
                return PartialView("Delete", comp);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        [Authorize(Roles = "admin")]
        public ActionResult DeleteRecord(int id)
        {
            Journal comp = _journalService.GetJournal(id);

            if (comp != null)
            {
                _journalService.Delete(id);
                _journalService.Save();
            }
            else
            {
                return Content("<h2>Такого объекта е существует!</h2>");
            }
            return RedirectToAction("Index");
        }

        public ActionResult Save(int id)
        {

            Journal journal = _journalService.GetJournal(id);

            string serialized = JsonConvert.SerializeObject(journal);

            XmlSerializer formatter = new XmlSerializer(typeof(Journal));

            StringWriter stringWriter = new StringWriter();
            formatter.Serialize(stringWriter, journal);

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath($"~/Entities/{User.Identity.Name}"));
            dir.Create();


            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_journal.json"), serialized);
            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_journal.xml"), stringWriter.ToString());

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
            XmlSerializer formatter = new XmlSerializer(typeof(Journal));
            string fileName = System.IO.Path.GetFileName(load.FileName);

            if (load != null)
            {
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

            Journal test = JsonConvert.DeserializeObject<Journal>(deserialize);
            _journalService.Add(test);
            _journalService.Save();


        }

        public void DeserializeXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Journal));

            StreamReader reader = new StreamReader(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
            Journal test = (Journal)serializer.Deserialize(reader);
            reader.Close();
            _journalService.Add(test);
            _journalService.Save();
        }
    }
}