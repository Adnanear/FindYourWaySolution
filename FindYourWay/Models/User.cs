﻿namespace FindYourWay.Models
{
  public class User
  {

    public int Id { get; set; }

    public string? Email { get; set; } = null;

    public string? Password { get; set; } = null;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime DeletedAt { get; set; }

  }
}
