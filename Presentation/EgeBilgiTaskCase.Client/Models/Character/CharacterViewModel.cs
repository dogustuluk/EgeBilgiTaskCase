using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
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
        public int? TypeId { get; set; } = 0;
        public int? SpeciesDbParameterTypeId { get; set; } = 1;
        public int? SpeciesId { get; set; } = 0;
        public int? LocationId { get; set; }
        public int Take { get; set; } = 15;
        public string? OrderBy { get; set; } = "Id ASC";
        public List<Character_AdminGrid_ViewModel>? MyGridData { get; set; }
        public Pagination? MyPagination { get; set; }

        public List<DataList1>? Species_DDL { get; set; }
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

    public class Character_AddNew_ViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public string? Image { get; set; }
        public int? OriginId { get; set; }
        public int? LocationId { get; set; }
        public List<int>? EpisodeIds { get; set; }
        public int? StatusId { get; set; }
        public int? TypeId { get; set; }
        public int? SpeciesId { get; set; }
        public string? Desc { get; set; }
        public CharacterDetail_AddNew_Dto? CharacterDetail { get; set; }
        public List<DataList1>? Species_DDL { get; set; }
        public List<DataList1>? Type_DDL { get; set; }
        public List<DataList1>? Episodes_DDL { get; set; }

        public MyResult? MyResult { get; set; }
    }
}
