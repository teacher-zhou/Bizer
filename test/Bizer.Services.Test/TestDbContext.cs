using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bizer.Services.Test;
//public class TestDbContext : DbContext
//{
//    public TestDbContext(DbContextOptions options) : base(options)
//    {
//    }

//    public DbSet<User> Users { get; set; }
//}
public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
