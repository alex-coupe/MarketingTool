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
            throw new NotImplementedException();
        }

        public void Edit(TemplateHistory item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TemplateHistory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TemplateHistory> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public List<TemplateHistory> ToList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TemplateHistory> Where(Expression<Func<TemplateHistory, bool>> predicate)
        {
            throw new NotImplementedException();
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

        public Task<TemplateHistory> GetAsync(Expression<Func<TemplateHistory, bool>> predicate, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TemplateHistory>> GetAllAsync(Expression<Func<TemplateHistory, bool>> predicate)
        {
            return await _context.TemplateHistory.Where(predicate).ToListAsync();
        }
    }
}
