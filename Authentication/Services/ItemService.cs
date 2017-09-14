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
        private IRepository<Item> _db_of_Items;
        public ItemSevice()
        {
            _db_of_Items = new SQLItemRepository();
        }

        public IEnumerable<Item> GetItemList()
        {
            return _db_of_Items.GetItemList();
        }

        public Item GetBook(int id)
        {
            return _db_of_Items.GetItem(id);
        }

        public void Add(Item item)
        {
            _db_of_Items.Create(item);
        }

        public void Update(Item item)
        {
            _db_of_Items.Update(item);
        }

        public void Delete(int id)
        {
            _db_of_Items.Delete(id);
        }

        public void Save()
        {
            _db_of_Items.Save();
        }

        public void Dispose()
        {
            _db_of_Items.Dispose();
        }
    }
}