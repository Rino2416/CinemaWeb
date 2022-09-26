using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Domain.Response;
using CinemaWeb.Domain.ViewModels;
using CinemaWeb.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Service.Implementations
{
    public class FilmService : IFilmService
    {

        private readonly IFilmRepository _filmRepository;

        public FilmService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public async Task<IBaseResponse<FilmViewModel>> CreateFilm(FilmViewModel filmViewModel)
        {
            var baseResponse = new BaseResponse<FilmViewModel>();
            try
            {
                var film = new Film()
                {
                    Description = filmViewModel.Description,
                    ReleaseDate = DateTime.Now,
                    Name = filmViewModel.Name,
                    Price = filmViewModel.Price,
                    GenreFilm = (GenreFilm)Convert.ToInt32(filmViewModel.GenreFilm),
                };

                await _filmRepository.Create(film);
            }
            catch (Exception ex)
            {
                return new BaseResponse<FilmViewModel>()
                {
                    Description = $"[GetFilm] : {ex.Message}"
                };
            }
            return baseResponse;
           
        }

        public async Task<IBaseResponse<bool>> DeleteFilm(int id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var film = await _filmRepository.Get(id);
                if (film == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                await _filmRepository.Delete(film);
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[GetFilm] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Film>> GetFilmByName(string name)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = await _filmRepository.GetByName(name);
                if (film == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }

                baseResponse.Data = film;
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[GetFilm] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Film>> GetFilm(int id)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = await _filmRepository.Get(id);
                if(film == null)
                {
                    baseResponse.Description = "Пользователь не найден";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                baseResponse.StatusCode = StatusCode.OK;
                baseResponse.Data = film;
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[GetFilm] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<IEnumerable<Film>>> GetFilms()
        {
            var baseResponse = new BaseResponse<IEnumerable<Film>>();
            try
            {
                var films = await _filmRepository.Select();
                if(films.Count == 0)
                {
                    baseResponse.Description = "Найдено 0 элементов";
                    baseResponse.StatusCode = StatusCode.OK;
                    return baseResponse;
                }
                baseResponse.Data = films;
                baseResponse.StatusCode = StatusCode.OK;
                

                return baseResponse;
            }
            catch (Exception ex)
            {
                return new BaseResponse<IEnumerable<Film>>()
                {
                    Description = $"[GetFilms] : {ex.Message}"
                };
            }
        }

        public async Task<IBaseResponse<Film>> Edit(int id, FilmViewModel model)
        {
            var baseResponse = new BaseResponse<Film>();
            try
            {
                var film = await _filmRepository.Get(id);
                if (film == null)
                {
                    baseResponse.StatusCode = StatusCode.FilmNotFound;
                    baseResponse.Description = "Не удалось найти данный фильм";
                    return baseResponse;
                }
                film.Description = model.Description;
                film.Price = model.Price;
                film.Name = model.Name;
                film.ReleaseDate = model.ReleaseDate;
                //GENRE FILM +!!!

                await _filmRepository.Update(film);
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<Film>()
                {
                    Description = $"[Edit] : {ex.Message}"
                };
            }
        }
    }
}


