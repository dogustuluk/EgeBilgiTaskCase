namespace EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty
{
    public class Character_Index_Dto
    {
        public int PageIndex { get; set; } = 1;
        public string? SearchText { get; set; }
        public List<Character_GridView_Dto> GridView { get; set; } = new List<Character_GridView_Dto>(); 
        public int Take { get; set; } = 25;
        public string? OrderBy { get; set; } = "Id ASC";
        //public Guid CreatedUser { get; set; }
        //public string CreatedUserName { get; set; }
        //public Guid UpdatedUser { get; set; }
        //public string UpdatedtedUserName { get; set; }
    }


    public class Character_GridView_Dto
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public int EpisodeId { get; set; }
        public string EpisodeName { get; set; }
        public int SpeciesId { get; set; }
        public string SpeciesName { get; set; }
        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }

    public class Character_Detail_Dto
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public string Origin { get; set; }
        public string Location { get; set; }
        public string Image { get; set; }
        public List<string> Episodes { get; set; }
    }

}
