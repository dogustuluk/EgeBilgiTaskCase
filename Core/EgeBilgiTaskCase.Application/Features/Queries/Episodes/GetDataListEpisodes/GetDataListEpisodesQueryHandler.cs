using EgeBilgiTaskCase.Application.Abstractions.Services.Common;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetDataListEpisodes
{
    public class GetDataListEpisodesQueryHandler : IRequestHandler<GetDataListEpisodesQueryRequest, OptResult<List<GetDataListEpisodesQueryResponse>>>
    {
        private readonly IEpisodeService _episodeService;
        private readonly IMapper _mapper;

        public GetDataListEpisodesQueryHandler(IEpisodeService episodeService, IMapper mapper)
        {
            _episodeService = episodeService;
            _mapper = mapper;
        }

        public async Task<OptResult<List<GetDataListEpisodesQueryResponse>>> Handle(GetDataListEpisodesQueryRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var myDataList = await _episodeService.GetDataListAsync();

                if (myDataList == null) return await OptResult<List<GetDataListEpisodesQueryResponse>>.FailureAsync(Messages.NullValue);

                var response = _mapper.Map<List<GetDataListEpisodesQueryResponse>>(myDataList);

                return await OptResult<List<GetDataListEpisodesQueryResponse>>.SuccessAsync(response);
            });
        }
    }
}
