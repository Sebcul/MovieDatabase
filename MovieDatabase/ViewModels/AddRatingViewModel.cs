using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDatabase.ViewModels
{
    public class AddRatingViewModel
    {
        private readonly List<int> _scores = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public AddRatingViewModel()
        {
            Movies = new List<SelectListItem>();
            ScoreList = new List<SelectListItem>();

            foreach (var score in _scores)
            {
                ScoreList.Add(new SelectListItem() { Text = score.ToString(), Value = score.ToString() });
            }
        }

        [Display(Name = "Movie")]
        public List<SelectListItem> Movies { get; set; }

        [Required]
        public int SelectedMovieId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ReviewerName { get; set; }

        public List<SelectListItem> ScoreList { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public string ReviewText { get; set; }
    }
}
