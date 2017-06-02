using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Models;
using MovieDatabase.Repositories;
using MovieDatabase.ViewModels;

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
            if (String.IsNullOrEmpty(searchText))
            {
                return RedirectToAction("Index", "Home");
            }
            var movies = _movieRepository.GetAllMovies().Where(m => m.Title.ToLower().Contains(searchText.ToLower()));
            return View("SearchResult", movies);
        }

        [HttpGet]
        public IActionResult MovieDetails(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            var actors = new List<Actor>();
            foreach (var movieActor in movie.MovieActors)
            {
                    actors.Add(movieActor.Actor);
            }
            var model = new MovieViewModel
            {
                Director = movie.Director, ProductionYear = movie.ProductionYear,
                Title = movie.Title, Actors = actors, Ratings = movie.Ratings.ToList()
        };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMovie()
        { 
            return View();
        }

        //TODO: Check if director exists when adding movie. If he/she does, use that one from database. Add directorrepository
        [HttpPost]
        public IActionResult AddMovie(AddMovieViewModel model)
        {
            if (ModelState.IsValid)
            {

                var movie = new Movie
                {
                    Director = new Director { DateOfBirth = model.DirectorDoB, Name = model.DirectorName },
                    Title = model.Title,
                    ProductionYear = model.ProductionYear
                };
                _movieRepository.AddMovie(movie);
                _movieRepository.SaveData();
                return RedirectToAction("MovieDetails", new {id = movie.Id});
            }

            return View();
        }
    }
}
