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

        public void RemoveDirector(int id)
        {
            if (!IsDirectorInAnyMovie(id))
            {
                var director = _dbContext.Directors.FirstOrDefault(d => d.Id == id);
                _dbContext.Directors.Remove(director);
            }
        }

        public Director GetDirectorById(int id)
        {
            return _dbContext.Directors.First(a => a.Id == id);
        }

        public void SaveData()
        {
            _dbContext.SaveChanges();
        }

        public bool IsDirectorInAnyMovie(int id)
        {
            return _dbContext.Movies.Any(d => d.DirectorId == id);
        }
    }
}
