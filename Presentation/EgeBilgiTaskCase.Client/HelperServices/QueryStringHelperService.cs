using System.Reflection;
using System.Web;

namespace EgeBilgiTaskCase.Client.HelperServices
{
    public static class QueryStringHelperService
    {
        public static string ToQueryString<T>(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(p => p.GetValue(model, null) != null)
                                      .Select(p => $"{HttpUtility.UrlEncode(p.Name)}={HttpUtility.UrlEncode(p.GetValue(model, null).ToString())}");

            return string.Join("&", properties);
        }

    }
}
