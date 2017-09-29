using Authentication.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Authentication.Services;
using Newtonsoft.Json;
//сделать сериализацию и десериализацию объектов. Фикс создание через запрос. Переделать репозиторий журнал. (Подять бд)
namespace Authentication.Controllers
{
    public class BooksController : Controller
    {
        private BookService _bookService;

        public BooksController()
        {
            _bookService = new BookService();
        }

        public ActionResult Index()
        {
            ViewBag.DataTable = _bookService.GetBookList().ToList();

            return View("Index");
        }

        public ActionResult Details(int id)
        {
            //foreach (var item in _bookService.GetBookList())
            //{
            //    _bookService.Add(item);
            //}
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

        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
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


            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_book.json"), serialized);
            System.IO.File.WriteAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_book.xml"), stringWriter.ToString());

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
            char some = Convert.ToChar(fileName[fileName.Length-1]);
            if(some == 'n') DeserializeJSON(fileName); // I don`t know, how it fix
            if(some == 'l') DeserializeXML(fileName);

            return RedirectToAction("Index");
        }

        public void DeserializeJSON(string fileName)
        {
            string deserialize = System.IO.File.ReadAllText(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));

            Book test = JsonConvert.DeserializeObject<Book>(deserialize);
            _bookService.Add(test);
            _bookService.Save();

            
        }

        public void DeserializeXML(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book));

            StreamReader reader = new StreamReader(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
            Book test = (Book)serializer.Deserialize(reader);
            reader.Close();
            _bookService.Add(test);
            _bookService.Save();
        }
        
    }
}