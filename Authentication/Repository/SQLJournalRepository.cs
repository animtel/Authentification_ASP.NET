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
            return _db.Journales;
        }

        public Journal GetItem(int id)
        {
            return _db.Journales.Find(id);
        }

        public void Create(Journal journal)
        {
            _db.Journales.Add(journal);
        }

        public void Update(Journal journal)
        {
            _db.Entry(journal).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Journal journal = _db.Journales.Find(id);
            if (journal != null)
                _db.Journales.Remove(journal);
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
