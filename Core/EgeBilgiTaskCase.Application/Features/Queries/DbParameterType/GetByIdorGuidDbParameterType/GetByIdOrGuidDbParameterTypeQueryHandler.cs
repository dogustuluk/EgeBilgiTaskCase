using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetByIdOrGuidDbParameterType
{
    public class GetByIdOrGuidDbParameterTypeQueryHandler : IRequestHandler<GetByIdOrGuidDbParameterTypeQueryRequest, OptResult<GetByIdOrGuidDbParameterTypeQueryResponse>>
    {
        private readonly IDbParameterTypeService _dbParameterTypeService;
        private readonly IMapper _mapper;

        public GetByIdOrGuidDbParameterTypeQueryHandler(IDbParameterTypeService dbParameterTypeService, IMapper mapper)
        {
            _dbParameterTypeService = dbParameterTypeService;
            _mapper = mapper;
        }

        public async Task<OptResult<GetByIdOrGuidDbParameterTypeQueryResponse>> Handle(GetByIdOrGuidDbParameterTypeQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                object value = null;
                if (request.Id != null) value = request.Id;
                if (request.Guid != null) value = request.Guid;
                var data = await _dbParameterTypeService.GetByIdOrGuid(value);

                var mappedData = _mapper.Map<GetByIdOrGuidDbParameterTypeQueryResponse>(data.Data);
                if (mappedData == null) return await OptResult<GetByIdOrGuidDbParameterTypeQueryResponse>.FailureAsync(Messages.NullData);

                return await OptResult<GetByIdOrGuidDbParameterTypeQueryResponse>.SuccessAsync(mappedData, Messages.Successfull);
            });
        }
    }
}
