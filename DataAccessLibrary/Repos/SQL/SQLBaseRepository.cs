using DataAccessLibrary.Repos.Interfaces;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;

namespace DataAccessLibrary.Repos.SQL
{
    public class SQLBaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MovieContext _context;

        public SQLBaseRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }
    }
}
