using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDatabase.ViewModels
{
    public class AddGenreViewModel
    {
        private readonly List<string> _genres;

        public AddGenreViewModel()
        {
            _genres = new List<string>();
            Genres = new List<SelectListItem>();
            Movies = new List<SelectListItem>();
            InitializeGenres();
        }

        public List<SelectListItem> Genres { get; set; }

        [Required]
        public string SelectedGenre { get; set; }

        [Display(Name = "Movie")]
        public List<SelectListItem> Movies { get; set; }

        [Required]
        public int SelectedMovieId { get; set; }

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
