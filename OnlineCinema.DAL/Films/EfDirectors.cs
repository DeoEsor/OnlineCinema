using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfDirectors : IDirectors
{
    private readonly FilmsContext _context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfDirectors(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        _context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }
    
    public async Task AddAsync(Director item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await _context.Directors.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Director item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.Directors.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Director>> GetListAsync()
    {
        return await _context.Directors
            .OrderBy(p => p.PersonalName)
            .ThenBy(p => p.PersonalName)
            .ToListAsync();
    }

    public async Task<Director> ChangeAsync(Director Director)
    {
        if (Director == null) throw new ArgumentNullException(nameof(Director));

        var persona = _context.Directors.FirstOrDefault(item => item.Id == Director.Id)
                      ?? throw new ArgumentException("No such Director in db", nameof(Director));

        persona.Country = Director.Country;
        persona.PersonalName = Director.PersonalName;
        await _unit.Commit();

        return persona;
    }

    public async Task<IReadOnlyList<Director>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await _context.Directors
            .Where(p => p.PersonalName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.PersonalName.LevenshteinDistance(name))
            .Take(limit) // Bug mb incorrect
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Director>> FindBySerialAsync(Serial serial, int limit = Int32.MaxValue)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        return serial.Directors
            .OrderBy(p => p.PersonalName)
            .Take(limit)
            .ToList(); // Bug mb incorrect
    }

    public async Task<IReadOnlyList<Director>> FindBySerialAsync(IReadOnlyCollection<Serial> serials, int limit = Int32.MaxValue)
    {
        if (serials == null) throw new ArgumentNullException(nameof(serials));

        var list = new List<Director>();
        
        foreach (var serial in serials)
            list.AddRange(serial.Directors
                .OrderBy(p => p.PersonalName));

        return list;
    }

    public async Task<IReadOnlyList<Director>> FindByFilmAsync(Film film, int limit = Int32.MaxValue)
    {
        if (film == null) throw new ArgumentNullException(nameof(film));

        return film.Directors
            .OrderBy(p => p.PersonalName)
            .ToList();
    }

    public async Task<IReadOnlyList<Director>> FindByFilmAsync(IReadOnlyCollection<Serial> films, int limit = Int32.MaxValue)
    {
        if (films == null) throw new ArgumentNullException(nameof(films));

        var list = new List<Director>();
        
        foreach (var film in films)
            list.AddRange(film.Directors
                .OrderBy(p => p.PersonalName));

        return list;
    }
}