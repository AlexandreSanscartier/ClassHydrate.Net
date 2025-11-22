namespace ClassHydrate.Net.Models
{
    internal interface IClassConstructorParameter
    {
        object? Value { get; set; }
    }

    internal class ClassConstructorParameter : IClassConstructorParameter
    {
        private readonly IClassConstructorParameterInfo _classConstructorParameterInfo;

        public ClassConstructorParameter(IClassConstructorParameterInfo classConstructorParameterInfo)
        {
            _classConstructorParameterInfo = classConstructorParameterInfo ?? throw new ArgumentNullException(nameof(classConstructorParameterInfo));
        }

        public object? Value {
            get; 
            set
            {
                if(value is null) 
                {
                    field = null;
                    return;
                }
                var valueType = value.GetType();
                if (!valueType.IsAssignableTo(_classConstructorParameterInfo.Type))
                    throw new InvalidOperationException($"Value of type '{valueType.FullName}' is not assignable to parameter of type '{_classConstructorParameterInfo.Type.FullName}'.");
               
                field = value;
            }
        }
    }
}
