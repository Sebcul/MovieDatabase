using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieDatabase.Models;
using MovieDatabase.Repositories;
using MovieDatabase.ViewModels;

namespace MovieDatabase.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IActorRepository _actorRepository;

        public MovieController(IMovieRepository movieRepository,
            IActorRepository actorRepository)
        {
            _movieRepository = movieRepository;
            _actorRepository = actorRepository;
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
                Director = movie.Director,
                ProductionYear = movie.ProductionYear,
                Title = movie.Title,
                Actors = actors,
                Ratings = movie.Ratings.ToList()
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Director = new Director {DateOfBirth = model.DirectorDoB, Name = model.DirectorName},
                    Title = model.Title,
                    ProductionYear = model.ProductionYear
                };
                _movieRepository.AddMovie(movie);
                _movieRepository.SaveData();
                return RedirectToAction("MovieDetails", new {id = movie.Id});
            }
            return View();
        }

        [HttpGet]
        public IActionResult AddActors()
        {
            var model = new AddActorViewModel();
            model.ActorDateOfBirth = null;
            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddActors(AddActorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var dbMovie = _movieRepository.GetMovieById(model.SelectedMovieId);
                var actor = new Actor
                {
                    Name = model.ActorName,
                    DateOfBirth = Convert.ToDateTime(model.ActorDateOfBirth)
                };

                _actorRepository.AddActorToMovie(actor, dbMovie);
                _movieRepository.SaveData();

                foreach (var movie in _movieRepository.GetAllMovies())
                {
                    model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
                }
                return RedirectToAction("AddActors");
            }
            return RedirectToAction("AddActors");
        }
    }
}
