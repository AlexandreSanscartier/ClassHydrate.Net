namespace ClassHydrate.ConsoleRunner.Models
{
    internal class DemoClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
        public Dictionary<int, string> DictionaryProperty { get; set; }
        public InternalDemoClass InternalDemoClass { get; set; }
    }
}
