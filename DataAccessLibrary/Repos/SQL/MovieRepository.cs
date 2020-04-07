using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        private readonly int _moviesPerPage = 20;
        private int CalculateFirstItemOfPage(int page) { return _moviesPerPage * page - _moviesPerPage; }

        public MovieRepository(MovieContext context) : base(context) { }

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
            long currentDateOneMonthAgo = Convert.ToInt64(new DateTime(DateTime.Today.Year, DateTime.Today.Month - 1, 1).ToString("yyyyMMdd"));
            long currentDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
            return await _context.Movies.Where(movie => Convert.ToInt64(movie.ReleaseDate.Replace("-", "")) > currentDateOneMonthAgo 
                                                                        && Convert.ToInt64(movie.ReleaseDate.Replace("-", "")) < currentDate)
                                        .OrderByDescending(movie => Convert.ToInt64(movie.ReleaseDate.Replace("-", "")))
                                        .Skip(CalculateFirstItemOfPage(page))
                                        .Take(_moviesPerPage)
                                        .ToListAsync();
        }

        public async Task<List<Movie>> GetPopularMovies(int page)
        {
            return await _context.Movies.OrderByDescending(movie => movie.Popularity)
                                        .Skip(CalculateFirstItemOfPage(page))
                                        .Take(_moviesPerPage)
                                        .ToListAsync();
        }

        public async Task<List<Movie>> GetTopRatedMovies(int page)
        {
            return await _context.Movies.Where(m => m.VoteCount > 1000)
                                        .OrderByDescending(m => m.VoteAverage)
                                        .Skip(CalculateFirstItemOfPage(page))
                                        .Take(_moviesPerPage)
                                        .ToListAsync();
        }

        public async Task<List<Movie>> GetUpcomingMovies(int page)
        {
            long currentDate = Convert.ToInt64(DateTime.Now.ToString("yyyyMMdd"));
            return await _context.Movies.Where(m => Convert.ToInt64(m.ReleaseDate.Replace("-", "")) > currentDate)
                                        .OrderBy(m => Convert.ToInt64(m.ReleaseDate.Replace("-", "")))
                                        .Skip(CalculateFirstItemOfPage(page))
                                        .Take(_moviesPerPage)
                                        .ToListAsync();
        }

        public bool IsIdExist(int id)
        {
            return _context.Movies.Any(movie => movie.OriginalId == id);
        }

        public async Task<int> GetIdByOriginalId(int id)
        {
            Movie movie = await _context.Movies.FirstAsync(m => m.OriginalId == id);
            return movie.Id;
        }

        public async Task<List<int>> GetAllOriginalId()
        {
            return await _context.Movies.Select(m => m.OriginalId).ToListAsync();
        }

        public async Task<Movie> GetMovieByOriginalId(int id)
        {
            return await _context.Movies.FirstAsync(m => m.OriginalId == id);
        }
    }
}
