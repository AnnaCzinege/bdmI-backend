using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class SQLMovieGenreRepository : SQLBaseRepository<MovieGenre>, IMovieGenreRepository
    {
        public SQLMovieGenreRepository(MovieContext context) : base(context) { }

        public async Task<List<int>> GetMovieGenreIds(int id)
        {
           return await _context.MovieGenres.Where(mg => mg.MovieId == id)
                                            .Select(mg => mg.GenreId)
                                            .ToListAsync();
        }
    }
}
