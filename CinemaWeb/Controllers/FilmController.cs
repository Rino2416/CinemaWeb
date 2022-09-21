using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CinemaWeb.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmRepository _filmRepository;

        public FilmController(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }
        [HttpGet]
        public IActionResult GetFilms()
        {
            var response = _filmRepository.Select();
            var respone1 = _filmRepository.GetByName("Шпачело");
            var respone2 = _filmRepository.Get(1);

            var film = new Film()
            {
            
                Name = "Антон",
                Description = "DFFDDF",
                Price = 25,
                ReleaseDate = DateTime.Now,
                //GenreFilm = (GenreFilm)3,
            };

            var respone3 = _filmRepository.Create(film);
            var respone4 = _filmRepository.Delete(film);
            return View(response);
        }
    }
}
