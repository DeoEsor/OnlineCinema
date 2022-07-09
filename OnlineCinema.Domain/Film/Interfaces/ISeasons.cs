namespace OnlineCinema.Domain.Interfaces;

public interface ISeasons : INameFindRepository<Season>
{
    Task<IReadOnlyList<Season>> FindBySerial(Serial serial);
    Task<Season> FindBySerial(Serial serial, int seasonNumber);
}