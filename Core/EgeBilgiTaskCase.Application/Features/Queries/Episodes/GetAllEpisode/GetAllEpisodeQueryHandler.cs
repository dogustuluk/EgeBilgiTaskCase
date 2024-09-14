using EgeBilgiTaskCase.Application.Abstractions.Services.Common;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetAllEpisode;
public class GetAllEpisodeQueryHandler : IRequestHandler<GetAllEpisodeQueryRequest, OptResult<List<GetAllEpisodeQueryResponse>>>
{
    private readonly IEpisodeService _episodeService;
    private readonly IMapper _mapper;

    public GetAllEpisodeQueryHandler(IEpisodeService episodeService, IMapper mapper)
    {
        _episodeService = episodeService;
        _mapper = mapper;
    }

    public async Task<OptResult<List<GetAllEpisodeQueryResponse>>> Handle(GetAllEpisodeQueryRequest request, CancellationToken cancellationToken)
    {
        return await ExceptionHandler.HandleOptResultAsync(async () =>
        {
            //  var predicate = _dbParameterSpecifications.GetAllPredicate(request);
            var datas = await _episodeService.GetAllEpisodeAsync(a => a.Id > 0, "");

            if (string.IsNullOrEmpty(request.OrderBy)) request.OrderBy = "DbParameterName1 ASC";

            var dataList = _mapper.Map<List<GetAllEpisodeQueryResponse>>(datas);

            return await OptResult<List<GetAllEpisodeQueryResponse>>.SuccessAsync(dataList, Messages.Successfull);
        });
    }
}
