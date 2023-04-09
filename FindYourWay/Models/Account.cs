namespace FindYourWay.Models
{
  public class Account
  {
        public int Id { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? AccessToken { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
