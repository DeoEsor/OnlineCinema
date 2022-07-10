using Microsoft.EntityFrameworkCore;
using OnlineCinema.Domain;

namespace OnlineCinema.DAL;

public class FilmsContext : DbContext
{
    public FilmsContext(DbContextOptions<FilmsContext> options) 
        : base(options)
    { }

    public DbSet<Film> Films { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Writer> Writers { get; set; }
    public DbSet<Serial> Serials { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
}