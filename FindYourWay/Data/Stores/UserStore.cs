﻿using FindYourWay.Models.Dto;

namespace FindYourWay.Data.Stores
{
    public static class UserStore
    {

        public static List<UserDto> _usersList = new List<UserDto>
        {
            new UserDto { Id = 1, Email = "adnane.jr2021@gmail.com" },
            new UserDto { Id = 2, Email = "souad.bae@gmail.com" },
        };

    }
}