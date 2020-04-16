using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IMovieGenreRepository : IGenericRepository<MovieGenre>
    {
        Task<List<int>> GetMovieGenreIds(int id);

        Task<bool> DoesPairExist(int movieId, int genreId);
    }
}
