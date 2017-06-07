using System.Collections.Generic;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IDirectorRepository
    {
        IEnumerable<Director> GetAllDirectors();
        IEnumerable<Movie> GetDirectedMovies(int id);
        Director GetDirectorById(int id);
    }
}
