using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Common
{
    public interface IRickAndMortyApiService
    {
        Task<CharacterListDto> GetCharactersAsync(int pageNumber);
        Task<LocationListDto> GetLocationsAsync(int pageNumber);
        Task<EpisodeListDto> GetEpisodesAsync(int pageNumber);

    }
}
