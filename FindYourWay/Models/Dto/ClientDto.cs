using FindYourWay.Enums;
using System.ComponentModel.DataAnnotations;

namespace FindYourWay.Models.Dto
{
    public class ClientDto
    {

        public int Id { get; set; }

        [Required]
        public string? Name { get; set; } = null;

        public string? Address { get; set; } = null;

        [EmailAddress]
        public string? Email { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; } = null;

        public string? City { get; set; } = null;

        public ClientStatus? Status { get; set; } = ClientStatus.Inactive;
    }
}
