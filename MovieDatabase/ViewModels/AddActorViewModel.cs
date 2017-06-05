using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDatabase.Attributes;

namespace MovieDatabase.ViewModels
{
    public class AddActorViewModel
    {
        public AddActorViewModel()
        {
            Movies = new List<SelectListItem>();
        }
        public List<SelectListItem> Movies { get; set; }

        [Required]
        public int SelectedMovieId { get; set; }

        [Required]
        [Display(Name = "Actor's name")]
        public string ActorName { get; set; }

        [Required]
        [Display(Name = "Actor's date of birth")]
        public DateTime? ActorDateOfBirth { get; set; }
    }
}
