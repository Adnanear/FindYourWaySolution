using FindYourWay.utils;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Services.User
{
  public interface IUserService
  {

    ServiceControllerBridge<List<UserDto>> GetUsers();
    ServiceControllerBridge<UserDto> GetUserById(int id);
    ServiceControllerBridge<UserDto> CreateUser(UserDto user);
    ServiceControllerBridge<UserDto> UpdateUserById(UserDto user);
    ServiceControllerBridge<UserDto> DeleteUserById(int id);
  }
}
