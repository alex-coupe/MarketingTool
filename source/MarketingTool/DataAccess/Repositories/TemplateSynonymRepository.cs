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

        public List<TemplateSynonym> GetAll()
        {
            return _context.TemplateSynonyms.ToList();
        }

        public async Task<IEnumerable<TemplateSynonym>> GetAllAsync()
        {
            return await _context.TemplateSynonyms.AsNoTracking()
               .ToListAsync();
        }

        public async Task<IEnumerable<TemplateSynonym>> GetAllAsync(Expression<Func<TemplateSynonym, bool>> predicate)
        {
            return await _context.TemplateSynonyms.Where(predicate).ToListAsync();
        }

        public async Task<TemplateSynonym> GetAsync(int id)
        {
            return await _context.TemplateSynonyms.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TemplateSynonym> GetAsync(Expression<Func<TemplateSynonym, bool>> predicate, int id)
        {
            return await _context.TemplateSynonyms.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var synonym = _context.TemplateSynonyms.Find(id);
            _context.TemplateSynonyms.Remove(synonym);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<TemplateSynonym> Where(Expression<Func<TemplateSynonym, bool>> predicate)
        {
            return _context.TemplateSynonyms.Where(predicate);
        }
    }
}
