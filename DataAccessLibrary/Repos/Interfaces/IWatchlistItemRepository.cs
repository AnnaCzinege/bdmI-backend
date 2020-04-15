using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.Interfaces
{
    public interface IWatchlistItemRepository : IGenericRepository<WatchlistItem>
    {
        Task<List<int>> GetWatchListByUser(string id);

        Task AddWatchListItem(string userId, int movieId);

        Task DeleteWatchListItem(string userId, int movieId);
    }
}
