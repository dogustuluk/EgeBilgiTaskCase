using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Common.Extensions;

namespace EgeBilgiTaskCase.Application.Features.Commands.Character.AddNewCharacter
{
    public class AddNewCharacterCommandHandler : IRequestHandler<AddNewCharacterCommandRequest, OptResult<AddNewCharacterCommandResponse>>
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public AddNewCharacterCommandHandler(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        public async Task<OptResult<AddNewCharacterCommandResponse>> Handle(AddNewCharacterCommandRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var mappedDto = _mapper.Map<Character_AddNew_Dto>(request);
                var data = await _characterService.AddNewAsync(mappedDto);
                if (!data.Succeeded)
                    return await OptResult<AddNewCharacterCommandResponse>.FailureAsync(data.Messages);
                var response = _mapper.Map<AddNewCharacterCommandResponse>(data.Data);
                return await OptResult<AddNewCharacterCommandResponse>.SuccessAsync(response);
            });
        }
    }
}
