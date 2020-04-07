﻿using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public class SQLGenreRepository : IGenreRepository
    {
        private readonly MovieContext _context;

        public SQLGenreRepository(MovieContext context)
        {
            _context = context;
        }

        public async Task<List<string>> GetGenres(List<int> movieGenreIds)
        {
            return await _context.Genres.Where(g => movieGenreIds.Contains(g.Id)).Select(g => g.Name).ToListAsync();
        }
    }
}
