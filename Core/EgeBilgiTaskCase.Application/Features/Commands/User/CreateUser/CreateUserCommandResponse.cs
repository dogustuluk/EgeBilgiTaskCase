namespace EgeBilgiTaskCase.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandResponse
    {
        public Guid Guid { get; set; }
        public string NameSurname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string GSM { get; set; }
    }
}