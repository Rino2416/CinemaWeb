using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CinemaWeb.Domain.Enum
{
    public enum Role
    {
        [Display(Name = "Пользователь")]
        Боевик = 0,
        [Display(Name = "Кассир")]
        Детектив = 1,
        [Display(Name = "Администратор")]
        Драма = 2,
    }
}
