using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public class SQLMovieGenreRepository : IMovieGenreRepository
    {
        private readonly MovieContext _context;

        public SQLMovieGenreRepository(MovieContext context)
        {
            _context = context;
        }

        public async List<int> GetMovieGenreIds(int id)
        {
           return await _context.MovieGenres.Where(mg => mg.MovieId == id).Select(mg => mg.GenreId).ToListAsync();
        }
    }
}
