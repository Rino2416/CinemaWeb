using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Response;
using CinemaWeb.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Service.Interfaces
{
    public interface IFilmService
    {
        Task<IBaseResponse<IEnumerable<Film>>> GetFilms();
        Task<IBaseResponse<FilmViewModel>> CreateFilm(FilmViewModel filmViewModel);
        Task<IBaseResponse<bool>> DeleteFilm(int id);
        Task<IBaseResponse<Film>> GetFilmByName(string name);
        Task<IBaseResponse<Film>> GetFilm(int id);
        Task<IBaseResponse<Film>> Edit(int id, FilmViewModel model);
    }
}
