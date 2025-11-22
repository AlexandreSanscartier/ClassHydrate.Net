using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Extensions
{
    internal static class ObjectExtensions
    {
        public static void SetPropertyValues<T>(this T target, IClassPropertyBag classPropertyBag)
        {
            if (target == null) throw new ArgumentNullException(nameof(target));

            var type = typeof(T);
            foreach (var keyValuePair in classPropertyBag)
            {
                var propInfo = type.GetProperty(keyValuePair.Value.Name);
                if (propInfo is null) continue;

                var targetType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;
                var value = keyValuePair.Value.Value;
                object convertedValue = keyValuePair.Value.Value;
                if (value != null && !targetType.IsInstanceOfType(value))
                {
                    if (targetType.IsEnum)
                    {
                        convertedValue = Enum.Parse(targetType, value.ToString());
                    }
                    else
                    {
                        convertedValue = Convert.ChangeType(value, targetType);
                    }
                }

                propInfo.SetValue(target, convertedValue);
            }
        }
    }
}
