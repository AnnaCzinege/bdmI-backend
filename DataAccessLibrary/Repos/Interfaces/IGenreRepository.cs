using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<List<string>> GetGenres(List<int> movieGenreIds);

        Task<bool> IsNameExist(string name);

        Task<int> GetIdByName(string name);
    }
}
