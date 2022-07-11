using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain.User.Interfaces;

public interface IUsers : IRepository<User>
{
    Task<User> FindByGuidAsync(int id);
    Task<User> FindByUsernameAsync(string username);
    Task<IReadOnlyList<User>> FindByNameAsync(string fullname);
    Task<User> FindByEmailAsync(string email);
}