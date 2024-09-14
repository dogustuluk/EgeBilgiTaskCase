using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetByIdorGuidDbParameter;
public class GetByIdOrGuidDbParameterQueryHandler : IRequestHandler<GetByIdOrGuidDbParameterQueryRequest, OptResult<GetByIdOrGuidDbParameterQueryResponse>>
{
    public readonly IDbParameterService _dbParameterService;
    private readonly IMapper _mapper;

    public GetByIdOrGuidDbParameterQueryHandler(IDbParameterService dbParameterService, IMapper mapper)
    {
        _dbParameterService = dbParameterService;
        _mapper = mapper;
    }

    public async Task<OptResult<GetByIdOrGuidDbParameterQueryResponse>> Handle(GetByIdOrGuidDbParameterQueryRequest request, CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            object value = null;
            if (request.Id != null) value = request.Id;
            if (request.Guid != null) value = request.Guid;
            var data = await _dbParameterService.GetByIdOrGuid(value);

            var mappedData = _mapper.Map<GetByIdOrGuidDbParameterQueryResponse>(data.Data);
            if (mappedData == null) return await OptResult<GetByIdOrGuidDbParameterQueryResponse>.FailureAsync(Messages.NullData);

            return await OptResult<GetByIdOrGuidDbParameterQueryResponse>.SuccessAsync(mappedData, Messages.Successfull);
        });
    }
}
