using EgeBilgiTaskCase.Application.Abstractions.Services.Management;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Common.Specifications;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllDbParameterType
{
    public class GetAllDbParameterTypeQueryHandler : IRequestHandler<GetAllDbParameterTypeQueryRequest, OptResult<List<GetAllDbParameterTypeQueryResponse>>>
    {
        private readonly IDbParameterTypeService _dbParameterTypeService;
        private readonly IMapper _mapper;
        private readonly DbParameterTypeSpecifications _dbParameterTypeSpecifications;
        public GetAllDbParameterTypeQueryHandler(IDbParameterTypeService dbParameterTypeService, IMapper mapper, DbParameterTypeSpecifications dbParameterTypeSpecifications)
        {
            _dbParameterTypeService = dbParameterTypeService;
            _mapper = mapper;
            _dbParameterTypeSpecifications = dbParameterTypeSpecifications;
        }

        public async Task<OptResult<List<GetAllDbParameterTypeQueryResponse>>> Handle(GetAllDbParameterTypeQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var predicate = _dbParameterTypeSpecifications.GetAllPredicate(request);
                var datas = await _dbParameterTypeService.GetAllDbParameterTypeAsync(predicate, "");

                if (string.IsNullOrEmpty(request.OrderBy)) request.OrderBy = "DbParameterTypeName ASC";

                var dataList = _mapper.Map<List<GetAllDbParameterTypeQueryResponse>>(datas);

                return await OptResult<List<GetAllDbParameterTypeQueryResponse>>.SuccessAsync(dataList, Messages.Successfull);
            });
        }
    }
}
