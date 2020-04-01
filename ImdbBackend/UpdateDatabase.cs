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
            movieIds = await GetMovieIds();
            DeleteFromDb(movieIds);
            await Update(movieIds);
            Console.WriteLine("Database update completed!");
        }

        private async Task<List<int>> GetMovieIds()
        {
            List<int> movieIds = new List<int>();
            string baseURL = "https://api.themoviedb.org/3/movie/popular?api_key=bb29364ab81ef62380611d162d85ecdb&language=en-US&page=";
            int totalMoviePages = 500;

            for (int page = 1; page <= totalMoviePages; page++)
            {
                string requestUri = baseURL + page;
                bool success = false;
                while (!success)
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        using (HttpResponseMessage res = await client.GetAsync(requestUri))
                        using (HttpContent content = res.Content)
                        {
                            string data = await content.ReadAsStringAsync();
                            movieIds.AddRange(JObject.Parse(data)["results"].Select(item => Convert.ToInt32(item["id"])).ToList());
                            success = true;
                        }
                    }
                    catch (HttpRequestException hre)
                    {
                        Console.WriteLine($"Exception thrown getting page data: {requestUri}");
                        Console.WriteLine(hre.Message);
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
                if (!_db.Movies.Any(movie => movie.OriginalId == movieId))
                {
                    string dynamicURL = $"https://api.themoviedb.org/3/movie/{movieId}?api_key=bb29364ab81ef62380611d162d85ecdb&language=en-US";
                    try
                    {
                        bool succes = false;
                        while (!succes)
                        {
                            try
                            {
                                using (HttpClient client = new HttpClient())
                                using (HttpResponseMessage res = await client.GetAsync(dynamicURL))
                                using (HttpContent content = res.Content)
                                {
                                    string data = await content.ReadAsStringAsync();
                                    JToken jsonObject = JObject.Parse(data);
                                    //deserialize and add
                                    DeserializeMovie(jsonObject); 
                                    DeserializeGenres(jsonObject);
                                    DeserializeLanguages(jsonObject);
                                    succes = true;
                                }
                            }
                            catch (HttpRequestException hre)
                            {
                                Console.WriteLine($"Exception thrown getting page data: {dynamicURL}");
                                Console.WriteLine(hre.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex is ArgumentNullException || ex is InvalidOperationException)
                        {
                            Console.WriteLine($"Exception thrown getting page data: {dynamicURL}");
                            Console.WriteLine(ex.Message);
                            ++_fetchCounter;
                            continue;
                        }
                    }
                    Console.WriteLine("Fetch was needed");
                }
                ++_fetchCounter;
                Console.WriteLine(_fetchCounter);
            }
        }

        private void DeserializeLanguages(JToken jsonObject)
        {
            foreach (var language in jsonObject["spoken_languages"])
            {
                string currentLanguageName = Convert.ToString(language["name"]);

                if (!_db.Languages.Any(l => l.Name == currentLanguageName))
                {
                    _db.Add(new Language() { Name = currentLanguageName });
                    _db.SaveChanges();
                }

                int currentMovieId = _db.Movies.Single(m => m.OriginalId == Convert.ToInt32(jsonObject["id"])).Id;
                int currentLanguageId = _db.Languages.Single(l => l.Name == Convert.ToString(language["name"])).Id;

                if (!_db.MovieLanguages.Where(ml=>ml.MovieId == currentMovieId).Any(ml => ml.LanguageId == currentLanguageId))
                {
                    _db.Add(new MovieLanguage()
                    {
                        LanguageId = currentLanguageId,
                        MovieId = currentMovieId
                    });
                    _db.SaveChanges();
                }
            }
        }

        private void DeserializeGenres(JToken jsonObject)
        {
            foreach (var genre in jsonObject["genres"])
            {
                string currentGenreName = Convert.ToString(genre["name"]);

                if (!_db.Genres.Any(g => g.Name == currentGenreName))
                {
                    _db.Add(new Genre() { Name = currentGenreName });
                    _db.SaveChanges();
                }

                int currentMovieId = _db.Movies.Single(m => m.OriginalId == Convert.ToInt32(jsonObject["id"])).Id;
                int currentGenreId = _db.Genres.Single(g => g.Name == Convert.ToString(genre["name"])).Id;

                if (!_db.MovieGenres.Where(mg => mg.MovieId == currentMovieId).Any(mg => mg.GenreId == currentGenreId))
                {
                    _db.Add(new MovieGenre() 
                    {
                        GenreId = currentGenreId,
                        MovieId = currentMovieId
                    });
                    _db.SaveChanges();
                }
               
            }
        }

        private void DeserializeMovie(JToken jsonObject)
        {
            Movie movieToAdd = new Movie()
            {
                OriginalId = Convert.ToInt32(jsonObject["id"]),
                OriginalTitle = Convert.ToString(jsonObject["original_title"]),
                Overview = Convert.ToString(jsonObject["overview"]),
                ReleaseDate = Convert.ToString(jsonObject["release_date"]),
                Runtime = Convert.ToString(jsonObject["runtime"]).Length == 0 ? 0 : Convert.ToInt32(jsonObject["runtime"]),
                VoteAverage = Convert.ToString(jsonObject["vote_average"]).Length == 0 ? 0.0 : Convert.ToDouble(jsonObject["vote_average"]),
                VoteCount = Convert.ToString(jsonObject["vote_count"]).Length == 0 ? 0 : Convert.ToInt32(jsonObject["vote_count"]),
                Popularity = Convert.ToString(jsonObject["popularity"]).Length == 0 ? 0.0 : Convert.ToDouble(jsonObject["popularity"]),
                PosterPath = Convert.ToString(jsonObject["poster_path"])
            };

            _db.Add(movieToAdd);
            _db.SaveChanges();
        }

        private void DeleteFromDb(List<int> movieIds)
        {
            List<int> movieIdsFromDb = _db.Movies.Select(movie => movie.OriginalId).ToList();

            foreach (int movieIdFromDb in movieIdsFromDb)
            {
                if (!movieIds.Contains(movieIdFromDb))
                {
                    Movie dataToDelete = _db.Movies.Single(movie => movie.OriginalId == movieIdFromDb);
                    _db.Remove(dataToDelete);
                    _db.SaveChanges();
                }
            }
        }
    }
}

