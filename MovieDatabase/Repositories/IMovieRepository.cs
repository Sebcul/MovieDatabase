using System.Collections.Generic;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        void AddMovie(Movie movie);
        void AddRating(Rating rating, int movieId);
        void AddGenre(Genre genre, int movieId);
        Genre CheckDbIfGenreExists(Genre genre);
        void DeleteMovie(int id);
        Movie GetMovieById(int id);
        IEnumerable<Movie> GetTopListMovies();
        void SaveData();
    }
}
