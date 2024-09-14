namespace EgeBilgiTaskCase.Application.Common.DTOs.Common;

public class GetAllPaged_Episode_Index_Dto
{
    public int PageIndex { get; set; } = 1;
    public string? SearchText { get; set; }
    public int Season { get; set; }
    public int Take { get; set; } = 25;
    public string? OrderBy { get; set; } = "Id ASC";
}
