﻿using Microsoft.EntityFrameworkCore;
using OnlineCinema.DAL.Core;
using OnlineCinema.Domain;
using OnlineCinema.Domain.Extensions;
using OnlineCinema.Domain.Interfaces;

namespace OnlineCinema.DAL;

public class EfSerials : ISerials
{
    public readonly FilmsContext Context;
    private readonly UnitOfWork<FilmsContext> _unit;

    public EfSerials(UnitOfWork<FilmsContext> filmsUnitOfWork)
    {
        if (filmsUnitOfWork == null) throw new ArgumentNullException(nameof(filmsUnitOfWork));

        Context = filmsUnitOfWork.Context;
        _unit = filmsUnitOfWork;
    }
    
    public async Task AddAsync(Serial item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        await Context.Serials.AddAsync(item);
        await _unit.Commit();
    }

    public async Task DeleteAsync(Serial item)
    {
        if (item == null) throw new ArgumentNullException(nameof(item));

        Context.Serials.Remove(item);
        await _unit.Commit();
    }

    public async Task<IReadOnlyList<Serial>> GetListAsync()
    {
        return await Context.Serials
            .OrderBy(p => p.SerialName)
            .ToListAsync();
    }

    public async Task<Serial> ChangeAsync(Serial serial)
    {
        if (serial == null) throw new ArgumentNullException(nameof(serial));

        var item = Context.Serials.FirstOrDefault(item => item.Id == serial.Id)
                      ?? throw new ArgumentException("No such Serial in db", nameof(serial));

        item.Cast = serial.Cast;
        item.Directors = serial.Directors;
        item.Writers = serial.Writers;
        item.SerialName = serial.SerialName;
        item.Description = serial.Description;
        item.Genres = serial.Genres;
        item.PosterSource = serial.PosterSource;
        await _unit.Commit();

        return serial;
    }

    public async Task<IReadOnlyList<Serial>> FindByNameAsync(string name, int limit = Int32.MaxValue)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        return await Context.Serials
            .Where(p => p.SerialName.LevenshteinDistance(name) < 10)
            .OrderBy(p => p.SerialName.LevenshteinDistance(name))
            .Take(limit) // Bug mb incorrect
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Serial>> FindByGenreAsync(Genre genres, int limit = Int32.MaxValue)
    {
        return await Context.Serials
            .Where(p => p.Genres == genres)
            .OrderBy(p => p.SerialName)
            .Take(limit)
            .ToListAsync();
    }
}