using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class SubscriptionLevelRepository : IRepository<SubscriptionLevel>
    {
        private DatabaseContext _context;

        public SubscriptionLevelRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(SubscriptionLevel item)
        {
            _context.SubscriptionLevels.Add(item);
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

        public void Edit(SubscriptionLevel item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<SubscriptionLevel>> GetAllAsync()
        {
            return await _context.SubscriptionLevels.AsNoTracking()
                .ToListAsync();

        }

        public async Task<SubscriptionLevel> GetAsync(int id)
        {
            return await _context.SubscriptionLevels.AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var subscriptionLevel = _context.SubscriptionLevels.Find(id);
            _context.SubscriptionLevels.Remove(subscriptionLevel);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<SubscriptionLevel> Where(Expression<Func<SubscriptionLevel, bool>> predicate)
        {
            return _context.SubscriptionLevels.Where(predicate);
        }

        public List<SubscriptionLevel> ToList()
        {
            return _context.SubscriptionLevels.ToList();
        }
    }
}
