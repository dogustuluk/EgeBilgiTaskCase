using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;
using EgeBilgiTaskCase.Application.Common.DTOs.Management;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Common.DTOs.User;
using EgeBilgiTaskCase.Application.Features.Commands.Character.AddNewCharacter;
using EgeBilgiTaskCase.Application.Features.Commands.User.CreateUser;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetAllDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetByIdorGuidDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetDataListDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetPagedDbParameter;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllDbParameterType;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameterType.GetAllPagedDbParameterType;
using EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetAllEpisode;
using EgeBilgiTaskCase.Application.Features.Queries.Episodes.GetDataListEpisodes;
using EgeBilgiTaskCase.Application.Features.Queries.User.GetByIdOrGuidUser;
using EgeBilgiTaskCase.Application.Utilities.Converters;
using EgeBilgiTaskCase.Domain.Entities.Character;
using EgeBilgiTaskCase.Domain.Entities.Identity;
using EgeBilgiTaskCase.Domain.Entities.Management;

namespace EgeBilgiTaskCase.Application.Common.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            #region GENERAL
            CreateMap(typeof(PaginatedList<>), typeof(PaginatedList<>)).ConvertUsing(typeof(PaginatedListConverter<,>));
            CreateMap<string, GetValueXQueryResponse>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));
            CreateMap<DataList1, GetDataListXQueryResponse>();

            #endregion

            #region USER
            CreateMap<CreateUserCommandRequest, CreateUser_Dto>()
            .ForMember(dest => dest.Guid, opt => opt.Ignore())
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));
            CreateMap<CreateUser_Dto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            CreateMap<CreateUser_Dto, CreateUserCommandResponse>().ReverseMap();

            CreateMap<CreateUserCommandResponse, OptResult<CreateUserCommandResponse>>();

            CreateMap<AppUser, GetByIdOrGuidUserQueryResponse>();

            #endregion

            #region CHARACTER
            CreateMap<Character, GetAllPagedCharacterQueryResponse>();
            CreateMap<GetAllPagedCharacterQueryRequest, Character_Index_Dto>();
            CreateMap<Character, GetAllPagedCharacterQueryRequest>();
            CreateMap<Character, GetAllPagedCharacterQueryResponse>();
            CreateMap<Character_GridView_Dto, GetAllPagedCharacterQueryResponse>();

            CreateMap<AddNewCharacterCommandRequest, Character_AddNew_Dto>();
            CreateMap<Character, Character_AddNew_Dto>().ReverseMap();
            CreateMap<Character, AddNewCharacterCommandResponse>();
            CreateMap<CharacterDetail_AddNew_Dto, CharacterDetail>();
            #endregion

            #region EPISODE
            CreateMap<DataList1, GetDataListEpisodesQueryResponse>();
            CreateMap<Episode, GetAllEpisodeQueryResponse>();

            #endregion

            #region LOCATION

            #endregion

            #region DBPARAMETER
            CreateMap<DataList1, GetDataListDbParameterQueryResponse>();
            CreateMap<DbParameter, GetAllDbParameterQueryResponse>();
            CreateMap<DbParameter, GetByIdOrGuidDbParameterQueryResponse>();

            CreateMap<DbParameter, GetAllDbParameterQueryResponse>();

            CreateMap<GetPagedDbParameterQueryRequest, GetAllPaged_DBParameter_Index_Dto>();
            CreateMap<DbParameter, GetPagedDbParameterQueryResponse>();
            #endregion

            #region DBPARAMETERTYPE
            CreateMap<DbParameterType, GetAllDbParameterTypeQueryResponse>();
            CreateMap<GetAllPagedDbParameterTypeQueryRequest, GetAllPaged_DBParameterType_Index_Dto>();
            CreateMap<DbParameterType, GetAllPagedDbParameterTypeQueryResponse>();
            #endregion
        }
    }
}
