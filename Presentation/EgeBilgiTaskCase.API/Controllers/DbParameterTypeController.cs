using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllDbParameterType;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllPagedDbParameterType;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetByIdOrGuidDbParameterType;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.API.Controllers
{
    [Route("api/Management/[controller]")]
    [ApiController]
    public class DbParameterTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DbParameterTypeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("GetAllDbParameterType")]
        public async Task<IActionResult> GetAllDbParameterType([FromQuery] GetAllDbParameterTypeQueryRequest request)
        {
            OptResult<List<GetAllDbParameterTypeQueryResponse>> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetAllPagedData")]
        public async Task<IActionResult> GetAllPagedData([FromQuery] GetAllPagedDbParameterTypeQueryRequest request)
        {
            OptResult<PaginatedList<GetAllPagedDbParameterTypeQueryResponse>> responsePaginatedData = await _mediator.Send(request);
            return Ok(responsePaginatedData);
        }
        [HttpGet]
        [Route("GetByIdOrGuidDbParameterType")]
        public async Task<IActionResult> GetByIdOrGuidDbParameterType([FromQuery] GetByIdOrGuidDbParameterTypeQueryRequest request)
        {
            OptResult<GetByIdOrGuidDbParameterTypeQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetDataListDbParameterType")]
        public async Task<IActionResult> GetDataListDbParameterType([FromQuery] GetDataListXQueryRequest request)
        {
            OptResult<List<GetDataListXQueryResponse>> response = await _mediator.Send(request);
            // return Ok(response);
            var convertedData = response.Data.Select(x => new DataList1
            {
                Id = x.Id.ToString(),
                Name = x.Name
            }).ToList();
            return Ok(convertedData);

        }
        [HttpGet]
        [Route("GetValueDbParameterType")]
        public async Task<IActionResult> GetValueDbParameterType([FromQuery] GetValueXQueryRequest request)
        {
            OptResult<GetValueXQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
