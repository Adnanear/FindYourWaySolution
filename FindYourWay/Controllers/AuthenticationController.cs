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

            var targetUserAccount = users.FirstOrDefault(x => x.Email == authBody.Email && x.Password == authBody.Password);
            if (targetUserAccount is null) return NotFound("Username or password is incorrect.");

            return Ok(targetUserAccount);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<IEnumerable<Account>>> SignUp([FromBody] AuthenticationDto.Signup authBody)
        {
            if (authBody.Password != authBody.Password2) return BadRequest("Passwords don't match.");

            var users = await _context.Accounts.ToListAsync();

            bool isUsernameAvailable = users.Exists(x => x.Email == authBody.Email);
            if (isUsernameAvailable) return StatusCode(StatusCodes.Status406NotAcceptable, "Username already exists.");

            Account newAccount = new Account
            {
                Email = authBody.Email,
                Password = authBody.Password,
                AccessToken = await GenerateUniqueAccessToken(64),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Accounts.Add(newAccount);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, newAccount);
        }


        private async Task<string> GenerateUniqueAccessToken(int? length = 16)
        {
            const string keys = "azertyuiopqsdfghjklmwxcvbnAZERTYUIOPQSDFGHJKLMWXCVBN0123456789-_~!:;,*$@%";
            var token = string.Empty;

            for(var i = 0; i < length; i++)
            {
                var randIdx = new Random().Next(0, (int)(length + 1));
                char targetKey = keys[randIdx];
                token += targetKey;
            }

            var users = await _context.Accounts.ToListAsync();
            bool exists = users.Exists(x => x.AccessToken == token);
            if(exists) return await GenerateUniqueAccessToken(length);

            return token;
        }

    }
}
