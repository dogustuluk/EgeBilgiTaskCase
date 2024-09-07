﻿using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
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
    }
}
