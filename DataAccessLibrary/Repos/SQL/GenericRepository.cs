using DataAccessLibrary.Repos.Interfaces;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;

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

        public void DeleteAsync(TEntity entity)
        {
             _context.Set<TEntity>().Remove(entity);
        }
    }
}
