using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IGenreRepository
    {
        Task<List<string>> GetGenres(List<int> movieGenreIds);
    }
}
