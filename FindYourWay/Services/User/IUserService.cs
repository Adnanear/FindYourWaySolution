using Microsoft.AspNetCore.Mvc;
using UserDto = FindYourWay.Models.Dto.UserDto;

namespace FindYourWay.Services.User
{
    public interface IUserService
    {

        public List<UserDto> GetUsers();
        public UserDto? GetUser(int id);
        public UserDto CreateUser([FromBody] UserDto user);
        public IActionResult UpdateUserById([FromBody] UserDto user);
        public IActionResult DeleteUserById(int id);
    }
}
