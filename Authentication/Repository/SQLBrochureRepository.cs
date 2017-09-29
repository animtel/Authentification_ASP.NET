using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Authentication.Models;
using Dapper;

namespace Authentication.Repository
{
    public class SQLBrochureRepository : IRepository<Brochure>
    {
        private TestBD _db;
        string connectionString = ConfigurationManager.ConnectionStrings["BooksStore"].ConnectionString;

        public SQLBrochureRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Brochure> GetItemList()
        {
            //return _db.Books;
            var brochure = new List<Brochure>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                brochure = db.Query<Brochure>("SELECT * FROM Brochures").ToList();
            }
            return brochure;
        }

        public Brochure GetItem(int id)
        {
            Brochure brochure = null;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                brochure = db.Query<Brochure>("SELECT * FROM Brochures WHERE Id = @id", new { id }).FirstOrDefault();
            }
            return brochure;
        }

        public void Create(Brochure brochure)
        {
            Delete(brochure.Id);
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "SET IDENTITY_INSERT Brochures ON; INSERT INTO Brochures (Id, Name, Theme, Color, Price) VALUES(@Id, @Name, @Theme, @Color, @Price); SELECT CAST(SCOPE_IDENTITY() as int); SET IDENTITY_INSERT BROCHURES OFF";
                int brochureId = db.Query<int>(sqlQuery, brochure).FirstOrDefault();
                brochure.Id = brochureId;
            }

        }

        public void Update(Brochure brochure)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Brochures SET Name = @Name, Theme = @Theme, Color = @Color WHERE Id = @Id";
                db.Execute(sqlQuery, brochure);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM brochures WHERE Id = @id";
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