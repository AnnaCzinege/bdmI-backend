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

        public async Task<int> GetIdByName(string name)
        {
            Genre genre = await _context.Genres.FirstAsync(g => g.Name == name);
            return genre.Id;
        }

        public async Task<bool> IsNameExist(string name)
        {
            return _context.Genres.Any(g => g.Name == name);
        }
    }
}
