using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MovieDatabase.ViewModels
{
    public class DeleteMovieViewModel
    {
        public DeleteMovieViewModel()
        {
            Movies = new List<SelectListItem>();
        }

        public List<SelectListItem> Movies { get; set; }

        [Required]
        public int SelectedMovieId { get; set; }
    }
}
