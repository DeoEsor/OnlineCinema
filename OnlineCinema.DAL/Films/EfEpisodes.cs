using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfEpisodes : IEpisodes
{
    public readonly FilmsContext Context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfEpisodes(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        Context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }
    
    public async Task AddAsync(Episode item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await Context.Episodes.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Episode item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        Context.Episodes.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Episode>> GetListAsync()
    {
        return await Context.Episodes
            .OrderBy(p => p.EpisodeName)
            .ToListAsync();
    }

    public async Task<Episode> ChangeAsync(Episode Episode)
    {
        if (Episode == null) throw new ArgumentNullException(nameof(Episode));

        var item = Context.Episodes.FirstOrDefault(item => item.Id == Episode.Id)
                   ?? throw new ArgumentException("No such Episode in db", nameof(Episode));

        item.Cast = Episode.Cast;
        item.Directors = Episode.Directors;
        item.Writers = Episode.Writers;
        item.SerialName = Episode.SerialName;
        item.Description = Episode.Description;
        item.PosterSource = Episode.PosterSource;
        await _unit.Commit();

        return Episode;
    }

    public async Task<IReadOnlyList<Episode>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await Context.Episodes
            .Where(p => p.SerialName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.SerialName.LevenshteinDistance(name))
            .Take(limit) // Bug mb incorrect
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Episode>> FindBySeason(Season season)
    {
        if (season == null) throw new ArgumentNullException(nameof(season));

        return Context.Seasons
            .First(s => s == season)
            .Episodes
            .ToList();
    }

    public async Task<IReadOnlyList<Episode>> FindBySerial(Serial serial)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        return Context.Serials
            .First(s => s == serial)
            .Seasons
            .SelectMany(s => s.Episodes)
            .ToList();
    }

    public async Task<Episode> FindBySeason(Season season, int episodeNumber)
    {
        if (season == null) throw new ArgumentNullException(nameof(season));

        return Context.Seasons
            .First(s => s == season)
            .Episodes
            .FirstOrDefault(s => s.EpisodeNumber == episodeNumber)
            ?? throw new InvalidOperationException("No such episode in serial");
    }
}