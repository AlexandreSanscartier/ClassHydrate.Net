using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace ClassHydrate.Net.Models
{
    public interface IClassPropertyBag : IReadOnlyDictionary<string, IClassProperty>
    {  
        Type ModelType { get; }
    }

    public interface IMutableClassPropertyBag : IClassPropertyBag
    {
        bool TryEditValue(string key, object? value);
    }

    public sealed class ClassPropertyBag : IMutableClassPropertyBag
    {
        private readonly IDictionary<string, IClassProperty> _properties;

        public ClassPropertyBag(Type modelType, IDictionary<string, IClassProperty> properties)
        {
            _properties = properties ?? throw new ArgumentNullException(nameof(properties));
            _properties = new Dictionary<string, IClassProperty>(
                properties,
                StringComparer.OrdinalIgnoreCase
            );
            ModelType = modelType ?? throw new ArgumentNullException(nameof(modelType));
        }

        public IClassProperty this[string key] => _properties[key];
        public IEnumerable<string> Keys => _properties.Keys;
        public IEnumerable<IClassProperty> Values => _properties.Values;
        public int Count => _properties.Count;
        public Type ModelType { get; init; }
        public bool ContainsKey(string key) => _properties.ContainsKey(key);
        public IEnumerator<KeyValuePair<string, IClassProperty>> GetEnumerator() => _properties.GetEnumerator();
        public bool TryGetValue(string key, [MaybeNullWhen(false)] out IClassProperty value) => _properties.TryGetValue(key, out value);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public bool TryEditValue(string key, object? value)
        {
            if (!_properties.TryGetValue(key, out var property))
            {
                return false;
            }

            var targetType = property.Type;
            if (value is null)
            {
                if (targetType.IsValueType && Nullable.GetUnderlyingType(targetType) is null)
                {
                    return false;
                }

                property.Value = null;
                return true;
            }

            var valueType = value.GetType();
            if (!targetType.IsAssignableFrom(valueType))
            {
                return false;
            }

            property.Value = value;
            return true;
        }
    }
}
