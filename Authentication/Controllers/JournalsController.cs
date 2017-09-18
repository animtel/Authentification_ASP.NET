using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Authentication.Models;
using Authentication.Services;

namespace Authentication.Controllers
{
    public class JournalsController : Controller
    {
        private JournalService _journalService;


        public JournalsController()
        {
            _journalService = new JournalService();
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.DataTable = _journalService.GetJournalsList().ToList();

            return View("Index");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            foreach (var item in _journalService.GetJournalsList())
            {
                _journalService.Add(item);
            }
            Journal it = _journalService.GetJournal(id);
            if (it != null)
            {
                return PartialView("Details", it);
            }
            return View("Index");
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
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

        [Authorize]
        public ActionResult Edit(int id)
        {
            Journal comp = _journalService.GetJournal(id);
            _journalService.Delete(id);
            if (comp != null)
            {
                return PartialView("Edit", comp);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Journal journal)
        {
            _journalService.Add(journal);
            return RedirectToAction("Index");
        }

        [Authorize]
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
        [Authorize]
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
    }
}