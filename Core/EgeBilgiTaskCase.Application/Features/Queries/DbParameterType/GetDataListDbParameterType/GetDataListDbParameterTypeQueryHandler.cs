using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetDataListDbParameterType
{
    public class GetDataListDbParameterTypeQueryHandler : IRequestHandler<GetDataListXQueryRequest, OptResult<List<GetDataListXQueryResponse>>>
    {
        public readonly IDbParameterTypeService _dbParameterTypeService;
        private readonly IMapper _mapper;

        public GetDataListDbParameterTypeQueryHandler(IDbParameterTypeService dbParameterTypeService, IMapper mapper)
        {
            _dbParameterTypeService = dbParameterTypeService;
            _mapper = mapper;
        }

        public async Task<OptResult<List<GetDataListXQueryResponse>>> Handle(GetDataListXQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var myDataList = await _dbParameterTypeService.GetDataListAsync(request.Filter);

                if (myDataList == null) return await OptResult<List<GetDataListXQueryResponse>>.FailureAsync(Messages.NullValue);

                var response = _mapper.Map<List<GetDataListXQueryResponse>>(myDataList);

                return await OptResult<List<GetDataListXQueryResponse>>.SuccessAsync(response);
            });
        }
    }
}
