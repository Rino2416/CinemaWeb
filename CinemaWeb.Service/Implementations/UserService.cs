using CinemaWeb.DAL.Interfaces;
using CinemaWeb.Domain.Entity;
using CinemaWeb.Domain.Enum;
using CinemaWeb.Domain.Extensions;
using CinemaWeb.Domain.Response;
using CinemaWeb.Domain.ViewModels;
using CinemaWeb.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaWeb.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IBaseRepository<Profile> _proFileRepository;

        public UserService(ILogger<UserService> logger, IBaseRepository<User> userRepository, IBaseRepository<Profile> proFileRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _proFileRepository = proFileRepository;
        }

        public async Task<IBaseResponse<User>> CreateUser(UserViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Name == model.Name);
                if(user != null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "Данный логин занят",
                       
                    };
                }
                user = new User()
                {
                    Name = model.Name,
                    Role = Enum.Parse<Role>(model.Role),
                    Password = HashPasswordHelper.HashPassowrd(model.Password),
                };
                await _userRepository.Create(user);

                var profile = new Profile()
                {
                    Address = String.Empty,
                    Age = 0,
                    UserId = user.Id,
                };
                
                await _proFileRepository.Create(profile);

                return new BaseResponse<User>
                {
                    Data = user,
                    Description = "Все прошло успешно",
                    StatusCode = StatusCode.OK 
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserService.Create] error: {ex.Message}");
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }

        }

        public async Task<IBaseResponse<bool>> DeleteUsers(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        StatusCode = StatusCode.UserNotFound,
                        Data = false
                    };
                }
                await _userRepository.Delete(user);
                _logger.LogInformation($"[UserService.DeleteUser] Удаление прошло успешно");

                return new BaseResponse<bool>
                {
                    StatusCode = StatusCode.OK,
                    Data = true
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.DeleteUser] error: {ex.Message}");
                return new BaseResponse<bool>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }

        public BaseResponse<Dictionary<int, string>> GetRoles()
        {
            try
            {
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

        public async Task<IBaseResponse<IEnumerable<UserViewModel>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll()
                    .Select(x => new UserViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Role = x.Role.GetDisplayName()
                    })
                    .ToListAsync();

                _logger.LogInformation($"[UserService.GetUsers] получено элементов {users.Count}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[UserSerivce.GetUsers] error: {ex.Message}");
                return new BaseResponse<IEnumerable<UserViewModel>>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
