using DataAccessLibrary.DataAccess;
using DataAccessLibrary.RepositoryContainer;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NUnit.Framework;
using System.Linq;

namespace ImdbBackEnd.Tests
{
    public class Tests
    {
        private DbContextOptions _options;
        private MovieContext _context;
        private IUnitOfWork _unitOfWork;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<MovieContext>()
                .UseInMemoryDatabase(databaseName: "DbContextInMemory")
                .Options;
            _context = new MovieContext(_options);
            _unitOfWork = Substitute.For<IUnitOfWork>();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}