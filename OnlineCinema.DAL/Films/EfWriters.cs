using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfWriters : IWriters
{
    private readonly FilmsContext _context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfWriters(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        _context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }
    
    public async Task AddAsync(Writer item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await _context.Writers.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Writer item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.Writers.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Writer>> GetListAsync()
    {
        return await _context.Writers
            .OrderBy(p => p.PersonalName)
            .ThenBy(p => p.PersonalName)
            .ToListAsync();
    }

    public async Task<Writer> ChangeAsync(Writer Writer)
    {
        if (Writer == null) throw new ArgumentNullException(nameof(Writer));

        var persona = _context.Writers.FirstOrDefault(item => item.Id == Writer.Id)
                      ?? throw new ArgumentException("No such Writer in db", nameof(Writer));

        persona.DateOfBirth = Writer.DateOfBirth;
        persona.PersonalName = Writer.PersonalName;
        await _unit.Commit();

        return persona;
    }

    public async Task<IReadOnlyList<Writer>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await _context.Writers
            .Where(p => p.PersonalName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.PersonalName.LevenshteinDistance(name))
            .Take(limit) // Bug mb incorrect
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Writer>> FindBySerialAsync(Serial serial, int limit = Int32.MaxValue)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        return serial.Writers
            .OrderBy(p => p.PersonalName)
            .Take(limit)
            .ToList(); // Bug mb incorrect
    }

    public async Task<IReadOnlyList<Writer>> FindBySerialAsync(IReadOnlyCollection<Serial> serials, int limit = Int32.MaxValue)
    {
        if (serials == null) throw new ArgumentNullException(nameof(serials));

        var list = new List<Writer>();
        
        foreach (var serial in serials)
            list.AddRange(serial.Writers
                .OrderBy(p => p.PersonalName));

        return list;
    }

    public async Task<IReadOnlyList<Writer>> FindByFilmAsync(Film film, int limit = Int32.MaxValue)
    {
        if (film == null) throw new ArgumentNullException(nameof(film));

        return film.Writers
            .OrderBy(p => p.PersonalName)
            .ToList();
    }

    public async Task<IReadOnlyList<Writer>> FindByFilmAsync(IReadOnlyCollection<Serial> films, int limit = Int32.MaxValue)
    {
        if (films == null) throw new ArgumentNullException(nameof(films));

        var list = new List<Writer>();
        
        foreach (var film in films)
            list.AddRange(film.Writers
                .OrderBy(p => p.PersonalName));

        return list;
    }
}