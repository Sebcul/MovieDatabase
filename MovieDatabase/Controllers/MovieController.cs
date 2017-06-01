using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Repositories;

namespace MovieDatabase.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;

        public MovieController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SearchMovie(string searchText)
        {
            var movies = _movieRepository.GetAllMovies().Where(m => m.Title.Contains(searchText));
            return View("SearchResult", movies);
        }
    }
}
