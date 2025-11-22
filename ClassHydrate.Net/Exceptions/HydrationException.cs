using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Exceptions
{
    public sealed class HydrationException : ClassHydratorException
    {
        public Type TargetType { get; }
        public IClassConstructorInfo? Constructor { get; }
        public IClassPropertyBag PropertyBag { get; }

        public HydrationException(
            Type targetType,
            IClassPropertyBag propertyBag,
            IClassConstructorInfo? constructor,
            string message,
            Exception? innerException = null)
            : base(message, innerException)
        {
            TargetType = targetType ?? throw new ArgumentNullException(nameof(targetType));
            PropertyBag = propertyBag ?? throw new ArgumentNullException(nameof(propertyBag));
            Constructor = constructor;
        }
    }
}
