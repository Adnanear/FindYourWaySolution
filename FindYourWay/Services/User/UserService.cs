using FindYourWay.Data.Stores;
using FindYourWay.utils;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Services.User
{
  public class UserService : IUserService
  {
    public ServiceControllerWrapper<Models.User> CreateUser(Models.User user)
    {
      if (user == null) return new(StatusCodes.Status400BadRequest);

      UserStore.usersList.Add(user);

      Models.User newUser = UserStore.usersList.FirstOrDefault(x => x.Email == user.Email)!;
      newUser.CreatedAt = DateTime.UtcNow;
      newUser.UpdatedAt = DateTime.UtcNow;

      return new(StatusCodes.Status201Created, newUser);
    }

    public ServiceControllerWrapper<Models.User> DeleteUserById(int id)
    {
      if (id is 0) return new(StatusCodes.Status400BadRequest);

      var user = UserStore.usersList.FirstOrDefault(x => x.Id == id);
      if (user is null) return new(StatusCodes.Status404NotFound);

      UserStore.usersList.Remove(user);

      return new(StatusCodes.Status204NoContent);
    }

    public ServiceControllerWrapper<Models.User> GetUserById(int id)
    {
      if (id is 0) return new(StatusCodes.Status400BadRequest);

      var user = UserStore.usersList.FirstOrDefault(x => x.Id == id);
      if (user is null) return new(StatusCodes.Status404NotFound);

      return new(StatusCodes.Status200OK, user);
    }

    public ServiceControllerWrapper<List<Models.User>> GetUsers()
    {
      return new(StatusCodes.Status202Accepted, UserStore.usersList);
    }

    public ServiceControllerWrapper<Models.User> UpdateUserById(Models.User user)
    {
      if (user == null || user.Id == 0) return new(StatusCodes.Status400BadRequest);

      var storedUser = UserStore.usersList.FirstOrDefault(x => x.Id == user.Id);
      if (storedUser == null) return new(StatusCodes.Status404NotFound);

      storedUser.Email = user.Email;
      storedUser.UpdatedAt = DateTime.UtcNow;

      return new(StatusCodes.Status202Accepted, storedUser);
    }
  }
}
