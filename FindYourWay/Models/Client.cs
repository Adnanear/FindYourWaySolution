using FindYourWay.Enums;

namespace FindYourWay.Models
{
    public class Client
    {

        public int Id { get; set; }

        public string? Name { get; set; } = null;

        public string? Address { get; set; } = null;

        public string? PhoneNumber { get; set; } = null;

        public string? City { get; set; } = null;

        public ClientStatus? Status { get; set; } = ClientStatus.Inactive;

    }
}
