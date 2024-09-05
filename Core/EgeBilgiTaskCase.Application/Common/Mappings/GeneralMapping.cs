using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using EgeBilgiTaskCase.Application.Utilities.Converters;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Application.Common.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListConverter<,>));

            CreateMap<Character, GetAllPagedCharacterQueryResponse>();
            CreateMap<GetAllPagedCharacterQueryRequest, Character_Index_Dto>();
            CreateMap<Character, GetAllPagedCharacterQueryRequest > ();
            CreateMap<Character, GetAllPagedCharacterQueryResponse>();
            //<PaginatedList<Character>, PaginatedList<GetAllPagedCharacterQueryResponse>>();




            CreateMap<DataList1, GetDataListXQueryResponse>();
        }
    }
}
