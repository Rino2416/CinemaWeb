using CinemaWeb.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.DAL.Interfaces
{
    public interface IBaseRepository<T> // методы для взаимодейсвтия с бд
    {
        Task Create(T entity); // добавление объекта

        IQueryable<T> GetAll(); // 1 объект из таблицы

        Task Delete(T entity); // удаление объекта

        Task<T> Update(T entity);
    }
}
