﻿using Authentication.Models;
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
           
            Book book = _bookService.GetBook(id);
            if (book != null)
            {
                return PartialView("Details", book);
            }
            return View("Index");
        }

        
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
            _bookService.Delete(id);
            
            return RedirectToAction("Index");
        }

        public FileResult Save(int id)
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

            string file_path = Server.MapPath($"~/Entities/{User.Identity.Name}/{id}_book.json");
            string file_type = "application/json";
            string file_name = $"{id}_book.json";

            return File(file_path, file_type, file_name);

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

            try
            {
                fileName = System.IO.Path.GetFileName(load.FileName);
            }
            catch(Exception e)
            {
                return Content("<h1>Неверный формат!</h1>");
            }
            if (load != null)
            {
                try
                {
                    // получаем имя файла
                    load.SaveAs(Server.MapPath($"~/Entities/{User.Identity.Name}/" + fileName));
                }
                catch(Exception e)
                {
                    load.SaveAs(Server.MapPath($"~/Entities/" + fileName));
                }
            }
            char some = Convert.ToChar(fileName[fileName.Length-1]);
            try
            {
                if (some == 'n')
                {
                    DeserializeJSON(fileName);
                }
                if (some == 'l')
                {
                    DeserializeXML(fileName);
                }
            }
            catch (Exception e)
            {
                return Content("Не правильный формат!");
            }

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