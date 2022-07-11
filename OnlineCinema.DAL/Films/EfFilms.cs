using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfFilms : IFilms
{
    public readonly FilmsContext Context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfFilms(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        Context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }

    public async Task AddAsync(Film item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await Context.Films.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Film item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        Context.Films.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Film>> GetListAsync()
    {
        return await Context.Films
            .OrderBy(p => p.FilmName)
            .ToListAsync();
    }

    public async Task<Film> ChangeAsync(Film item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        var film = Context.Films.FirstOrDefault(film => film.Id == item.Id)
                      ?? throw new ArgumentException("No such Film in db", nameof(item));

        film.Cast = item.Cast;
        film.Description = item.Description;
        film.FilmName = item.FilmName;
        film.Writers = item.Writers;
        film.Directors = item.Directors;
        film.MagnetLink = item.MagnetLink;
        
        await _unit.Commit();

        return film;
    }

    public async Task<IReadOnlyList<Film>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await Context.Films
            .Where(p => p.FilmName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.FilmName.LevenshteinDistance(name))
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Film>> FindByGenreAsync(Genre genres, int limit = Int32.MaxValue)
    {

        return await Context.Films
            .Where(p => p.Genres == genres)
            .OrderBy(p => p.FilmName)
            .Take(limit)
            .ToListAsync();
    }
}