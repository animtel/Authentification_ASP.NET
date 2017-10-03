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
    public class SQLJournalRepository : IRepository<Journal>
    {
        private TestBD _db;

        string connectionString = ConfigurationManager.ConnectionStrings["BooksStore"].ConnectionString;

        public SQLJournalRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Journal> GetItemList()
        {
            var journals = new List<Journal>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                journals = db.Query<Journal>("SELECT * FROM Journales").ToList();
            }
            return journals;
        }

        public Journal GetItem(int id)
        {
            Journal journal = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                journal = db.Query<Journal>("SELECT * FROM Journales WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return journal;
        }

        public void Create(Journal journal)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Journales VALUES(@Name, @Author, @Year, @Publishing, @Number, @Price)";
                int journalId = db.Query<int>(sqlQuery, journal).FirstOrDefault();
                journal.Id = journalId;
            }

        }
       

        public void Update(Journal journal)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Journales SET Name = @Name, Author = @Author, Year = @Year, Publishing = @Publishing, Number = @Number, Price = @Price WHERE Id = @Id";
                db.Execute(sqlQuery, journal);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Journales WHERE Id = @id";
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
