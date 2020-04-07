using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllMovies();

        Task<Movie> GetMovieDetails(int id);

        Task<List<Movie>> GetNowPlayingMovies(int page);

        Task<List<Movie>> GetPopularMovies(int page);

        Task<List<Movie>> GetTopRatedMovies(int page);

        Task<List<Movie>> GetUpcominMovies(int page);
    }
}
