using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Common
{
    public interface ILocationService
    {
        Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType);



        Task<int> SaveOrGetLocationParameterId(string name, int typeId, int dimensionId);
        Task SaveAllLocationToDatabase();
        Task SaveLocationToDatabase(LocationDto characterDto);

    }
}
