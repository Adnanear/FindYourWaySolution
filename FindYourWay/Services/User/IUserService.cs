using FindYourWay.utils;
using FindYourWay.Models.Dto;

namespace FindYourWay.Services.User
{
    // This is a simple interface defining `UserService` methods
  public interface IUserService
  {
    Task<ServiceControllerWrapper<List<Models.Account>>> GetUsers();

    Task<ServiceControllerWrapper<Models.Account>> GetUserById(int id);

    Task<ServiceControllerWrapper<Models.Account>> CreateUser(AccountDto user);

    Task<ServiceControllerWrapper<Models.Account>> UpdateUserById(int id, AccountDto user);

    Task<ServiceControllerWrapper<Models.Account>> DeleteUserById(int id);
  }
}
