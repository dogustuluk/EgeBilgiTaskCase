using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Client.Models;
using System.Text;
using System.Text.Json;

namespace EgeBilgiTaskCase.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl;

        public HttpClientService(IHttpClientFactory httpClientFactory, string baseUrl)
        {
            _httpClientFactory = httpClientFactory;
            _baseUrl = baseUrl;
        }
        private string BuildUrl(RequestParameters requestParameters, string id = null)
        {
            if (!string.IsNullOrEmpty(requestParameters.FullEndpoint))
            {
                return requestParameters.FullEndpoint;
            }

            var url = $"{requestParameters.BaseUrl ?? _baseUrl}/{requestParameters.Folder}/{requestParameters.Controller}";

            if (!string.IsNullOrEmpty(requestParameters.Action))
            {
                url += $"/{requestParameters.Action}";
            }

            if (!string.IsNullOrEmpty(id))
            {
                url += $"/{id}";
            }

            if (!string.IsNullOrEmpty(requestParameters.QueryString))
            {
                url += $"?{requestParameters.QueryString}";
            }

            return url;
        }

        private HttpRequestMessage CreateRequest(HttpMethod method, string url, object body = null, Dictionary<string, string> headers = null)
        {
            var request = new HttpRequestMessage(method, url);

            if (body != null)
            {
                var jsonData = JsonSerializer.Serialize(body);
                request.Content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            return request;
        }

        public async Task<OptResult<T>> GetAsync<T>(RequestParameters requestParameters, string id = null)
        {
            var url = BuildUrl(requestParameters, id);
            var request = CreateRequest(HttpMethod.Get, url, headers: requestParameters.Headers);
            var response = await SendRequestAsync(request);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var optResult = JsonSerializer.Deserialize<OptResult<T>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (optResult == null)
                throw new Exception("Deserialization failed or response was null.");
            if (!optResult.Succeeded)
                throw new Exception($"Request failed with message: {optResult.Message}");

            return optResult;
        }
        public async Task<string> GetJsonAsync(RequestParameters requestParameters)
        {
            var url = BuildUrl(requestParameters);
            var request = CreateRequest(HttpMethod.Get, url, headers: requestParameters.Headers);
            var response = await SendRequestAsync(request);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return jsonResponse;
        }

        public async Task<List<DataList1>> GetDataListAsync(RequestParameters requestParameters, string id = null)
        {
            var url = BuildUrl(requestParameters, id);

            var request = CreateRequest(HttpMethod.Get, url, headers: requestParameters.Headers);

            var response = await SendRequestAsync(request);

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var dataList = JsonSerializer.Deserialize<List<DataList1>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (dataList == null)
                throw new Exception("Deserialization failed or response was null.");

            return dataList;
        }

        public async Task<OptResult<OptResultClient>> PostAsync(RequestParameters requestParameters, object body)
        {
            var client = _httpClientFactory.CreateClient();
            var url = BuildUrl(requestParameters);

            var jsonContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, jsonContent);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            // Yanıtı doğrudan inceleyin
            Console.WriteLine("JSON Response: " + jsonResponse);

            var result = JsonSerializer.Deserialize<OptResult<OptResultClient>>(jsonResponse, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return result;

        }

        public async Task<T> PutAsync<T>(RequestParameters requestParameters, object body)
        {
            var url = BuildUrl(requestParameters);
            var request = CreateRequest(HttpMethod.Put, url, body, requestParameters.Headers);
            var response = await SendRequestAsync(request);
            return await DeserializeResponse<T>(response);
        }

        public async Task DeleteAsync<T>(RequestParameters requestParameters, string id)
        {
            var url = BuildUrl(requestParameters, id);
            var request = CreateRequest(HttpMethod.Delete, url, headers: requestParameters.Headers);
            await SendRequestAsync(request);
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var responseData = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseData);
        }

        public async Task<T> PostAsync2<T>(RequestParameters requestParameters, object body)
        {
            var client = _httpClientFactory.CreateClient();
            var url = BuildUrl(requestParameters);

            var jsonContent = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            var jsonContentString = await jsonContent.ReadAsStringAsync();
            Console.WriteLine("JSON Content: " + jsonContentString);


            try
            {
                var response = await client.PostAsync(url, jsonContent);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Response Content: " + responseContent);
                var result = JsonSerializer.Deserialize<T>(responseContent, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return result;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("HttpRequestException: " + ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }



            //response.EnsureSuccessStatusCode();

            //var jsonResponse = await response.Content.ReadAsStringAsync();

            //Console.WriteLine("JSON Response: " + jsonResponse);

            //var result = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions
            //{
            //    PropertyNameCaseInsensitive = true
            //});

            return default;
        }

        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            var client = _httpClientFactory.CreateClient("MyApiClient");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            return response;
        }
        private string GenerateQueryString<TRequest>(TRequest request)
        {
            // Use reflection or another method to generate query strings from the request object.
            var properties = typeof(TRequest).GetProperties()
                                .Where(p => p.GetValue(request) != null)
                                .Select(p => $"{p.Name}={p.GetValue(request)}");

            return string.Join("&", properties);
        }
    }
}
