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
    public class RecipientSchemaRepository : IRepository<RecipientSchema>
    {
        private DatabaseContext _context;
        public RecipientSchemaRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(RecipientSchema item)
        {
            _context.RecipientSchemas.Add(item);
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

        public void Edit(RecipientSchema item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public List<RecipientSchema> GetAll()
        {
            return _context.RecipientSchemas.ToList();
        }

        public async Task<IEnumerable<RecipientSchema>> GetAllAsync()
        {
            return await _context.RecipientSchemas.AsNoTracking()
               .ToListAsync();
        }

        public async Task<IEnumerable<RecipientSchema>> GetAllAsync(Expression<Func<RecipientSchema, bool>> predicate)
        {
            return await _context.RecipientSchemas.Where(predicate).ToListAsync();
        }

        public async Task<RecipientSchema> GetAsync(int id)
        {
            return await _context.RecipientSchemas.AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<RecipientSchema> GetAsync(Expression<Func<RecipientSchema, bool>> predicate)
        {
            return await _context.RecipientSchemas.Where(predicate).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var schema = _context.RecipientSchemas.Find(id);
            _context.RecipientSchemas.Remove(schema);
        }

      
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<RecipientSchema> Where(Expression<Func<RecipientSchema, bool>> predicate)
        {
            return _context.RecipientSchemas.Where(predicate);
        }
    }
}
