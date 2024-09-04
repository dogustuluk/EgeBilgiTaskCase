namespace EgeBilgiTaskCase.Domain.Entities.Character
{
    public class Character : BaseEntity
    {
        public int CharacterApiId { get; set; }
        public int OriginId { get; set; }
        public int LocationId { get; set; }
        public List<int> EpisodeIds { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        //public Location Origin { get; set; }
        //public Location Location { get; set; }
    }

    public class Location : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Dimension { get; set; }
       // public ICollection<Character> Residents { get; set; } = new List<Character>();
    }

    public class Episode : BaseEntity
    {
        public string Name { get; set; }
        public DateTime AirDate { get; set; }
      //  public ICollection<Character> Characters { get; set; }
    }

}
