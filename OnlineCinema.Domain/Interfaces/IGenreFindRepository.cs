namespace OnlineCinema.Domain.Interfaces;

public interface IGenreFindRepository <T> : IRepository<T>
    where T: class
{
    Task<IReadOnlyList<T>> FindByGenreAsync(Genre genres, int limit = int.MaxValue);
}