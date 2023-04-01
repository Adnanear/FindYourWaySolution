namespace FindYourWay.Models
{
  public class User
  {

    public int Id { get; set; }

        public string? Email { get; set; } = null;

        public string? Password { get; set; } = null;

        public DateTime? CreatedAt { get; set; } = null;

        public DateTime? UpdatedAt { get; set; } = null;

        public DateTime? DeletedAt { get; set; } = null;

  }
}
