using ClassHydrate.Net.Exceptions;
using ClassHydrate.Net.Extensions;
using ClassHydrate.Net.Models;
using ClassHydrate.Net.Services;
using System.Reflection;
namespace ClassHydrate.Net
{
    /// <summary>
    /// Hydrator interface to Hydrate and Dehydrate class types.
    /// </summary>
    public interface IHydrator
    {
        /// <summary>
        /// Hydrate a class type from a dictionary of property names and <seealso cref="IClassProperty"/> objects.
        /// </summary>
        /// <typeparam name="T">The type T to Hydrate.</typeparam>
        /// <param name="classPropertyBag">A Dictionary of class properties.</param>
        /// <returns>The Hydrated object with the propertyValues set.</returns>
        T Hydrate<T>(IClassPropertyBag classPropertyBag);

        /// <summary>
        /// Dehydrate a class type to a dictionary of property names and <seealso cref="IClassProperty"/> objects
        /// </summary>
        /// <typeparam name="T">The type to Dehydrate</typeparam>
        /// <returns>A Dictionary with propertyName as a key and an <seealso cref="IClassProperty"/> as a value.</returns>
        IMutableClassPropertyBag Dehydrate<T>();

        /// <summary>
        /// Dehydrate a class type to a dictionary of property names and <seealso cref="IClassProperty"/> objects.
        /// </summary>
        /// <typeparam name="T">The type to Dehydrate.</typeparam>
        /// <param name="model">The instance of the type to Dehydrate.</param>
        /// <returns>A Dictionary with propertyName as a key and an <seealso cref="IClassProperty"/> as a value.</returns>
        IMutableClassPropertyBag Dehydrate<T>(T model);
    }

    /// <inheritdoc/>
    internal class Hydrator : IHydrator
    {
        public IMutableClassPropertyBag Dehydrate<T>()
        {
            var type = typeof(T);
            var classProperties = type.ToClassPropertyBag();
            return classProperties;
        }

        public IMutableClassPropertyBag Dehydrate<T>(T model) 
        {
            var type = typeof(T);
            var classProperties = type.ToClassPropertyBagWithValues(model);
            return classProperties;
        }

        public T Hydrate<T>(IClassPropertyBag classPropertyBag)
        {
            var targetType = typeof(T);
            var allConstructorInfos = targetType.GetConstructorInfos();
            if (!allConstructorInfos.Any())
            {
                throw new HydrationException(
                    targetType,
                    classPropertyBag,
                    constructor: null,
                    message: $"Type '{targetType.FullName}' has no public constructors.");
            }

            var classConstructorSelector = new ClassConstructorSelector(allConstructorInfos);
            var bestConstructorInfo = classConstructorSelector.Best(classPropertyBag);
            if (bestConstructorInfo is null)
            {
                throw new HydrationException(
                    targetType,
                    classPropertyBag,
                    constructor: null,
                    message: $"No suitable constructor found for type '{targetType.FullName}'.");
            }

            var extractedConstructorProperties = ClassPropertyConstructorExtractor.Extract(classPropertyBag, bestConstructorInfo);

            try
            {
                var hydratedObject = bestConstructorInfo.Invoke<T>(extractedConstructorProperties.Results);

                var propertiesLeftToSet = classPropertyBag
                    .Where(x => !extractedConstructorProperties.PropertyNames.Contains(x.Key))
                    .ToClassPropertyBag(classPropertyBag);

                hydratedObject.SetPropertyValues<T>(propertiesLeftToSet);

                return hydratedObject;
            }
            catch (Exception ex) when (
                ex is ArgumentException ||
                ex is TargetParameterCountException ||
                ex is TargetInvocationException ||
                ex is InvalidOperationException)
            {
                throw new HydrationException(
                    targetType,
                    classPropertyBag,
                    bestConstructorInfo,
                    message: $"Failed to hydrate type '{targetType.FullName}'. See inner exception for details.",
                    innerException: ex);
            }
        }
    }
}
