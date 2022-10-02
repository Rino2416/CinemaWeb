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
    public class FilmRepository : IBaseRepository<Film>
    {
        private readonly ApplicationDbContext _db;

        public FilmRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task Create(Film entity)
        {
            await _db.Film.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Film entity)
        {
            _db.Film.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public IQueryable<Film> GetAll()
        {
            return _db.Film;
        }

        public async Task<Film> Update(Film entity)
        {
            _db.Film.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
