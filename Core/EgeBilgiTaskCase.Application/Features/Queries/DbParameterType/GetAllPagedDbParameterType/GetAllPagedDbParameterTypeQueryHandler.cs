using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.DTOs.Management;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllPagedDbParameterType
{
    public class GetAllPagedDbParameterTypeQueryHandler : IRequestHandler<GetAllPagedDbParameterTypeQueryRequest, OptResult<PaginatedList<GetAllPagedDbParameterTypeQueryResponse>>>
    {
        private readonly IDbParameterTypeService _dbParameterTypeService;
        private readonly IMapper _mapper;

        public GetAllPagedDbParameterTypeQueryHandler(IDbParameterTypeService dbParameterTypeService, IMapper mapper)
        {
            _dbParameterTypeService = dbParameterTypeService;
            _mapper = mapper;
        }

        public async Task<OptResult<PaginatedList<GetAllPagedDbParameterTypeQueryResponse>>> Handle(GetAllPagedDbParameterTypeQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var model = _mapper.Map<GetAllPaged_DBParameterType_Index_Dto>(request);

                var result = await _dbParameterTypeService.GetAllPagedDbParameterTypeAsync(model);
                var response = _mapper.Map<PaginatedList<GetAllPagedDbParameterTypeQueryResponse>>(result.Data);

                if (result == null) return await OptResult<PaginatedList<GetAllPagedDbParameterTypeQueryResponse>>.FailureAsync(Messages.UnSuccessfull);

                return await OptResult<PaginatedList<GetAllPagedDbParameterTypeQueryResponse>>.SuccessAsync(response, Messages.Successfull);
            });
        }
    }
}
