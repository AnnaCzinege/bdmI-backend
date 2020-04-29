using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary.Repos.SQL
{
    public class WatchlistItemRepository : GenericRepository<WatchlistItem>, IWatchlistItemRepository
    {
        public WatchlistItemRepository(MovieContext context) : base(context) { }

        public async Task<List<Movie>> GetWatchListOfUser(string id)
        {
            var movieIds = await _context.WatchlistItems.Where(wl => wl.UserId == id).Select(wl => wl.MovieId).ToListAsync();
            return await _context.Movies.Where(m => movieIds.Contains(m.Id)).ToListAsync();
        }

        public async Task AddWatchListItem(string userId, int movieId)
        {
            var watchlist = _context.WatchlistItems.Where(wl => wl.MovieId == movieId && wl.UserId == userId);

            if (watchlist.Count() == 0)
            {
                await _context.WatchlistItems.AddAsync(new WatchlistItem
                {
                    UserId = userId,
                    MovieId = movieId
                });
                await _context.SaveChangesAsync();  
            }
        }

        public async Task DeleteWatchListItem(string userId, int movieId)
        {
            WatchlistItem itemToDelete = _context.WatchlistItems.Single(wl => wl.MovieId == movieId && wl.UserId == userId);

            _context.WatchlistItems.Remove(itemToDelete);

            await _context.SaveChangesAsync();
        }
    }
}
