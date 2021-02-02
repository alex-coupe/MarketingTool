using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
            _context.ListRecipients.Add(item);
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
            _context.Entry(item).State = EntityState.Modified;
        }

      
        public async Task<IEnumerable<ListRecipient>> GetAllAsync(Expression<Func<ListRecipient, bool>> predicate)
        {
            return await _context.ListRecipients.Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }
               

        public async Task<ListRecipient> GetAsync(Expression<Func<ListRecipient, bool>> predicate)
        {
            return await _context.ListRecipients.Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var listRecipient = _context.ListRecipients.Find(id);
            _context.ListRecipients.Remove(listRecipient);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
