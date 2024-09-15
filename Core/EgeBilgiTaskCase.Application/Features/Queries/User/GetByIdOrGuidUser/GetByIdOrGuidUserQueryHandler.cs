using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;
using HospitalManagement.Application.Abstractions.Services.Users;

namespace EgeBilgiTaskCase.Application.Features.Queries.User.GetByIdOrGuidUser;
public class GetByIdOrGuidUserQueryHandler : IRequestHandler<GetByIdOrGuidUserQueryRequest, OptResult<GetByIdOrGuidUserQueryResponse>>
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public GetByIdOrGuidUserQueryHandler(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public async Task<OptResult<GetByIdOrGuidUserQueryResponse>> Handle(GetByIdOrGuidUserQueryRequest request, CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            object value = null;
            if (request.Id != null) value = request.Id;
            if (request.Guid != null) value = request.Guid;
            var data = await _userService.GetByIdOrGuid(value);

            var mappedData = _mapper.Map<GetByIdOrGuidUserQueryResponse>(data.Data);
            if (mappedData == null) return await OptResult<GetByIdOrGuidUserQueryResponse>.FailureAsync(Messages.NullData);

            return await OptResult<GetByIdOrGuidUserQueryResponse>.SuccessAsync(mappedData, Messages.Successfull);
        });
    }
}
