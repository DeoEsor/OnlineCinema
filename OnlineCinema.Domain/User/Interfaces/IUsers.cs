using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.Domain.User.Interfaces;

public interface IUsers : IRepository<User>
{
    Task<User> FindByGuidAsync(Guid id);
    Task<User> FindByUsernameAsync(Guid id);
    Task<IReadOnlyList<User>> FindByNameAsync(string fullname);
    Task<User> FindByEmailAsync(string email);
}