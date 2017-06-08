using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDatabase.Attributes;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class AddMovieViewModel
    {
        private readonly List<string> _genres;

        public AddMovieViewModel()
        {
            Genres = new List<SelectListItem>();
            _genres = new List<string>();
            InitializeGenres();
        }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Director's name")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Director's date of birth")]
        public DateTime? DirectorDoB { get; set; }


        [Required]
        [RangeUntilCurrentYear(1850, ErrorMessage = "Please enter a valid year.")]
        [Display(Name = "Production year")]
        public int? ProductionYear { get; set; }

        public List<SelectListItem> Genres { get; set; }

        [Required]
        public string SelectedGenre { get; set; }

        private void InitializeGenres()
        {
            _genres.AddRange(new List<string>
                {
                    "Sci-Fi",
                    "Thriller",
                    "Horror",
                    "Fantasy",
                    "Drama",
                    "Adventure",
                    "Romance",
                    "Action",
                    "Documentary",
                    "Crime",
                    "Mystery"
                });
            foreach (var genre in _genres)
            {
                Genres.Add(new SelectListItem() { Text = genre, Value = genre });
            }
        }
    }


}
