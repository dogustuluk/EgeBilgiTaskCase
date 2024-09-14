namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllDbParameterType
{
    public class GetAllDbParameterTypeQueryRequest : IRequest<OptResult<List<GetAllDbParameterTypeQueryResponse>>>
    {
        public string? OrderBy { get; set; } = "Id ASC";
    }
}