using DataAccessLibrary.Models;
using DataAccessLibrary.RepositoryContainer;
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
        private readonly IUnitOfWork _unitOfWork;
        private int _fetchCounter = 0;

        public UpdateDatabase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<int> movieIds = await GetMovieIds();
            await DeleteFromDb(movieIds);
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
                        using HttpResponseMessage res = await new HttpClient().GetAsync(requestUri);
                        string data = await res.Content.ReadAsStringAsync();
                        movieIds.AddRange(JObject.Parse(data)["results"].Select(item => Convert.ToInt32(item["id"])).ToList());
                        success = true;
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
            List<int> movieIdsFromDb = await _unitOfWork.MovieRepository.GetAllOriginalId();

            foreach (var movieId in movieIds)
            {
                if (!movieIdsFromDb.Contains(movieId))
                {
                    string dynamicURL = $"https://api.themoviedb.org/3/movie/{movieId}?api_key=bb29364ab81ef62380611d162d85ecdb&language=en-US";
                    try
                    {
                        bool succes = false;
                        while (!succes)
                        {
                            try
                            {
                                using HttpResponseMessage res = await new HttpClient().GetAsync(dynamicURL);
                                string data = await res.Content.ReadAsStringAsync();
                                JToken jsonObject = JObject.Parse(data);
                                await AddNewMovie(jsonObject);
                                await AddNewGenres(jsonObject);
                                await AddNewMovieGenre(jsonObject);
                                await AddNewLanguages(jsonObject);
                                await AddNewMovieLanguages(jsonObject);
                                succes = true;
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

        private async Task AddNewLanguages(JToken jsonObject)
        {
            List<string> languageNamesFromDb = await _unitOfWork.LanguageRepository.GetAllName();

            foreach (var language in jsonObject["spoken_languages"])
            {
                string currentLanguageName = Convert.ToString(language["name"]);

                if (!languageNamesFromDb.Contains(currentLanguageName))
                {
                   await _unitOfWork.LanguageRepository.AddAsync(new Language() { Name = currentLanguageName });
                   await _unitOfWork.SaveAsync();
                }
            }
        }

        private async Task AddNewMovieLanguages(JToken jsonObject)
        {
            foreach (var language in jsonObject["spoken_languages"])
            {
                int currentMovieId = await _unitOfWork.MovieRepository.GetIdByOriginalId(Convert.ToInt32(jsonObject["id"]));
                int currentLanguageId = await _unitOfWork.LanguageRepository.GetIdByName(Convert.ToString(language["name"]));

                if (!await _unitOfWork.MovieLanguageRepository.DoesPairExist(currentMovieId, currentLanguageId))
                {
                    await _unitOfWork.MovieLanguageRepository.AddAsync(new MovieLanguage()
                    {
                        LanguageId = currentLanguageId,
                        MovieId = currentMovieId
                    });
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        private async Task AddNewGenres(JToken jsonObject)
        {
            List<string> genreNamesFromDb = await _unitOfWork.GenreRepository.GetAllName();

            foreach (var genre in jsonObject["genres"])
            {
                string currentGenreName = Convert.ToString(genre["name"]);

                if (!genreNamesFromDb.Contains(currentGenreName))
                {
                    await _unitOfWork.GenreRepository.AddAsync(new Genre() { Name = currentGenreName });
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        private async Task AddNewMovieGenre(JToken jsonObject)
        {
            foreach (var genre in jsonObject["genres"])
            {
                int currentMovieId = await _unitOfWork.MovieRepository.GetIdByOriginalId(Convert.ToInt32(jsonObject["id"]));
                int currentGenreId = await _unitOfWork.GenreRepository.GetIdByName(Convert.ToString(genre["name"]));

                if (!await _unitOfWork.MovieGenreRepository.DoesPairExist(currentMovieId, currentGenreId))
                {
                    await _unitOfWork.MovieGenreRepository.AddAsync(new MovieGenre() 
                    {
                        GenreId = currentGenreId,
                        MovieId = currentMovieId
                    });
                    await _unitOfWork.SaveAsync();
                }
               
            }
        }

        private async Task AddNewMovie(JToken jsonObject)
        {
            await _unitOfWork.MovieRepository.AddAsync(new Movie()
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
            });
            await _unitOfWork.SaveAsync();
        }

        private async Task DeleteFromDb(List<int> movieIds)
        {
            List<int> movieIdsFromDb = await _unitOfWork.MovieRepository.GetAllOriginalId();

            foreach (int movieIdFromDb in movieIdsFromDb)
            {
                if (!movieIds.Any(mId => mId == movieIdFromDb))
                {
                    await _unitOfWork.MovieRepository.Remove(await _unitOfWork.MovieRepository.GetMovieByOriginalId(movieIdFromDb));
                    await _unitOfWork.SaveAsync();
                }
            }
        }
    }
}