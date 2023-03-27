using FindYourWay.utils;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Services.User
{
  public interface IUserService
  {

    ServiceControllerWrapper<List<UserDto>> GetUsers();
    ServiceControllerWrapper<UserDto> GetUserById(int id);
    ServiceControllerWrapper<UserDto> CreateUser(UserDto user);
    ServiceControllerWrapper<UserDto> UpdateUserById(UserDto user);
    ServiceControllerWrapper<UserDto> DeleteUserById(int id);
  }
}
