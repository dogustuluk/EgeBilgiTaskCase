using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.API.Controllers
{
    [Route("api/Management/[controller]")]
    [ApiController]
    public class DbParameterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DbParameterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("GetDataListDbParameter")]
        public async Task<IActionResult> GetDataListDbParameter([FromQuery] GetDataListDbParameterQueryRequest request)
        {
            OptResult<List<GetDataListDbParameterQueryResponse>> response = await _mediator.Send(request);
            var convertedData = response.Data.Select(x => new DataList1
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToList();
            return Ok(convertedData);

        }
    }
}
