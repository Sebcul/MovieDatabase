using System.Collections.Generic;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAllMovies();
        void AddMovie(Movie movie);
    }
}
