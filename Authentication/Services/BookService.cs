using Authentication.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authentication.Models;

namespace Authentication.Services
{
    public class BookService
    {
        private IRepository<Book> _db_of_Books;
        public BookService()
        {
            _db_of_Books = new SQLBookRepository();
        }

        public IEnumerable<Book> GetBookList()
        {
            return _db_of_Books.GetItemList();
        }

        public Book GetBook(int id)
        {
            return _db_of_Books.GetItem(id);
        }

        public void Add(Book book)
        {
            _db_of_Books.Create(book);
        }

        public void Update(Book book)
        {
            _db_of_Books.Update(book);
        }

        public void Delete(int id)
        {
            _db_of_Books.Delete(id);
        }

        public void Save()
        {
            _db_of_Books.Save();
        }

        public void Dispose()
        {
            _db_of_Books.Dispose();
        }


    }
}