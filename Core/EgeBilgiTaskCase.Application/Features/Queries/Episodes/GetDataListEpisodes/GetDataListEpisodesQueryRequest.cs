namespace EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetDataListEpisodes
{
    public class GetDataListEpisodesQueryRequest : IRequest<OptResult<List<GetDataListEpisodesQueryResponse>>>
    {
        public string? SelectedText { get; set; }

    }
}