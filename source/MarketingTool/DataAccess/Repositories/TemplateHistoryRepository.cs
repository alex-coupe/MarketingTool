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

        public async Task<TemplateHistory> GetAsync(Expression<Func<TemplateHistory, bool>> predicate, string[] includes)
        {
            var model = _context.TemplateHistory.Where(predicate);

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

        public async Task<IEnumerable<TemplateHistory>> GetAllAsync(Expression<Func<TemplateHistory, bool>> predicate, string[] includes)
        {
            var models = _context.TemplateHistory.Where(predicate);

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
    }
}
