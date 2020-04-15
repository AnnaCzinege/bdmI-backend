using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(MovieContext context) : base(context) { }

        public async Task<int> GetIdByName(string name)
        {
            Language language = await _context.Languages.FirstAsync(l => l.Name == name);
            return language.Id;
        }

        public async Task<List<string>> GetLanguages(List<int> movieLanguageIds)
        {
            return await _context.Languages.Where(l => movieLanguageIds.Contains(l.Id))
                                            .Select(l => l.Name)
                                            .ToListAsync();
        }

        public async Task<bool> IsNameExist(string name)
        {
            return _context.Languages.Any(l => l.Name == name);
        }
    }
}
