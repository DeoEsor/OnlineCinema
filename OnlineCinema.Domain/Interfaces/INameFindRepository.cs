namespace OnlineCinema.Domain.Interfaces;

public interface INameFindRepository<T> : IRepository<T>
where T :class
{
    Task<IReadOnlyList<T>> FindByNameAsync(string name, int limit = int.MaxValue);
}