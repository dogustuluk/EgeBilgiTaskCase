using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;

namespace EgeBilgiTaskCase.Application.Abstractions.Services.Character
{
    public interface ILocationService
    {
        Task<int> SaveOrGetLocationParameterId(string name, int typeId, int dimensionId);


        Task SaveAllLocationToDatabase();
        Task SaveLocationToDatabase(LocationDto characterDto);

    }
}
