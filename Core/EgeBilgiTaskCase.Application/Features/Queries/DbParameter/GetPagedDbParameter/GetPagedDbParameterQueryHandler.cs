using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.DTOs.Management;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetPagedDbParameter;
public class GetPagedDbParameterQueryHandler : IRequestHandler<GetPagedDbParameterQueryRequest, OptResult<PaginatedList<GetPagedDbParameterQueryResponse>>>
{
    private readonly IDbParameterService _dbParameterService;
    private readonly IMapper _mapper;

    public GetPagedDbParameterQueryHandler(IDbParameterService dbParameterService, IMapper mapper)
    {
        _dbParameterService = dbParameterService;
        _mapper = mapper;
    }

    public async Task<OptResult<PaginatedList<GetPagedDbParameterQueryResponse>>> Handle(GetPagedDbParameterQueryRequest request, CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            var model = _mapper.Map<GetAllPaged_DBParameter_Index_Dto>(request);

            var result = await _dbParameterService.GetAllPagedDbParameterAsync(model);
            var response = _mapper.Map<PaginatedList<GetPagedDbParameterQueryResponse>>(result.Data);

            if (result == null) return await OptResult<PaginatedList<GetPagedDbParameterQueryResponse>>.FailureAsync(Messages.UnSuccessfull);

            return await OptResult<PaginatedList<GetPagedDbParameterQueryResponse>>.SuccessAsync(response, Messages.Successfull);
        });
    }
}
