namespace EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter
{
    public class GetAllPagedCharacterQueryRequest : IRequest<OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>>>
    {
        public string? OrderBy { get; set; }
    }
}