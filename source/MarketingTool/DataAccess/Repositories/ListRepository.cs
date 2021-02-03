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
    public class ListRepository : IRepository<List>
    {
        private DatabaseContext _context;
        public ListRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(List item)
        {
            _context.Lists.Add(item);
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

        public void Edit(List item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

    
        public async Task<IEnumerable<List>> GetAllAsync(Expression<Func<List, bool>> predicate, string[] includes)
        {
            var models = _context.Lists.Where(predicate);

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

        public async Task<List> GetAsync(Expression<Func<List, bool>> predicate, string[] includes)
        {
            var model = _context.Lists.Where(predicate);

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
            var list = _context.Lists.Find(id);
            _context.Lists.Remove(list);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
