using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string Review { get; set; }
        public int Score { get; set; }
        public DateTime ReviewDate { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
