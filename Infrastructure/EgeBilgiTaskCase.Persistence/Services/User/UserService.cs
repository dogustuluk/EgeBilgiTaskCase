using EgeBilgiTaskCase.Application.Common.DTOs.User;
using EgeBilgiTaskCase.Application.Repositories;
using EgeBilgiTaskCase.Domain.Entities.Identity;
using HospitalManagement.Application.Abstractions.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagement.Persistence.Services.User
{
    [Service(ServiceLifetime.Scoped)]
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppUserReadRepository _userReadRepository;
        private readonly IMapper _mapper;
        private readonly UserSpecifications _userSpecifications;

        public UserService(UserManager<AppUser> userManager, IMapper mapper, IAppUserReadRepository userReadRepository, UserSpecifications userSpecifications)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userReadRepository = userReadRepository;
            _userSpecifications = userSpecifications;
        }

        public async Task<OptResult<CreateUser_Dto>> CreateAsync(CreateUser_Dto model)
        {
            try
            {
                var appUser = _mapper.Map<AppUser>(model);

                var existingUserName = await _userManager.FindByNameAsync(model.UserName);
                if (existingUserName != null)
                {
                    return OptResult<CreateUser_Dto>.Failure("UserName already exists.");
                }
                var existingEmail = await _userManager.FindByEmailAsync(model.Email);
                if (existingEmail != null)
                {
                    return OptResult<CreateUser_Dto>.Failure("Email already exists.");
                }

                appUser.Guid = Guid.NewGuid();
                IdentityResult result = await _userManager.CreateAsync(appUser, model.Password);

                OptResult<CreateUser_Dto> response = new() { Succeeded = result.Succeeded };

                if (result.Succeeded)
                {
                    response.Code = 200; //200?204?
                    response.Data = _mapper.Map<CreateUser_Dto>(appUser);
                }
                else
                    foreach (var error in result.Errors)
                        response.Message += $"{error.Code}-{error.Description}\n";

                return response;
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }

        }
        public async Task<OptResult<UpdateUser_Dto>> UpdateAsync(UpdateUser_Dto model)
        {
            return null;
        }
        public async Task<List<AppUser>> GetAllUser(Expression<Func<AppUser, bool>>? predicate, string? include)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                var users = await _userReadRepository.GetAllAsync(predicate, include);
                return await users.ToListAsync();
            });
        }

        public async Task<OptResult<PaginatedList<AppUser>>> GetAllPagedListAsync(GetAllPagedUser_Index_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                //  var predicate = _userSpecifications.GetAllPagedPredicate(model);
                if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "NameSurname ASC";

                PaginatedList<AppUser> pagedUsers;

                pagedUsers = await _userReadRepository.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy);

                return await OptResult<PaginatedList<AppUser>>.SuccessAsync(pagedUsers, Messages.Successfull);
            });
        }

        public async Task<OptResult<AppUser>> GetByIdOrGuid(object criteria)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                if (criteria == null)
                    return await OptResult<AppUser>.FailureAsync(Messages.NullValue);

                AppUser myHospital = null;

                if (criteria is Guid guid)
                    myHospital = await _userReadRepository.GetByGuidAsync(guid);
                else if (criteria is int id)
                    myHospital = await _userReadRepository.GetByIdAsync(id);

                if (myHospital == null)
                    return await OptResult<AppUser>.FailureAsync(Messages.NullData);

                return await OptResult<AppUser>.SuccessAsync(myHospital);
            });
        }

        public Task<List<DataList1>> GetDataListAsync(int? filter)
        {
            return ExceptionHandler.HandleAsync(async () =>
            {
                List<DataList1> returnDataList = new();

                var datas = await _userReadRepository.GetDataAsync(a => a.Id > 0, "", 10000, "NameSurname ASC");
                foreach (var data in datas)
                {
                    returnDataList.Add(new DataList1() { Guid = "", Id = data.Id.ToString(), Name = data.NameSurname });
                }
                return returnDataList;
            });
        }

        public async Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                var data = await _userReadRepository.GetValueAsync("AspNetUsers", column, sqlQuery, 1);
                if (data != null) return data;
                return Messages.NullData;
            });
        }

        public async Task UpdateRefreshToken(string refreshToken, AppUser user, DateTime accessTokenDate, int addOnAccessTokenDate)
        {
            if (user == null)
                throw new ArgumentException(); //custom yap
            user.RefreshToken = refreshToken;
            user.RefreshTokenEndDate = accessTokenDate.AddSeconds(addOnAccessTokenDate);
            await _userManager.UpdateAsync(user);
        }
    }
}
