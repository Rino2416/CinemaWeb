using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CinemaWeb.Domain.Enum
{
    public enum GenreFilm
    {
        [Display(Name = "Боевик")]
        Боевик = 0,
        [Display(Name = "Детектив")]
        Детектив = 1,
        [Display(Name = "Драма")]
        Драма = 2,
        [Display(Name = "Комедия")]
        Комедия = 3,
        [Display(Name = "Мультфильм")]
        Cartoon = 4,
        [Display(Name = "Катастрофа")]
        Катастрофа = 5
    }
}
