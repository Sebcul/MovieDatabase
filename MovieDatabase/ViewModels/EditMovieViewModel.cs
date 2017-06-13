using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class EditMovieViewModel
    {
        public int? MovieId { get; set; }
        public string Title { get; set; }
        public int ProductionYear { get; set; }
        public List<Actor> Actors { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}
