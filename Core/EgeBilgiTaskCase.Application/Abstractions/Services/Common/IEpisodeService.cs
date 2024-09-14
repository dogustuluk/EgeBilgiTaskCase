using EgeBilgiTaskCase.Application.Common.DTOs.Common;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Common
{
    public interface IEpisodeService
    {
        // Task<OptResult<Episode>> CreateEpisodeAsync(Create_Episode_Dto model);
        //  Task<OptResult<Episode>> UpdateEpisodeAsync(Update_Episode_Dto model);
        Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
        Task<List<DataList1>> GetDataListAsync();


        Task<OptResult<Episode>> GetByIdOrGuid(object criteria);
        Task<List<Episode>> GetAllEpisodeAsync(Expression<Func<Episode, bool>>? predicate, string? include);
        Task<OptResult<PaginatedList<Episode>>> GetAllPagedEpisodeAsync(GetAllPaged_Episode_Index_Dto model);
        Task<bool> ExistsEpisodeAsync(Expression<Func<Episode, bool>> predicate);
        Task<Episode> GetEntity(Expression<Func<Episode, bool>> method, bool tracking = true);



        Task SaveAllEpisodeToDatabase();
        Task SaveEpisodeToDatabase(EpisodeDto episodeDto);
    }
}
