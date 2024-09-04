using System.Text.Json;

namespace EgeBilgiTaskCase.Application.Utilities.Helpers
{
    public static class PredicateHelper
    {
        public static Expression<Func<T, bool>> BuildPredicate<T>(Dictionary<string, object> filters)
        {
            var predicate = PredicateBuilder.New<T>(true);
            foreach (var filter in filters)
            {
                var propertyName = filter.Key;
                var propertyValue = filter.Value;

                var parameter = Expression.Parameter(typeof(T), "x");
                var member = Expression.Property(parameter, propertyName);
                var propertyType = member.Type;

                object convertedValue = ConvertJsonElement(propertyValue, propertyType);

                var constant = Expression.Constant(convertedValue, propertyType);
                var body = Expression.Equal(member, constant);
                var lambda = Expression.Lambda<Func<T, bool>>(body, parameter);
                predicate = predicate.And(lambda);
            }
            return predicate;
        }

        private static object ConvertJsonElement(object value, Type propertyType)
        {
            if (value is JsonElement jsonElement)
            {
                return propertyType switch
                {
                    Type t when t == typeof(string) => jsonElement.GetString(),
                    Type t when t == typeof(int) => jsonElement.GetInt32(),
                    Type t when t == typeof(Guid) => jsonElement.GetGuid(),
                    Type t when t == typeof(DateTime) => jsonElement.GetDateTime(),
                    Type t when t == typeof(bool) => jsonElement.GetBoolean(),
                    _ => throw new NotSupportedException($"Conversion for type {propertyType} is not supported.")
                };
            }
            else
            {
                return Convert.ChangeType(value, propertyType);
            }
        }
    }
}
