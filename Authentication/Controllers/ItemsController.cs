using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Authentication.Models;
using Authentication.Services;

namespace Authentication.Controllers
{
    public class ItemsController : Controller
    {
        private ItemSevice _item_service;
        private JournalService _journal_service;
        private BookService _book_service;
        public ItemsController()
        {
            _item_service = new ItemSevice();
            _book_service = new BookService();
            _journal_service = new JournalService();
        }

        public void DeletElements()
        {
            for (int i = 0; i < _item_service.GetItemList().ToList().Count + 1; i++)
            {
                _item_service.Delete(i);
            }
        }

        // GET: Items
        public ActionResult Index()
        {
            int id_of_items = 0;
            DeletElements();

            foreach (var item in _book_service.GetBookList())
            {

                _item_service.Add(new Item { Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = "-", Type = "Book" });

            }

            foreach (var item in _journal_service.GetJournalsList())
            {

                _item_service.Add(new Item { Id = id_of_items++, Name = item.Name, Author = item.Author, Price = item.Price, Number = item.Number, Type = "Jurnal" });

            }
            _item_service.Save();

            ViewBag.DataTable = _item_service.GetItemList();
            return View();
        }



    }
}