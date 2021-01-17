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
    public class UserRepository : IRepository<User>
    {
        private DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(User item)
        {
            _context.Users.Add(item);
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

        public void Edit(User item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.AsNoTracking()
                .ToListAsync();
        }

        public async Task<User> GetAsync(int id)
        {
            return await _context.Users.AsNoTracking()
               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var user = _context.Users.Find(id);
            _context.Users.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<User> Where(Expression<Func<User, bool>> predicate)
        {
            return _context.Users.Where(predicate);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public async Task<IEnumerable<User>> GetAllAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users.Where(predicate).ToListAsync();
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> predicate, int id)
        {
            return await _context.Users.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
