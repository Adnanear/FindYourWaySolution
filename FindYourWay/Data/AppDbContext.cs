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

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Client> Client { get; set; }

  }
}
