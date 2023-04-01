using FindYourWay.utils;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Services.User
{
  public interface IUserService
  {

    ServiceControllerWrapper<List<Models.User>> GetUsers();
    ServiceControllerWrapper<Models.User> GetUserById(int id);
    ServiceControllerWrapper<Models.User> CreateUser(Models.User user);
    ServiceControllerWrapper<Models.User> UpdateUserById(Models.User user);
    ServiceControllerWrapper<Models.User> DeleteUserById(int id);
  }
}
