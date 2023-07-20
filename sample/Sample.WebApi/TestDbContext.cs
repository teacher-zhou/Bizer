using Microsoft.EntityFrameworkCore;
using Sample.Services;

namespace Sample.WebApi;
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}