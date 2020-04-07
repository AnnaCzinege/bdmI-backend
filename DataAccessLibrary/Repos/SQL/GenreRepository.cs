using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieContext context) : base(context) { }

        public async Task<List<string>> GetGenres(List<int> movieGenreIds)
        {
            return await _context.Genres.Where(g => movieGenreIds.Contains(g.Id))
                                        .Select(g => g.Name)
                                        .ToListAsync();
        }
    }
}
