using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authentication.Models;
using Authentication.Repository;

namespace Authentication.Services
{
    public class ItemSevice
    {
        private IRepository<Item> _dbItems;
        public ItemSevice()
        {
            _dbItems = new SQLItemRepository();
        }

        public IEnumerable<Item> GetItemList()
        {
            return _dbItems.GetItemList();
        }

        public Item GetBook(int id)
        {
            return _dbItems.GetItem(id);
        }

        public void Add(Item item)
        {
            _dbItems.Create(item);
        }

        public void Update(Item item)
        {
            _dbItems.Update(item);
        }

        public void Delete(int id)
        {
            _dbItems.Delete(id);
        }

        public void Save()
        {
            _dbItems.Save();
        }

        public void Dispose()
        {
            _dbItems.Dispose();
        }
    }
}