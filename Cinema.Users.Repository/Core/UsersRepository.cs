using Cinema.Users.Repository.Contexts;
using OnlineCinema.Domain;

namespace Cinema.Users.Repository.Core;

public class UsersRepository : IRepository<User>, IDisposable, IAsyncDisposable
{

    public UsersRepository(UsersDb db)
    {
        UsersDb = db;
    }
    
    private UsersDb UsersDb { get; set; } //DI-Container Transit 

    public IEnumerable<User> Data => UsersDb.Users;

    public void AddData(User data)
    {
        UsersDb.Users.Add(data);
        UsersDb.SaveChanges();
    }

    public void AddData(IEnumerable<User> data)
    {
        foreach (var user in data)
        {
            if (UsersDb.Users.Any(u => u.Id == user.Id))
                throw new ArgumentException(nameof(user.Id));
            UsersDb.Users.Add(user);
        }
        UsersDb.SaveChanges();
    }

    public async Task AddDataAsync(User data, CancellationToken cancellationToken = default)
    {
        if (UsersDb.Users.Any(u => u.Id == data.Id))
            throw new ArgumentException(nameof(data.Id));
        await UsersDb.Users.AddAsync(data, cancellationToken);
        await UsersDb.SaveChangesAsync(cancellationToken);
    }

    public async Task AddDataAsync(IEnumerable<User> data, CancellationToken cancellationToken = default)
    {
        foreach (var user in data)
        {
            if (UsersDb.Users.Any(u => u.Id == user.Id))
                throw new ArgumentException(nameof(user.Id));
            await UsersDb.Users.AddAsync(user, cancellationToken);
        }
        await UsersDb.SaveChangesAsync(cancellationToken);
    }

    public void ChangeData(int id, User data)
    {
        throw new NotImplementedException();
    }

    public void ChangeData(int id, IEnumerable<User> data)
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
            UsersDb?.Dispose();
            UsersDb = null!;
        }
    }

    protected virtual async ValueTask DisposeAsyncCore()
    {
        if (UsersDb is not null)
            await UsersDb.DisposeAsync().ConfigureAwait(false);

        UsersDb = null;
    }

    #endregion

}