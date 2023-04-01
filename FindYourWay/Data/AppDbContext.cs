using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      base.OnConfiguring(optionsBuilder);
      optionsBuilder.UseSqlServer(string.Format("Server=localhostSQLEXPRESS;Database=master;Trusted_Connection=True;"));
    }

    public DbSet<User>? Users { get; set; }
  }
}
