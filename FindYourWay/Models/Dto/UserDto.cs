using System.ComponentModel.DataAnnotations;

namespace FindYourWay.Models.Dto
{
  public class UserDto
  {
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [MinLength(1)]
    public string? Password { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
  }
}
