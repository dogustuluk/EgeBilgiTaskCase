using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Application.Repositories.Character;
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

        public CharacterService(IRickAndMortyApiService rickAndMortyApiService, ICharacterWriteRepository characterWriteRepositoryService, IMapper mapper, ICharacterReadRepository characterReadRepositoryService)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _characterWriteRepositoryService = characterWriteRepositoryService;
            _mapper = mapper;
            _characterReadRepositoryService = characterReadRepositoryService;
        }

        public async Task SaveCharacterToDatabase(CharacterDto characterDto)
        {
            var characterEntity = new Character
            {
                CharacterApiId = characterDto.Id,
                Name = characterDto.Name,
                Status = characterDto.Status,
                Species = characterDto.Species,
                Type = characterDto.Type,
                Gender = characterDto.Gender,
                Image = characterDto.Image,
                OriginId = ExtractIdFromUrl(characterDto.Origin.Url),
                LocationId = ExtractIdFromUrl(characterDto.Location.Url),
                EpisodeIds = ExtractIdsFromUrls(characterDto.Episode)
            };

            var data = await _characterWriteRepositoryService.AddAsyncReturnEntity(characterEntity);
            await _characterWriteRepositoryService.SaveChanges();

        }
        public async Task SaveAllCharactersToDatabase()
        {
            //try
            //{
            //    var characterDtos = _rickAndMortyApiService.GetCharactersAsync().Result;

            //    foreach (var characterDto in characterDtos)
            //    {
            //        SaveCharacterToDatabase(characterDto);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    var err = ex.Message;
            //    throw;
            //}
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

        public async Task<OptResult<PaginatedList<Character>>> GetAllPagedCharacterAsync(Character_Index_Dto model)
        {
            // var predicate = _errorSpecications.GetAllPagedPredicate(model);
            if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id DESC";

            PaginatedList<Character> pagedDatas;

            pagedDatas = await _characterReadRepositoryService.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy, true);

            return await OptResult<PaginatedList<Character>>.SuccessAsync(pagedDatas, Messages.Successfull);
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
