﻿using System;
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
        private readonly IMovieRepository _movieRepository;

        public ActorController(IActorRepository actorRepository, IMovieRepository movieRepository)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
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
    }
}
