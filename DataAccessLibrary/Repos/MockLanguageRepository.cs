using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public class MockLanguageRepository : ILanguageRepository
    {
        public Task<List<string>> GetLanguages(List<int> movieLanguageIds)
        {
            throw new NotImplementedException();
        }
    }
}
