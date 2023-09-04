using Bizer.Services;

using Microsoft.EntityFrameworkCore;
using Sample.Services;

namespace Sample.WebApi;
public class TestDbContext : BizerDbContext
{
    public TestDbContext(IServiceProvider serviceProvider, DbContextConfigureOptions options) : base(serviceProvider, options)
    {
    }

    public DbSet<User> Users { get; set; }
}