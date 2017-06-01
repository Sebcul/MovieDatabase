using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IActorRepository
    {
        IEnumerable<Actor> GetAllActors();
        void AddActor(Actor actor);
    }
}
