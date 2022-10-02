﻿using CinemaWeb.Domain.ViewModels;
using CinemaWeb.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> GetFilms()
        {
            var response = _FilmService.GetFilms();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [HttpGet]
        public async Task<IActionResult> GetFilm(int id, bool isJson)
        {
            var response = await _FilmService.GetFilm(id);
            if (isJson)
            {
                return Json(response.Data);
            }
            return PartialView("GetFilm", response.Data);
        }

        [Authorize(Roles = "Admin")] // только для админа
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _FilmService.DeleteFilm(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetFilms");
            }
            return View("Error", $"{response.Description}");
        }

        public IActionResult Compare() => PartialView();

        [HttpGet]
        [Authorize(Roles = "Admin")] // только для админа
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return PartialView();

            var response = await _FilmService.GetFilm(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpGet]
        public async Task<IActionResult> Save(FilmViewModel model)
        {
            ModelState.Remove("DateCreate");
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Image.Length);
                    }
                    await _FilmService.Create(model, imageData);
                }
                else
                {
                    await _FilmService.Edit(model.Id, model);
                }
                return RedirectToAction("GetCars");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetCar(string term, int page = 1, int pageSize = 5)
        {
            var response = await _FilmService.GetFilm(term);
            return Json(response.Data);
        }

        [HttpPost]
        public JsonResult GetTypes()
        {
            var types = _FilmService.GetTypes();
            return Json(types.Data);
        }
    }

}

