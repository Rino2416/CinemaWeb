using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Controllers
{
    public class HomeController : Controller
    {

      

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


    }
}