using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Services
{
    internal interface IClassPropertyConstructorAligner
    {
        object[] AlignPropertiesForConstructor(IClassPropertyBag classPropertyBag, IClassConstructorInfo classConstructorInfo);
    }

    internal class ClassPropertyConstructorAligner : IClassPropertyConstructorAligner
    {
        public object[] AlignPropertiesForConstructor(IClassPropertyBag classPropertyBag, IClassConstructorInfo classConstructorInfo)
        {
            var objectList = new List<object>();
            var orderedParameters = classConstructorInfo.Parameters.OrderBy(x => x.Position);
            foreach (var parameter in orderedParameters)
            {
                if (classPropertyBag.TryGetValue(parameter.Name, out var classProperty))
                {
                    var valueToAdd = classProperty.Value ?? parameter.DefaultValue ?? throw new ArgumentNullException($"The property '{parameter.Name}' required for the constructor of '{classConstructorInfo.Name}' cannot be null.");
                    objectList.Add(valueToAdd);
                }
            }
            return objectList.ToArray();
        }
    }
}
