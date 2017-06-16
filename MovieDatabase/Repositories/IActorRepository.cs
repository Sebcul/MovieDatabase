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
        void UpdateActor(Actor actor);
        void RemoveActor(int id);
        bool IsActorInAnyMovie(int id);
        void SaveData();
        Actor GetActorById(int id);
    }
}
