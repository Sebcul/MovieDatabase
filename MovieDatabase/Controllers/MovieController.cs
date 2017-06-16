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
                Genres = movie.Genres,
                MovieId = movie.Id
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
        public IActionResult AddMovie(AddMovieViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var movie = new Movie
                {
                    Director = new Director
                    {
                        DateOfBirth = Convert.ToDateTime(viewModel.DirectorDoB),
                        Name = viewModel.DirectorName
                    },
                    Title = viewModel.Title,
                    ProductionYear = Convert.ToInt32(viewModel.ProductionYear)
                };
                var genre = _movieRepository.CheckDbIfGenreExists(new Genre {GenreName = viewModel.SelectedGenre});
                movie.Genres.Add(genre);
                _movieRepository.AddMovie(movie);
                _movieRepository.SaveData();
                return RedirectToAction("MovieDetails", new {id = movie.Id});
            }
            var model = new AddMovieViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult AddActors(int? id)
        {
            var model = new AddActorViewModel();
            model.ActorDateOfBirth = null;
            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            if (id != null)
            {
                model.SelectedMovieId = (int)id;
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
            if (viewModel.SelectedMovieId != 0)
            {
                _movieRepository.DeleteMovie(viewModel.SelectedMovieId);
                _movieRepository.SaveData();
            }


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
        public IActionResult AddRating(int? id)
        {
            var model = new AddRatingViewModel();

            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            if (id != null)
            {
                model.SelectedMovieId = (int) id;
            }
            return View(model);
        }

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

        [HttpGet]
        public IActionResult AddGenre(int? id)
        {
            var model = new AddGenreViewModel();

            foreach (var movie in _movieRepository.GetAllMovies())
            {
                model.Movies.Add(new SelectListItem() {Text = movie.Title, Value = movie.Id.ToString()});
            }
            if (id != null)
            {
                model.SelectedMovieId = (int)id;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult AddGenre(AddGenreViewModel viewModel)
        {
            _movieRepository.AddGenre(new Genre {GenreName = viewModel.SelectedGenre}, viewModel.SelectedMovieId);
            _movieRepository.SaveData();
            return RedirectToAction("MovieDetails", new {id = viewModel.SelectedMovieId});
        }

        [HttpGet]
        public IActionResult EditMovie(int id)
        {
            var movie = _movieRepository.GetMovieById(id);
            var actors = new List<Actor>();
            foreach (var movieActor in movie.MovieActors)
            {
                actors.Add(movieActor.Actor);
            }
            var model = new EditMovieViewModel
            {
                Actors = actors,
                Ratings = movie.Ratings,
                MovieId = movie.Id,
                ProductionYear = movie.ProductionYear,
                Title = movie.Title,
                Genres = movie.Genres
            };

            return View(model);
        }

        [HttpPost]
        public void EditTitleAndProductionYear(string movieTitle, int movieProductionYear, int movieId)
        {
            var movie = _movieRepository.GetMovieById(movieId);
            movie.Title = movieTitle;
            movie.ProductionYear = movieProductionYear;

            _movieRepository.UpdateMovie(movie);
            _movieRepository.SaveData();
        }

        [HttpPost]
        public void RemoveActor(int movieId, int actorId)
        {
            var movie = _movieRepository.GetMovieById(movieId);
            var actor = movie.MovieActors.FirstOrDefault(ma => ma.ActorId == actorId && ma.MovieId == movieId);
            if (actor != null)
            {
                movie.MovieActors.Remove(actor);
                _movieRepository.UpdateMovie(movie);
                _movieRepository.SaveData();
            }
        }

        [HttpPost]
        public void RemoveRating(int movieId, int ratingId)
        {
            _movieRepository.RemoveRatingFromMovie(ratingId, movieId);
            _movieRepository.SaveData();
        }

        [HttpPost]
        public void RemoveGenre(int movieId, int genreId)
        {
            _movieRepository.RemoveGenreFromMovie(genreId, movieId);
            _movieRepository.SaveData();
        }
    }
}