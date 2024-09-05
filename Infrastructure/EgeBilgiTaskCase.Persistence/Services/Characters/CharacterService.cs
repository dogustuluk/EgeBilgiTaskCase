using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Repositories.Character;
using EgeBilgiTaskCase.Application.Repositories.Common;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Persistence.Services.Characters
{
    [Service(ServiceLifetime.Scoped)]
    public class CharacterService : ICharacterService
    {
        private readonly IRickAndMortyApiService _rickAndMortyApiService;
        private readonly ICharacterWriteRepository _characterWriteRepositoryService;
        private readonly ICharacterReadRepository _characterReadRepositoryService;
        private readonly IMapper _mapper;
        private readonly IDbParameterReadRepository _dbParameterReadRepository;
        private readonly IDbParameterWriteRepository _dbParameterWriteRepository;
        private readonly ICharacterDetailWriteRepository _characterDetailWriteRepository;

        public CharacterService(IRickAndMortyApiService rickAndMortyApiService, ICharacterWriteRepository characterWriteRepositoryService, IMapper mapper, ICharacterReadRepository characterReadRepositoryService, IDbParameterReadRepository dbParameterReadRepository, IDbParameterWriteRepository dbParameterWriteRepository, ICharacterDetailWriteRepository characterDetailWriteRepository)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _characterWriteRepositoryService = characterWriteRepositoryService;
            _mapper = mapper;
            _characterReadRepositoryService = characterReadRepositoryService;
            _dbParameterReadRepository = dbParameterReadRepository;
            _dbParameterWriteRepository = dbParameterWriteRepository;
            _characterDetailWriteRepository = characterDetailWriteRepository;
        }

        public async Task SaveCharacterToDatabase(CharacterDto characterDto)
        {
            var characterEntity = new Character
            {
                CharacterApiId = characterDto.Id,
                Name = characterDto.Name,
                Gender = characterDto.Gender,
                Image = characterDto.Image,
              //  EpisodeIds = ExtractIdsFromUrls(characterDto.Episode)
            };

            var data = await _characterWriteRepositoryService.AddAsyncReturnEntity(characterEntity);
            await _characterWriteRepositoryService.SaveChanges();

            int speciesId = await SaveOrGetDbParameterId(characterDto.Species, 1); // 2=Species
            int typeId = await SaveOrGetDbParameterId(characterDto.Type, 2); // 3=Type
            int statusId = await SaveOrGetDbParameterId(characterDto.Status, 3); // 1=Status

            var guid = Guid.NewGuid();
            var characterDetailEntity = new CharacterDetail
            {
                CharacterId = data.Id,
                OriginId = ExtractIdFromUrl(characterDto.Origin.Url),
                LocationId = ExtractIdFromUrl(characterDto.Location.Url),
                StatusId = statusId,
                SpeciesId = speciesId,
                TypeId = typeId,
                EpisodeIds = ExtractIdsFromUrls(characterDto.Episode),
                CreatedUser = guid,
                CreatedDate = DateTime.UtcNow,
                UpdatedUser = guid,
                UpdatedDate = DateTime.UtcNow,
            };

            await _characterDetailWriteRepository.AddAsyncReturnEntity(characterDetailEntity);
            await _characterDetailWriteRepository.SaveChanges();
        }
        public async Task SaveAllCharactersToDatabase()
        {
            int pageNumber = 1;
            bool hasMorePages = true;

            try
            {
                while (hasMorePages)
                {
                    var characterListDto = _rickAndMortyApiService.GetCharactersAsync(pageNumber).Result;

                    foreach (var characterDto in characterListDto.Results)
                    {
                        await SaveCharacterToDatabase(characterDto);
                    }

                    hasMorePages = pageNumber < characterListDto.Info.Pages;
                    pageNumber++;
                }
            }
            catch (Exception ex)
            {
                var err = ex.Message;
                throw;
            }


        }

        public async Task<int> SaveOrGetDbParameterId(string value, int parameterTypeId)
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
                isEditable = true,
                isActive = true
            };

            var savedParameter = await _dbParameterWriteRepository.AddAsyncReturnEntity(newParameter);
            await _dbParameterWriteRepository.SaveChanges();
            return savedParameter.Id;
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



        public async Task<OptResult<PaginatedList<Character>>> GetAllPagedCharacterAsync(Character_Index_Dto model)
        {
            // var predicate = _errorSpecications.GetAllPagedPredicate(model);
            if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id DESC";

            PaginatedList<Character> pagedDatas;

            pagedDatas = await _characterReadRepositoryService.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy, true);

            return await OptResult<PaginatedList<Character>>.SuccessAsync(pagedDatas, Messages.Successfull);
        }
    }
}
