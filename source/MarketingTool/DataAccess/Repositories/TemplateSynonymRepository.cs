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
    public class TemplateSynonymRepository : IRepository<TemplateSynonym>
    {
        private DatabaseContext _context;
        public TemplateSynonymRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(TemplateSynonym item)
        {
            _context.TemplateSynonyms.Add(item);
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

        public void Edit(TemplateSynonym item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

    
        public async Task<IEnumerable<TemplateSynonym>> GetAllAsync(Expression<Func<TemplateSynonym, bool>> predicate, string[] includes)
        {
            var models = _context.TemplateSynonyms.Where(predicate);

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

        
        public async Task<TemplateSynonym> GetAsync(Expression<Func<TemplateSynonym, bool>> predicate, string[] includes)
        {
            var model = _context.TemplateSynonyms.Where(predicate);

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
            var synonym = _context.TemplateSynonyms.Find(id);
            _context.TemplateSynonyms.Remove(synonym);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
