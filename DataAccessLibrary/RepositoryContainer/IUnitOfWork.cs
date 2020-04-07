using DataAccessLibrary.Repos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.RepositoryContainer
{
    public interface IUnitOfWork
    {
        IMovieRepository MovieRepository { get; }
        IMovieGenreRepository MovieGenreRepository { get; }
        IMovieLanguageRepository MovieLanguageRepository { get; }
        IGenreRepository GenreRepository { get; }
        ILanguageRepository LanguageRepository { get; }

        Task SaveAsync();
    }
}
