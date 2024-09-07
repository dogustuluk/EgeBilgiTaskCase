namespace EgeBilgiTaskCase.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandRequest : IRequest<OptResult<LoginUserCommandResponse>>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
