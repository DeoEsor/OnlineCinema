using Cinema.Users.Repository.Contexts;
using OnlineCinema.Domain;

namespace Cinema.Users.Repository.Core;

public class FilmsRepository : IRepository<Film>, IDisposable, IAsyncDisposable
{

    public FilmsRepository(FilmsDb db)
    {
        FilmsDb = db;
    }
    
    private FilmsDb FilmsDb { get; set; } //DI-Container Transit 

    public IEnumerable<Film> Data => FilmsDb.Films;

    public void AddData(Film data)
    {
        FilmsDb.Films.Add(data);
        FilmsDb.SaveChanges();
    }

    public void AddData(IEnumerable<Film> data)
    {
        foreach (var user in data)
        {
            if (FilmsDb.Films.Any(u => u.Id == user.Id))
                throw new ArgumentException(nameof(user.Id));
            FilmsDb.Films.Add(user);
        }
        FilmsDb.SaveChanges();
    }

    public async Task AddDataAsync(Film data, CancellationToken cancellationToken = default)
    {
        if (FilmsDb.Films.Any(u => u.Id == data.Id))
            throw new ArgumentException(nameof(data.Id));
        await FilmsDb.Films.AddAsync(data, cancellationToken);
        await FilmsDb.SaveChangesAsync(cancellationToken);
    }

    public async Task AddDataAsync(IEnumerable<Film> data, CancellationToken cancellationToken = default)
    {
        foreach (var user in data)
        {
            if (FilmsDb.Films.Any(u => u.Id == user.Id))
                throw new ArgumentException(nameof(user.Id));
            await FilmsDb.Films.AddAsync(user, cancellationToken);
        }
        await FilmsDb.SaveChangesAsync(cancellationToken);
    }

    public void ChangeData(int id, Film data)
    {
        throw new NotImplementedException();
    }

    public void ChangeData(int id, IEnumerable<Film> data)
    {
        throw new NotImplementedException();
    }

    #region Dispose Region

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public async ValueTask DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);

        Dispose(disposing: false);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            FilmsDb?.Dispose();
            FilmsDb = null!;
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (FilmsDb is not null)
            await FilmsDb.DisposeAsync().ConfigureAwait(false);

        FilmsDb = null;
    }

    #endregion

}