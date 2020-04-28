using Autofac;
using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
using ImdbBackend;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbBackEnd.Tests
{
    public class Tests
    {
        private DbContextOptions _options;
        private MovieContext _context;
        private IUnitOfWork _unitOfWork;
        private IContainer _container;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "DbContextInMemory")
                .Options;
            _context = new MovieContext(_options);
            _context.Movies.Add(new Movie()
            {
                Id = 1,
                OriginalId = 1,
                OriginalTitle = "Title 1",
                Overview = "First movie in fake db.",
                ReleaseDate = "2020-04-28",
                Runtime = 120,
                VoteAverage = 8.7,
                VoteCount = 2564,
                Popularity = 25.4,
                PosterPath = "nope"
            });
            _context.Genres.Add(new Genre()
            {
                Id = 1,
                Name = "Drama"
            });
            _context.Languages.Add(new Language()
            {
                Id = 1,
                Name = "English"
            });
            _context.MovieGenres.Add(new MovieGenre()
            {
                GenreId = 1,
                MovieId = 1
            });
            _context.MovieLanguages.Add(new MovieLanguage()
            {
                LanguageId = 1,
                MovieId = 1
            });
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _container = GetMockedContainer(_context, _unitOfWork);
        }

        [Test]
        public async Task Test_GetMovieByOriginalId()
        {
            Movie movie = await _unitOfWork.MovieRepository.GetMovieByOriginalId(420818);
            Assert.AreEqual("The Lion King", movie.OriginalTitle);
        }

        private IContainer GetMockedContainer(MovieContext context, IUnitOfWork unitOfWork)
        {
            var builder = new ContainerBuilder();

            builder.RegisterInstance(context).As<MovieContext>();
            builder.RegisterInstance(unitOfWork).As<IUnitOfWork>();

            return builder.Build();
        }
    }
}