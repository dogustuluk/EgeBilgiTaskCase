using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;

namespace EgeBilgiTaskCase.Application.Features.Commands.Character.AddNewCharacter
{
    public class AddNewCharacterCommandRequest : IRequest<OptResult<AddNewCharacterCommandResponse>>
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public CharacterDetail_AddNew_Dto? CharacterDetail { get; set; }
    }
}