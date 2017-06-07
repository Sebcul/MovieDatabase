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
        void AddActorToMovie(Actor actor, Movie movie);
        IEnumerable<Movie> GetMoviesForActor(int id);
        Actor GetActorById(int id);
    }
}
