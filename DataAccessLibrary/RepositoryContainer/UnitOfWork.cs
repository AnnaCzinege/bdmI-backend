using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Repos.Interfaces;
using System.Threading.Tasks;

namespace DataAccessLibrary.RepositoryContainer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MovieContext _context;

        public IMovieRepository MovieRepository { get; set; }

        public IMovieGenreRepository MovieGenreRepository { get; set; }

        public IMovieLanguageRepository MovieLanguageRepository { get; set; }

        public IGenreRepository GenreRepository { get; set; }

        public ILanguageRepository LanguageRepository { get; set; }

        public IUserRepository UserRepository { get; set; }

        public IWatchlistItemRepository WatchlistItemRepository { get; set; }

        public UnitOfWork(MovieContext context, 
                            IMovieRepository movieRepository,
                            IMovieGenreRepository movieGenreRepository,
                            IMovieLanguageRepository movieLanguageRepository,
                            IGenreRepository genreRepository,
                            ILanguageRepository languageRepository,
                            IUserRepository userRepository,
                            IWatchlistItemRepository watchlistItemRepository)
        {
            _context = context;
            MovieRepository = movieRepository;
            MovieGenreRepository = movieGenreRepository;
            MovieLanguageRepository = movieLanguageRepository;
            GenreRepository = genreRepository;
            LanguageRepository = languageRepository;
            UserRepository = userRepository;
            WatchlistItemRepository = watchlistItemRepository;
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
