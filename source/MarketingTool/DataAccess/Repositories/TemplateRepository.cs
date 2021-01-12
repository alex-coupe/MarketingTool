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
    public class TemplateRepository : IRepository<Template>
    {
        private DatabaseContext _context;
        public TemplateRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(Template item)
        {
            throw new NotImplementedException();
        }

        public void Edit(Template item)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Template>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Template> GetAsync(int id)
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

        public List<Template> ToList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Template> Where(Expression<Func<Template, bool>> predicate)
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

        public async Task<IEnumerable<Template>> GetAllAsync(Expression<Func<Template, bool>> predicate)
        {
            return await _context.Templates.Where(predicate).ToListAsync();
        }

        public async Task<Template> GetAsync(Expression<Func<Template, bool>> predicate, int id)
        {
            return await _context.Templates.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
