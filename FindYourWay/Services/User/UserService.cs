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
            // We set the Application database context coming from the caller
            _context = context;
        }

        // Method to create a new user
        public async Task<ServiceControllerWrapper<Models.Account>> CreateUser(AccountDto user)
        {
            // If the user is null or has an Id which is equal to 0
            // We respond with 400 Bad Request
            if (user is null || user.Id is 0) return new(StatusCodes.Status400BadRequest);

            // We build a new user instance
            Models.Account newUser = new()
            {
                Email = user.Email,
                Password = user.Password,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            // We add the newly created instance to the context
            _context.Accounts.Add(newUser);

            // And then save the changes
            await _context.SaveChangesAsync();

            // Since everything was good, we then have to respond with a `201 Created` along with the new user record
            return new(StatusCodes.Status201Created, newUser);
        }

        // Deleting user by id
        public async Task<ServiceControllerWrapper<Models.Account>> DeleteUserById(int id)
        {
            // We check if the targeted user exists 
            var targetUser = await _context.Accounts.FindAsync(id);

            // If not we respond with `404 Not found`
            if (targetUser is null) return new(StatusCodes.Status404NotFound);

            // Otherwise, we remove it from the db context
            _context.Accounts.Remove(targetUser);

            // And then save the changes
            await _context.SaveChangesAsync();

            // And then respond with a `202 Accepted`
            return new(StatusCodes.Status202Accepted);
        }

        // Get user by id
        public async Task<ServiceControllerWrapper<Models.Account>> GetUserById(int id)
        {
            // Check if the targeted user exists
            var user = await _context.Accounts.FindAsync(id);

            // If not we throw an error 404
            if (user is null) return new(StatusCodes.Status404NotFound);

            // Otherwise we return the actual user
            return new(StatusCodes.Status200OK, user);
        }

        // Get All available users
        public async Task<ServiceControllerWrapper<List<Models.Account>>> GetUsers()
        {
            // Fetch the list of all users
            var users = await _context.Accounts.ToListAsync();

            // And respond with `200 Ok` + array of users
            return new(StatusCodes.Status200OK, users);
        }

        // Update a user by id
        public async Task<ServiceControllerWrapper<Models.Account>> UpdateUserById(int id, AccountDto user)
        {
            // Check if user exists
            var targetUser = await _context.Accounts.FindAsync(id);

            // If not, we throw `404 Not Found`
            if (targetUser is null) return new(StatusCodes.Status404NotFound);

            // Update fields
            targetUser.Email = user.Email;
            targetUser.Password = user.Password;
            targetUser.UpdatedAt = DateTime.UtcNow;
            
            // Save changes
            await _context.SaveChangesAsync();

            // Respond with `202 Accepted`
            return new(StatusCodes.Status202Accepted);
        }
    }
}

