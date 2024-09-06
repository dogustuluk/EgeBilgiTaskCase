namespace EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter
{
    public class GetAllPagedCharacterQueryRequest : IRequest<OptResult<PaginatedList<GetAllPagedCharacterQueryResponse>>>
    {
        public int PageIndex { get; set; } = 1;
        public string? SearchText { get; set; }
        // public int ActiveStatus { get; set; } = 1;
        public int? StatusId { get; set; }
        public int? SpeciesId { get; set; }
        public int? LocationId { get; set; }
        public int Take { get; set; } = 25;
        public string? OrderBy { get; set; } = "CharacterApiId ASC";
    }
}