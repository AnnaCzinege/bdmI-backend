using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Mock
{
    public class MockGenreRepository : IGenreRepository
    {
        public Task<List<string>> GetGenres(List<int> movieGenreIds)
        {
            throw new NotImplementedException();
        }
    }
}
