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
    public class TemplateHistoryRepository : IRepository<TemplateHistory>
    {
        private DatabaseContext _context;
        public TemplateHistoryRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(TemplateHistory item)
        {
            _context.TemplateHistory.Add(item);
        }

        public void Edit(TemplateHistory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Remove(int id)
        {
            var templateHistory = _context.TemplateHistory.Find(id);
            _context.TemplateHistory.Remove(templateHistory);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
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

        public async Task<TemplateHistory> GetAsync(Expression<Func<TemplateHistory, bool>> predicate)
        {
            return await _context.TemplateHistory.Where(predicate)
                .Include(x => x.Template)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TemplateHistory>> GetAllAsync(Expression<Func<TemplateHistory, bool>> predicate)
        {
            return await _context.TemplateHistory.Where(predicate)
                .Include(x => x.Template)
                .ToListAsync();
        }
    }
}
