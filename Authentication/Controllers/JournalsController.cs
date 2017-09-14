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
        JournalService _journal_service;


        public JournalsController()
        {
            _journal_service = new JournalService();
        }

        public ActionResult Index()
        {
            ViewBag.DataTable = _journal_service.GetJournalsList().ToList();

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            foreach (var item in _journal_service.GetJournalsList())
            {
                _journal_service.Add(item);
            }
            Journal it = _journal_service.GetJournal(id);
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
                _journal_service.Add(journal);
                _journal_service.Save();
                return RedirectToAction("Index");
            }
            return View(journal);
        }

        public ActionResult Edit(int id)
        {
            Journal comp = _journal_service.GetJournal(id);
            _journal_service.Delete(id);
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
            _journal_service.Add(journal);
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            Journal comp = _journal_service.GetJournal(id);
            if (comp != null)
            {
                return PartialView("Delete", comp);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult DeleteRecord(int id)
        {
            Journal comp = _journal_service.GetJournal(id);

            if (comp != null)
            {
                _journal_service.Delete(id);
                _journal_service.Save();
            }
            return RedirectToAction("Index");
        }
    }
}