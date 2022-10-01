using CinemaWeb.DAL.Interfaces;
using CinemaWeb.DAL.Repositories;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Domain.Extensions;
using CinemaWeb.Domain.Response;
using CinemaWeb.Domain.ViewModels;
using CinemaWeb.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaWeb.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
        }
        public async Task<IBaseResponse<UserViewModel>> CreateUser(UserViewModel userViewModel)
        {
            var baseResponse = new BaseResponse<UserViewModel>();
            try
            {
                var users = new User()
                {
                    Name = userViewModel.Name,
                    Id = userViewModel.Id,
                    Role = (Role)Convert.ToInt32(userViewModel.Role)

                };
                await _userRepository.Create(users);

            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserService.GetUsers] error: {ex.Message}");
                return new BaseResponse<UserViewModel>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Ошибка создания: {ex.Message}"
                    
                };
            }
            return baseResponse;
        }

        public async Task<IBaseResponse<bool>> DeleteUsers(long id)
        {
            var baseResponse = new BaseResponse<bool>()
            {
                Data = true
            };
            try
            {
                var users = await _userRepository.Get(long id);
                if(users == null)
                {
                    baseResponse.Description = "Данного пользователя нету";
                    baseResponse.StatusCode = StatusCode.UserNotFound;
                    return baseResponse;
                }
                await _userRepository.Delete(users);
                return baseResponse;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Ошибка при удалении: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try {
                var roles = ((Role[])Enum.GetValues(typeof(Role)))
                        .ToDictionary(k => (int)k, t => t.GetDisplayName());
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = roles,
                    StatusCode = StatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<User>> GetUsers(long id)
        {
            var baseResponse = new BaseResponse<IEnumerable<User>>();
            try
            {
                var users = await _userRepository.Select();
                if(users.Count == 0)
                {
                    baseResponse.Description = "Наёдено 0 пользователей";
                    baseResponse.StatusCode = StatusCode.OK;
                    return (IBaseResponse<User>)baseResponse;
                }
                baseResponse.Data = users;
                baseResponse.StatusCode = StatusCode.OK;
                return (IBaseResponse<User>)baseResponse;
            }
            catch(Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetUser] : {ex.Message}"
                };
            }
        }
    }
}
