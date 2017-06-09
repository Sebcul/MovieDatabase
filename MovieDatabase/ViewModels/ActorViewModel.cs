using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class ActorViewModel
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IEnumerable<Movie> Movies { get; set; }
    }
}
