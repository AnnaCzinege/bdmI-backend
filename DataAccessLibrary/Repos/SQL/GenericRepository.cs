using DataAccessLibrary.Repos.Interfaces;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repos.SQL
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MovieContext _context;

        public GenericRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task Remove(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> Find(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }
    }
}
