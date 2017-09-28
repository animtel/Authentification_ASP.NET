using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authentication.Models;

namespace Authentication.Repository
{
    public class SQLBrochureRepository : IRepository<Brochure>
    {
        private TestBD _db;

        public SQLBrochureRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Brochure> GetItemList()
        {
            //return _db.Books;

            var some = _db.Brochures.SqlQuery("SELECT * FROM dbo.Brochures").ToList();
            return some;
        }

        public Brochure GetItem(int id)
        {
            string query = $"SELECT * FROM dbo.Brochures WHERE id={id}";
            var item = _db.Brochures.SqlQuery(query).ToList()[0];
            return item;
            //return _db.Books.Find(id);
        }

        public void Create(Brochure brochure)
        {
            string query = $"INSERT INTO dbo.Books VALUES ({brochure.Id},{brochure.Name},{brochure.Color},{brochure.Theme},{brochure.Price})";
            var some = _db.Brochures.SqlQuery(query);
            //_db.Books.Add(book);
        }

        public void Update(Brochure brochure)
        {
            string query = $"UPDATE dbo.Brochures SET Id = {brochure.Id}, Name = {brochure.Name}, Color = {brochure.Color}, Theme = {brochure.Theme},Price = {brochure.Price}";
            _db.Brochures.SqlQuery(query);
            //_db.Entry(book).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            string query = $"DELETE FROM dbo.Brochures WHERE Id = {id}";
            Brochure brochure = _db.Brochures.Find(id);
            try
            {
                if (brochure != null)
                {
                    var some = _db.Brochures.SqlQuery(query).ToList();
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