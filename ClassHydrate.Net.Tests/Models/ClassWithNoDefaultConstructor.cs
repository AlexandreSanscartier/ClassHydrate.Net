namespace ClassHydrate.Net.Tests.Models
{
    internal class ClassWithNoDefaultConstructor
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;

        public ClassWithNoDefaultConstructor(int id)
        {
            Id = id;
        }

        public ClassWithNoDefaultConstructor(int id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public ClassWithNoDefaultConstructor(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public ClassWithNoDefaultConstructor(int id, decimal price, string name)
        {
            Id = id;
            Price = price;
            Name = name;
        }
    }
}
