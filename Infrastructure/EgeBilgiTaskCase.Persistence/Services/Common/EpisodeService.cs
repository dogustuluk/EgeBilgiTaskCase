using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EgeBilgiTaskCase.Persistence.Services.Common
{
    [Service(ServiceLifetime.Scoped)]
    public class EpisodeService : IEpisodeService
    {
        private readonly IRickAndMortyApiService _rickAndMortyApiService;
        private readonly IEpisodeWriteRepository _writeRepository;
        private readonly IEpisodeReadRepository _readRepository;
        private readonly IMapper _mapper;


        public EpisodeService(IRickAndMortyApiService rickAndMortyApiService, IMapper mapper, IEpisodeReadRepository readRepository, IEpisodeWriteRepository writeRepository)
        {
            _rickAndMortyApiService = rickAndMortyApiService;
            _mapper = mapper;
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public async Task<string> GetValue(string? table, string column, string sqlQuery, int? dbType)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                var data = await _readRepository.GetValueAsync("Episodes", column, sqlQuery, 2);
                if (data != null) return data;
                return Messages.NullData;
            });
        }
        public async Task<List<DataList1>> GetDataListAsync()
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                List<DataList1> returnDataList = new();
                var datas = await _readRepository.GetDataAsync(a => a.Id > 0, "", 10000, "Id ASC");
                foreach (var data in datas)
                {
                    returnDataList.Add(new DataList1() { Guid = "", Id = data.Id.ToString(), Name = data.Name.ToString() });
                }
                return returnDataList;
            });
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
                AirDate = episodeDto.AirDate,
                ApiCreatedDate = episodeDto.ApiCreatedDate,
                EpisodeStamp = episodeDto.Episode,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            var data = await _writeRepository.AddAsyncReturnEntity(episodeEntity);
            await _writeRepository.SaveChanges();
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

        public async Task<OptResult<Episode>> GetByIdOrGuid(object criteria)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                if (criteria == null)
                    return await OptResult<Episode>.FailureAsync(Messages.NullValue);

                Episode myData = null;

                if (criteria is Guid guid)
                    myData = await _readRepository.GetByGuidAsync(guid);
                else if (criteria is int id)
                    myData = await _readRepository.GetByIdAsync(id);

                if (myData == null)
                    return await OptResult<Episode>.FailureAsync(Messages.NullData);

                return await OptResult<Episode>.SuccessAsync(myData);
            });
        }

        public async Task<List<Episode>> GetAllEpisodeAsync(Expression<Func<Episode, bool>>? predicate, string? include)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                var datas = await _readRepository.GetAllAsync(predicate, include);
                return await datas.ToListAsync();
            });
        }

        public async Task<OptResult<PaginatedList<Episode>>> GetAllPagedEpisodeAsync(GetAllPaged_Episode_Index_Dto model)
        {
            return await ExceptionHandler.HandleOptResultAsync(async () =>
            {
                //  var predicate = _dbParameterSpecifications.GetAllPagedPredicate(model);
                if (string.IsNullOrEmpty(model.OrderBy)) model.OrderBy = "Id DESC";

                PaginatedList<Episode> pagedDbParameters;

                pagedDbParameters = await _readRepository.GetDataPagedAsync(a => a.Id > 0, "", model.PageIndex, model.Take, model.OrderBy);

                return await OptResult<PaginatedList<Episode>>.SuccessAsync(pagedDbParameters, Messages.Successfull);
            });
        }

        public async Task<bool> ExistsEpisodeAsync(Expression<Func<Episode, bool>> predicate)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                return await _readRepository.ExistsAsync(predicate);
            });
        }

        public async Task<Episode> GetEntity(Expression<Func<Episode, bool>> method, bool tracking = true)
        {
            return await ExceptionHandler.HandleAsync(async () =>
            {
                return await _readRepository.GetSingleEntityAsync(method);
            });
        }
    }
}
