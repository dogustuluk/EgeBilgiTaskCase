namespace EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse
{
    public class GetValueXQueryRequest : IRequest<OptResult<GetValueXQueryResponse>>
    {
        public string ColumnName { get; set; }
        public int DataId { get; set; }
    }
}
