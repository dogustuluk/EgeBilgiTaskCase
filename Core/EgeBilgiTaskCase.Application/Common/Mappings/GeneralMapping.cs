using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Common.DTOs.User;
using EgeBilgiTaskCase.Application.Features.Commands.Character.AddNewCharacter;
using EgeBilgiTaskCase.Application.Features.Commands.User.CreateUser;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetDataListEpisodes;
using EgeBilgiTaskCase.Application.Utilities.Converters;
using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Domain.Entities.Identity;

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
            CreateMap<Character_GridView_Dto, GetAllPagedCharacterQueryResponse>();

            CreateMap<AddNewCharacterCommandRequest, Character_AddNew_Dto>();
            CreateMap<Character, Character_AddNew_Dto>().ReverseMap();
            CreateMap<Character, AddNewCharacterCommandResponse>();
            CreateMap<CharacterDetail_AddNew_Dto, CharacterDetail>();
           

            CreateMap<CreateUserCommandRequest, CreateUser_Dto>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateUser_Dto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            
            CreateMap<CreateUser_Dto, CreateUserCommandResponse>().ReverseMap();
            
            CreateMap<CreateUserCommandResponse, OptResult<CreateUserCommandResponse>>();



            CreateMap<DataList1, GetDataListDbParameterQueryResponse>();
            CreateMap<DataList1, GetDataListEpisodesQueryResponse>();

            CreateMap<DataList1, GetDataListXQueryResponse>();
        }
    }
}
