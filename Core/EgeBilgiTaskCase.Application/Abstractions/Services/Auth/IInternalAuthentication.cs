namespace EgeBilgiTaskCase.Application.Abstractions.Services.Auth
{
    public interface IInternalAuthentication
    {
        Task<Application.Common.DTOs.Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime);
        Task<Application.Common.DTOs.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}