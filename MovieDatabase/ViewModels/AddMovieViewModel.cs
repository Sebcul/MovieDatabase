using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Attributes;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class AddMovieViewModel
    {
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Director's name")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Director's date of birth")]
        public DateTime DirectorDoB { get; set; }


        [Required]
        [RangeUntilCurrentYear(1850, ErrorMessage = "Please enter a valid year.")]
        [Display(Name = "Production year")]
        public int ProductionYear { get; set; }

    }
}
