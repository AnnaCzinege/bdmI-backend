using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IMovieLanguageRepository : IGenericRepository<MovieLanguage>
    {
        Task<List<int>> GetMovieLanguageIds(int id);

        Task<bool> IsPairExist(int movieId, int languageId);
    }
}
