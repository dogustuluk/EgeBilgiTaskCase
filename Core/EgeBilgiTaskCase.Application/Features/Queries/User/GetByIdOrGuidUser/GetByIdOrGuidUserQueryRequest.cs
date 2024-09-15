namespace EgeBilgiTaskCase.Application.Features.Queries.User.GetByIdOrGuidUser;

public class GetByIdOrGuidUserQueryRequest : IRequest<OptResult<GetByIdOrGuidUserQueryResponse>>
{
    public int? Id { get; set; }
    public Guid? Guid { get; set; }
}