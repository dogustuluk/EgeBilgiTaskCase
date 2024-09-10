using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Common
{
    public interface IEpisodeService
    {
        Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);
        Task<List<DataList1>> GetDataListAsync();



        Task SaveAllEpisodeToDatabase();
        Task SaveEpisodeToDatabase(EpisodeDto episodeDto);
    }
}
