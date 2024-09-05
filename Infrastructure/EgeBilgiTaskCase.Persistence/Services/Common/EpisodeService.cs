using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;

namespace EgeBilgiTaskCase.Persistence.Services.Common
{
    [Service(ServiceLifetime.Scoped)]
    public class EpisodeService : IEpisodeService
    {
        private readonly IRickAndMortyApiService _rickAndMortyApiService;
        private readonly IEpisodeWriteRepository _episodeWriteRepositoryService;
        private readonly IMapper _mapper;


        public EpisodeService(IRickAndMortyApiService rickAndMortyApiService, IEpisodeWriteRepository episodeWriteRepositoryService, IMapper mapper)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _episodeWriteRepositoryService = episodeWriteRepositoryService;
            _mapper = mapper;
        }
       

        //
        public async Task SaveAllEpisodeToDatabase()
        {
            int pageNumber = 1;
            bool hasMorePages = true;

            try
            {
                while (hasMorePages)
                {
                    var locationListDto = _rickAndMortyApiService.GetEpisodesAsync(pageNumber).Result;

                    foreach (var locationDto in locationListDto.Results)
                    {
                        await SaveEpisodeToDatabase(locationDto);
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
        public async Task SaveEpisodeToDatabase(EpisodeDto episodeDto)
        {
            var episodeEntity = new Episode
            {
                EpisodeApiId = episodeDto.Id,
                Name = episodeDto.Name,
                AirDate= episodeDto.AirDate,
                ApiCreatedDate= episodeDto.ApiCreatedDate,
                EpisodeStamp = episodeDto.Episode,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var data = await _episodeWriteRepositoryService.AddAsyncReturnEntity(episodeEntity);
            await _episodeWriteRepositoryService.SaveChanges();
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
