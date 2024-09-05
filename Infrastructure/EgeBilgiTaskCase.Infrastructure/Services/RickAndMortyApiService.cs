using EgeBilgiTaskCase.Application.Abstractions.Services.Character;
using EgeBilgiTaskCase.Application.Common.DTOs.RickAndMorty;
using EgeBilgiTaskCase.Domain.Entities.Character;
using Newtonsoft.Json;

namespace EgeBilgiTaskCase.Infrastructure.Services
{
    public class ApiResponse<T>
    {
        public List<T> Results { get; set; } 
        public Info Info { get; set; }
    }

    public class Info
    {
        public int Count { get; set; }
        public int Pages { get; set; } 
        public string Next { get; set; } 
        public string Prev { get; set; } 
    }

    public class RickAndMortyApiService : IRickAndMortyApiService
    {
        private readonly HttpClient _httpClient;

        public RickAndMortyApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CharacterListDto> GetCharactersAsync(int pageNumber)
        {
            var response = await _httpClient.GetAsync($"https://rickandmortyapi.com/api/character/?page={pageNumber}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var characterListDto = JsonConvert.DeserializeObject<CharacterListDto>(content);  
            
            return characterListDto;
        }


        public async Task<EpisodeListDto> GetEpisodesAsync(int pageNumber)
        {
            var response = await _httpClient.GetAsync($"https://rickandmortyapi.com/api/episode/?page={pageNumber}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<EpisodeListDto>(content);
            return result;
        }

        public async Task<LocationListDto> GetLocationsAsync(int pageNumber)
        {
            var response = await _httpClient.GetAsync($"https://rickandmortyapi.com/api/location/?page={pageNumber}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<LocationListDto>(content);
            return result;
        }

    }
}
