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
    public class CampaignRepository : IRepository<Campaign>
    {
        private DatabaseContext _context;

        public CampaignRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(Campaign item)
        {
            _context.Campaigns.Add(item);
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

        public void Edit(Campaign item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            return await _context.Campaigns.AsNoTracking()
               .ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetAllAsync(Expression<Func<Campaign, bool>> predicate)
        {
            return await _context.Campaigns.Where(predicate).ToListAsync();
        }

        public async Task<Campaign> GetAsync(int id)
        {
            return await _context.Campaigns.AsNoTracking()
              .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Campaign> GetAsync(Expression<Func<Campaign, bool>> predicate, int id)
        {
            return await _context.Campaigns.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var campaign = _context.Campaigns.Find(id);
            _context.Campaigns.Remove(campaign);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public List<Campaign> GetAll()
        {
            return _context.Campaigns.ToList();
        }

        public IEnumerable<Campaign> Where(Expression<Func<Campaign, bool>> predicate)
        {
            return _context.Campaigns.Where(predicate);
        }
    }
}
