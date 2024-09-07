using EgeBilgiTaskCase.Application.Abstractions.Services.Auth;
using EgeBilgiTaskCase.Application.Common.Extensions;
using EgeBilgiTaskCase.Application.Constants;

namespace EgeBilgiTaskCase.Application.Features.Commands.User.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, OptResult<LoginUserCommandResponse>>
    {
        private readonly IInternalAuthentication _internalAuthentication;

        public LoginUserCommandHandler(IInternalAuthentication internalAuthentication)
        {
            _internalAuthentication = internalAuthentication;
        }

        public async Task<OptResult<LoginUserCommandResponse>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var token = await _internalAuthentication.LoginAsync(request.UsernameOrEmail, request.Password, 48);

                if (token == null)
                {
                    return await OptResult<LoginUserCommandResponse>.FailureAsync(Messages.UnSuccessfull);
                }

                var response = new LoginUserCommandResponse() { Token = token };
                return await OptResult<LoginUserCommandResponse>.SuccessAsync(response);
            });
        }
    }
}
