using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface ILanguageRepository
    {
        Task<List<string>> GetLanguages(List<int> movieLanguageIds);
    }
}
