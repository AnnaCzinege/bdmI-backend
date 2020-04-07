using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Mock
{
    public class MockLanguageRepository
    {
        public Task<List<string>> GetLanguages(List<int> movieLanguageIds)
        {
            throw new NotImplementedException();
        }
    }
}
