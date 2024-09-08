namespace EgeBilgiTaskCase.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandRequest : IRequest<OptResult<CreateUserCommandResponse>>
    {
        public Guid Guid { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string? Gender { get; set; }
        public string? IdentityNo { get; set; }
        public string? GSM { get; set; }
        public string? GSMPersonal { get; set; }
        public string? Email { get; set; }
        public int StatusId { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}