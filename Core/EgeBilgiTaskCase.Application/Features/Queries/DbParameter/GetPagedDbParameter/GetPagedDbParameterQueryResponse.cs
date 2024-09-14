namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetPagedDbParameter;

public class GetPagedDbParameterQueryResponse
{
    public Guid Guid { get; set; }
    public int DbParameterTypeId { get; set; }
    public int ParentId { get; set; }
    public int ItemType { get; set; }
    public string? DBParameterName1 { get; set; }
    public string? DBParameterName2 { get; set; }
    public string? Prefix { get; set; }
    public int RMemberId { get; set; }
    public int SortOrderNo { get; set; }
    public bool isActive { get; set; }
    public bool isEditable { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}