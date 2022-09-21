using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.DAL.Repositories
{
    public class FilmRepository : IFilmRepository
    {
        private readonly ApplicationDbContext _db;

        public FilmRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Create(Film entity)
        {
            _db.Film.Add(entity);
            _db.SaveChanges();
            return true;
        }

        public bool Delete(Film entity)
        {
            _db.Film.Remove(entity);
            _db.SaveChanges();
             return false;
        }

        public Film Get(int id)
        {
            return _db.Film.FirstOrDefault(x => x.Id == id);
        }

        public Film GetByName(string name)
        {
            return _db.Film.FirstOrDefault(x => x.Name == name);
        }

        public IEnumerable<Film> Select()
        {
             return _db.Film.ToList() ; //Обращение к таблице Film(получение списка данных)
        }
    }
}
