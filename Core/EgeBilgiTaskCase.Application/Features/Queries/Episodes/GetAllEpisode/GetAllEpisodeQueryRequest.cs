namespace EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetAllEpisode;

public class GetAllEpisodeQueryRequest : IRequest<OptResult<List<GetAllEpisodeQueryResponse>>>
{
    public string? OrderBy { get; set; } = "Id ASC";
}