using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Repositories.Character;
using EgeBilgiTaskCase.Domain.Entities.Character;
using Microsoft.EntityFrameworkCore;

namespace EgeBilgiTaskCase.Persistence.Services.Characters
{
    [Service(ServiceLifetime.Scoped)]
    public class CharacterService : ICharacterService
    {
        private readonly IRickAndMortyApiService _rickAndMortyApiService;
        private readonly ICharacterWriteRepository _characterWriteRepositoryService;
        private readonly ICharacterReadRepository _characterReadRepositoryService;
        private readonly IMapper _mapper;
        private readonly IDbParameterWriteRepository _dbParameterWriteRepository;
        private readonly ICharacterDetailWriteRepository _characterDetailWriteRepository;
        private readonly CharacterSpecifications _characterSpecifications;
        private readonly IDbParameterService _dbParameterService;
        private readonly ILocationService _locationService;
        private readonly IEpisodeService _episodeService;


        public CharacterService(IRickAndMortyApiService rickAndMortyApiService, ICharacterWriteRepository characterWriteRepositoryService, IMapper mapper, ICharacterReadRepository characterReadRepositoryService, IDbParameterWriteRepository dbParameterWriteRepository, ICharacterDetailWriteRepository characterDetailWriteRepository,CharacterSpecifications characterSpecifications, IDbParameterService dbParameterService, ILocationService locationService, IEpisodeService episodeService)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _characterWriteRepositoryService = characterWriteRepositoryService;
            _mapper = mapper;
            _characterReadRepositoryService = characterReadRepositoryService;
            _dbParameterWriteRepository = dbParameterWriteRepository;
            _characterDetailWriteRepository = characterDetailWriteRepository;
            _characterSpecifications = characterSpecifications;
            _dbParameterService = dbParameterService;
            _locationService = locationService;
            _episodeService = episodeService;
        }

        public async Task SaveCharacterToDatabase(CharacterDto characterDto)
        {
            var characterEntity = new Character
            {
                CharacterApiId = characterDto.Id,
                Name = characterDto.Name,
                Gender = characterDto.Gender,
                Image = characterDto.Image,
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

            var existingParameter = _dbParameterService.GetEntity(p => p.DBParameterName1 == value && p.DbParameterTypeId == parameterTypeId).Result;
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


        public async Task<OptResult<PaginatedList<Character_GridView_Dto>>> GetAllPagedCharacterAsync(Character_Index_Dto model)
        {
            var predicate = _characterSpecifications.GetAllPagedPredicate(model);
            if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id ASC";
            PaginatedList<Character> pagedDatas;

            pagedDatas = await _characterReadRepositoryService.GetDataPagedAsyncInclude(predicate, query => query.Include(c => c.CharacterDetails), model.PageIndex, model.Take, model.OrderBy, true);

            var gridViewDtoList = new List<Character_GridView_Dto>();

            foreach (var character in pagedDatas.Data)
            {
                string? statusName = "Not Specified";
                string? speciesName = "Not Specified";
                string? typeName = "Not Specified";
                string? lastKnownLocation = "Not Specified";
                string? firstSeen = "Not Specified";

                if (character.CharacterDetails != null)
                {
                    statusName = await _dbParameterService.GetValue("", "DBParameterName1", $"Id = {character.CharacterDetails.StatusId}", 2) ?? "Not Specified";

                    speciesName = await _dbParameterService.GetValue("", "DBParameterName1", $"Id = {character.CharacterDetails.SpeciesId}", 2) ?? "Not Specified";

                     typeName = await _dbParameterService.GetValue("", "DBParameterName1", $"Id = {character.CharacterDetails.TypeId}", 2) ?? "Not Specified";

                     lastKnownLocation = await _locationService.GetValue("", "Name", $"LocationApiId = {character.CharacterDetails.LocationId}", 2) ?? "Not Specified";


                    var episodesIds = character.CharacterDetails.EpisodeIds;
                    var firstEpisodeId = episodesIds.FirstOrDefault();

                    firstSeen = await _episodeService.GetValue("", "Name", $"EpisodeApiId = {firstEpisodeId}", 2) ?? "Not Specified";
                }
                

                var dto = new Character_GridView_Dto
                {
                    CharacterId = character.Id,
                    CharacterApiId = character.CharacterApiId,
                    CharacterGuid = character.Guid,
                    CharacterName = character.Name,
                    Image = character.Image,
                    LastKnownLocationName = lastKnownLocation,
                    FirstSeenName = firstSeen,
                    SpeciesName = speciesName,
                    Gender = character.Gender,
                    StatusName = statusName
                };
                gridViewDtoList.Add(dto);
            }

            var paginatedResult = new PaginatedList<Character_GridView_Dto>
            {
                Data = gridViewDtoList,
                Pagination = new Pagination
                {
                    TotalPages = pagedDatas.Pagination.TotalPages,
                    TotalRecords = pagedDatas.Pagination.TotalRecords,
                    PageIndex = pagedDatas.Pagination.PageIndex,
                    PageSize = pagedDatas.Pagination.PageSize,
                    HasNextPage = pagedDatas.Pagination.HasNextPage,
                    HasPreviousPage = pagedDatas.Pagination.HasPreviousPage
                }
            };

            return await OptResult<PaginatedList<Character_GridView_Dto>>.SuccessAsync(paginatedResult, Messages.Successfull);
        }

        public async Task<OptResult<Character>> AddNewAsync(Character_AddNew_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                var mappedModel = _mapper.Map<Character>(model);
                bool isExist = await _characterReadRepositoryService.ExistsAsync(a => a.Name == model.Name);
                if (isExist)
                    return await OptResult<Character>.FailureAsync(Messages.AddedDataIsAlready);
                mappedModel.Guid = Guid.NewGuid();
                
                var data = await _characterWriteRepositoryService.AddAsyncReturnEntity(mappedModel);
                await _characterWriteRepositoryService.SaveChanges();

                if (model.CharacterDetail != null)
                {
                    var mappedCharacterModel = _mapper.Map<CharacterDetail>(model.CharacterDetail);
                    mappedCharacterModel.Guid = Guid.NewGuid();
                    mappedCharacterModel.CharacterId = data.Id;
                    var characterDetailData = await _characterDetailWriteRepository.AddAsyncReturnEntity(mappedCharacterModel);
                    await _characterDetailWriteRepository.SaveChanges();
                }
                return await OptResult<Character>.SuccessAsync(data);
            });
        }
    }
}
