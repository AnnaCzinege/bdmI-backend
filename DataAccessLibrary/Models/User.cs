using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLibrary.Models
{
    public partial class User : IdentityUser
    {
        public List<WatchlistItem> Watchlist { get; set; }
    }
}
