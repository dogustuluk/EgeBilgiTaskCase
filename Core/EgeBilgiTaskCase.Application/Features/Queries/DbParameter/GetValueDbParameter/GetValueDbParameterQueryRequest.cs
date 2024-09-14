namespace EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetValueDbParameter;

public class GetValueDbParameterQueryRequest : IRequest<OptResult<GetValueDbParameterQueryResponse>>
{
    public string ColumnName { get; set; }
    public int DataId { get; set; }
}