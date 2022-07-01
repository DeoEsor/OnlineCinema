using Microsoft.Extensions.Configuration;
using OnlineCinema.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Users.Repository.Contexts;

public sealed class UsersDb : DbContext
{
    private readonly string _connectionString = string.Empty;

    // @"Server=localhost,1433;Database=UsersBase;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd"
    public UsersDb(IConfigurationSection configurationSection)
    {
        _connectionString = configurationSection.GetSection("ConnectionString").Value 
                            ?? throw new NullReferenceException(nameof(configurationSection));
    }
    
    public DbSet<User> Users => Set<User>();
    
    public UsersDb() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        => optionsBuilder
            .UseSqlServer(_connectionString);
}