using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Response;
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
    }
}
