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
            //return _db.Books;
            
            var some = _db.Books.SqlQuery("SELECT * FROM dbo.Books").ToList();
            return some;
        }

        public Book GetItem(int id)
        {
            string query = $"SELECT * FROM dbo.Books WHERE id={id}";
            var test = _db.Books.SqlQuery(query).ToList();
            return test[0];
            //return _db.Books.Find(id);
        }

        public void Create(Book book)
        {
            string query = $"INSERT INTO dbo.Books (Id, Name, Author, Price) VALUES ({book.Id},{book.Name},{book.Author},{book.Price})";
            var some = _db.Books.SqlQuery(query).ToList();
            //_db.Books.Add(book);
        }

        public void Update(Book book)
        {
            _db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            string query = $"DELETE FROM dbo.Books WHERE Id={id}";
            Book book = _db.Books.Find(id);
            try
            {
                if (book != null)
                {
                    var some = _db.Books.SqlQuery(query).ToList();
                }
            }
            catch (Exception ex)
            {
                
            }
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