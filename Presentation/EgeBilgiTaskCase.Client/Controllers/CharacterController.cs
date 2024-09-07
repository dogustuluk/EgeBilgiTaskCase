using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.Character.GetAllPagedCharacter;
using EgeBilgiTaskCase.Client.HelperServices;
using EgeBilgiTaskCase.Client.Models.Character;
using EgeBilgiTaskCase.Client.Models;
using EgeBilgiTaskCase.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EgeBilgiTaskCase.Application.Common.DTOs._0RequestResponse;

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


            // var speciesString = QueryStringHelperService.ToQueryString(model.SpeciesId);
            //  var speciesString = "ParentId=4&DbParameterType=1";


            //var species_DDL_Response = await _httpClientService.GetDataListAsync(
            //new RequestParameters
            //{
            //    Action = "GetDataListDbParameter",
            //    Controller = "DbParameter",
            //    Folder = "Management",
            //    QueryString = speciesString
            //});

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
    }

}
