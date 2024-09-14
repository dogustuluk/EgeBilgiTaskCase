namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetPagedDbParameter;

public class GetPagedDbParameterQueryRequest : IRequest<OptResult<PaginatedList<GetPagedDbParameterQueryResponse>>>
{
    public int PageIndex { get; set; } = 1;
    public string? SearchText { get; set; }
    public int DbParameterTypeId { get; set; }
    public int ParentId { get; set; }
    public int ActiveStatus { get; set; } = 1;
    public int Take { get; set; } = 25;
    public string? OrderBy { get; set; } = "Id ASC";
}