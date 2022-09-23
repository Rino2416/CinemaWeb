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

        public async Task<bool> Create(Film entity)
        {
            await _db.Film.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Film entity)
        {
            _db.Film.Remove(entity);
            await _db.SaveChangesAsync();
             return false;
        }

        public async Task<Film> Get(int id)
        {
            return await _db.Film.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Film> GetByName(string name)
        {
            return await _db.Film.FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<List<Film>> Select()
        {
             return await _db.Film.ToListAsync() ; //Обращение к таблице Film(получение списка данных)
        }

        public async Task<Film> Update(Film entity)
        {
            _db.Film.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
