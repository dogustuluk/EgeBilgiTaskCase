using EgeBilgiTaskCase.Application.Common.DTOs.User;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;
using HospitalManagement.Application.Abstractions.Services.Users;

namespace EgeBilgiTaskCase.Application.Features.Queries.User.GetAllPagedUser;
public class GetAllPagedUserQueryHandler : IRequestHandler<GetAllPagedUserQueryRequest, OptResult<PaginatedList<GetAllPagedUserQueryResponse>>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetAllPagedUserQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<OptResult<PaginatedList<GetAllPagedUserQueryResponse>>> Handle(GetAllPagedUserQueryRequest request, CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            var model = _mapper.Map<GetAllPagedUser_Index_Dto>(request);

            var result = await _userService.GetAllPagedListAsync(model);
            var response = _mapper.Map<PaginatedList<GetAllPagedUserQueryResponse>>(result.Data);

            if (!result.Succeeded)
                return await OptResult<PaginatedList<GetAllPagedUserQueryResponse>>.FailureAsync(result.Messages);

            return await OptResult<PaginatedList<GetAllPagedUserQueryResponse>>.SuccessAsync(response, Messages.Successfull);
        });
    }
}
