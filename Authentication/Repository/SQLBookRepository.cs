using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Authentication.Models;

namespace Authentication.Repository
{
    public class SQLBookRepository : IRepository<Book>
    {
        private TestBD _db;

        public SQLBookRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Book> GetItemList()
        {
            return _db.Books;
        }

        public Book GetItem(int id)
        {
            return _db.Books.Find(id);
        }

        public void Create(Book book)
        {
            _db.Books.Add(book);
        }

        public void Update(Book book)
        {
            _db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Book book = _db.Books.Find(id);
            if (book != null)
                _db.Books.Remove(book);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}