using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Constants;
using EgeBilgiTaskCase.Application.Features.Commands.Character.AddNewCharacter;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using EgeBilgiTaskCase.Client.HelperServices;
using EgeBilgiTaskCase.Client.Models;
using EgeBilgiTaskCase.Client.Models.Character;
using EgeBilgiTaskCase.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EgeBilgiTaskCase.Client.Controllers
{
    public class CharacterController : Controller
    {
        private readonly IHttpClientService _httpClientService;

        public CharacterController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index(Character_AdminIndex_ViewModel model)
        {
            var queryString = QueryStringHelperService.ToQueryString(model);

            var response = await _httpClientService.GetAsync<PaginatedList<GetAllPagedCharacterQueryResponse>>(
                    new RequestParameters
                    {
                        Action = "GetAllPagedCharacter",
                        Controller = "Character",
                        Folder = "Character",
                        QueryString = queryString
                    }
                );

            List<Character_AdminGrid_ViewModel> myData = new();

            foreach (var data in response.Data.Data)
            {
                myData.Add(new Character_AdminGrid_ViewModel()
                {
                    Id = data.CharacterId,
                    CharacterApiId = data.CharacterApiId,
                    Guid = data.CharacterGuid,
                    CharacterName = data.CharacterName,
                    Gender = data.Gender,
                    Image = data.Image,
                    SpeciesName = data.SpeciesName,
                    StatusName = data.StatusName,

                });
            }

            var speciesString = QueryStringHelperService.ToQueryString(new
            {
                DbParameterTypeId = 1,
                ParentId = 4
            });
            var species_DDL_Response = await _httpClientService.GetDataListAsync(
                new RequestParameters
                {
                    Action = "GetDataListDbParameter",
                    Controller = "DbParameter",
                    Folder = "Management",
                    QueryString = speciesString
                });



            List<DataList1> species_DDL = species_DDL_Response;




            Character_AdminIndex_ViewModel MYRESULT = new Character_AdminIndex_ViewModel()
            {
                PageTitle = "Rick And Morty",
                SubPageTitle = "Karakterler",
                MyGridData = myData,
                MyPagination = response.Data.Pagination,
                SearchText = model.SearchText,
                LocationId = model.LocationId,
                // SpeciesId = model.SpeciesId,
                StatusId = model.StatusId,
                Take = model.Take,
                PageIndex = model.PageIndex,
                OrderBy = model.OrderBy,
                Species_DDL = species_DDL
            };

            return View(MYRESULT);

        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AddNew()
        {
            //if (User.Identity != null && User.Identity.IsAuthenticated)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            var model = new Character_AddNew_ViewModel();

            var speciesString = QueryStringHelperService.ToQueryString(new { DbParameterTypeId = 1, ParentId = 4 });
            var species_DDL_Response = await _httpClientService.GetDataListAsync(new RequestParameters
            {
                Action = "GetDataListDbParameter",
                Controller = "DbParameter",
                Folder = "Management",
                QueryString = speciesString
            });
            List<DataList1> species_DDL = species_DDL_Response;

            var typeString = QueryStringHelperService.ToQueryString(new { DbParameterTypeId = 2, ParentId = 4 });
            var type_DDL_Response = await _httpClientService.GetDataListAsync(new RequestParameters
            {
                Action = "GetDataListDbParameter",
                Controller = "DbParameter",
                Folder = "Management",
                QueryString = typeString
            });
            List<DataList1> type_DDL = type_DDL_Response;

            var episodes_DDL_Response = await _httpClientService.GetDataListAsync(new RequestParameters
            {
                Action = "GetDataListEpisode",
                Controller = "Episode",
                Folder = "Common",
            });
            List<DataList1> episode_DDL = episodes_DDL_Response;

            model.Species_DDL = species_DDL;
            model.Type_DDL = type_DDL;
            model.Episodes_DDL = episode_DDL;


            return View(model);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddNew(Character_AddNew_ViewModel model)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            MyResult MyResult = new();
            MyResult.isOk = false;
            MyResult.ResultText = Messages.GeneralError;
            MyResult.ListURL = "/Character/Character/Index";
            MyResult.ListText = "Karakter listesine dön";
            MyResult.AddURL = "/Character/Character/AddNew";
            MyResult.AddText = "Yeni Karakter Ekle";
            MyResult.DetailsURL = "";
            MyResult.DetailsText = "";
            MyResult.isFancy = false;


            var request = new AddNewCharacterCommandRequest
            {
                Name = model.Name,
                Gender = model.Gender,
                Image = model.Image ?? "/images/characters/RickAndMortyBoth.png",
                CharacterDetail = model.CharacterDetail != null ? new CharacterDetail_AddNew_Dto
                {
                    OriginId = 0,
                    LocationId = 0,
                    EpisodeIds = model.CharacterDetail.EpisodeIds,
                    StatusId = model.CharacterDetail.StatusId,
                    TypeId = model.CharacterDetail.TypeId,
                    SpeciesId = model.CharacterDetail.SpeciesId,
                    Desc = ""
                } : null
            };

            var requestParameters = new RequestParameters {Action = "AddNewCharacter",Controller = "Character",Folder = "Character"};
            var result = await _httpClientService.PostAsync2<OptResult<AddNewCharacterCommandResponse>>(requestParameters, request);
            if (!result.Succeeded)
            {
                MyResult.ResultText = string.Join(",", result.Messages);

                model.MyResult = MyResult;
            }

            if (result.Data == null)
            {
                MyResult.ResultText = Messages.DataNotFound;
                MyResult.ListURL = "/Character/Index";
                MyResult.ListText = "Karakter listesine dön";
                MyResult.AddURL = "/Character/AddNew";
                MyResult.AddText = "Yeni Karakter Ekle";
                MyResult.DetailsURL = "";
                MyResult.DetailsText = "";
                MyResult.isFancy = false;

                model.MyResult = MyResult;

                return View(model);
            }

            string ResultText = Messages.SuccessfullyAdded;
            MyResult.isOk = true;
            MyResult.ResultText = ResultText;
            MyResult.DetailsURL = "/Character/Update?Guid=" + result.Data.Guid;
            MyResult.DetailsText = "Karakter Detayına Git";

            model.MyResult = MyResult;

            return View(model);
        }
    }

}
