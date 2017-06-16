using System.Collections.Generic;
using MovieDatabase.Models;

namespace MovieDatabase.Repositories
{
    public interface IDirectorRepository
    {
        IEnumerable<Director> GetAllDirectors();
        IEnumerable<Movie> GetDirectedMovies(int id);
        void UpdateDirector(Director director);
        void RemoveDirector(int id);
        bool IsDirectorInAnyMovie(int id);
        void SaveData();
        Director GetDirectorById(int id);
    }
}
