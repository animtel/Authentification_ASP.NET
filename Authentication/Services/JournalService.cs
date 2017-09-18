using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Authentication.Models;
using Authentication.Repository;

namespace Authentication.Services
{
    public class JournalService
    {
        private IRepository<Journal> _dbJournals;
        public JournalService()
        {
            _dbJournals = new SQLJournalRepository();
        }

        public IEnumerable<Journal> GetJournalsList()
        {
            return _dbJournals.GetItemList();
        }

        public Journal GetJournal(int id)
        {
            return _dbJournals.GetItem(id);
        }

        public void Add(Journal journal)
        {
            _dbJournals.Create(journal);
        }

        public void Update(Journal journal)
        {
            _dbJournals.Update(journal);
        }

        public void Delete(int id)
        {
            _dbJournals.Delete(id);
        }

        public void Save()
        {
            _dbJournals.Save();
        }

        public void Dispose()
        {
            _dbJournals.Dispose();
        }
    }
}