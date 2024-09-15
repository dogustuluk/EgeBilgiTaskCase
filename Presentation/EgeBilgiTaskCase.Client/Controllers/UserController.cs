using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;
using EgeBilgiTaskCase.Client.Models;
using EgeBilgiTaskCase.Client.Models.User;
using EgeBilgiTaskCase.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EgeBilgiTaskCase.Client.Controllers;
public class UserController : Controller
{
    private readonly IHttpClientService _httpClientService;

    public UserController(IHttpClientService httpClientService)
    {
        _httpClientService = httpClientService;
    }

    public async Task<IActionResult> Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> Update(Guid? Guid)
    {
        if (Guid == null)
        {
            return BadRequest("Guid değeri gereklidir."); //daha düzgün bir sayfaya gönder
        }
        var requestParameters = new RequestParameters
        {
            Folder = "User",
            Controller = "User",
            Action = "GetByIdOrGuidUser",
            QueryString = $"Guid={Guid}"
        };

        var myUserResponse = await ExceptionHandler.HandleOptResultAsync(() => _httpClientService.GetAsync<User_Update_ViewModel>(requestParameters));

        if (myUserResponse.Data == null || !myUserResponse.Succeeded)
        {
            return Redirect("/Auth/AccessDenied?isFancy=false&Message=" + Messages.DataNotFound);
        }
        myUserResponse.Data.PageTitle = "Kullanıcı Güncelleme";
        myUserResponse.Data.SubPageTitle = $"{myUserResponse.Data.NameSurname} Adlı Kullanıcının Detayları";

        return View(myUserResponse.Data);
    }
}
