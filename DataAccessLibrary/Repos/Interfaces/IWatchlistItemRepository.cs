using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IWatchlistItemRepository : IGenericRepository<WatchlistItem>
    {
        Task<List<Movie>> GetWatchListOfUser(string id);

        Task AddWatchListItem(string userId, int movieId);

        Task DeleteWatchListItem(string userId, int movieId);
    }
}
