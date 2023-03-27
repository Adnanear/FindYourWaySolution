using FindYourWay.Data.Stores;
using FindYourWay.Models.Dto;
using FindYourWay.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> GetUser(int id) {
            return Ok(_userService.GetUser(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> CreateUser([FromBody]UserDto user)
        {
            if( user == null ) return BadRequest();

            UserStore.usersList.Add(user);
            UserDto newUser = UserStore.usersList.FirstOrDefault(x => x.Email == user.Email)!;

            newUser.CreatedAt = DateTime.UtcNow;
            newUser.UpdatedAt = DateTime.UtcNow;

            return StatusCode(StatusCodes.Status201Created, newUser);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserById([FromBody]UserDto user)
        {
            if (user == null || user.Id == 0) return BadRequest();

            var storedUser = UserStore.usersList.FirstOrDefault(x => x.Id == user.Id);
            if( storedUser == null) return NotFound();

            storedUser.Email = user.Email;
            storedUser.UpdatedAt = DateTime.UtcNow;

            return StatusCode(StatusCodes.Status202Accepted, storedUser);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUserById(int id)
        {
            if( id == 0 ) return BadRequest();

            var user = UserStore.usersList.FirstOrDefault(x => x.Id == id);
            if( user == null ) return NotFound();

            UserStore.usersList.Remove(user);

            return StatusCode(StatusCodes.Status204NoContent, user);
        }

    }
}
