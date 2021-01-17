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
    public class CampaignJobRepository : IRepository<CampaignJob>
    {
        private DatabaseContext _context;
        public CampaignJobRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(CampaignJob item)
        {
            _context.CampaignJobs.Add(item);
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

        public void Edit(CampaignJob item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public List<CampaignJob> GetAll()
        {
            return _context.CampaignJobs.ToList();
        }

        public async Task<IEnumerable<CampaignJob>> GetAllAsync()
        {
            return await _context.CampaignJobs.AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<CampaignJob>> GetAllAsync(Expression<Func<CampaignJob, bool>> predicate)
        {
            return await _context.CampaignJobs.Where(predicate).ToListAsync();
        }

        public async Task<CampaignJob> GetAsync(int id)
        {
            return await _context.CampaignJobs.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CampaignJob> GetAsync(Expression<Func<CampaignJob, bool>> predicate, int id)
        {
            return await _context.CampaignJobs.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var campaignJob = _context.CampaignJobs.Find(id);
            _context.CampaignJobs.Remove(campaignJob);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<CampaignJob> Where(Expression<Func<CampaignJob, bool>> predicate)
        {
            return _context.CampaignJobs.Where(predicate);
        }
    }
}
