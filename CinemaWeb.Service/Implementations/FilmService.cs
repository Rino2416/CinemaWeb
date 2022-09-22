using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Domain.Response;
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

    }
}


