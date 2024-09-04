using EgeBilgiTaskCase.Application.Abstractions.Services.Auth;
using EgeBilgiTaskCase.Application.Abstractions.Token;
using EgeBilgiTaskCase.Application.Common.DTOs;
using EgeBilgiTaskCase.Domain.Entities.Identity;
using HospitalManagement.Application.Abstractions.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EgeBilgiTaskCase.Persistence.Services.Auth
{
    [Service(ServiceLifetime.Scoped)]
    public class AuthService : IAuthService
    {
        readonly HttpClient _httpClient; 
        readonly IConfiguration _configuration;
        readonly UserManager<AppUser> _userManager;
        readonly ITokenHandler _tokenHandler;
        readonly SignInManager<AppUser> _signInManager;
        readonly IUserService _userService;

        public AuthService(HttpClient httpClient, IConfiguration configuration, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserService userService, ITokenHandler tokenHandler)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public async Task<Token> LoginAsync(string userNameOrEmail, string password, int accessTokenLifeTime)
        {
            AppUser? user = await _userManager.FindByNameAsync(userNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(userNameOrEmail);

            if (user == null)
                throw new ArgumentException(); //custom yap

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                //refresh token opt.
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 15);
                return token;
            }
            throw new ArgumentException(); //custom yap
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(a => a.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(15, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 300);
                return token;
            }
            else
                throw new ArgumentException(); //custom yap
        }
    }
}
