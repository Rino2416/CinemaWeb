using CinemaWeb.Domain.Enum;

namespace CinemaWeb.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; } // ошибка в бд(exception)
        public StatusCode StatusCode { get; set; }
        public T Data { get; set; } // Результат запроса

    }

    public interface IBaseResponse<T> // Generic
    {
        T Data { get; set; }
        StatusCode StatusCode { get; set; }
    }
}
