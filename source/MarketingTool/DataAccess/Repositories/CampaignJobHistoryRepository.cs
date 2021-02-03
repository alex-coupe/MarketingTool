using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public async Task<IEnumerable<CampaignJobHistory>> GetAllAsync(Expression<Func<CampaignJobHistory, bool>> predicate, string[] includes)
        {
            var models = _context.CampaignJobHistory.Where(predicate);

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
              
        public async Task<CampaignJobHistory> GetAsync(Expression<Func<CampaignJobHistory, bool>> predicate, string[] includes)
        {
            var model = _context.CampaignJobHistory.Where(predicate);

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

        public void Remove(int id)
        {
            var campaignJobHistory = _context.CampaignJobHistory.Find(id);
            _context.CampaignJobHistory.Remove(campaignJobHistory);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }

}
