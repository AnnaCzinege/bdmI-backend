using DataAccessLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface ILanguageRepository : IGenericRepository<Language>
    {
        Task<List<string>> GetLanguages(List<int> movieLanguageIds);
    }
}
