using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private DatabaseContext _context;
        public ClientRepository(DatabaseContext context)
        {
            _context = context;
        }

        public void Add(Client item)
        {
            _context.Clients.Add(item);
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

        public void Edit(Client item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }
        public async Task<IEnumerable<Client>> GetAllAsync(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate)
                .AsNoTracking()
                .ToListAsync();            
        }

        public async Task<Client> GetAsync(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }      

        public void Remove(int id)
        {
            var client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
      
    }
}
