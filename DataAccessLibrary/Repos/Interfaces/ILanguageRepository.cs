using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface ILanguageRepository
    {
        Task<List<string>> GetLanguages(List<int> movieLanguageIds);
    }
}
