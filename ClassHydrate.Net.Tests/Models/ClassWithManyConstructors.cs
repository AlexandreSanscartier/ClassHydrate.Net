using System.ComponentModel;

namespace ClassHydrate.Net.Tests.Models
{
    internal class ClassWithManyConstructors
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Name { get; set; } = string.Empty;

        public ClassWithManyConstructors()
        {
            
        }

        public ClassWithManyConstructors(int id)
        {
            Id = id;
        }

        public ClassWithManyConstructors(int id, decimal price)
        {
            Id = id;
            Price = price;
        }

        public ClassWithManyConstructors(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public ClassWithManyConstructors(int id, decimal price, string name)
        {
            Id = id;
            Price = price;
            Name = name;
        }
    }
}
