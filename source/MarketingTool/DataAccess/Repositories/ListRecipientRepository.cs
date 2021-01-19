using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ListRecipientRepository : IRepository<ListRecipient>
    {
        private DatabaseContext _context;
        public ListRecipientRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(ListRecipient item)
        {
            throw new NotImplementedException();
        }
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Edit(ListRecipient item)
        {
            throw new NotImplementedException();
        }

        public List<ListRecipient> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListRecipient>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ListRecipient>> GetAllAsync(Expression<Func<ListRecipient, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ListRecipient> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListRecipient> GetAsync(Expression<Func<ListRecipient, bool>> predicate, int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListRecipient> Where(Expression<Func<ListRecipient, bool>> predicate)
        {
            return _context.ListRecipients.Where(predicate);
        }
    }
}
