namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetByIdorGuidDbParameter;

public class GetByIdOrGuidDbParameterQueryRequest : IRequest<OptResult<GetByIdOrGuidDbParameterQueryResponse>>
{
    public int? Id { get; set; }
    public Guid? Guid { get; set; }
}