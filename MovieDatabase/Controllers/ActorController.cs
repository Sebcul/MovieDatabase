using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Repositories;
using MovieDatabase.ViewModels;

namespace MovieDatabase.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepository _actorRepository;

        public ActorController(IActorRepository actorRepository, IMovieRepository movieRepository)
        {
            _actorRepository = actorRepository;
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            var model = _actorRepository.GetAllActors();
            return View(model);
        }

        [HttpGet]
        public IActionResult ActorDetails(int id)
        {
            var actor = _actorRepository.GetActorById(id);
            var movies = _actorRepository.GetMoviesForActor(id);

            var model = new ActorViewModel
            {
                ActorId = actor.Id,
                Name = actor.Name,
                DateOfBirth = actor.DateOfBirth,
                Movies = movies
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult ActorSearch()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchActor(string s)
        {
            var model = _actorRepository.GetAllActors().Where(a => a.Name.ToLower().Contains(s.ToLower()));
            return View("ActorSearch", model);
        }

        [HttpGet]
        public IActionResult EditActor(int id, int? errorId)
        {
            var actor = _actorRepository.GetActorById(id);
            var model = new ActorViewModel
            {
                ActorId = id,
                DateOfBirth = actor.DateOfBirth,
                Name = actor.Name
            };
            if (errorId == 1)
            {
                ViewBag.ErrorMessage = "Can't remove an actor that is in an existing movie.";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditActor(ActorViewModel viewModel)
        {
            var actor = _actorRepository.GetActorById((int)viewModel.ActorId);

            actor.Name = viewModel.Name;
            actor.DateOfBirth = viewModel.DateOfBirth;

            _actorRepository.UpdateActor(actor);
            _actorRepository.SaveData();

            return RedirectToAction("ActorDetails", new {id = viewModel.ActorId});
        }

        [HttpGet]
        public IActionResult RemoveActor(int id)
        {
            if (!_actorRepository.IsActorInAnyMovie(id))
            {
                _actorRepository.RemoveActor(id);
                _actorRepository.SaveData();
                return RedirectToAction("ListAll");
            }
            else
            {
                return RedirectToAction("EditActor", new { id = id, errorId = 1 });
            }
        }
    }
}
