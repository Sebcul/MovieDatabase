using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.Repositories;
using MovieDatabase.ViewModels;

namespace MovieDatabase.Controllers
{
    public class DirectorController : Controller
    {
        private readonly IDirectorRepository _directorRepository;

        public DirectorController(IDirectorRepository directorRepository)
        {
            _directorRepository = directorRepository;
        }

        [HttpGet]
        public IActionResult ListAll()
        {
            var model = _directorRepository.GetAllDirectors();
            return View(model);
        }

        [HttpGet]
        public IActionResult DirectorDetails(int id)
        {
            var director = _directorRepository.GetDirectorById(id);
            var movies = _directorRepository.GetDirectedMovies(id);

            var model = new DirectorViewModel()
            {
                Name = director.Name,
                DateOfBirth = director.DateOfBirth,
                DirectedMovies = movies
            };
            return View(model);
        }

        [HttpGet]
        public IActionResult DirectorSearch()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SearchDirector(string s)
        {
            var model = _directorRepository.GetAllDirectors().Where(a => a.Name.ToLower().Contains(s.ToLower()));
            return View("DirectorSearch", model);
        }
    }
}
