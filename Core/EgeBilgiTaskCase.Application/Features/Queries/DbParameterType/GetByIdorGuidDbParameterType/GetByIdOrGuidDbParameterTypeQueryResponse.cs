namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetByIdOrGuidDbParameterType
{
    public class GetByIdOrGuidDbParameterTypeQueryResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string? DbParameterTypeName { get; set; }
        public bool isEditable { get; set; }
        public bool isForList { get; set; }
        public int ItemTypeId { get; set; }
        public string? ItemColumName { get; set; }
        public string? Params { get; set; }
    }
}