namespace OnlineCinema.Domain.Interfaces;

public interface IEpisodes : INameFindRepository<Episode>
{
    Task<IReadOnlyList<Episode>> FindBySeason(Season season);
    Task<IReadOnlyList<Episode>> FindBySerial(Serial serial);
    Task<Episode> FindBySeason(Season season, int episodeNumber);
}