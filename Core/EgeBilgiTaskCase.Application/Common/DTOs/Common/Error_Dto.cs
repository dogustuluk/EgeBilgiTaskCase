namespace EgeBilgiTaskCase.Application.Common.DTOs.Common
{
    public class Create_Error_Dto
    {
        public int UserId { get; set; }
        public int? ErrorType { get; set; }
        public string? ErrorUrl { get; set; }
        public string? ErrorDesc { get; set; }
        public string? Operation { get; set; }
        public string? ErrorPlace { get; set; }
    }
    public class GetAllPaged_Error_Dto
    {
        public int PageIndex { get; set; } = 1;
        public string? SearchText { get; set; }
        public int? ErrorType { get; set; }
        public int Take { get; set; } = 25;
        public string? OrderBy { get; set; } = "Id ASC";

    }

}