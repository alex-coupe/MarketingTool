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
    public class EmailStatusRepository : IRepository<EmailStatus>
    {
        private DatabaseContext _context;
        public EmailStatusRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Add(EmailStatus item)
        {
            _context.EmailStatuses.Add(item);
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

        public void Edit(EmailStatus item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
               

        public async Task<IEnumerable<EmailStatus>> GetAllAsync(Expression<Func<EmailStatus, bool>> predicate)
        {
            return await _context.EmailStatuses.Where(predicate)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<EmailStatus> GetAsync(Expression<Func<EmailStatus, bool>> predicate)
        {
            return await _context.EmailStatuses.Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public void Remove(int id)
        {
            var emailStatus = _context.EmailStatuses.Find(id);
            _context.EmailStatuses.Remove(emailStatus);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
