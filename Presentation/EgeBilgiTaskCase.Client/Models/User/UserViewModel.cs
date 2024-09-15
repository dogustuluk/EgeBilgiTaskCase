namespace EgeBilgiTaskCase.Client.Models.User;

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
    public string GSM { get; set; }
    public string GSMPersonal { get; set; }
    public string Email { get; set; }
    public int EmailConfirmed { get; set; }
    public int StatusId { get; set; }
    public DateTime BirthDate { get; set; }
    public int TwoFactorEnabled { get; set; }
    public DateTime LockoutEnd { get; set; }
    public int LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}
