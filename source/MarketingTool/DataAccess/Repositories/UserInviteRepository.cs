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
        public void Add(UserInvite item)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
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
