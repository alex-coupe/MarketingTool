using DataAccess.Models;
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserInvite>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserInvite>> GetAllAsync(Expression<Func<UserInvite, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<UserInvite> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UserInvite> GetAsync(Expression<Func<UserInvite, bool>> predicate, int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            var userInvite = _context.UserInvites.Find(id);
            _context.UserInvites.Remove(userInvite);
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public List<UserInvite> ToList()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserInvite> Where(Expression<Func<UserInvite, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
