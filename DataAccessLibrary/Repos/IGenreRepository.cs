using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IGenreRepository
    {
        Task<List<string>> GetGenres(List<int> movieGenreIds);

    }
}
