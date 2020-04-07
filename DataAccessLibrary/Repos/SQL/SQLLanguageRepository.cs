using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class SQLLanguageRepository : ILanguageRepository
    {
        private readonly MovieContext _context;

        public SQLLanguageRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetLanguages(List<int> movieLanguageIds)
        {
            return await _context.Languages.Where(l => movieLanguageIds.Contains(l.Id))
                                            .Select(l => l.Name)
                                            .ToListAsync();
        }
    }
}
