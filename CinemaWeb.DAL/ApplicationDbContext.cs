using CinemaWeb.Domain.Entity;
using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.DAL
{
    public class ApplicationDbContext : DbContext // Компоненты для работы с БД
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) // Установка связи с БД(регистрация app asp .net)
        {
            Database.EnsureCreated();
        }

        public DbSet<Film> Films { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
    }
}
