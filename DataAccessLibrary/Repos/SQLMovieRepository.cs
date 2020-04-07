using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public class SQLMovieRepository : IMovieRepository
    {
        private readonly MovieContext _context;

        public SQLMovieRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        public async Task<Movie> GetMovieDetails(int id)
        {
            return await _context.Movies.FindAsync(id);
        }

        public async Task<List<Movie>> GetNowPlayingMovies(int page)
        {
            int moviesPerPage = 20;
            int from = page * moviesPerPage - moviesPerPage; //in the db ids start w/1, not 0, but first we want to skip 0 items
            long currentDateOneMonthAgo = Convert.ToInt64(new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, 1).ToString("yyyyMMdd"));
            long currentDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
            return await _context.Movies.Where(movie => Convert.ToInt64(movie.ReleaseDate.Replace("-", "")) > currentDateOneMonthAgo && Convert.ToInt64(movie.ReleaseDate.Replace("-", "")) < currentDate)
                .OrderByDescending(movie => Convert.ToInt64(movie.ReleaseDate.Replace("-", "")))
                .Skip(from)
                .Take(moviesPerPage)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetPopularMovies(int page)
        {
            int moviesPerPage = 20;
            int from = page * moviesPerPage - moviesPerPage;
            return await _context.Movies.OrderByDescending(movie => movie.Popularity)
                .Skip(from)
                .Take(moviesPerPage)
                .Select(movie => movie)
                .ToListAsync();
        }

        public async Task<List<Movie>> GetTopRatedMovies(int page)
        {
            int moviesPerPage = 20;
            int from = moviesPerPage * page - moviesPerPage;
            var movies = _context.Movies.Where(m => m.VoteCount > 1000).OrderByDescending(m => m.VoteAverage).Skip(from).Take(moviesPerPage);
            return await movies.ToListAsync();
        }

        public async Task<List<Movie>> GetUpcominMovies(int page)
        {
            long currentDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
            int moviesPerPage = 20;
            int from = moviesPerPage * page - moviesPerPage;
            var movies = _context.Movies.Where(m => Convert.ToInt64(m.ReleaseDate.Replace("-", "")) > currentDate).OrderBy(m => Convert.ToInt64(m.ReleaseDate.Replace("-", ""))).Skip(from).Take(moviesPerPage);
            return await movies.ToListAsync();
        }
    }
}
