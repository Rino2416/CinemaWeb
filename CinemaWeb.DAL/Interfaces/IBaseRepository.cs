using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.DAL.Interfaces
{
    public interface IBaseRepository<T> // методы для взаимодейсвтия с бд
    {
        bool Create(T entity); // добавление объекта

        T Get(int id); // 1 объект из таблицы

        IEnumerable<T> Select(); // возвращение коллекции элементов

        bool Delete(T entity); // удаление объекта

    }
}
