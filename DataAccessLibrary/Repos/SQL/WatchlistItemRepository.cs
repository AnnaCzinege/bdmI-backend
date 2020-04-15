using DataAccessLibrary.DataAccess;
using DataAccessLibrary.Models;
using DataAccessLibrary.Repos.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Repos.SQL
{
    public class WatchlistItemRepository : GenericRepository<WatchlistItem>, IWatchlistItemRepository
    {
        public WatchlistItemRepository(MovieContext context) : base(context) { }
    }
}
