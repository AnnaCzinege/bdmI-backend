using DataAccessLibrary.DataAccess;
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
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            throw new NotImplementedException();
        }

        private async Task<List<int>> GetMovies()
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
                            ++_fetchCounter;
                            continue;
                        }
                    }
                }
                ++_fetchCounter;
                Console.WriteLine(_fetchCounter);
            }
        }

    }
}
