using EgeBilgiTaskCase.Application.Common.GenericObjects;

namespace EgeBilgiTaskCase.Client.Models.Character
{
    public class Character_Index_ViewModel
    {
        public string? PageTitle { get; set; }
        public string? SubPageTitle { get; set; }
        public int PageIndex { get; set; } = 1;
        public string? SearchText { get; set; }
        public int? StatusId { get; set; }
        public int? SpeciesId { get; set; }
        public int? LocationId { get; set; }
        public int Take { get; set; } = 6;
        public string? OrderBy { get; set; } = "Id ASC";
        public List<Character_GridView_ViewModel>? MyGridData { get; set; }
        public Pagination? MyPagination { get; set; }
    }

    public class Character_GridView_ViewModel
    {
        public string CharacterName { get; set; }
        public string LastKnownLocationName { get; set; }
        public string? EpisodeName { get; set; }
        public string? FirstSeenName { get; set; }
        public string SpeciesName { get; set; }
        public string StatusName { get; set; }
        public string Gender { get; set; }
        public string Image { get; set; }
    }


    public class Character_AdminIndex_ViewModel
    {
        public string? PageTitle { get; set; }
        public string? SubPageTitle { get; set; }
        public int PageIndex { get; set; } = 1;
        public string? SearchText { get; set; }
        public int? StatusId { get; set; }
        public int? SpeciesId { get; set; }
        public int? LocationId { get; set; }
        public int Take { get; set; } = 15;
        public string? OrderBy { get; set; } = "Id ASC";
        public List<Character_AdminGrid_ViewModel>? MyGridData { get; set; }
        public Pagination? MyPagination { get; set; }
    }
    public class Character_AdminGrid_ViewModel
    {
        public int Id { get; set; }
        public int CharacterApiId { get; set; }
        public Guid Guid { get; set; }
        public string Image { get; set; }
        public string CharacterName { get; set; }
        public string StatusName { get; set; }
        public string SpeciesName { get; set; }
        public string Gender { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
    }
}
