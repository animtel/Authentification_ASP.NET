using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Authentication.Models;
using Authentication.Services;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace Authentication.Controllers
{
    public class ItemsController : Controller
    {
        private ItemSevice _itemService;
        private JournalService _journalService;
        private BookService _bookService;
        public ItemsController()
        {
            _itemService = new ItemSevice();
            _bookService = new BookService();
            _journalService = new JournalService();
            DeletElements();
        }

        public void DeletElements()
        {
            for (int i = 0; i < _itemService.GetItemList().ToList().Count + 1; i++)
            {
                _itemService.Delete(i);
            }
        }

        // GET: Items
        [Authorize(Roles = "user")]
        public ActionResult Index()
        {
            int id_of_items = 0;
            DeletElements();

            foreach (var item in _bookService.GetBookList())
            {

                _itemService.Add(new Item { Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = "-", Type = "Book" });

            }

            foreach (var item in _journalService.GetJournalsList())
            {

                _itemService.Add(new Item { Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = item.Number, Type = "Jurnal" });

            }
            _itemService.Save();

            ViewBag.DataTable = _itemService.GetItemList();
            return View();
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            
            Item item = _itemService.GetItem(id);
            if (item != null)
            {
                return PartialView("Details", item);
            }
            return View("Index");
        }


        [Authorize(Roles = "admin")]
        public ActionResult Delete(int id)
        {
            Item comp = _itemService.GetItem(id);
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
            Item it = _itemService.GetItem(id);

            if (it != null)
            {
                _itemService.Delete(id);
                _itemService.Save();
            }
            else
            {
                return Content("<h2>Такого объекта е существует!</h2>");
            }
            return RedirectToAction("Index");
        }

    }
}