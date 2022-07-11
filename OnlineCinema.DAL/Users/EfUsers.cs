using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.User;
using OnlineCinema.Domain.User.Interfaces;

namespace OnlineCinema.DAL.Users;

public class EfUsers : IUsers
{
    public readonly UsersContext Context;

    public EfUsers(UnitOfWork<UsersContext> usersUnitOfWork)
    {
        if (usersUnitOfWork == null) throw new ArgumentNullException(nameof(usersUnitOfWork));

        Context = usersUnitOfWork.Context;
    }

    public async Task AddAsync(User item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await Context.Users.AddAsync(item);
        await Context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        Context.Users.Remove(item);
        await Context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<User>> GetListAsync()
    {
        return await Context.Users
            .OrderBy(p => p.PersonalName)
            .ThenBy(p => p.PersonalName)
            .ToListAsync();
    }

    public async Task<User> ChangeAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var persona = Context.Users.FirstOrDefault(item => item.Id == user.Id)
                      ?? throw new ArgumentException("No such user in db", nameof(user));

        persona.DateOfBirth = user.DateOfBirth;
        persona.PersonalName = user.PersonalName;
        await Context.SaveChangesAsync();

        return persona;
    }

    public async Task<User> FindByGuidAsync(int id)
    {
        return Context.Users.FirstOrDefault(user => user.Id == id)
               ?? throw new ArgumentException("Not found user with such id", nameof(id));
    }

    public async Task<User> FindByUsernameAsync(string username)
    {
        return Context.Users.FirstOrDefault(user => Equals(user.Username, username))
               ?? throw new ArgumentException("Not found user with such username", nameof(username));
    }

    public async Task<IReadOnlyList<User>> FindByNameAsync(string fullname)
    {
        if (fullname == null) throw new ArgumentNullException(nameof(fullname));

        return await Context.Users
            .Where(p => p.PersonalName.LevenshteinDistance(fullname) < 10)
            .OrderBy(p => p.PersonalName.LevenshteinDistance(fullname))
            .ToListAsync();
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));

        return Context.Users.FirstOrDefault(user => Equals(user.Email, email))
               ?? throw new ArgumentException("Not found user with such email", nameof(email));
    }
}