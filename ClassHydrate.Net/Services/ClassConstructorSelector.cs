using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Services
{
    internal interface IClassConstructorSelector
    {
        IClassConstructorInfo? Best(IClassPropertyBag classPropertyBag);
    }

    internal class ClassConstructorSelector : IClassConstructorSelector
    {
        private readonly IEnumerable<ClassConstructorInfo> _classConstructorInfos;

        public ClassConstructorSelector(IEnumerable<ClassConstructorInfo> classConstructorInfos)
        {
            _classConstructorInfos = classConstructorInfos;
        }

        public IClassConstructorInfo? Best(IClassPropertyBag classPropertyBag)
        {
            var allConstructorArguments = GetAllConstructorArguments();
            var relevantProperties = classPropertyBag
                .Where(p => allConstructorArguments.Contains(p.Key, StringComparer.OrdinalIgnoreCase))
                .Select(p => p.Key);

            var bestConstructor = SelectBestConstructor(relevantProperties);
            return bestConstructor;
        }

        private IEnumerable<string> GetAllConstructorArguments()
        {
            return _classConstructorInfos.SelectMany(c => c.Parameters)
                .Select(p => p.Name)
                .Distinct(StringComparer.OrdinalIgnoreCase);
        }

        private IClassConstructorInfo? SelectBestConstructor(
            IEnumerable<string> relevantProperties)
        {
            var constructorsSortedByPropertyCount = _classConstructorInfos
                .OrderByDescending(c => c.Parameters.Count());

            foreach (var constructor in constructorsSortedByPropertyCount)
            {
                var constructorParameterNames = constructor.Parameters
                    .Select(p => p.Name);
                if (constructorParameterNames
                    .All(p => relevantProperties
                        .Contains(p, StringComparer.OrdinalIgnoreCase)))
                {
                    return constructor;
                }
            }
            return null;
        }
        // Get all possible constructors for the type
        // Find the constructor that matches the provided parameters
        // Take the constructor that maximies the number of matched parameters
        // If no constructor matches, throw an exception
    }
}
