namespace ClassHydrate.Net.Models
{
    public interface IClassProperty
    {
        string Name { get; set; }
        object? Value { get; set; }
        Type Type { get; set; }
    }
    public class ClassProperty : IClassProperty
    {
        public string Name { get; set; }
        public object? Value { get; set; }
        public Type Type { get; set; }
    }
}
