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
    public class CampaignJobHistoryRepository : IRepository<CampaignJobHistory>
    {
        private DatabaseContext _context;
        public CampaignJobHistoryRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(CampaignJobHistory item)
        {
            _context.CampaignJobHistory.Add(item);
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

        public void Edit(CampaignJobHistory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public List<CampaignJobHistory> GetAll()
        {
            return _context.CampaignJobHistory.ToList();
        }

        public async Task<IEnumerable<CampaignJobHistory>> GetAllAsync()
        {
            return await _context.CampaignJobHistory.AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignJobHistory>> GetAllAsync(Expression<Func<CampaignJobHistory, bool>> predicate)
        {
            return await _context.CampaignJobHistory.Where(predicate).ToListAsync();
        }

        public async Task<CampaignJobHistory> GetAsync(int id)
        {
            return await _context.CampaignJobHistory.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CampaignJobHistory> GetAsync(Expression<Func<CampaignJobHistory, bool>> predicate, int id)
        {
            return await _context.CampaignJobHistory.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var campaignJobHistory = _context.CampaignJobHistory.Find(id);
            _context.CampaignJobHistory.Remove(campaignJobHistory);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<CampaignJobHistory> Where(Expression<Func<CampaignJobHistory, bool>> predicate)
        {
            return _context.CampaignJobHistory.Where(predicate);
        }
    }
}
