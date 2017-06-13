using System.Collections.Generic;
using System.Linq;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly MovieDbContext _dbContext;

        public DirectorRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Director> GetAllDirectors()
        {
            return _dbContext.Directors;
        }
        public IEnumerable<Movie> GetDirectedMovies(int id)
        {
            return _dbContext.Movies.Where(d => d.DirectorId == id);
        }

        public void UpdateDirector(Director director)
        {
            _dbContext.Directors.Update(director);
        }

        public Director GetDirectorById(int id)
        {
            return _dbContext.Directors.First(a => a.Id == id);
        }

        public void SaveData()
        {
            _dbContext.SaveChanges();
        }


    }
}
