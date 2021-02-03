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
            _context.Templates.Add(item);
        }

        public void Edit(Template item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Remove(int id)
        {
            var template = _context.Templates.Find(id);
            _context.Templates.Remove(template);
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

        public async Task<IEnumerable<Template>> GetAllAsync(Expression<Func<Template, bool>> predicate, string[] includes)
        {
           var models = _context.Templates.Where(predicate);

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

        public async Task<Template> GetAsync(Expression<Func<Template, bool>> predicate, string[] includes)
        {
            var model = _context.Templates.Where(predicate);

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
