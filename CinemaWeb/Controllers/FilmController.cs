using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Domain.ViewModels;
using CinemaWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        public async Task<IActionResult> GetFilm(int id)
        {
            var response = await _FilmService.GetFilm(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
        [Authorize(Roles = "Admin")] // только для админа
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _FilmService.DeleteFilm(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetFilms");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")] // только для админа
        public async Task<IActionResult> Save(int id) 
        {
            if(id == 0)
            {
                return View();
            }

            var response = await _FilmService.GetFilm(id);
            if(response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(FilmViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Id == 0)
                {
                    await _FilmService.CreateFilm(model);
                }
                else
                {
                    await _FilmService.Edit(model.Id, model);
                }
            }
            return RedirectToAction("GetFilms");
        }

    }
}
