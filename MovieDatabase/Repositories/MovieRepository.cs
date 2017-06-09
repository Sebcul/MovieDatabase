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
                .Include(r => r.Ratings).Include(g => g.Genres)
            .Include(ma => ma.MovieActors)
            .ThenInclude(a => a.Actor);
        }

        public Movie GetMovieById(int id)
        {
            return _dbContext.Movies.Include(d => d.Director)
                .Include(r => r.Ratings).Include(g => g.Genres)
                .Include(ma => ma.MovieActors)
                .ThenInclude(a => a.Actor).First(m => m.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            movie.Director = CheckDbIfDirectorExists(movie.Director);
            _dbContext.Movies.Add(movie);
        }


        public void AddRating(Rating rating, int movieId)
        {
            var movie = GetMovieById(movieId);
            movie.Ratings.Add(rating);
        }

        public void AddGenre(Genre genre, int movieId)
        {
            genre = CheckDbIfGenreExists(genre);

            var movie = GetMovieById(movieId);
            if (IsGenreOnMovie(movie, genre.GenreName))
            {
                return;
            }

            movie.Genres.Add(genre);
        }

        public void DeleteMovie(int id)
        {
            _dbContext.Movies.Remove(GetMovieById(id));
        }

        public IEnumerable<Movie> GetTopListMovies()
        {
            var movies = _dbContext.Movies.Include(r => r.Ratings).Where(m => m.AverageScore > 0).OrderByDescending(m => m.AverageScore).ToList();
            return movies;
        }

        private Director CheckDbIfDirectorExists(Director director)
        {
            var directorInDb =
                _dbContext.Directors.FirstOrDefault(d => d.Name.ToLower() == director.Name.ToLower() 
                && d.DateOfBirth == director.DateOfBirth);

            return directorInDb ?? director;
        }

        public Genre CheckDbIfGenreExists(Genre genre)
        {
            var genreInDb = _dbContext.Genres.FirstOrDefault(g => g.GenreName == genre.GenreName);

            return genreInDb ?? genre;
        }

        private bool IsGenreOnMovie(Movie movie, string genreName)
        {
            foreach (var genre in movie.Genres)
            {
                if (genre.GenreName.ToLower() == genreName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public void SaveData()
        {
            _dbContext.SaveChanges();
        }
    }
}
