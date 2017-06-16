using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<Movie> GetMoviesForActor(int id)
        {
            var movies = (from a in _dbContext.Actors
                join ma in _dbContext.MovieActors on a.Id equals ma.ActorId
                          where ma.ActorId == id
                join m in _dbContext.Movies on ma.MovieId equals  m.Id
                select m);
            return movies;
        }

        public void UpdateActor(Actor actor)
        {
            _dbContext.Actors.Update(actor);
        }

        public void RemoveActor(int id)
        {
            if (!IsActorInAnyMovie(id))
            {
                var actor = _dbContext.Actors.FirstOrDefault(d => d.Id == id);
                _dbContext.Actors.Remove(actor);
            }
        }

        public Actor GetActorById(int id)
        {
            return _dbContext.Actors.First(a => a.Id == id);
        }

        private bool ActorExists(string name, DateTime dateOfBirth)
        {
            var actorInDb =
                _dbContext.Actors.FirstOrDefault(a => a.Name.ToLower() == name.ToLower()
                                                      && a.DateOfBirth == dateOfBirth);

            return actorInDb != null;
        }

        public void SaveData()
        {
            _dbContext.SaveChanges();
        }

        public bool IsActorInAnyMovie(int id)
        {
            var movies = _dbContext.Movies.Include(ma => ma.MovieActors).ToList();

            foreach (var movie in movies)
            {
                if (movie.MovieActors.Any(a => a.ActorId == id))
                {
                    return true;
                }
            }
            return false;
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
