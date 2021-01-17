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

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.AsNoTracking()
               .ToListAsync();
        }

        public async Task<Client> GetAsync(int id)
        {
            return await _context.Clients.AsNoTracking()
               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Remove(int id)
        {
            var client = _context.Clients.Find(id);
            _context.Clients.Remove(client);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public IEnumerable<Client> Where(Expression<Func<Client, bool>> predicate)
        {
            return _context.Clients.Where(predicate);
        }

        public List<Client> GetAll()
        {
            return _context.Clients.ToList();
        }

        public async Task<IEnumerable<Client>> GetAllAsync(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate).ToListAsync();
        }

        public async Task<Client> GetAsync(Expression<Func<Client, bool>> predicate, int id)
        {
            return await _context.Clients.Where(predicate).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}
