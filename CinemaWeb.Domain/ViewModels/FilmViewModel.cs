using CinemaWeb.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace CinemaWeb.Domain.ViewModels
{
    public class FilmViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Name { get; set; } // Название фильма

        [Display(Name = "Жанр фильма")]
        [Required(ErrorMessage = "Выберите жанр фильма")]
        public string GenreFilm { get; set; } // Жанр

        [Display(Name = "Описание")]
        [MinLength(50, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
        public string Description { get; set; } // Описание

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        public decimal Price { get; set; } // Цена
        public DateTime ReleaseDate { get; set; } // Дата выхода
        public byte[]? Image { get; set; }

    }
}
