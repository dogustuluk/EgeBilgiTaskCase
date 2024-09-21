namespace EgeBilgiTaskCase.Application.Features.Queries.User.GetAllPagedUser;

public class GetAllPagedUserQueryResponse
{
    public int Id { get; set; }
    public Guid Guid { get; set; }
    public string NameSurname { get; set; }
    public string UserName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string GSM { get; set; }
    public string GSMPersonal { get; set; }
    public int StatusId { get; set; }
    public DateTime BirthDate { get; set; }
    public string Image { get; set; }
}