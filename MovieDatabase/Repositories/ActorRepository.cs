using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public class ActorRepository : IActorRepository
    {
        private readonly MovieDbContext _dbContext;

        public ActorRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Actor> GetAllActors()
        {
            return _dbContext.Actors;
        }

        public void AddActorToMovie(Actor actor, Movie movie)
        {
            if (!ActorExists(actor.Name, actor.DateOfBirth))
            {
                _dbContext.Actors.Add(actor);
            }
            else if (IsActorInMovie(actor, movie))
            {
                return;
            }
            else
            {
                actor = _dbContext.Actors.FirstOrDefault(a => a.Name.ToLower() == actor.Name.ToLower()
                                                              && a.DateOfBirth == actor.DateOfBirth);
            }

            _dbContext.MovieActors.Add(new MovieActors
            {
                Actor = actor,
                Movie = movie
            });
        }

        private bool ActorExists(string name, DateTime dateOfBirth)
        {
            var actorInDb =
                _dbContext.Actors.FirstOrDefault(a => a.Name.ToLower() == name.ToLower()
                                                      && a.DateOfBirth == dateOfBirth);

            return actorInDb != null;
        }

        private bool IsActorInMovie(Actor actor, Movie movie)
        {
            foreach (var movieActors in movie.MovieActors)
            {
                if (movieActors.Actor.Name.ToLower() == actor.Name.ToLower()
                    && movieActors.Actor.DateOfBirth == actor.DateOfBirth)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
