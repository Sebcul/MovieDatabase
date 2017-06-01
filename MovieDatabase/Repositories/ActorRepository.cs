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

        public void AddActor(Actor actor)
        {
            _dbContext.Actors.Add(actor);
        }
    }
}
