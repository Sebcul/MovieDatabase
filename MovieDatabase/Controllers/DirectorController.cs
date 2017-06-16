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
                DirectorId = director.Id,
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

        [HttpGet]
        public IActionResult EditDirector(int id, int? errorId)
        {
            var director = _directorRepository.GetDirectorById(id);

            var model = new DirectorViewModel()
            {
                DirectorId = id,
                DateOfBirth = director.DateOfBirth,
                Name = director.Name
            };
            if (errorId == 1)
            {
                ViewBag.ErrorMessage = "Can't remove a director that is in an existing movie.";
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditDirector(DirectorViewModel viewModel)
        {
            var director = _directorRepository.GetDirectorById((int)viewModel.DirectorId);

            director.Name = viewModel.Name;
            director.DateOfBirth = viewModel.DateOfBirth;

            _directorRepository.UpdateDirector(director);
            _directorRepository.SaveData();

            return RedirectToAction("DirectorDetails", new { id = viewModel.DirectorId });
        }

        [HttpGet]
        public IActionResult RemoveDirector(int id)
        {
            if (!_directorRepository.IsDirectorInAnyMovie(id))
            {
                _directorRepository.RemoveDirector(id);
                _directorRepository.SaveData();
                return RedirectToAction("ListAll");
            }
            else
            {
                return RedirectToAction("EditDirector", new { id = id, errorId = 1 });
            }
        }
    }
}
