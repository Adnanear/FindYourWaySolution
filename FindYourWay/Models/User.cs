﻿namespace FindYourWay.Models
{
    public class UserDto
    {

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime DeletedAt { get; set; }

    }
}