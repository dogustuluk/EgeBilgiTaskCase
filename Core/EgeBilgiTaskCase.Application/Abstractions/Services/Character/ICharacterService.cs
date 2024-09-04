using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Character
{
    public interface ICharacterService
    {
        Task<OptResult<PaginatedList<Domain.Entities.Character.Character>>> GetAllPagedCharacterAsync(Character_Index_Dto model);

        Task SaveCharacterToDatabase(CharacterDto characterDto);
        Task SaveAllCharactersToDatabase();
    }
}
