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
    public class UserInviteRepository : IRepository<UserInvite>
    {
        private DatabaseContext _context;
        public UserInviteRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(UserInvite item)
        {
            _context.UserInvites.Add(item);
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

        public void Edit(UserInvite item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<UserInvite>> GetAllAsync()
        {
            return await _context.UserInvites.AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<UserInvite>> GetAllAsync(Expression<Func<UserInvite, bool>> predicate)
        {
            return await _context.UserInvites.Where(predicate).ToListAsync();
        }

        public async Task<UserInvite> GetAsync(int id)
        {
            return await _context.UserInvites.AsNoTracking()
             .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserInvite> GetAsync(Expression<Func<UserInvite, bool>> predicate, int id)
        {
            return await _context.UserInvites.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var userInvite = _context.UserInvites.Find(id);
            _context.UserInvites.Remove(userInvite);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public List<UserInvite> GetAll()
        {
            return _context.UserInvites.ToList();
        }

        public IEnumerable<UserInvite> Where(Expression<Func<UserInvite, bool>> predicate)
        {
            return _context.UserInvites.Where(predicate);
        }
    }
}
