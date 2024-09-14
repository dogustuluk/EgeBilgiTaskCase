using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetValueDbParameterType
{
    public class GetValueDbParameterTypeQueryHandler : IRequestHandler<GetValueXQueryRequest, OptResult<GetValueXQueryResponse>>
    {
        private readonly IDbParameterTypeService _dbParameterTypeService;
        private readonly IMapper _mapper;

        public GetValueDbParameterTypeQueryHandler(IDbParameterTypeService dbParameterTypeService, IMapper mapper)
        {
            _dbParameterTypeService = dbParameterTypeService;
            _mapper = mapper;
        }

        public async Task<OptResult<GetValueXQueryResponse>> Handle(GetValueXQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var data = await _dbParameterTypeService.GetValue("", request.ColumnName, $"\"Id\" = {request.DataId}", 1);
                var mappedData = _mapper.Map<GetValueXQueryResponse>(data);
                if (data == null)
                    return await OptResult<GetValueXQueryResponse>.FailureAsync(Messages.NullData);

                return await OptResult<GetValueXQueryResponse>.SuccessAsync(mappedData, Messages.Successfull);
            });
        }
    }
}
