namespace OnlineCinema.Domain.Person.Interfaces;

public interface IUsers
{
    Task Add(User user);
    Task<IReadOnlyList<User>> GetListAsync();
    Task<User> ChangeAsync(User user);
}