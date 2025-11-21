using ClassHydrate.Net.Models;
namespace ClassHydrate.Net
{
    public interface IClassHydrator
    {
        T HydrateClass<T>(IDictionary<string, IClassProperty> propertyValues) where T : new();
        IDictionary<string, IClassProperty> DehydrateClass<T>(T classInstance) where T : new();
    }
    internal class ClassHydrator : IClassHydrator
    {
        public IDictionary<string, IClassProperty> DehydrateClass<T>(T classInstance) where T : new()
        {
            throw new NotImplementedException();
        }

        public T HydrateClass<T>(IDictionary<string, IClassProperty> propertyValues) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}
