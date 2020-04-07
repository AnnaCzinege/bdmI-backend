using DataAccessLibrary.Repos.Interfaces;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;

namespace DataAccessLibrary.Repos.SQL
{
    public class SQLBaseRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly MovieContext _context;

        public SQLBaseRepository()
        {

        }

        public Task<TEntity> AddAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<TEntity> DeleteAsync(TEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
