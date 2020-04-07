using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Repos;
using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
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

        public UnitOfWork(MovieContext context, 
                            IMovieRepository movieRepository,
                            IMovieGenreRepository movieGenreRepository,
                            IMovieLanguageRepository movieLanguageRepository,
                            IGenreRepository genreRepository,
                            ILanguageRepository languageRepository)
        {
            _context = context;
            MovieRepository = movieRepository;
            MovieGenreRepository = movieGenreRepository;
            MovieLanguageRepository = movieLanguageRepository;
            GenreRepository = genreRepository;
            LanguageRepository = languageRepository;
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
