using FindYourWay.Controllers;
using FindYourWay.Data.Stores;
using FindYourWay.Models;
using FindYourWay.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Services.User
{
    public class UserService : IUserService
    {

        private readonly UserController _controllerInstance;
        public UserService(UserController sender)
        {
            _controllerInstance = sender;
        }

        public Models.Dto.UserDto CreateUser([FromBody] Models.Dto.UserDto user)
        {
            throw new NotImplementedException();
        }

        public IActionResult DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Models.Dto.UserDto? GetUser(int id)
        {
            if (id == 0) return null;

            var user = UserStore.usersList.FirstOrDefault(x => x.Id == id);
            if (user == null) return null;

            return user;
        }

        public List<Models.Dto.UserDto> GetUsers()
        {
            return UserStore.usersList;
        }

        public IActionResult UpdateUserById([FromBody] Models.Dto.UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
