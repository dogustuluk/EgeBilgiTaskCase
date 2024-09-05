namespace EgeBilgiTaskCase.Domain.Entities.Character
{
    public class Character : BaseEntity
    {
        public int CharacterApiId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        
    }
    public class CharacterDetail : BaseEntity
    {
        public int CharacterId { get; set; }
        public int? StatusId { get; set; }
        public int? SpeciesId { get; set; }
        public int? TypeId { get; set; }
        public int? LocationId { get; set; }
        public int? OriginId { get; set; }

        public List<int>? EpisodeIds { get; set; }
        public string? Desc { get; set; }
        
    }
    public class Location : BaseEntity
    {
        public int LocationApiId { get; set; }
        public string Name { get; set; }
        public int DimensionId { get; set; }
        public int TypeId { get; set; }
        public List<int> Residents { get; set; }
        public DateTime? ApiCreatedDate { get; set; }
    }

    public class Episode : BaseEntity
    {
        public string Name { get; set; }
        public DateTime AirDate { get; set; }

    }


}
