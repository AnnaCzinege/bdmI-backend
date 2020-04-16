using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IGenreRepository : IGenericRepository<Genre>
    {
        Task<List<string>> GetGenres(List<int> movieGenreIds);

        Task<int> GetIdByName(string name);

        Task<List<string>> GetAllName();
    }
}
