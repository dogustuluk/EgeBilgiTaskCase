using EgeBilgiTaskCase.Application.Common.GenericObjects;

namespace EgeBilgiTaskCase.Client.Models.User;

public class User_Index_ViewModel
{
    public string? PageTitle { get; set; }
    public string? SubPageTitle { get; set; }
    public int PageIndex { get; set; } = 1;
    public string? SearchText { get; set; }
    public int? StatusId { get; set; }

    public int Take { get; set; } = 15;
    public string? OrderBy { get; set; } = "Id ASC";
    public List<User_IndexGrid_ViewModel>? MyGridData { get; set; }
    public Pagination? MyPagination { get; set; }
}
public class User_IndexGrid_ViewModel
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public string GSM { get; set; }
    public string GSMPersonal { get; set; }
    public string Email { get; set; }
    public int StatusId { get; set; }
    public string Image { get; set; }
}
public class User_Update_ViewModel
{
    public string? PageTitle { get; set; }
    public string? SubPageTitle { get; set; }
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public string IdentityNo { get; set; }
    public string Image { get; set; }
    public string GSM { get; set; }
    public string GSMPersonal { get; set; }
    public string Email { get; set; }
    public int EmailConfirmed { get; set; }
    public int PhoneNumberConfirmed { get; set; }
    public int StatusId { get; set; }
    public DateTime BirthDate { get; set; }
    public int TwoFactorEnabled { get; set; }
    public DateTime LockoutEnd { get; set; }
    public int LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}
