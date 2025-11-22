using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Services
{
    internal interface IClassConstructor
    {
        object ConstructInstance(IClassConstructorInfo classConstructorInfo, IClassPropertyBag classPropertyBag);
        T ConstructInstance<T>(IClassConstructorInfo classConstructorInfo, IClassPropertyBag classPropertyBag) where T : new();
    }

    internal class ClassConstructor : IClassConstructor
    {
        public object ConstructInstance(IClassConstructorInfo classConstructorInfo, IClassPropertyBag classPropertyBag)
            => ConstructInstance(classConstructorInfo, classPropertyBag);

        public T ConstructInstance<T>(IClassConstructorInfo classConstructorInfo, IClassPropertyBag classPropertyBag) where T : new()
        {
            if(!typeof(T).IsAssignableFrom(classConstructorInfo.Type))
                throw new InvalidOperationException($"Type '{classConstructorInfo.Type.FullName}' is not assignable to '{typeof(T).FullName}'.");

            //var instance = classConstructorInfo.Invoke();

            return default;
        }
    }
}