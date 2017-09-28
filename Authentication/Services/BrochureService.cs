using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authentication.Models;
using Authentication.Repository;

namespace Authentication.Services
{
    public class BrochureService
    {
        private IRepository<Brochure> _dbBrochures;
        public BrochureService()
        {
            _dbBrochures = new SQLBrochureRepository();
        }

        public IEnumerable<Brochure> GetBrochureList()
        {
            return _dbBrochures.GetItemList();
        }

        public Brochure GetBrochure(int id)
        {
            return _dbBrochures.GetItem(id);
        }

        public void Add(Brochure brochure)
        {
            _dbBrochures.Create(brochure);
        }

        public void Update(Brochure brochure)
        {
            _dbBrochures.Update(brochure);
        }

        public void Delete(int id)
        {
            _dbBrochures.Delete(id);
        }

        public void Save()
        {
            _dbBrochures.Save();
        }

        public void Dispose()
        {
            _dbBrochures.Dispose();
        }
    }
}