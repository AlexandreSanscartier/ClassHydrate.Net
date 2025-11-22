using ClassHydrate.Net.Exceptions;
using ClassHydrate.Net.Models;
using ClassHydrate.Net.Tests.Models;

namespace ClassHydrate.Net.Tests
{
    public class HydratorTests
    {
        [Fact]
        public void Hydrator_WhenDehydrateCalled_ReturnsPropertyDictionary()
        {
            // Arrange
            var hydrator = new Hydrator();
            var propertyNameList = new List<string>
            {
                "Id",
                "Name",
                "Population",
                "Price",
                "Rating",
                "CreatedDate"
            };

            // Act
            var result = hydrator.Dehydrate<PrimitiveClass>();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Count);
            Assert.Equal(propertyNameList, result.Keys.ToList());
        }

        [Fact]
        public void Hydrator_WhenDehydrateCalledWithPopulatedObject_ReturnsPopulatedDictionary()
        {
            // Arrange
            var hydrator = new Hydrator();
            var primitiveClass = new PrimitiveClass()
            {
                Id = 1,
                Name = "Test Name",
                Population = 1000L,
                Price = 19.99m,
                Rating = 4.5f,
                CreatedDate = new DateTime(2024, 1, 1)
            };

            // Act
            var result = hydrator.Dehydrate(primitiveClass);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Count);
            Assert.Equal(primitiveClass.Id, result["Id"].Value);
            Assert.Equal(primitiveClass.Name, result["Name"].Value);
            Assert.Equal(primitiveClass.Population, result["Population"].Value);
            Assert.Equal(primitiveClass.Price, result["Price"].Value);
            Assert.Equal(primitiveClass.Rating, result["Rating"].Value);
            Assert.Equal(primitiveClass.CreatedDate, result["CreatedDate"].Value);
        }

        [Fact]
        public void Hydrator_WhenHydrationCalledWithConstructorParameters_ReturnsHydratedObject()
        {
            // Arrange
            var hydrator = new Hydrator();
            var classPropertyBag = new ClassPropertyBag(
                typeof(ClassWithManyConstructors),
                new Dictionary<string, IClassProperty>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Id", new ClassProperty() { Type = typeof(int), Value = 42, Name = "Id" } },
                    { "Name", new ClassProperty() { Type = typeof(string), Value = "Hydrated Name", Name = "Name" } },
                    { "Price", new ClassProperty() { Type = typeof(decimal), Value = 29.99m, Name = "Price" } },
                }
            );

            // Act
            var result = hydrator.Hydrate<ClassWithManyConstructors>(classPropertyBag);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(42, result.Id);
            Assert.Equal("Hydrated Name", result.Name);
            Assert.Equal(29.99m, result.Price);
        }

        [Fact]
        public void Hydrator_WhenHydrationCalledWithNonExistantConstructorParameters_Throws()
        {
            // Arrange
            var hydrator = new Hydrator();
            var classPropertyBag = new ClassPropertyBag(
                typeof(ClassWithNoDefaultConstructor),
                new Dictionary<string, IClassProperty>(StringComparer.OrdinalIgnoreCase)
                {
                    { "Name", new ClassProperty() { Type = typeof(string), Value = "Hydrated Name", Name = "Name" } },
                }
            );

            // Act & Assert
            Assert.Throws<HydrationException>(() => hydrator.Hydrate<ClassWithNoDefaultConstructor>(classPropertyBag));
        }
    }
}
