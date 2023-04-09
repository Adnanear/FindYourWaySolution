using FindYourWay.Data;
using FindYourWay.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FindYourWay.Controllers
{
    [Route("/api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly AppDbContext _context;

        public AuthenticationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult<IEnumerable<Account>>> SignIn([FromBody] AuthenticationDto.Signin authBody)
        {
            var users = await _context.Accounts.ToListAsync();

            var targetUserAccount = users.FirstOrDefault(x => x.Email == authBody.Username && x.Password == authBody.Password);
            if (targetUserAccount is null) return NotFound("Username or password is incorrect.");

            return Ok(targetUserAccount);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<IEnumerable<Account>>> SignUp([FromBody] AuthenticationDto.Signup authBody)
        {
            if (authBody.Password != authBody.Password2) return BadRequest("Passwords don't match.");

            var users = await _context.Accounts.ToListAsync();

            bool isUsernameAvailable = users.Exists(x => x.Email == authBody.Username);
            if (isUsernameAvailable) return StatusCode(StatusCodes.Status406NotAcceptable, "Username already exists.");

            Account newAccount = new Account
            {
                Email = authBody.Username,
                Password = authBody.Password,
                AccessToken = string.Format("TOKEN-{0}", authBody.Username),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, newAccount);
        }

    }
}
