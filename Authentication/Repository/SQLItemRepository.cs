using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Authentication.Repository
{
    public class SQLItemRepository : IRepository<Item>
    {
        private TestBD _db;

        string connectionString = ConfigurationManager.ConnectionStrings["BooksStore"].ConnectionString;

        public SQLItemRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Item> GetItemList()
        {
            //return _db.Books;
            var items = new List<Item>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                items = db.Query<Item>("SELECT * FROM Items").ToList();
            }
            return items;
        }

        public Item GetItem(int id)
        {
            Item item = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                item = db.Query<Item>("SELECT * FROM Items WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return item;
        }

        public void Create(Item item)
        {
            Delete(item.Id);
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SET IDENTITY_INSERT Items ON; INSERT INTO Items (Id, Name, Author, Price, Number, Type) VALUES(@Id, @Name, @Author, @Price, @Number, @Type); SELECT CAST(SCOPE_IDENTITY() as int); SET IDENTITY_INSERT Items OFF";
                int itemId = db.Query<int>(sqlQuery, item).FirstOrDefault();
                item.Id = itemId;
            }

        }

        public void Update(Item item)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Items SET Name = @Name, Author = @Author, Price = @Price, Number=@Number, Type=@Type WHERE Id = @Id";
                db.Execute(sqlQuery, item);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Items WHERE Id = @id";
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
