using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetAllDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetByIdorGuidDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetPagedDbParameter;
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
        [HttpGet]
        [Route("GetPagedData")]
        public async Task<IActionResult> GetAllPagedData([FromQuery] GetPagedDbParameterQueryRequest request)
        {
            OptResult<PaginatedList<GetPagedDbParameterQueryResponse>> responsePaginatedData = await _mediator.Send(request);
            return Ok(responsePaginatedData);
        }
        [HttpGet]
        [Route("GetAllDbParameter")]
        public async Task<IActionResult> GetAllDbParameter([FromQuery] GetAllDbParameterQueryRequest request)
        {
            OptResult<List<GetAllDbParameterQueryResponse>> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetByIdOrGuidDbParameterType")]
        public async Task<IActionResult> GetByIdOrGuidDbParameterType([FromQuery] GetByIdOrGuidDbParameterQueryRequest request)
        {
            OptResult<GetByIdOrGuidDbParameterQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetValueDbParameter")]
        public async Task<IActionResult> GetValueDbParameter([FromQuery] GetValueXQueryRequest request)
        {
            OptResult<GetValueXQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
