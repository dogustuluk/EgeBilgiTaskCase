namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetAllDbParameter;

public class GetAllDbParameterQueryResponse
{
    public int Id { get; set; }
    public int DbParameterTypeId { get; set; }
    public int ParentId { get; set; }
    public int ItemType { get; set; }
    public string? DBParameterName1 { get; set; }
    public string? DBParameterName2 { get; set; }
    public int Param1 { get; set; }
    public double Param2 { get; set; }
    public string? Param3 { get; set; }
    public string? Param4 { get; set; }
    public string? Param5 { get; set; }
    public string? LocationsJson { get; set; }
    public string? ConditionsJson { get; set; }
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