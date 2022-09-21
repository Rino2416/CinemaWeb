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
        Action = 0,
        [Display(Name = "Детектив")]
        Detective = 1,
        [Display(Name = "Драма")]
        Drama = 2,
        [Display(Name = "Комедия")]
        Comedy = 3,
        [Display(Name = "Мультфильм")]
        Cartoon = 4,
        [Display(Name = "Катастрофа")]
        Catastrophe = 5
    }
}
