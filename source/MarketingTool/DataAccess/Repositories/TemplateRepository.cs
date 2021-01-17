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

        public async Task<IEnumerable<Template>> GetAllAsync()
        {
            return await _context.Templates.ToListAsync();
        }

        public async Task<Template> GetAsync(int id)
        {
            return await _context.Templates.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var template = _context.Templates.Find(id);
            _context.Templates.Remove(template);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public List<Template> GetAll()
        {
            return _context.Templates.ToList();
        }

        public IEnumerable<Template> Where(Expression<Func<Template, bool>> predicate)
        {
            return _context.Templates.Where(predicate);
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
