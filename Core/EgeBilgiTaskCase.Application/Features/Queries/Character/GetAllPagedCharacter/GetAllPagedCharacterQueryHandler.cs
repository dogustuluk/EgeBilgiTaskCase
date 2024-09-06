using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter
{
    public class GetAllPagedCharacterQueryHandler : IRequestHandler<GetAllPagedCharacterQueryRequest, OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>>>
    {
        private readonly ICharacterService _characterService;
        private readonly IMapper _mapper;

        public GetAllPagedCharacterQueryHandler(ICharacterService characterService, IMapper mapper)
        {
            _characterService = characterService;
            _mapper = mapper;
        }

        public async Task<OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>>> Handle(GetAllPagedCharacterQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var model = _mapper.Map<Character_Index_Dto>(request);

                var result = await _characterService.GetAllPagedCharacterAsync(model);
                var response = _mapper.Map<PaginatedList<GetAllPagedCharacterQueryResponse>>(result.Data);

                if (result == null) return await OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>>.FailureAsync(result.Messages);

                return await OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>>.SuccessAsync(response, Messages.Successfull);
            });
        }
    }
}
