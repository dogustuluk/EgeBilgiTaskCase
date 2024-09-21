namespace EgeBilgiTaskCase.Application.Features.Queries.User.GetAllPagedUser;

public class GetAllPagedUserQueryRequest : IRequest<OptResult<PaginatedList<GetAllPagedUserQueryResponse>>>
{
    public int PageIndex { get; set; } = 1;
    public string? SearchText { get; set; }
    // public int ActiveStatus { get; set; } = 1;
    public int? StatusId { get; set; }
    public int Take { get; set; } = 25;
    public string? OrderBy { get; set; } = "Id DESC";
}