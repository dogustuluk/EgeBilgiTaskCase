using EgeBilgiTaskCase.Application.Common.GenericObjects;
using EgeBilgiTaskCase.Client.Models;

namespace EgeBilgiTaskCase.Client.Services
{
    public interface IHttpClientService
    {
        //Task<HttpResponseMessage> SendRequestAsync(string method, string url, HttpContent content = null);
        //string BuildUrl(string controller, string action, Dictionary<string, string> queryParams = null);
        //Task<T> GetAsync<T>(string controller, string action, Dictionary<string, string> queryParams = null);
        //Task<T> PostAsync<T>(string controller, string action, object data, Dictionary<string, string> queryParams = null);
        //Task<T> PutAsync<T>(string controller, string action, object data, Dictionary<string, string> queryParams = null);
        //Task DeleteAsync(string controller, string action, Dictionary<string, string> queryParams = null);

        //Task<T> GetAsync<T>(RequestParameters requestParameters, string id = null);
        Task<OptResult<T>> GetAsync<T>(RequestParameters requestParameters, string id = null);
        Task<List<DataList1>> GetDataListAsync(RequestParameters requestParameters, string id = null);
        Task<T> PostAsync2<T>(RequestParameters requestParameters, object body);
        Task<OptResult<OptResultClient>> PostAsync(RequestParameters requestParameters, object body);
        Task<T> PutAsync<T>(RequestParameters requestParameters, object body);
        Task DeleteAsync<T>(RequestParameters requestParameters, string id);


    }
}
