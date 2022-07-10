using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfSeasons : ISeasons
{
    private readonly FilmsContext _context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfSeasons(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        _context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }
    
    public async Task AddAsync(Season item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await _context.Seasons.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Season item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        _context.Seasons.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Season>> GetListAsync()
    {
        return await _context.Seasons
            .OrderBy(p => p.SerialName)
            .ToListAsync();
    }

    public async Task<Season> ChangeAsync(Season Season)
    {
        if (Season == null) throw new ArgumentNullException(nameof(Season));

        var item = _context.Seasons.FirstOrDefault(item => item.Id == Season.Id)
                      ?? throw new ArgumentException("No such Season in db", nameof(Season));

        item.Cast = Season.Cast;
        item.Directors = Season.Directors;
        item.Writers = Season.Writers;
        item.SerialName = Season.SerialName;
        item.Description = Season.Description;
        item.PosterSource = Season.PosterSource;
        await _unit.Commit();

        return Season;
    }

    public async Task<IReadOnlyList<Season>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await _context.Seasons
            .Where(p => p.SerialName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.SerialName.LevenshteinDistance(name))
            .Take(limit) // Bug mb incorrect
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Season>> FindBySerial(Serial serial)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        return await _context.Seasons
            .Where(p => p.SerialName == serial.SerialName)
            .ToListAsync();
    }

    public async Task<Season> FindBySerial(Serial serial, int seasonNumber)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        return _context.Seasons
            .Where(p => p.SerialName == serial.SerialName)
            .FirstOrDefault(p => p.SeasonNumber == seasonNumber)
            ?? throw new ArgumentException("Such season is not founded", nameof(seasonNumber));
    }
}