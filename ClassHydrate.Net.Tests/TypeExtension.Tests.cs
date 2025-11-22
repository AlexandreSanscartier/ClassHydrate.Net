using ClassHydrate.Net.Extensions;
using ClassHydrate.Net.Models;
using ClassHydrate.Net.Tests.Models;
using System.Reflection;

namespace ClassHydrate.Net.Tests
{
    public class TypeExtensionTests
    {
        private enum TestEnum
        {
            Foo,
            Bar
        }

        [Theory]
        // CLR primitives
        [InlineData(typeof(bool), true)]
        [InlineData(typeof(byte), true)]
        [InlineData(typeof(sbyte), true)]
        [InlineData(typeof(short), true)]
        [InlineData(typeof(ushort), true)]
        [InlineData(typeof(int), true)]
        [InlineData(typeof(uint), true)]
        [InlineData(typeof(long), true)]
        [InlineData(typeof(ulong), true)]
        [InlineData(typeof(char), true)]
        [InlineData(typeof(float), true)]
        [InlineData(typeof(double), true)]

        // Nullable primitives
        [InlineData(typeof(int?), true)]
        [InlineData(typeof(bool?), true)]

        // Enum & nullable enum
        [InlineData(typeof(TestEnum), true)]
        [InlineData(typeof(TestEnum?), true)]

        // Other primitive-like types in your method
        [InlineData(typeof(string), true)]
        [InlineData(typeof(decimal), true)]
        [InlineData(typeof(DateTime), true)]
        [InlineData(typeof(DateTimeOffset), true)]
        [InlineData(typeof(TimeSpan), true)]
        [InlineData(typeof(Guid), true)]

        // Nullable versions of those
        [InlineData(typeof(decimal?), true)]
        [InlineData(typeof(DateTime?), true)]
        [InlineData(typeof(DateTimeOffset?), true)]
        [InlineData(typeof(TimeSpan?), true)]
        [InlineData(typeof(Guid?), true)]

        // Non-primitive-like types
        [InlineData(typeof(object), false)]
        [InlineData(typeof(Uri), false)]
        [InlineData(typeof(List<int>), false)]
        [InlineData(typeof(int[]), false)]
        [InlineData(typeof(Dictionary<string, object>), false)]
        public void IsPrimitivateLike_ReturnsExpected(Type type, bool expected)
        {
            // Act
            var result = type.IsPrimitivateLike();

            // Assert
            Assert.Equal(expected, result);
        }

        [Fact]
        public void PrimitiveClass_GetPropertyNames()
        {
            // Arrange
            var primitiveClassType = typeof(PrimitiveClass);
            var primitiveClassPropertyNames = new List<string>
            {
                "Id",
                "Name",
                "Population",
                "Price",
                "Rating",
                "CreatedDate"
            };

            // Act
            var propertyTypeResults = primitiveClassType.GetPropertyNames();

            // Assert
            Assert.Equal(primitiveClassPropertyNames, propertyTypeResults);
        }

        [Fact]
        public void PrimitiveClass_ToDictionary()
        {
            // Arrange
            var primitiveClassType = typeof(PrimitiveClass);
            var primitiveClassPropertyNames = new List<string>
            {
                "Id",
                "Name",
                "Population",
                "Price",
                "Rating",
                "CreatedDate"
            };
            // Act
            var propertyDictionary = primitiveClassType.ToClassPropertyBag();
            // Assert
            Assert.Equal(6, propertyDictionary.Count);
            foreach (var propertyName in primitiveClassPropertyNames)
            {
                Assert.True(propertyDictionary.ContainsKey(propertyName));
                Assert.Equal(propertyName, propertyDictionary[propertyName].Name);
                Assert.Null(propertyDictionary[propertyName].Value);
            }
        }

        [Fact]
        public void PrimitiveClass_ToDictionaryWithValues()
        {
            // Arrange
            var primitiveClassType = typeof(PrimitiveClass);
            var primitiveClassInstance = new PrimitiveClass
            {
                Id = 1,
                Name = "Test",
                Population = 1000,
                Price = 9.99m,
                Rating = 4.5f,
                CreatedDate = new DateTime(2024, 1, 1)
            };

            // Act
            var propertyDictionary = primitiveClassType.ToClassPropertyBagWithValues(primitiveClassInstance);

            // Assert
            Assert.Equal(6, propertyDictionary.Count);
            Assert.Equal(1, propertyDictionary["Id"].Value);
            Assert.Equal("Test", propertyDictionary["Name"].Value);
            Assert.Equal(1000L, propertyDictionary["Population"].Value);
            Assert.Equal(9.99m, propertyDictionary["Price"].Value);
            Assert.Equal(4.5f, propertyDictionary["Rating"].Value);
            Assert.Equal(new DateTime(2024, 1, 1), propertyDictionary["CreatedDate"].Value);
        }

        [Fact]
        public void TypedExtensions_GetConstructorInfos_ReturnsAllConstructors()
        {
            // Arrange
            var classConstructorType = typeof(ClassWithManyConstructors);

            // Act
            var constructorInfos = classConstructorType.GetConstructorInfos();

            // Assert
            Assert.Equal(5, constructorInfos.Length);
        }
    }
}
