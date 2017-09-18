using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Authentication.Services;

namespace Authentication.Controllers
{
    public class BooksController : Controller
    {
        private BookService _bookService;

        public BooksController()
        {
            _bookService = new BookService();
        }

        [Authorize]
        public ActionResult Index()
        {
            ViewBag.DataTable = _bookService.GetBookList().ToList();

            return View("Index");
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            foreach (var item in _bookService.GetBookList())
            {
                _bookService.Add(item);
            }
            Book it = _bookService.GetBook(id);
            if (it != null)
            {
                return PartialView("Details", it);
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
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.Add(book);
                _bookService.Save();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Book book = _bookService.GetBook(id);
            if (book != null)
            {
                return PartialView("Edit", book);
            }
            return View("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Book book)
        {
            _bookService.Update(book);
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
            Book comp = _bookService.GetBook(id);
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
            Book comp = _bookService.GetBook(id);

            if (comp != null)
            {
                _bookService.Delete(id);
                _bookService.Save();
            }
            else
            {
                return Content("<h2>Такого объекта е существует!</h2>");
            }
            return RedirectToAction("Index");
        }
    }
}