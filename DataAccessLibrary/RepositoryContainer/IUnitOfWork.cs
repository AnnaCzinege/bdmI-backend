using DataAccessLibrary.Repos.Interfaces;
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
        IUserRepository UserRepository { get; }
        IWatchlistItemRepository WatchlistItemRepository { get; }
        Task SaveAsync();
    }
}
