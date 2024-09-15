using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Commands.User.CreateUser;
using EgeBilgiTaskCase.Application.Features.Queries.User.GetByIdOrGuidUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.API.Controllers
{
    [Route("api/User/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("CreateUser")]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            OptResult<CreateUserCommandResponse> response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        [Route("GetByIdOrGuidUser")]
        public async Task<IActionResult> GetByIdOrGuidUser([FromQuery] GetByIdOrGuidUserQueryRequest request)
        {
            OptResult<GetByIdOrGuidUserQueryResponse> response = await _mediator.Send(request);
            return Ok(response);
        }

    }
}
