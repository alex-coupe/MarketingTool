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
    public class RecipientRepository : IRepository<Recipient>
    {
        private DatabaseContext _context;
        public RecipientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(Recipient item)
        {
            _context.Recipients.Add(item);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Edit(Recipient item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

      
        public async Task<IEnumerable<Recipient>> GetAllAsync(Expression<Func<Recipient, bool>> predicate)
        {
            return await _context.Recipients.Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Recipient> GetAsync(Expression<Func<Recipient, bool>> predicate)
        {
            return await _context.Recipients.Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var recipient = _context.Recipients.Find(id);
            _context.Recipients.Remove(recipient);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
