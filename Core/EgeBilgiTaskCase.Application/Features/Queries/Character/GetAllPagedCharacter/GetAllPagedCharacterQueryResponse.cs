namespace EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter
{
    public class GetAllPagedCharacterQueryResponse
    {
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public string CharacterName { get; set; }
        public string LastKnownLocationName { get; set; }
        public string? EpisodeName { get; set; }
        public string? FirstSeenName { get; set; }
        public string SpeciesName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }
}