using Authentication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Authentication.Services;
using Newtonsoft.Json;

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
            Book book = _bookService.GetBook(id);
            if (book != null)
            {
                return PartialView("Details", book);
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
            Book book = _bookService.GetBook(id);
            if (book != null)
            {
                return PartialView("Delete", book);
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

        public ActionResult Save(int id)
        {
            
            Book book = _bookService.GetBook(id);

            string serialized = JsonConvert.SerializeObject(book);

            XmlSerializer formatter = new XmlSerializer(typeof(Book));

            StringWriter stringWriter = new StringWriter();
            formatter.Serialize(stringWriter, book);

            DirectoryInfo dir = new DirectoryInfo(Server.MapPath($"~/Entities/{User.Identity.Name}"));
            dir.Create();


            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}.json"), serialized);
            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}.xml"), stringWriter.ToString());

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
            XmlSerializer formatter = new XmlSerializer(typeof(Book));
            string fileName = System.IO.Path.GetFileName(load.FileName);

            if (load != null)
            {
                // получаем имя файла
                load.SaveAs(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
            }

            using (FileStream fs = new FileStream(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName), FileMode.OpenOrCreate))
            {
                Book book = (Book)formatter.Deserialize(fs);
                _bookService.Add(book);

            }
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //public ActionResult Load(HttpPostedFileBase load)
        //{

        //    string fileName = System.IO.Path.GetFileName(load.FileName);

        //    if (load != null)
        //    {
        //        // получаем имя файла
        //        load.SaveAs(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
        //    }

        //    DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Book));

        //    using (FileStream fs = new FileStream(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName), FileMode.OpenOrCreate))
        //    {
        //        Book book = (Book)jsonFormatter.ReadObject(fs);
        //        _bookService.Add(book);
        //    }

        //    return RedirectToAction("Index");
        //}
    }
}