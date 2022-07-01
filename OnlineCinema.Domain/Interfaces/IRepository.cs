namespace OnlineCinema.Domain;

public interface IRepository<T>
{
    
    IEnumerable<T> Data { get; }
    void AddData(T data);
    void AddData(IEnumerable<T> data);
    
    Task AddDataAsync(T data, CancellationToken cancellationToken = default);
    Task AddDataAsync(IEnumerable<T> data, CancellationToken cancellationToken = default);
    
    void ChangeData(int id, T data);
    void ChangeData(int id, IEnumerable<T> data);
}