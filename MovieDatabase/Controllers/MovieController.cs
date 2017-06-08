﻿using System;
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
    //TODO: Check if AverageScore is null in moviedetails view. Add toplist view
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


        public IActionResult SearchMovie(string s)
        {
            if (String.IsNullOrEmpty(s))
            {
                return RedirectToAction("Index", "Home");
            }
            var movies = _movieRepository.GetAllMovies().Where(m => m.Title.ToLower().Contains(s.ToLower()));
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
                Ratings = movie.Ratings.ToList(),
                AverageScore = movie.AverageScore,
                Genres = movie.Genres
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult AddMovie()
        {
            var model = new AddMovieViewModel();
            model.DirectorDoB = null;
            model.ProductionYear = null;
            return View(model);
        }

        [HttpPost]
        public IActionResult AddMovie(AddMovieViewModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Director = new Director {DateOfBirth = Convert.ToDateTime(model.DirectorDoB), Name = model.DirectorName},
                    Title = model.Title,
                    ProductionYear = Convert.ToInt32(model.ProductionYear)
                };
                var genre = _movieRepository.CheckDbIfGenreExists(new Genre { GenreName = model.SelectedGenre });
                movie.Genres.Add(genre);
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

        [HttpGet]
        public IActionResult DeleteMovie()
        {
            var model = new DeleteMovieViewModel();

            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteMovie(DeleteMovieViewModel viewModel)
        {
            _movieRepository.DeleteMovie(viewModel.SelectedMovieId);
            _movieRepository.SaveData();

            var model = new DeleteMovieViewModel();
            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            return RedirectToAction("DeleteMovie", model);
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            var model = _movieRepository.GetAllMovies();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddRating()
        {
            var model = new AddRatingViewModel();

            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult AddRating(AddRatingViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var rating = new Rating
                {
                    Review = viewModel.ReviewText,
                    ReviewDate = DateTime.Now,
                    ReviewerName = viewModel.ReviewerName,
                    Score = viewModel.Score
                };
                _movieRepository.AddRating(rating, viewModel.SelectedMovieId);
                _movieRepository.SaveData();
            }
            return RedirectToAction("MovieDetails", new {id = viewModel.SelectedMovieId});
        }

        [HttpGet]
        public IActionResult TopList()
        {
            var model = _movieRepository.GetTopListMovies();
            return View(model);
        }
    }
}