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
        Task<bool> Create(T entity); // добавление объекта

        Task<T> Get(int id); // 1 объект из таблицы

        Task<List<T>> Select(); // возвращение коллекции элементов

        Task<bool> Delete(T entity); // удаление объекта

      

    }
}
