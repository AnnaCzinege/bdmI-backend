using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Repos
{
    public interface IMovieGenreRepository
    {
        Task<List<int>> GetMovieGenreIds(int id);
    }
}
