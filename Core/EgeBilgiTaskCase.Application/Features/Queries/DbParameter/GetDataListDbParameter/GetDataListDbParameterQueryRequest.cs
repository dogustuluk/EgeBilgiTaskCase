namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter
{
    public class GetDataListDbParameterQueryRequest : IRequest<OptResult<List<GetDataListDbParameterQueryResponse>>>
    {
        public string? SelectedText { get; set; }
        public int ParentId { get; set; }
        public int DbParameterTypeId { get; set; }
    }
}
