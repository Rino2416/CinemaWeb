using Microsoft.AspNetCore.Mvc;

namespace CinemaWeb.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
