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
    public class UserPermissionRepository : IRepository<UserPermission>
    {
        private DatabaseContext _context;

        public UserPermissionRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(UserPermission item)
        {
            _context.UserPermissions.Add(item);
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


        public void Edit(UserPermission item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public async Task<IEnumerable<UserPermission>> GetAllAsync(Expression<Func<UserPermission, bool>> predicate = null, string[] includes = null)
        {
            var models = _context.UserPermissions.Where(predicate);

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

        public async Task<UserPermission> GetAsync(Expression<Func<UserPermission, bool>> predicate = null, string[] includes = null)
        {
            var model = _context.UserPermissions.Where(predicate);

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
            var campaign = _context.UserPermissions.Find(id);
            _context.UserPermissions.Remove(campaign);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
