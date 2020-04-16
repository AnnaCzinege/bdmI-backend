using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<List<Movie>> GetNowPlayingMovies(int page);

        Task<List<Movie>> GetPopularMovies(int page);

        Task<List<Movie>> GetTopRatedMovies(int page);

        Task<List<Movie>> GetUpcomingMovies(int page);

        Task<int> GetIdByOriginalId(int id);

        Task<List<int>> GetAllOriginalId();

        Task<Movie> GetMovieByOriginalId(int id);

        Task<Movie> ExtendedFind(int id);
    }
}
