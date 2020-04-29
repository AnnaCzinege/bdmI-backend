using DataAccessLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

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
        public DbSet<User> User { get; set; }
        public DbSet<WatchlistItem> WatchlistItems { get; set; } //MARKED


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieGenre>().HasKey(mg => new { mg.MovieId, mg.GenreId });
            modelBuilder.Entity<MovieLanguage>().HasKey(ml => new { ml.MovieId, ml.LanguageId });
            modelBuilder.Entity<WatchlistItem>().HasKey(w => new { w.MovieId, w.UserId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
    }
}
