using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain.Core;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.User;
using OnlineCinema.Domain.User.Interfaces;

namespace OnlineCinema.DAL.Users;

public class EfUsers : IUsers
{
    private readonly UsersContext _context;

    public EfUsers(UnitOfWork<UsersContext> usersUnitOfWork)
    {
        if (usersUnitOfWork == null) throw new ArgumentNullException(nameof(usersUnitOfWork));

        _context = usersUnitOfWork.Context;
    }

    public async Task AddAsync(User item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await _context.Users.AddAsync(item);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(User item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.Users.Remove(item);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<User>> GetListAsync()
    {
        return await _context.Users
            .OrderBy(p => p.PersonalName.LastName.Value)
            .ThenBy(p => p.PersonalName.FirstName.Value)
            .ToListAsync();
    }

    public async Task<User> ChangeAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));

        var persona = _context.Users.FirstOrDefault(item => item.Id == user.Id)
                      ?? throw new ArgumentException("No such user in db", nameof(user));

        persona.DateOfBirth = user.DateOfBirth;
        persona.PersonalName = user.PersonalName;
        await _context.SaveChangesAsync();

        return persona;
    }

    public async Task<IReadOnlyList<User>> GetOlderThan(Age age)
    {
        if (age == null) throw new ArgumentNullException(nameof(age));

        return await _context.Users
            .Where(p => p.Age.Value > age.Value)
            .OrderBy(p => p.PersonalName.LastName.Value)
            .ThenBy(p => p.PersonalName.FirstName.Value)
            .ToListAsync();
    }

    public async Task<User> FindByGuidAsync(Guid id)
    {
        return _context.Users.FirstOrDefault(user => user.Id == id)
               ?? throw new ArgumentException("Not found user with such id", nameof(id));
    }

    public async Task<User> FindByUsernameAsync(string username)
    {
        return _context.Users.FirstOrDefault(user => Equals(user.Username, username))
               ?? throw new ArgumentException("Not found user with such username", nameof(username));
    }

    public async Task<IReadOnlyList<User>> FindByNameAsync(string fullname)
    {
        if (fullname == null) throw new ArgumentNullException(nameof(fullname));

        return await _context.Users
            .Where(p => p.PersonalName.FullName.LevenshteinDistance(fullname) < 10)
            .OrderBy(p => p.PersonalName.FullName.LevenshteinDistance(fullname))
            .ToListAsync();
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        if (email == null) throw new ArgumentNullException(nameof(email));

        return _context.Users.FirstOrDefault(user => Equals(user.Email, email))
               ?? throw new ArgumentException("Not found user with such email", nameof(email));
    }
}