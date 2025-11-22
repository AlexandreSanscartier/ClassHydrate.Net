using ClassHydrate.Net.Models;

namespace ClassHydrate.Net.Exceptions
{
    public class ClassHydratorException : Exception
    {
        public ClassHydratorException() { }

        public ClassHydratorException(string message) : base(message) { }

        public ClassHydratorException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
