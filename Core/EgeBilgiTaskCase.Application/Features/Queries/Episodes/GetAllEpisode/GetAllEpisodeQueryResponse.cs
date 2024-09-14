namespace EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetAllEpisode;

public class GetAllEpisodeQueryResponse
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string Name { get; set; }
    public Guid CreatedUser { get; set; }
    public DateTime CreatedDate { get; set; }
    public Guid UpdatedUser { get; set; }
    public DateTime UpdatedDate { get; set; }
    public int EpisodeApiId { get; set; }
    public string EpisodeStamp { get; set; }
}