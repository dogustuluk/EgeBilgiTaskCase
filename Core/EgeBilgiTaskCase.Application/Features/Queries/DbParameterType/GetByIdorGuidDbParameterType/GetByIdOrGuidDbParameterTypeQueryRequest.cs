namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetByIdOrGuidDbParameterType
{
    public class GetByIdOrGuidDbParameterTypeQueryRequest : IRequest<OptResult<GetByIdOrGuidDbParameterTypeQueryResponse>>
    {
        public int? Id { get; set; }
        public Guid? Guid { get; set; }
    }
}