using System.Collections.Generic;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        Movie GetMovieById(int id);
        IEnumerable<Movie> GetTopListMovies();
        Genre CheckDbIfGenreExists(Genre genre);
        void AddMovie(Movie movie);
        void AddRating(Rating rating, int movieId);
        void AddGenre(Genre genre, int movieId);
        void DeleteMovie(int id);
        void UpdateMovie(Movie movie);
        void RemoveRatingFromMovie(int ratingId, int movieId);
        void RemoveGenreFromMovie(int genreId, int movieId);
        void SaveData();
    }
}
