using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class DirectorViewModel
    {
        public int? DirectorId { get; set; }
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Movie> DirectedMovies { get; set; }
    }
}
