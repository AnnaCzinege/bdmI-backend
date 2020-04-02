using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImdbBackend.FetchModels
{
    public class AllMovies
    {
        public int Id { get; set; }
        public int OriginalId { get; set; }
        public string OriginalTitle { get; set; }
    }
}
