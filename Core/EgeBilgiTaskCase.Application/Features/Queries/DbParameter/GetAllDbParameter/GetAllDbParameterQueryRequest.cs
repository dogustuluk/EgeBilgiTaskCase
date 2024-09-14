namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetAllDbParameter;

public class GetAllDbParameterQueryRequest : IRequest<OptResult<List<GetAllDbParameterQueryResponse>>>
{
    public int? DbParameterTypeId { get; set; }
    public int? ParentId { get; set; }
    public int? ItemType { get; set; }
    public int? isActive { get; set; }
    public string? OrderBy { get; set; } = "Id ASC";
}