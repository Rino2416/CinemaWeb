using CinemaWeb.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Domain.Entity
{
    public class Film
    {
        public int Id { get; set; } // id
        public string Name { get; set; } // Название фильма
        public GenreFilm GenreFilm { get; set; } // Жанр
        public string Description { get; set; } // Описание
        public decimal Price { get; set; } // Цена
        public DateTime ReleaseDate { get; set; } // Дата выхода


    }
}
