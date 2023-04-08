using Microsoft.EntityFrameworkCore;
using FindYourWay.Models;

namespace FindYourWay.Data
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<FindYourWay.Models.Client> Client { get; set; } = default!;
  }
}
