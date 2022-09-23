using CinemaWeb.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Domain.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } // Название фильма
        public string GenreFilm { get; set; } // Жанр
        public string Description { get; set; } // Описание
        public decimal Price { get; set; } // Цена
        public DateTime ReleaseDate { get; set; } // Дата выхода

    }
}
