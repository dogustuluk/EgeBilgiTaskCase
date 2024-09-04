using EgeBilgiTaskCase.Domain.Entities.Identity;

namespace EgeBilgiTaskCase.Application.Abstractions.Token
{
    public interface ITokenHandler
    {
        Common.DTOs.Token CreateAccessToken(int hour, AppUser appUser);
        string CreateRefreshToken();
    }
}
