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

     
        public void Remove(int id)
        {
            var subscriptionLevel = _context.SubscriptionLevels.Find(id);
            _context.SubscriptionLevels.Remove(subscriptionLevel);
        }

       
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

       
        public async Task<IEnumerable<SubscriptionLevel>> GetAllAsync(Expression<Func<SubscriptionLevel, bool>> predicate, string[] includes)
        {
            var models = _context.SubscriptionLevels.Where(predicate);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    models.Include(include);
                }
                return await models.ToListAsync();
            }

            return await models.AsNoTracking().ToListAsync();
        }

        public async Task<SubscriptionLevel> GetAsync(Expression<Func<SubscriptionLevel, bool>> predicate, string[] includes)
        {
            var model = _context.SubscriptionLevels.Where(predicate);

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    model.Include(include);
                }
                return await model.FirstOrDefaultAsync();
            }
            return await model.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
