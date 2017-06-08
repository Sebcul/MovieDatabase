using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Models
{
    public class Genre
    {
        public int Id { get; set; }
        public string GenreName { get; set; }

        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
