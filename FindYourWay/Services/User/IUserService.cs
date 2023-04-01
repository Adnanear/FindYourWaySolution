using FindYourWay.utils;
using FindYourWay.Models;

namespace FindYourWay.Services.User
{
  public interface IUserService
  {

    Task<ServiceControllerWrapper<List<Models.User>>> GetUsers();

    Task<ServiceControllerWrapper<Models.User>> GetUserById(int id);

    Task<ServiceControllerWrapper<Models.User>> CreateUser(Models.User user);

    Task<ServiceControllerWrapper<Models.User>> UpdateUserById(int id, Models.User user);

    Task<ServiceControllerWrapper<Models.User>> DeleteUserById(int id);
  }
}
