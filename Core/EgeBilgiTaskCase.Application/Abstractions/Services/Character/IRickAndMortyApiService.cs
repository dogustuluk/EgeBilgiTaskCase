using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Character
{
    public interface IRickAndMortyApiService
    {
        Task<CharacterListDto> GetCharactersAsync(int pageNumber);
        //Task<List<Location>> GetLocationsAsync();
        //Task<List<Episode>> GetEpisodesAsync();

    }
}
