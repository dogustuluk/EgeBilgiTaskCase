using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Application.Features.Queries.DbParameter.GetPagedDbParameter;
using EgeBilgiTaskCase.Client.HelperServices;
using EgeBilgiTaskCase.Client.Models;
using EgeBilgiTaskCase.Client.Models.Management;
using EgeBilgiTaskCase.Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.Client.Controllers;
public class DbParameterController : Controller
{
    private readonly IHttpClientService _httpClientService;

    public DbParameterController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<IActionResult> Index(DbParameter_Index_ViewModel model)
    {
        var queryString = QueryStringHelperService.ToQueryString(model);

        var response = await _httpClientService.GetAsync<PaginatedList<GetPagedDbParameterQueryResponse>>(
                    new RequestParameters
                    {
                        Action = "GetPagedData",
                        Controller = "DbParameter",
                        Folder = "Management",
                        QueryString = queryString
                    }
                );

        List<DbParameter_Grid_ViewModel> myData = new();

        foreach (var data in response.Data.Data)
        {
            //var dbParameterTypeQueryString = QueryStringHelperService.ToQueryString(new
            //{
            //    DbParameterTypeId = data.DbParameterTypeId,
            //    ParentId = 4
            //});
            myData.Add(new DbParameter_Grid_ViewModel()
            {
                Guid = data.Guid,
                DBParameterName1 = data.DBParameterName1,
                DbParameterTypeId = data.DbParameterTypeId,
                ParentId = data.ParentId,
                SortOrderNo = data.SortOrderNo,
                IsActive = data.isActive,
                IsEditable = data.isEditable,
                RMemberId = data.RMemberId,
                CreatedDate = data.CreatedDate,
                UpdatedDate = data.UpdatedDate,
            });
        }

        //ddl
        var parent_DDL_Response = await _httpClientService.GetDataListAsync(
                new RequestParameters
                {
                    Action = "GetDataListDbParameterType",
                    Controller = "DbParameterType",
                    Folder = "Management",
                    QueryString = "ParentId=0"
                });
        List<DataList1> parent_DDL = parent_DDL_Response;


        DbParameter_Index_ViewModel MYRESULT = new()
        {
            PageTitle = "Yönetim",
            SubPageTitle = "Veri Tabanı Değişkenleri",
            MyGridData = myData,
            MyPagination = response.Data.Pagination,
            SearchText = model.SearchText,
            Take = model.Take,
            PageIndex = model.PageIndex,
            OrderBy = model.OrderBy,
        };
        return View(MYRESULT);
    }
}
