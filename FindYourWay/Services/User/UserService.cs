using FindYourWay.Data;
using FindYourWay.Models.Dto;
using FindYourWay.utils;
using Microsoft.EntityFrameworkCore;

namespace FindYourWay.Services.User
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceControllerWrapper<Models.User>> CreateUser(UserDto user)
        {
            if (user is null || user.Id is 0) return new(StatusCodes.Status400BadRequest);

            Models.User newUser = new()
            {
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            return new(StatusCodes.Status201Created, newUser);
        }

        public async Task<ServiceControllerWrapper<Models.User>> DeleteUserById(int id)
        {
            var targetUser = await _context.Users.FindAsync(id);
            if (targetUser is null) return new(StatusCodes.Status404NotFound);

            _context.Users.Remove(targetUser);

            await _context.SaveChangesAsync();
            return new(StatusCodes.Status202Accepted);
        }

        public async Task<ServiceControllerWrapper<Models.User>> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user is null) return new(StatusCodes.Status404NotFound);

            return new(StatusCodes.Status200OK, user);
        }

        public async Task<ServiceControllerWrapper<List<Models.User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return new(StatusCodes.Status200OK, users);
        }

        public async Task<ServiceControllerWrapper<Models.User>> UpdateUserById(int id, UserDto user)
        {
            var targetUser = await _context.Users.FindAsync(id);
            if (targetUser is null) return new(StatusCodes.Status404NotFound);

            targetUser.Email = user.Email;
            targetUser.Password = user.Password;
            targetUser.UpdatedAt = DateTime.UtcNow;
            
            await _context.SaveChangesAsync();
            return new(StatusCodes.Status202Accepted);
        }
    }
}

