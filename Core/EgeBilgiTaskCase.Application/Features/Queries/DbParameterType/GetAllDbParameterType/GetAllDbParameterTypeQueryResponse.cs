namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllDbParameterType
{
    public class GetAllDbParameterTypeQueryResponse
    {
        public string? DbParameterTypeName { get; set; }
        public bool isEditable { get; set; }
        public bool isForList { get; set; }
        public int ItemTypeId { get; set; }
        public string? ItemColumName { get; set; }
        public string? Params { get; set; }
    }
}