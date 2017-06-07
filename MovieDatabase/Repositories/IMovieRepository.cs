using System.Collections.Generic;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        void AddMovie(Movie movie);
        void AddRating(Rating rating, int movieId);
        void DeleteMovie(int id);
        Movie GetMovieById(int id);
        void SaveData();
    }
}
