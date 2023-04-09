using System.ComponentModel.DataAnnotations;

namespace FindYourWay.Models.Dto
{
    public class AuthenticationDto
    {

        public class Signin
        {
            [Required]
            [EmailAddress]
            public string? Email { get; set; }

            [Required]
            public string? Password { get; set; }
        }

        public class Signup
        {
            [Required]
            [EmailAddress]
            public string? Email { get; set; }

            [Required]
            public string? Password { get; set; }

            [Required]
            public string? Password2 { get; set; }
        }

    }
}
