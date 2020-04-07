using DataAccessLibrary.Models;
using DataAccessLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
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
