namespace EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty
{
    
    public class CharacterListDto
    {
        public List<CharacterDto> Results { get; set; }
        public InfoDto Info { get; set; }
    }
    public class InfoDto
    {
        public int Pages { get; set; }
    }
    public class CharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
        public OriginDto Origin { get; set; }
        public LocationDto Location { get; set; }
        public List<string> Episode { get; set; }
    }

    public class OriginDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }


    public class LocationListDto
    {
        public List<LocationDto> Results { get; set; }
        public InfoDto Info { get; set; }
    }
    public class LocationDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Dimension { get; set; }
        public List<string> Residents { get; set; }
        public string Url { get; set; }
        public DateTime? ApiCreatedDate { get; set; }
    }


    public class EpisodeListDto
    {
        public List<EpisodeDto> Results { get; set; }
        public InfoDto Info { get; set; }
    }
    public class EpisodeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? AirDate { get; set; }
        public string Episode { get; set; }
        public DateTime? ApiCreatedDate { get; set; }
    }

}
