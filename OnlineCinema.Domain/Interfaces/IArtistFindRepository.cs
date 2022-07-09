namespace OnlineCinema.Domain.Interfaces;

public interface IArtistFindRepository<T> : INameFindRepository<T>
    where T: class
{
    Task<IReadOnlyList<T>> FindBySerialAsync(Serial serial, int limit = int.MaxValue);
    Task<IReadOnlyList<T>> FindBySerialAsync(IReadOnlyCollection<Serial> serials, int limit = int.MaxValue);
    Task<IReadOnlyList<T>> FindByFilmAsync(Film serial, int limit = int.MaxValue);
    Task<IReadOnlyList<T>> FindByFilmAsync(IReadOnlyCollection<Serial> films, int limit = int.MaxValue);
}