using DataAccessLibrary.Models;
using DataAccessLibrary.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Mock
{
    public class MockMovieRepository : IMovieRepository
    {
        private List<Movie> _movies;

        public Task<List<Movie>> GetAllMovies()
        {
            throw new NotImplementedException();
        }


        public Task<Movie> GetMovieDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetNowPlayingMovies(int page)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetPopularMovies(int page)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetTopRatedMovies(int page)
        {
            throw new NotImplementedException();
        }

        public Task<List<Movie>> GetUpcominMovies(int page)
        {
            throw new NotImplementedException();
        }
    }
}
