using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbBackend.DTOs
{
    public class WatchlistDTO
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
    }
}
