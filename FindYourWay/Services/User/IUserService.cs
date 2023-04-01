using FindYourWay.utils;
using FindYourWay.Models.Dto;

namespace FindYourWay.Services.User
{
  public interface IUserService
  {
    Task<ServiceControllerWrapper<List<Models.User>>> GetUsers();

    Task<ServiceControllerWrapper<Models.User>> GetUserById(int id);

    Task<ServiceControllerWrapper<Models.User>> CreateUser(UserDto user);

    Task<ServiceControllerWrapper<Models.User>> UpdateUserById(int id, UserDto user);

    Task<ServiceControllerWrapper<Models.User>> DeleteUserById(int id);
  }
}
