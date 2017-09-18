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
        private IRepository<Book> _dbBooks;
        public BookService()
        {
            _dbBooks = new SQLBookRepository();
        }

        public IEnumerable<Book> GetBookList()
        {
            return _dbBooks.GetItemList();
        }

        public Book GetBook(int id)
        {
            return _dbBooks.GetItem(id);
        }

        public void Add(Book book)
        {
            _dbBooks.Create(book);
        }

        public void Update(Book book)
        {
            _dbBooks.Update(book);
        }

        public void Delete(int id)
        {
            _dbBooks.Delete(id);
        }

        public void Save()
        {
            _dbBooks.Save();
        }

        public void Dispose()
        {
            _dbBooks.Dispose();
        }


    }
}