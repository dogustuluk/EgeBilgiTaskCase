using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.API.Controllers
{
    [Route("api/Character/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly IRickAndMortyApiService _rickAndMortyApiService;
        private readonly ICharacterService _characterService;
        private readonly IMediator _mediator;
        public CharacterController(IRickAndMortyApiService rickAndMortyApiService, ICharacterService characterService, IMediator mediator)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _characterService = characterService;
            _mediator = mediator;
        }
        
        [HttpPost("characters")]
        public async Task<IActionResult> GetCharacters()
        {
            try
            {
                var characters = _characterService.SaveAllCharactersToDatabase();
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllPagedCharacter")]
        public async Task<IActionResult> GetAllPagedCharacter([FromQuery] GetAllPagedCharacterQueryRequest request)
        {
            OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
