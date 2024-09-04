namespace EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty
{
    public class Character_Index_Dto
    {
        public int PageIndex { get; set; } = 1;
        public string? SearchText { get; set; }
        public string? CharacterName { get; set; }
        public int? Location { get; set; }
        public int? Episode { get; set; }
        public string? Species { get; set; }
        public string? Status { get; set; }
        public string? Gender { get; set; }
        public int Take { get; set; } = 25;
        public string? OrderBy { get; set; } = "Id ASC";
    }
}
