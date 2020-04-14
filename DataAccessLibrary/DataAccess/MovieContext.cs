using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.DataAccess
{
    public class MovieContext : IdentityDbContext
    {
        public MovieContext(DbContextOptions options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<MovieLanguage> MovieLanguages { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieLanguage>().HasKey(ml => new { ml.MovieId, ml.LanguageId });
        }
    }
}
