using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Mock
{
    class MockMovieLanguageRepository
    {
        public Task<List<int>> GetMovieLanguageIds(int id)
        {
            throw new NotImplementedException();
        }

    }
}
