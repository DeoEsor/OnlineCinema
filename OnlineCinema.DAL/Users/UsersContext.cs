using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineCinema.Domain.User;

namespace OnlineCinema.DAL.Users;

public class UsersContext : DbContext
{
    public UsersContext(DbContextOptions<UsersContext> options)
        : base(options)
    {
    }

    public UsersContext()
    {}

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new UserConfiguration());
    }
}

public class DesignTimeFilmsContextFactory : IDesignTimeDbContextFactory<UsersContext>
{
    public UsersContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UsersContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost,1433;Database=UsersDB;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd");

        return new UsersContext(optionsBuilder.Options);

    }
}