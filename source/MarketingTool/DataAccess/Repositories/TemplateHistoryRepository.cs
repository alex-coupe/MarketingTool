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

        public async Task<IEnumerable<TemplateHistory>> GetAllAsync()
        {
            return await _context.TemplateHistory.AsNoTracking()
               .Include(x => x.Template)
               .ToListAsync();
        }

        public async Task<TemplateHistory> GetAsync(int id)
        {
            return await _context.TemplateHistory.AsNoTracking()
            .Include(x => x.Template)
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var templateHistory = _context.TemplateHistory.Find(id);
            _context.TemplateHistory.Remove(templateHistory);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public List<TemplateHistory> GetAll()
        {
            return _context.TemplateHistory.ToList();
        }

        public IEnumerable<TemplateHistory> Where(Expression<Func<TemplateHistory, bool>> predicate)
        {
            return _context.TemplateHistory.Where(predicate);
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

        public async Task<TemplateHistory> GetAsync(Expression<Func<TemplateHistory, bool>> predicate, int id)
        {
            return await _context.TemplateHistory.Where(predicate).Where(x => x.Id == id)
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
