using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Models;

namespace MovieDatabase.ViewModels
{
    public class MovieViewModel
    {
        public string Title { get; set; }
        public Director Director { get; set; }
        public IEnumerable<Actor> Actors{ get; set; }
        public int ProductionYear { get; set; }
    }
}
