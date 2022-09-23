using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Domain.Enum
{
    public enum StatusCode
    {
        FilmNotFound = 10,
        UserNotFound = 0,
        OK = 200,
        InternalServerError = 500,
    }
}
