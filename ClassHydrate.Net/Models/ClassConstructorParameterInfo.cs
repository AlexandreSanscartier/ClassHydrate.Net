using System.Reflection;

namespace ClassHydrate.Net.Models
{
    /// <summary>
    /// Represents a class constructor parameter.
    /// </summary>
    public interface IClassConstructorParameterInfo
    {
        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Indicates whether the parameter has a default value.
        /// </summary>
        bool HasDefaultValue { get; }

        /// <summary>
        /// Gets the default value of the parameter, if any.
        /// </summary>
        object? DefaultValue { get; }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Gets the position of the parameter in the constructor's parameter list.
        /// </summary>
        int Position { get; }

        /// <summary>
        /// Indicates whether the parameter is optional.
        /// </summary>
        bool IsOptional { get; }

    }

    /// <inheritdoc/>
    internal class ClassConstructorParameterInfo : IClassConstructorParameterInfo
    {
        private readonly ParameterInfo _parameterInfo;

        public ClassConstructorParameterInfo(ParameterInfo parameterInfo)
        {
            _parameterInfo = parameterInfo ?? throw new ArgumentNullException(nameof(parameterInfo));

            Name = _parameterInfo.Name ?? throw new ArgumentException("Parameter must have a name.", nameof(_parameterInfo));
            Type = _parameterInfo.ParameterType ?? throw new ArgumentException("Parameter type must not be null.", nameof(_parameterInfo));
            HasDefaultValue = _parameterInfo.HasDefaultValue;
            DefaultValue = _parameterInfo.HasDefaultValue ? _parameterInfo.DefaultValue : null;
            Position = _parameterInfo.Position;
            IsOptional = _parameterInfo.IsOptional;
        }

        public string Name { get; init; }
        public bool HasDefaultValue { get; init; }
        public object? DefaultValue { get; init; }
        public Type Type { get; init; }
        public int Position { get; init; }
        public bool IsOptional { get; init; }
    }
}
