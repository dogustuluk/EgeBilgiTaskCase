using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Common
{
    public interface IEpisodeService
    {
        Task SaveAllEpisodeToDatabase();
        Task SaveEpisodeToDatabase(EpisodeDto episodeDto);
    }
}
