using FindYourWay.Data.Stores;
using FindYourWay.Models.Dto;
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
            return Ok(UserStore._usersList);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> GetUser(int id) {

            if (id == 0) return BadRequest();

            var user = UserStore._usersList.FirstOrDefault((x) => x.Id == id);
            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> CreateUser([FromBody]UserDto user)
        {
            if( user == null ) return BadRequest();

            UserStore._usersList.Add(user);
            UserDto newUser = UserStore._usersList.FirstOrDefault(x => x.Email == user.Email)!;

            newUser.CreatedAt = DateTime.UtcNow;
            newUser.UpdatedAt = DateTime.UtcNow;

            return StatusCode(StatusCodes.Status201Created, newUser);
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateUserById(int id, [FromBody]UserDto user)
        {
            if (id == 0 || id != user.Id) return BadRequest();
            if (user == null) return NotFound();

            var storedUser = UserStore._usersList.FirstOrDefault(x => x.Id == user.Id);
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

            var user = UserStore._usersList.FirstOrDefault(x => x.Id == id);
            if( user == null ) return NotFound();

            UserStore._usersList.Remove(user);

            return StatusCode(StatusCodes.Status204NoContent, user);
        }
    }
}
