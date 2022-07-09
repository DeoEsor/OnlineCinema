namespace OnlineCinema.Domain.Interfaces;

public interface IRepository<T>
where T : class
{
    Task AddAsync(T item);
    Task DeleteAsync(T item);
    Task<IReadOnlyList<T>> GetListAsync();
    Task<T> ChangeAsync(T item);
}