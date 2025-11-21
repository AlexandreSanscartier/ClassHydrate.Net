using ClassHydrate.Net.Models;
using System.Reflection;
using System.Text.Json.Serialization;

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
        public static IEnumerable<IPropertyTypeResult> GetTypePropertyTypeResult(this Type type)
        {
            var propertyResults = type.GetProperties().Select(
                x => new PropertyTypeResult(x)
            );
            return propertyResults;
        }

        /// <summary>
        /// Takes the C# type and turns the properties into a Dictionary of <seealso cref="IClassProperty"/>.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to create a Dictionary of <seealso cref="IClassProperty"/>.</param>
        /// <returns>A Dictionary of <seealso cref="IClassProperty"/> with the name of the property as the key.</returns>
        public static IDictionary<string, IClassProperty> ToDictionary(this Type type)
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
            return classPropertyDictionary;
        }

        /// <summary>
        /// Takes the C# type and turns the properties into a Dictionary of <seealso cref="IClassProperty"/> 
        /// populated by the <seealso cref="T"/> model.
        /// </summary>
        /// <param name="type">The <seealso cref="Type"/> to create a Dictionary of <seealso cref="IClassProperty"/>.</param>
        /// <returns>A Dictionary of <seealso cref="IClassProperty"/> with the name of the property as the key and values.</returns>
        public static IDictionary<string, IClassProperty> ToDictionaryWithValues<T>(this Type type, T model) 
            where T : new()
        {
            if(model is null) throw new ArgumentNullException(nameof(model));
            var properties = type.GetTypePropertyTypeResult();
            var classPropertyDictionary = new Dictionary<string, IClassProperty>();
            foreach (var property in properties)
            {
                var value = property.GetValueFromConcreteObject(model);
                var modelProperty = new ClassProperty()
                {
                    Name = property.Name,
                    Type = property.Type,
                    Value = value
                };
                classPropertyDictionary.Add(property.Name, modelProperty);
            }
            return classPropertyDictionary;
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
    }
}
