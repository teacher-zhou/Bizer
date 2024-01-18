using Bizer.Services.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using Sample.Services;

namespace Sample.WebApi;
public class TestDbContext : BizerDbContext
{
    public TestDbContext(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    public DbSet<User> Users { get; set; }
}