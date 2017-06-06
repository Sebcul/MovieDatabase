using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MovieDbContext _dbContext;

        public MovieRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _dbContext.Movies.Include(d => d.Director)
                .Include(r => r.Ratings)
            .Include(ma => ma.MovieActors)
            .ThenInclude(a => a.Actor);
        }

        public Movie GetMovieById(int id)
        {
            return _dbContext.Movies.Include(d => d.Director)
                .Include(r => r.Ratings)
                .Include(ma => ma.MovieActors)
                .ThenInclude(a => a.Actor).First(m => m.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            movie.Director = DirectorExists(movie.Director);
            _dbContext.Movies.Add(movie);
        }

        public void DeleteMovie(int id)
        {
            _dbContext.Movies.Remove(GetMovieById(id));
        }

        private Director DirectorExists(Director director)
        {
            var directorInDb =
                _dbContext.Directors.First(d => d.Name.ToLower() == director.Name.ToLower() 
                && d.DateOfBirth == director.DateOfBirth);

            return directorInDb ?? director;
        }

        public void SaveData()
        {
            _dbContext.SaveChanges();
        }
    }
}
