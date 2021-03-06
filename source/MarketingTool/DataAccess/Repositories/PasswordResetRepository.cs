﻿using DataAccess.Models;
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

        public void Remove(int id)
        {
            var passwordReset = _context.PasswordResets.Find(id);
            _context.PasswordResets.Remove(passwordReset);
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PasswordReset>> GetAllAsync(Expression<Func<PasswordReset, bool>> predicate, string[] includes)
        {
            var models = _context.PasswordResets.Where(predicate);

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

        public async Task<PasswordReset> GetAsync(Expression<Func<PasswordReset, bool>> predicate, string[] includes)
        {
            var model = _context.PasswordResets.Where(predicate);

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
    }
}
