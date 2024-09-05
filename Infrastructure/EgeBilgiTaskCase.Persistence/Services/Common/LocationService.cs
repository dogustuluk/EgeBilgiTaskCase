using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Persistence.Services.Common
{
    [Service(ServiceLifetime.Scoped)]
    public class LocationService : ILocationService
    {
        private readonly IRickAndMortyApiService _rickAndMortyApiService;
        private readonly ILocationWriteRepository _locationWriteRepositoryService;
        private readonly ILocationReadRepository _locationReadRepositoryService;
        private readonly IMapper _mapper;
        private readonly IDbParameterReadRepository _dbParameterReadRepository;
        private readonly IDbParameterWriteRepository _dbParameterWriteRepository;

        public LocationService(IRickAndMortyApiService rickAndMortyApiService, ILocationWriteRepository locationWriteRepositoryService, ILocationReadRepository locationReadRepositoryService, IMapper mapper, IDbParameterReadRepository dbParameterReadRepository, IDbParameterWriteRepository dbParameterWriteRepository)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _locationWriteRepositoryService = locationWriteRepositoryService;
            _locationReadRepositoryService = locationReadRepositoryService;
            _mapper = mapper;
            _dbParameterReadRepository = dbParameterReadRepository;
            _dbParameterWriteRepository = dbParameterWriteRepository;
        }

        public async Task<int> SaveOrGetLocationParameterId(string name, int typeId, int dimensionId)
        {
            int dimensionParameterId = await SaveOrGetDbParameterIdAsync(dimensionId.ToString(), 7, 5); 

            int typeParameterId = await SaveOrGetDbParameterIdAsync(typeId.ToString(), 6, 5); 

            var locationParameter = await _dbParameterReadRepository.GetSingleEntityAsync(p => p.DBParameterName1 == name && p.DbParameterTypeId == 5);
            if (locationParameter != null)
                return locationParameter.Id;

            var newLocationParameter = new DbParameter
            {
                DBParameterName1 = name,
                DbParameterTypeId = typeParameterId,
                Id = dimensionParameterId,
                ParentId = 5,
                isEditable = true,
                isActive = true
            };

            var savedLocationParameter = await _dbParameterWriteRepository.AddAsyncReturnEntity(newLocationParameter);
            await _dbParameterWriteRepository.SaveChanges();
            return savedLocationParameter.Id;
        }


        //
        public async Task<int> SaveOrGetDbParameterIdAsync(string value, int parameterTypeId, int parentId)
        {
            if (string.IsNullOrEmpty(value))
                return 0;

            var existingParameter = _dbParameterReadRepository.GetSingleEntityAsync(p => p.DBParameterName1 == value && p.DbParameterTypeId == parameterTypeId).Result;
            if (existingParameter != null)
                return existingParameter.Id;

            var newParameter = new DbParameter
            {
                DBParameterName1 = value,
                DbParameterTypeId = parameterTypeId,
                ParentId = parentId,
                isEditable = true,
                isActive = true
            };

            var savedParameter = await _dbParameterWriteRepository.AddAsyncReturnEntity(newParameter);
            await _dbParameterWriteRepository.SaveChanges();
            return savedParameter.Id;
        }

        //
        public async Task SaveAllLocationToDatabase()
        {
            int pageNumber = 1;
            bool hasMorePages = true;

            try
            {
                while (hasMorePages)
                {
                    var locationListDto = _rickAndMortyApiService.GetLocationsAsync(pageNumber).Result;

                    foreach (var locationDto in locationListDto.Results)
                    {
                        await SaveLocationToDatabase(locationDto);
                    }

                    hasMorePages = pageNumber < locationListDto.Info.Pages;
                    pageNumber++;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }
        }

        //
        public async Task SaveLocationToDatabase(LocationDto locationDto)
        {
            var dimensionId = await SaveOrGetDbParameterIdAsync(locationDto.Dimension, 7, 5); 
            var typeId = await SaveOrGetDbParameterIdAsync(locationDto.Type, 6, 5);

            var locationEntity = new Location
            {
                LocationApiId = locationDto.Id,
                Name = locationDto.Name,
                TypeId = typeId,
                DimensionId = dimensionId,
                Residents = ExtractIdsFromUrls(locationDto.Residents),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var data = await _locationWriteRepositoryService.AddAsyncReturnEntity(locationEntity);
            await _locationWriteRepositoryService.SaveChanges();
        }



        public int ExtractIdFromUrl(string url)
        {
            if (string.IsNullOrEmpty(url))
                return 0;

            var parts = url.Split('/');
            return int.Parse(parts.Last());
        }
        public List<int> ExtractIdsFromUrls(List<string> urls)
        {
            return urls.Select(url => ExtractIdFromUrl(url)).ToList();
        }
    }
}
