using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IMovieGenreRepository
    {
        Task<List<int>> GetMovieGenreIds(int id);
    }
}
