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
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<PasswordReset>> GetAllAsync()
        {
            return await _context.PasswordResets.AsNoTracking().ToListAsync();
        }

        public Task<PasswordReset> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            var passwordReset = _context.PasswordResets.Find(id);
            _context.PasswordResets.Remove(passwordReset);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public List<PasswordReset> ToList()
        {
            return _context.PasswordResets.ToList();
        }

        public IEnumerable<PasswordReset> Where(Expression<Func<PasswordReset, bool>> predicate)
        {
            return _context.PasswordResets.Where(predicate);
        }

        public Task<IEnumerable<PasswordReset>> GetAllAsync(Expression<Func<PasswordReset, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<PasswordReset> GetAsync(Expression<Func<PasswordReset, bool>> predicate, int id)
        {
            throw new NotImplementedException();
        }
    }
}
