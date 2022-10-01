using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Domain.Extensions;
using CinemaWeb.Domain.Response;
using CinemaWeb.Domain.ViewModels;
using CinemaWeb.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Service.Implementations
{
    public class FilmService : IFilmService
    {

        private readonly IBaseRepository<Film> _filmRepository;

        public FilmService(IBaseRepository<Film> filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<IBaseResponse<Film>> Create(FilmViewModel model, byte[] imageData)
        {
            try
            {
                var film = new Film()
                {
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    GenreFilm = (GenreFilm)Convert.ToInt32(model.GenreFilm),
                    ReleaseDate = DateTime.Now,
                    Avatar = imageData
                };
                await _filmRepository.Create(film);
                return new BaseResponse<Film>()
                {
                    StatusCode = StatusCode.OK,
                    Data = film
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[Create] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteFilm(int id)
        {
            try
            {
                var film = await _filmRepository.GetAll().FirstOrDefaultAsync(x => x.Id==id);
                if (film == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Такого фильма нету",
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _filmRepository.Delete(film);
                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Film>> Edit(int id, FilmViewModel model)
        {
            try
            {
                var film = await _filmRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if(film == null)
                {
                    return new BaseResponse<Film>()
                    {
                        Description = "Фильм не найден",
                        StatusCode = StatusCode.FilmNotFound
                    };
                }
                film.Description = model.Description;
                film.Price = model.Price;
                film.Name = model.Name;
                film.ReleaseDate = model.ReleaseDate;

                await _filmRepository.Update(film);

                return new BaseResponse<Film>()
                {
                    Data = film,
                    StatusCode = StatusCode.OK
                };
                //TODO добавить редактирование жанров
            }
            catch(Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<FilmViewModel>> GetFilm(int id)
        {
            try
            {
                var film = await _filmRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if(film == null)
                {
                    return new BaseResponse<FilmViewModel>()
                    {
                        Description = "Данного фильма нету",
                        StatusCode = StatusCode.UserNotFound,
                    };
                }

                var data = new FilmViewModel()
                {
                    ReleaseDate = film.ReleaseDate,
                    Description = film.Description,
                    Name = film.Name,
                    Price = film.Price,
                    GenreFilm = film.GenreFilm.GetDisplayName(),
                    Image = film.Avatar,
                };
                return new BaseResponse<FilmViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<FilmViewModel>()
                {
                    Description = $"[GetFilm] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Dictionary<int, string>>> GetFilm(string term)
        {
            var baseResponse = new BaseResponse<Dictionary<int, string>>();
            try
            {
                var films = await _filmRepository.GetAll()
                    .Select(x => new FilmViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        ReleaseDate = x.ReleaseDate,
                        Price = x.Price,
                        GenreFilm = x.GenreFilm.GetDisplayName()
                    })
                    .Where(x => EF.Functions.Like(x.Name, $"%{term}%"))
                    .ToDictionaryAsync(x => x.Id, t => t.Name);

                baseResponse.Data = films;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public IBaseResponse<List<Film>> GetFilms()
        {
            try
            {
                var films = _filmRepository.GetAll().ToList();
                if (!films.Any())
                {
                    return new BaseResponse<List<Film>>()
                    {
                        Description = "Нечего получать",
                        StatusCode = StatusCode.OK
                    };
                }

                return new BaseResponse<List<Film>>()
                {
                    Data = films,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<List<Film>>()
                {
                    Description = $"[GetFilms] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((GenreFilm[])Enum.GetValues(typeof(GenreFilm)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}


