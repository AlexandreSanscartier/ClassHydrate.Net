using ClassHydrate.Net.Extensions;
using System.Reflection;

namespace ClassHydrate.Net.Models
{
    /// <summary>
    /// A wrapper for PropertyInfo to be used in ClassHydrate.Net.
    /// </summary>
    internal interface IClassPropertyInfo
    {
        string Name { get; }
        Type Type { get; }
        bool IsPrimitiveLike { get; }
    }

    internal class ClassPropertyInfo : IClassPropertyInfo
    {
        private readonly PropertyInfo _propertyInfo;

        public ClassPropertyInfo(PropertyInfo propertyInfo)
        {
            _propertyInfo = propertyInfo;
        }

        public string Name => _propertyInfo.Name;
        public Type Type => _propertyInfo.PropertyType;
        public bool IsPrimitiveLike => Type.IsPrimitivateLike();
    }
}
