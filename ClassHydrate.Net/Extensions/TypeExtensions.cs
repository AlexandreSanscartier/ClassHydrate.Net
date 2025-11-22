using ClassHydrate.Net.Models;
using System.Reflection;

namespace ClassHydrate.Net.Extensions
{
    /// <summary>
    /// Class that contains extension methods for <seealso cref="Type"/>.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Gets the property names for the given <seealso cref="Type"/>.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to get the property names for.</param>
        /// <returns>A list of <seealso cref="string"/> that represent property names.</returns>
        public static IEnumerable<string> GetPropertyNames(this Type type)
        {
            var propertyNames = type.GetProperties().Select(x => x.Name);
            return propertyNames;
        }

        /// <summary>
        /// Turns the <seealso cref="Type"/> properties into a <seealso cref="IPropertyTypeResult"/> objects."/>
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to get the <seealso cref="IPropertyTypeResult"/> for.</param>
        /// <returns>The list of <seealso cref="IPropertyTypeResult"/>.</returns>
        public static IEnumerable<IClassPropertyInfo> GetTypePropertyTypeResult(this Type type)
        {
            var classPropertyInfos = type.GetProperties().Select(
                x => new ClassPropertyInfo(x)
            );
            return classPropertyInfos;
        }

        /// <summary>
        /// Takes the C# type and turns the properties into a Dictionary of <seealso cref="IClassProperty"/>.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to create a Dictionary of <seealso cref="IClassProperty"/>.</param>
        /// <returns>A Dictionary of <seealso cref="IClassProperty"/> with the name of the property as the key.</returns>
        public static IClassPropertyBag ToClassPropertyBag(this Type type)
        {
            var properties = type.GetTypePropertyTypeResult();
            var classPropertyDictionary = new Dictionary<string, IClassProperty>();
            foreach (var property in properties)
            {
                var modelProperty = new ClassProperty()
                {
                    Name = property.Name,
                    Type = property.Type
                };
                classPropertyDictionary.Add(property.Name, modelProperty);
            }
            var classPropertyBag = new ClassPropertyBag(type, classPropertyDictionary);
            return classPropertyBag;
        }

        /// <summary>
        /// Takes the C# type and turns the properties into a Dictionary of <seealso cref="IClassProperty"/> 
        /// populated by the <seealso cref="T"/> model.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to create a Dictionary of <seealso cref="IClassProperty"/>.</param>
        /// <returns>A Dictionary of <seealso cref="IClassProperty"/> with the name of the property as the key and values.</returns>
        public static IClassPropertyBag ToClassPropertyBagWithValues<T>(this Type type, T model) 
        {
            if(model is null) throw new ArgumentNullException(nameof(model));
            var properties = type.GetTypePropertyTypeResult();
            var classPropertyDictionary = new Dictionary<string, IClassProperty>();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var value = GetValueFromConcreteObject(propertyName, model);
                var modelProperty = new ClassProperty()
                {
                    Name = propertyName,
                    Type = property.Type,
                    Value = value
                };
                classPropertyDictionary.Add(property.Name, modelProperty);
            }
            var classPropertyBagWithValues = new ClassPropertyBag(type, classPropertyDictionary);
            return classPropertyBagWithValues;
        }

        /// <summary>
        /// Gets whether the type is primitive-like.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to check for primitive-likeness.</param>
        /// <returns>Whether the <seealso cref="Type"/> is primitive-like.</returns>
        public static bool IsPrimitivateLike(this Type type)
        {
            // Unwrap Nullable<T>
            type = Nullable.GetUnderlyingType(type) ?? type;

            return
                type.IsPrimitive ||
                type.IsEnum ||
                type == typeof(string) ||
                type == typeof(decimal) ||
                type == typeof(DateTime) ||
                type == typeof(DateTimeOffset) ||
                type == typeof(TimeSpan) ||
                type == typeof(Guid);
        }

        /// <summary>
        /// Gets the <seealso cref="ClassConstructorInfo"/> for the type <seealso cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get constructors for.</param>
        /// <returns>A list of <seealso cref="ClassConstructorInfo"/>.</returns>
        public static ClassConstructorInfo[] GetConstructorInfos(this Type type)
        {
            var constructors = type.GetConstructors();
            var classConstructoInfos = constructors.Select(x => new ClassConstructorInfo(x)).ToArray();
            return classConstructoInfos;
        }

        private static PropertyInfo? GetPropertyInfoForValue(this Type type, string propertyName)
            => type.GetProperty(propertyName);

        private static object GetValueFromConcreteObject(string name, object value)
        {
            var valueType = value.GetType();
            var propertyInfo = valueType.GetPropertyInfoForValue(name);
            if (propertyInfo is null) throw new ArgumentException($"Property '{name}' does not exist on type '{valueType.FullName}'.", nameof(name));
            var objectValue = propertyInfo.GetValue(value, null);
            if (objectValue is null) throw new InvalidOperationException($"Property '{name}' on type '{valueType.FullName}' is null.");
            return objectValue;
        }
    }
}
