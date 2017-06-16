using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Attributes;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class EditMovieViewModel
    {
        public int? MovieId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Title must be 2-100 characters.")]
        public string Title { get; set; }

        [RangeUntilCurrentYear(1850, ErrorMessage = "Please enter a valid year.")]
        [Required]
        public int ProductionYear { get; set; }

        public List<Actor> Actors { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}
