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

        public List<List> GetAll()
        {
            return _context.Lists.ToList();
        }

        public async Task<IEnumerable<List>> GetAllAsync()
        {
            return await _context.Lists.AsNoTracking()
               .ToListAsync();
        }

        public async Task<IEnumerable<List>> GetAllAsync(Expression<Func<List, bool>> predicate)
        {
            return await _context.Lists.Where(predicate).ToListAsync();
        }

        public async Task<List> GetAsync(int id)
        {
            return await _context.Lists.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List> GetAsync(Expression<Func<List, bool>> predicate, int id)
        {
            return await _context.Lists.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var list = _context.Lists.Find(id);
            _context.Lists.Remove(list);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<List> Where(Expression<Func<List, bool>> predicate)
        {
            return _context.Lists.Where(predicate);
        }
    }
}
