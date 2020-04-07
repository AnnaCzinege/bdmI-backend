using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos
{
    public interface IMovieGenreRepository
    {
        Task<List<int>> GetMovieGenreIds(int id);
    }
}
