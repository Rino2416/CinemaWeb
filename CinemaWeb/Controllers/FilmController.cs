using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace CinemaWeb.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _FilmService;

        public FilmController(IFilmService filmService)
        {
            _FilmService = filmService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilmsAsync()
        {
          var resopnse = await _FilmService.GetFilms();
            if (resopnse.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(resopnse.Data);
            }
            return RedirectToAction("Error");
        }
    }
}
