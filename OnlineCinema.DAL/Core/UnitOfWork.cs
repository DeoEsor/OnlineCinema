using Microsoft.EntityFrameworkCore;

namespace OnlineCinema.DAL.Core;

public class UnitOfWork <TContext> : IDisposable
where TContext : DbContext
{
    private readonly TContext _context;

    private bool _disposed;

    protected UnitOfWork()
    {
    }

    public UnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    internal TContext Context
    {
        get
        {
            if (_disposed) throw new ObjectDisposedException(GetType().FullName);
            return _context;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task Commit()
    {
        if (_disposed) throw new ObjectDisposedException(GetType().FullName);

        await _context.SaveChangesAsync();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed) return;

        if (disposing) _context.Dispose();
        _disposed = true;
    }

    ~UnitOfWork()
    {
        Dispose(false);
    }
}