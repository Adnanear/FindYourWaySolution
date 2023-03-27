using FindYourWay.Data.Stores;
using FindYourWay.Services.User;
using FindYourWay.utils;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Controllers
{
  [Route("api/users")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
      _userService = userService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<UserDto>> GetUsers()
    {
      var service = _userService.GetUsers();
      return StatusCode(service.code, service.response);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDto> GetUser(int id)
    {
      var service = _userService.GetUserById(id);

      if (service.code is StatusCodes.Status404NotFound) return NotFound();
      if (service.code is StatusCodes.Status400BadRequest) return BadRequest();

      return StatusCode(service.code, service.response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<UserDto> CreateUser([FromBody] UserDto user)
    {
      var service = _userService.CreateUser(user);
      return StatusCode(service.code, service.response);
    }


    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<UserDto> UpdateUserById([FromBody] UserDto user)
    {
      var service = _userService.UpdateUserById(user);
      return StatusCode(service.code, service.response);
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteUserById(int id)
    {
      var service = _userService.DeleteUserById(id);
      return StatusCode(service.code, service.response);
    }

  }
}
