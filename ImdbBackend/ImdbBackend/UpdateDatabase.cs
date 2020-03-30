using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ImdbBackend
{
    public class UpdateDatabase : BackgroundService
    {
        private readonly MovieContext _db;
        private int _fetchCounter = 0;

        public UpdateDatabase(MovieContext db)
        {
            _db = db;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<int> movieIds = new List<int>();
            movieIds = await ForceToFetchIds(movieIds);
            DeleteFromDb(movieIds);
            await ForceToFetchDetails(movieIds);
            Console.WriteLine("Database update completed!");
        }

        private async Task<List<int>> GetMovieIds()
        {
            List<int> movieIds = new List<int>();
            string baseURL = "https://api.themoviedb.org/3/movie/popular?api_key=bc3417b21d3ce5c6f51a602d8422eff9&language=en-US&page=";
            int totalMoviePages = 500;

            for (int page = 1; page <= totalMoviePages; page++)
            {
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage res = await client.GetAsync(baseURL + page))
                    {
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            movieIds.AddRange(JObject.Parse(data)["results"].Select(item => Convert.ToInt32(item["id"])).ToList());
                        }
                    }
                }
                Console.WriteLine(movieIds.Count());
            }
            return movieIds;
        }

        private async Task Update(List<int> movieIds)
        {
            foreach (var movieId in movieIds)
            {
                if (!_db.Movies.Select(movie => movie.MovieId).Contains(movieId))
                {
                    Console.WriteLine("Need fetch");
                    string dynamicURL = $"https://api.themoviedb.org/3/movie/{movieId}?api_key=bc3417b21d3ce5c6f51a602d8422eff9&language=en-US";
                    using (HttpClient client = new HttpClient())
                    {
                        try
                        {
                            using (HttpResponseMessage res = await client.GetAsync(dynamicURL))
                            {
                                using (HttpContent content = res.Content)
                                {
                                    string data = await content.ReadAsStringAsync();
                                    JToken jsonObject = JObject.Parse(data);
                                    DeserializeJson(jsonObject);
                                }
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            Console.WriteLine(movieId);
                            ++_fetchCounter;
                            continue;
                        }
                    }
                }
                ++_fetchCounter;
                Console.WriteLine(_fetchCounter);
            }
        }

        private void DeserializeJson(JToken jsonObject)
        {
            _db.Add(new Movie()
            {
                MovieId = Convert.ToInt32(jsonObject["id"]),
                OriginalTitle = Convert.ToString(jsonObject["original_title"]),
                Overview = Convert.ToString(jsonObject["overview"]),
                Genres = jsonObject["genres"].Select(genre => new Genre() { Name = Convert.ToString(genre["name"]) }).ToList(),
                SpokenLanguages = jsonObject["spoken_languages"].Select(language => new Language() { Name = Convert.ToString(language["name"]) }).ToList(),
                ReleaseDate = Convert.ToString(jsonObject["release_date"]),
                Runtime = Convert.ToString(jsonObject["runtime"]).Length == 0 ? 0 : Convert.ToInt32(jsonObject["runtime"]),
                VoteAverage = Convert.ToString(jsonObject["vote_average"]).Length == 0 ? 0.0 : Convert.ToDouble(jsonObject["vote_average"]),
                VoteCount = Convert.ToString(jsonObject["vote_count"]).Length == 0 ? 0 : Convert.ToInt32(jsonObject["vote_count"]),
                Popularity = Convert.ToString(jsonObject["popularity"]).Length == 0 ? 0.0 : Convert.ToDouble(jsonObject["popularity"]),
                PosterPath = Convert.ToString(jsonObject["poster_path"])
            });
            //If you change anything in the database, you need to save the chnages, otherwise the query won't run just wait
            _db.SaveChanges();
        }

        private async Task<List<int>> ForceToFetchIds(List<int> movieIds)
        {
            while (movieIds.Count() < 10000)
            {
                try
                {
                    movieIds = await GetMovieIds();
                }
                catch (HttpRequestException)
                {
                    continue;
                }
            }

            return movieIds;
        }

        private async Task ForceToFetchDetails(List<int> movieIds)
        {
            while (_fetchCounter < movieIds.Count())
            {
                try
                {
                    await Update(movieIds);
                }
                catch (HttpRequestException)
                {
                    _fetchCounter = 0;
                    continue;
                }
            }
        }

        private void DeleteFromDb(List<int> movieIds)
        {
            List<int> movieIdsFromDb = _db.Movies.Select(movie => movie.MovieId).ToList();

            foreach (int movieIdFromDb in movieIdsFromDb)
            {
                if (!movieIds.Contains(movieIdFromDb))
                {
                    Movie dataToDelete = _db.Movies.Include(m => m.SpokenLanguages)
                        .Include(m => m.Genres)
                        .Single(movie => movie.MovieId == movieIdFromDb);
                    _db.Remove(dataToDelete);
                    _db.SaveChanges();
                }
            }
        }
    }


}
}
