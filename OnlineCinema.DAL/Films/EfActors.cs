using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfActors : IActors
{
    private readonly FilmsContext _context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfActors(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        _context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }

     public async Task AddAsync(Actor item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await _context.Actors.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Actor item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.Actors.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Actor>> GetListAsync()
    {
        return await _context.Actors
            .OrderBy(p => p.PersonalName)
            .ThenBy(p => p.PersonalName)
            .ToListAsync();
    }

    public async Task<Actor> ChangeAsync(Actor Actor)
    {
        if (Actor == null) throw new ArgumentNullException(nameof(Actor));

        var persona = _context.Actors.FirstOrDefault(item => item.Id == Actor.Id)
                      ?? throw new ArgumentException("No such Actor in db", nameof(Actor));

        persona.Update(Actor);
        await _unit.Commit();

        return persona;
    }

    public async Task<IReadOnlyList<Actor>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await _context.Actors
            .Where(p => p.PersonalName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.PersonalName.LevenshteinDistance(name))
            .Take(limit) // Bug mb incorrect
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Actor>> FindBySerialAsync(Serial serial, int limit = Int32.MaxValue)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        return serial.Cast
            .OrderBy(p => p.PersonalName)
            .Take(limit)
            .ToList(); // Bug mb incorrect
    }

    public async Task<IReadOnlyList<Actor>> FindBySerialAsync(IReadOnlyCollection<Serial> serials, int limit = Int32.MaxValue)
    {
        if (serials == null) throw new ArgumentNullException(nameof(serials));

        var list = new List<Actor>();
        
        foreach (var serial in serials)
            list.AddRange(serial.Cast
                .OrderBy(p => p.PersonalName));

        return list;
    }

    public async Task<IReadOnlyList<Actor>> FindByFilmAsync(Film film, int limit = Int32.MaxValue)
    {
        if (film == null) throw new ArgumentNullException(nameof(film));

        return film.Cast
            .OrderBy(p => p.PersonalName)
            .ToList();
    }

    public async Task<IReadOnlyList<Actor>> FindByFilmAsync(IReadOnlyCollection<Serial> films, int limit = Int32.MaxValue)
    {
        if (films == null) throw new ArgumentNullException(nameof(films));

        var list = new List<Actor>();
        
        foreach (var film in films)
            list.AddRange(film.Cast
                .OrderBy(p => p.PersonalName));

        return list;
    }
}