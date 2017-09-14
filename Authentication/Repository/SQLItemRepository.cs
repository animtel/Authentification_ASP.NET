using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Repository
{
    public class SQLItemRepository : IRepository<Item>
    {
        private TestBD _db;

        public SQLItemRepository()
        {
            this._db = new TestBD();
        }

        public IEnumerable<Item> GetItemList()
        {
            return _db.Items;
        }

        public Item GetItem(int id)
        {
            return _db.Items.Find(id);
        }

        public void Create(Item item)
        {
            _db.Items.Add(item);
        }

        public void Update(Item paper)
        {
            _db.Entry(paper).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            Item paper = _db.Items.Find(id);
            if (paper != null)
                _db.Items.Remove(paper);
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
