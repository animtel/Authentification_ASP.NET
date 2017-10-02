using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Authentication.Models;
using Dapper;

namespace Authentication.Repository
{
    public class SQLBookRepository : IRepository<Book>
    {
        private TestBD _db;
        string connectionString = ConfigurationManager.ConnectionStrings["BooksStore"].ConnectionString;

        public SQLBookRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Book> GetItemList()
        {
            //return _db.Books;
            var books = new List<Book>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                books = db.Query<Book>("SELECT * FROM Books").ToList();
            }
            return books;
        }

        public Book GetItem(int id)
        {
            Book book = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                book = db.Query<Book>("SELECT * FROM dbo.Books WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return book;
        }

        public void Create(Book book)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Books VALUES(@Name, @Author, @Price)";
                int bookId = db.Query<int>(sqlQuery, book).FirstOrDefault();
                book.Id = bookId;
            }
            
        }

        public void Update(Book book)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE dbo.Books SET Name = @Name, Author = @Author, Price = @Price WHERE Id = @Id";
                db.Execute(sqlQuery, book);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM dbo.Books WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
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