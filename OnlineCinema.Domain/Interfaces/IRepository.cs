namespace OnlineCinema.Domain;

public interface IRepository<T>
{
    IEnumerable<T> Data { get; }

    Task AddDataAsync(T data, CancellationToken cancellationToken = default);
    Task AddDataAsync(IEnumerable<T> data, CancellationToken cancellationToken = default);

    Task ChangeDataAsync(int id, T data);
    Task ChangeDataAsync(IEnumerable<T> data);
}