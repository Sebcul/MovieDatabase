using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Attributes;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{

    public class MovieViewModel
    {
        public int? MovieId { get; set; }
        public string Title { get; set; }
        public Director Director { get; set; }
        public List<Actor> Actors{ get; set; }
        public int ProductionYear { get; set; }
        public List<Rating> Ratings { get; set; }
        public double AverageScore { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
