using EgeBilgiTaskCase.Application.Abstractions.Services.Common;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetDataListEpisodes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.API.Controllers
{
    [Route("api/Common/[controller]")]
    [ApiController]
    public class EpisodeController : ControllerBase
    {
        private readonly IEpisodeService _episodeService;
        private readonly IMediator _mediator;

        public EpisodeController(IEpisodeService episodeService, IMediator mediator)
        {
            _episodeService = episodeService;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetDataListEpisode")]
        public async Task<IActionResult> GetDataListDbParameter([FromQuery] GetDataListEpisodesQueryRequest request)
        {
            OptResult<List<GetDataListEpisodesQueryResponse>> response = await _mediator.Send(request);
            var convertedData = response.Data.Select(x => new DataList1
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToList();
            return Ok(convertedData);

        }

        [HttpPost("episode")]
        public async Task<IActionResult> GetEpisodes()
        {
            try
            {
                var characters = _episodeService.SaveAllEpisodeToDatabase();
                return Ok(characters);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
            }
        }
    }
}
