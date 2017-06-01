using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Repositories;

namespace MovieDatabase.Controllers
{
    public class ActorController : Controller
    {
        private readonly IActorRepository _actorRepository;

        public ActorController(IActorRepository actorRepository)
        {
            _actorRepository = actorRepository;
        }
    }
}
