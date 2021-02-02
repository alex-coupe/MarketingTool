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
    public class PasswordResetRepository : IRepository<PasswordReset>
    {
        private DatabaseContext _context;
        public PasswordResetRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(PasswordReset item)
        {
            _context.Add(item);
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

        public void Edit(PasswordReset item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<PasswordReset>> GetAllAsync()
        {
            return await _context.PasswordResets.AsNoTracking().ToListAsync();
        }

        public async Task<PasswordReset> GetAsync(int id)
        {
            return await _context.PasswordResets.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var passwordReset = _context.PasswordResets.Find(id);
            _context.PasswordResets.Remove(passwordReset);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public List<PasswordReset> GetAll()
        {
            return _context.PasswordResets.ToList();
        }

        public IEnumerable<PasswordReset> Where(Expression<Func<PasswordReset, bool>> predicate)
        {
            return _context.PasswordResets.Where(predicate).Include(x => x.User);
        }

        public async Task<IEnumerable<PasswordReset>> GetAllAsync(Expression<Func<PasswordReset, bool>> predicate)
        {
            return await _context.PasswordResets.AsNoTracking()
               .ToListAsync();
        }

        public async Task<PasswordReset> GetAsync(Expression<Func<PasswordReset, bool>> predicate)
        {
            return await _context.PasswordResets.Where(predicate).FirstOrDefaultAsync();
        }
    }
}
