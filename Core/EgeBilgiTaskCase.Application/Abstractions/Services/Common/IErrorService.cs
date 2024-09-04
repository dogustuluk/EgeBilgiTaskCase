using EgeBilgiTaskCase.Application.Common.DTOs.Common;
using EgeBilgiTaskCase.Domain.Entities.Common;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Common
{
    public interface IErrorService
    {
        Task<OptResult<Error>> AddErrorAsync(Create_Error_Dto model);
        Task<OptResult<PaginatedList<Error>>> GetAllPagedErrorAsync(GetAllPaged_Error_Dto model);
    }
}
