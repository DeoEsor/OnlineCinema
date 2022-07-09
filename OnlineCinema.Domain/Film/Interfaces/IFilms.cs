namespace OnlineCinema.Domain.Interfaces;

public interface IFilms : INameFindRepository<Film>, IGenreFindRepository<Film>
{
    
}