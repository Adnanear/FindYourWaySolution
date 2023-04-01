﻿using FindYourWay.Models;
using FindYourWay.Models.Dto;
using FindYourWay.Services.User;
using Microsoft.AspNetCore.Mvc;

namespace FindYourWay.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var service = await _service.GetUsers();
            return StatusCode(service.code, service.response);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var service = await _service.GetUserById(id);

            if (service.code is StatusCodes.Status404NotFound) return NotFound();
            if (service.code is StatusCodes.Status400BadRequest) return BadRequest();

            return StatusCode(service.code, service.response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto user)
        {
            var service = await _service.CreateUser(user);
            return StatusCode(service.code, service.response);
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> UpdateUserById(int id, [FromBody] UserDto user)
        {
            var service = await _service.UpdateUserById(id, user);
            return StatusCode(service.code, service.response);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> DeleteUserById(int id)
        {
            var service = await _service.DeleteUserById(id);
            return StatusCode(service.code, service.response);
        }
    }
}
