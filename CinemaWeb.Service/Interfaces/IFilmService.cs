using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Response;
using CinemaWeb.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Service.Interfaces
{
    public interface IFilmService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();

        IBaseResponse<List<Film>> GetFilms();

        Task<IBaseResponse<FilmViewModel>> GetFilm(int id);

        Task<BaseResponse<Dictionary<int, string>>> GetFilm(string term);

        Task<IBaseResponse<Film>> Create(FilmViewModel model, byte[] imageData);

        Task<IBaseResponse<bool>> DeleteFilm(int id);

        Task<IBaseResponse<Film>> Edit(int id, FilmViewModel model);
    }
}
