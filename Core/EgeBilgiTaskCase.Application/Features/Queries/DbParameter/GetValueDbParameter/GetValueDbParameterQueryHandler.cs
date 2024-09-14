using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetValueDbParameter;
public class GetValueDbParameterQueryHandler : IRequestHandler<GetValueXQueryRequest, OptResult<GetValueXQueryResponse>>
{
    private readonly IDbParameterService _dbParameterService;
    private readonly IMapper _mapper;

    public GetValueDbParameterQueryHandler(IDbParameterService dbParameterService, IMapper mapper)
    {
        _dbParameterService = dbParameterService;
        _mapper = mapper;
    }

    public async Task<OptResult<GetValueXQueryResponse>> Handle(GetValueXQueryRequest request, CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            var data = await _dbParameterService.GetValue("", request.ColumnName, $"\"Id\" = {request.DataId}", 1);
            var mappedData = _mapper.Map<GetValueXQueryResponse>(data);
            if (data == null)
                return await OptResult<GetValueXQueryResponse>.FailureAsync(Messages.NullData);

            return await OptResult<GetValueXQueryResponse>.SuccessAsync(mappedData, Messages.Successfull);
        });
    }
}
