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
    public class TimestepRepository : IRepository<Timestep>
    {
        private DatabaseContext _context;
        public TimestepRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(Timestep item)
        {
            _context.Timesteps.Add(item);
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

        public List<Timestep> GetAll()
        {
            return _context.Timesteps.ToList();
        }

        public async Task<IEnumerable<Timestep>> GetAllAsync()
        {
            return await _context.Timesteps.AsNoTracking()
              .ToListAsync();
        }

        public async Task<IEnumerable<Timestep>> GetAllAsync(Expression<Func<Timestep, bool>> predicate)
        {
            return await _context.Timesteps.Where(predicate).ToListAsync();
        }

        public async Task<Timestep> GetAsync(int id)
        {
            return await _context.Timesteps.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Timestep> GetAsync(Expression<Func<Timestep, bool>> predicate, int id)
        {
            return await _context.Timesteps.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var timestep = _context.Timesteps.Find(id);
            _context.Timesteps.Remove(timestep);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<Timestep> Where(Expression<Func<Timestep, bool>> predicate)
        {
            return _context.Timesteps.Where(predicate);
        }
    }
}
