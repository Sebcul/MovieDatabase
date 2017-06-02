using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieDatabase.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ProductionYear { get; set; }

        public Director Director { get; set; }
        public int DirectorId { get; set; }

        public ICollection<Rating> Ratings { get; set; }
        public ICollection<MovieActors> MovieActors { get; set; }
    }
}