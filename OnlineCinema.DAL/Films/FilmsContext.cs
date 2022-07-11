using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineCinema.Domain;

namespace OnlineCinema.DAL;

public class FilmsContext : DbContext
{
    public FilmsContext(DbContextOptions<FilmsContext> options) 
        : base(options)
    { }
    
    public FilmsContext()
    { }

    public DbSet<Film> Films { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Writer> Writers { get; set; }
    public DbSet<Serial> Serials { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }

}

public class DesignTimeFilmsContextFactory : IDesignTimeDbContextFactory<FilmsContext>
{
    public FilmsContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FilmsContext>();
        optionsBuilder.UseSqlServer("Data Source=localhost,1433;Database=FilmsDB;Trusted_Connection=False;User ID=sa;Password=Pa55w0rd");

        return new FilmsContext(optionsBuilder.Options);

    }
}