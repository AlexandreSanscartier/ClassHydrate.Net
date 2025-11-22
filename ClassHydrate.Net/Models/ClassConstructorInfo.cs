using System.Reflection;

namespace ClassHydrate.Net.Models
{
    /// <summary>
    /// Represents a class constructor.
    /// </summary>
    public interface IClassConstructorInfo
    {
        /// <summary>
        /// Gets the name of the constructor's declaring type.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the namespace of the constructor's declaring type.
        /// </summary>
        string Namespace { get; }

        /// <summary>
        /// Gets the type that declares the constructor.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Indicates whether the constructor is public.
        /// </summary>
        bool IsPublic { get; }

        /// <summary>
        /// Gets the parameters of the constructor.
        /// </summary>
        IEnumerable<IClassConstructorParameterInfo> Parameters { get; }

        /// <summary>
        /// Invokes the constructor with the specified parameters.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>The constructed object.</returns>
        object Invoke(object[] constructorProperties);

        /// <summary>
        /// Invokes the constructor with the specified parameters and returns an instance of type T.
        /// </summary>
        /// <typeparam name="T">The type T to be constructed</typeparam>.
        /// <param name="parameters"></param>
        /// <returns>The constructed object of type T.</returns>
        T Invoke<T>(object[] constructorProperties);
    }

    /// <inheritdoc/>
    internal class ClassConstructorInfo : IClassConstructorInfo
    {
        private readonly ConstructorInfo _constructorInfo;
        private readonly ClassConstructorParameterInfo[] _parameters;

        public ClassConstructorInfo(ConstructorInfo constructorInfo)
        {
            _constructorInfo = constructorInfo ?? throw new ArgumentNullException(nameof(constructorInfo));

            Type = _constructorInfo.DeclaringType
               ?? throw new ArgumentException(
                   "Constructor must have a declaring type.",
                   nameof(constructorInfo));

            var parameterInfos = _constructorInfo.GetParameters();
            _parameters = parameterInfos.Select(pi => new ClassConstructorParameterInfo(pi)).ToArray();
        }

        public string Name => Type.Name;
        public string Namespace => Type.Namespace ?? string.Empty;
        public Type Type { get; init; }
        public bool IsPublic => _constructorInfo.IsPublic;
        public IEnumerable<IClassConstructorParameterInfo> Parameters => _parameters;

        public object Invoke(object[] constructorProperties)
        {
            var instance = _constructorInfo.Invoke(constructorProperties);
            return instance;
        }

        public T Invoke<T>(object[] constructorProperties)
        {
            var instance = Invoke(constructorProperties);
            return (T)instance;
        }
    }
}
