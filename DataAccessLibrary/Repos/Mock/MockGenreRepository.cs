﻿using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Mock
{
    public class MockGenreRepository 
    {
        public Task<List<string>> GetGenres(List<int> movieGenreIds)
        {
            throw new NotImplementedException();
        }
    }
}
