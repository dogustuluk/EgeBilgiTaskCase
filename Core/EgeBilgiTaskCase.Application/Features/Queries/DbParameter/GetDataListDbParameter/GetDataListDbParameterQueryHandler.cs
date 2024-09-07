using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter
{
    public class GetDataListDbParameterQueryHandler : IRequestHandler<GetDataListDbParameterQueryRequest, OptResult<List<GetDataListDbParameterQueryResponse>>>
    {
        public readonly IDbParameterService _dbParameterService;
        private readonly IMapper _mapper;

        public GetDataListDbParameterQueryHandler(IDbParameterService dbParameterService, IMapper mapper)
        {
            _dbParameterService = dbParameterService;
            _mapper = mapper;
        }

        public async Task<OptResult<List<GetDataListDbParameterQueryResponse>>> Handle(GetDataListDbParameterQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var myDataList = await _dbParameterService.GetDataListAsync(request.DbParameterTypeId, request.ParentId);

                if (myDataList == null) return await OptResult<List<GetDataListDbParameterQueryResponse>>.FailureAsync(Messages.NullValue);

                var response = _mapper.Map<List<GetDataListDbParameterQueryResponse>>(myDataList);

                return await OptResult<List<GetDataListDbParameterQueryResponse>>.SuccessAsync(response);
            });
        }
    }
}
