using System.ComponentModel.DataAnnotations;

namespace FindYourWay.Models.Dto
{
  public class UserDto
  {
    public int Id { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; } = null;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
  }
}
