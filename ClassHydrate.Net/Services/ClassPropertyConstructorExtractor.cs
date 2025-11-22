using ClassHydrate.Net.Models;
using ClassHydrate.Net.Services.Models;

namespace ClassHydrate.Net.Services
{
    internal static class ClassPropertyConstructorExtractor
    {
        public static PropertyConstructorExtractorResult Extract(IClassPropertyBag classPropertyBag, IClassConstructorInfo classConstructorInfo)
        {
            var objectList = new List<object>();
            var propertyNameList = new List<string>();
            var orderedParameters = classConstructorInfo.Parameters.OrderBy(x => x.Position);
            foreach (var parameter in orderedParameters)
            {
                if (classPropertyBag.TryGetValue(parameter.Name, out var classProperty))
                {
                    var valueToAdd = classProperty.Value ?? parameter.DefaultValue ?? throw new ArgumentNullException($"The property '{parameter.Name}' required for the constructor of '{classConstructorInfo.Name}' cannot be null.");
                    objectList.Add(valueToAdd);
                    propertyNameList.Add(parameter.Name);
                }
            }
            var propertyConstructorExtractorResult = new PropertyConstructorExtractorResult()
            {
                PropertyNames = propertyNameList,
                Results = objectList
            };
            return propertyConstructorExtractorResult;
        }
    }
}
