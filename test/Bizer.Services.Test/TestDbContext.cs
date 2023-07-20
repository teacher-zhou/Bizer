using Microsoft.EntityFrameworkCore;

namespace Bizer.Services.Test;
public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}