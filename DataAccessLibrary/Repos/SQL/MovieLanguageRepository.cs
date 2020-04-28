using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class MovieLanguageRepository : GenericRepository<MovieLanguage>, IMovieLanguageRepository
    {
        public MovieLanguageRepository(MovieContext context) : base(context) { }

        public async Task<List<int>> GetMovieLanguageIds(int id)
        {
            return await _context.MovieLanguages.Where(ml => ml.MovieId == id)
                                                .Select(ml => ml.LanguageId)
                                                .ToListAsync();
        }

        public async Task<bool> DoesPairExist(int movieId, int languageId)
        {
            return _context.MovieLanguages.Where(ml => ml.MovieId == movieId).Any(ml => ml.LanguageId == languageId);
        }
    }
}
