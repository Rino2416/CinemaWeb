using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<bool> Create(User entity)
        {
            await _db.Users.AddAsync(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(User entity)
        {
            _db.Users.Remove(entity);
            await _db.SaveChangesAsync();
            return false;
        }

        public async Task<User> Get(int id)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<User>> Select()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> Update(User entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
