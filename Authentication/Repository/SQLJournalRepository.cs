using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Repository
{
    public class SQLJournalRepository : IRepository<Journal>
    {
        private TestBD _db;

        public SQLJournalRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Journal> GetItemList()
        {
            var some = _db.Journales.SqlQuery("SELECT * FROM dbo.Journales").ToList();
            return some;
        }

        public Journal GetItem(int id)
        {
            string query = $"SELECT * FROM dbo.Journales WHERE id={id}";
            var item = _db.Journales.SqlQuery(query).ToList()[0];
            return item;
        }

        public void Create(Journal journal)
        {
            string query = $"INSERT INTO dbo.Books VALUES ({journal.Id},{journal.Name},{journal.Author},{journal.Number},{journal.Price})";
            var some = _db.Journales.SqlQuery(query);
        }

        public void Update(Journal journal)
        {
            string query = $"UPDATE dbo.Journales SET Id = {journal.Id}, Name = {journal.Name}, Author = {journal.Author} , Number = {journal.Number} , Price = {journal.Price}";
            _db.Books.SqlQuery(query).ToList();
        }

        public void Delete(int id)
        {
            Journal journal = _db.Journales.Find(id);
            string query = $"DELETE FROM dbo.Journales WHERE Id={id}";
            try
            {
                if (journal != null)
                {
                    var some = _db.Journales.SqlQuery(query).ToList();
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
