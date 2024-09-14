using EgeBilgiTaskCase.Application.Common.GenericObjects;

namespace EgeBilgiTaskCase.Client.Models.Management;

public class DbParameter_Index_ViewModel
{
    public string? PageTitle { get; set; }
    public string? SubPageTitle { get; set; }
    public int PageIndex { get; set; } = 1;
    public string? SearchText { get; set; }
    public int DbParameterTypeId { get; set; }
    public int ParentId { get; set; }
    public int ActiveStatus { get; set; } = 1;
    public int Take { get; set; } = 15;
    public string? OrderBy { get; set; } = "Id ASC";
    public List<DbParameter_Grid_ViewModel>? MyGridData { get; set; }
    public Pagination? MyPagination { get; set; }

    public List<DataList1>? Parent_DDL { get; set; }
    public List<DataList1>? DbParameterType_DDL { get; set; }
}
public class DbParameter_Grid_ViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public int DbParameterTypeId { get; set; }
    public string DbParameterTypeName { get; set; }
    public int ParentId { get; set; }
    public string ParentName { get; set; }
    public string DBParameterName1 { get; set; }
    public string Param1 { get; set; }
    public string Param2 { get; set; }
    public int RMemberId { get; set; }
    public string RMemberName { get; set; }
    public int SortOrderNo { get; set; }
    public bool IsActive { get; set; }
    public bool IsEditable { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedByName { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string UpdatedByName { get; set; }
}

