namespace EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter
{
    public class GetAllPagedCharacterQueryResponse
    {
        public int CharacterId { get; set; }
        public int CharacterApiId { get; set; }
        public Guid CharacterGuid { get; set; }
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