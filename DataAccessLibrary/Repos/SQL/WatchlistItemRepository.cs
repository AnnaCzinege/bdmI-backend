using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class WatchlistItemRepository : GenericRepository<WatchlistItem>, IWatchlistItemRepository
    {
        public WatchlistItemRepository(MovieContext context) : base(context) { }

        public async Task<List<int>> GetWatchListByUser(string id)
        {
            return await _context.WatchlistItems.Where(wl => wl.UserId == id)
                                             .Select(wl => wl.MovieId)
                                             .ToListAsync();
        }

        public async Task AddWatchListItem(string userId, int movieId)
        {
   
            await _context.WatchlistItems.AddAsync(new WatchlistItem
            {
                UserId = userId,
                MovieId = movieId
            });
            await _context.SaveChangesAsync();
        }

        public async Task DeleteWatchListItem(string userId, int movieId)
        {
            WatchlistItem itemToDelete = (WatchlistItem)_context.WatchlistItems.Where(wl => wl.MovieId == movieId && wl.UserId == userId);

            _context.WatchlistItems.Remove(itemToDelete);

            await _context.SaveChangesAsync();

        }


    }
}
