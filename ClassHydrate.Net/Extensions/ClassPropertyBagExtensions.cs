using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Extensions
{
    internal static class ClassPropertyBagExtensions
    {
        public static IClassPropertyBag ToClassPropertyBag(
            this IEnumerable<KeyValuePair<string, IClassProperty>> source,
            Type modelType)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (modelType == null) throw new ArgumentNullException(nameof(modelType));

            return new ClassPropertyBag(modelType, source);
        }

        // convenience overload keeping the original bag's ModelType
        public static IClassPropertyBag ToClassPropertyBag(
            this IEnumerable<KeyValuePair<string, IClassProperty>> source,
            IClassPropertyBag template)
            => source.ToClassPropertyBag(template.ModelType);
    }
}
