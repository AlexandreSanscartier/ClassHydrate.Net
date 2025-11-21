using ClassHydrate.Net.Extensions;
using System.Reflection;

namespace ClassHydrate.Net.Models
{
    public interface IPropertyTypeResult
    {
        public string Name { get; }

        public Type Type { get; }

        public bool IsEnumerable { get; }

        public bool IsPrimitiveLike { get; }

        public object GetValueFromConcreteObject(object value);

        public void SetPropertyValueForObject(object valueObject, object newValue);
    }

    public class PropertyTypeResult : IPropertyTypeResult
    {
        private readonly PropertyInfo _propertyInfo = default!;
        public string Name => _propertyInfo.Name;
        public Type Type => IsPropertyEnumerable(_propertyInfo) ? _propertyInfo.PropertyType.GetGenericArguments()[0] : _propertyInfo.PropertyType;
        public bool IsEnumerable => IsPropertyEnumerable(_propertyInfo);
        public bool IsPrimitiveLike => Type.IsPrimitivateLike();
        public PropertyTypeResult(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        private bool IsPropertyEnumerable(PropertyInfo propertyInfo)
            => typeof(IEnumerable<object>).IsAssignableFrom(propertyInfo.PropertyType) && propertyInfo.PropertyType != typeof(string);

        public object GetValueFromConcreteObject(object value)
        {
            // TODO: allow null values if it's appropriate, for now for simplicity leave as is.
            var propertyInfo = GetPropertyInfoForValue(value);
            if (propertyInfo is null) throw new ArgumentException($"Property '{Name}' does not exist on type '{Type.FullName}'.", nameof(Name));
            var objectValue = propertyInfo.GetValue(value, null);
            if (objectValue is null) throw new InvalidOperationException($"Property '{Name}' on type '{Type.FullName}' is null.");
            return objectValue;
        }

        public void SetPropertyValueForObject(object valueObject, object newValue)
        {
            var propertyInfo = GetPropertyInfoForValue(valueObject);
            if (propertyInfo is null) throw new ArgumentException($"Property '{Name}' does not exist on type '{Type.FullName}'.", nameof(Name));
            propertyInfo.SetValue(valueObject, newValue);
        }

        private PropertyInfo? GetPropertyInfoForValue(object value)
            => value.GetType().GetProperty(Name);
    }
}
