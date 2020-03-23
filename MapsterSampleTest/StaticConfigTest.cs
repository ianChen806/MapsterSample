using FluentAssertions;
using Mapster;
using MapsterSampleTest.Model;
using Xunit;

namespace MapsterSampleTest
{
    public class StaticConfigTest
    {
        public StaticConfigTest()
        {
            TypeAdapterConfig.GlobalSettings.Default.PreserveReference(true);
        }

        [Fact]
        public void CustomConvert()
        {
            TypeAdapterConfig<ClassA, ClassB>
                .NewConfig()
                .Ignore(b => b.Price)
                .Map(b => b.Name, a => $"{a.Id}_{a.Name}");
            
            var classA = new ClassA()
            {
                Id = 1,
                Name = "Test",
                Price = 12.34m
            };
            var classB = classA.Adapt<ClassB>();
            
            classB.Should().BeEquivalentTo(new ClassB()
            {
                Id = 1,
                Name = "1_Test",
                Price = 0m,
            });
        }

        [Fact]
        public void InnerClass()
        {
            TypeAdapterConfig<ClassB, MyClassB>.ForType();
            var classD = new ClassD
            {
                Id = 1,
                ClassB = new ClassB
                {
                    Id = 2,
                    Name = "Test",
                    Price = 23m
                }
            };
            var myClassA = classD.Adapt<MyClassA>();
            myClassA.Should().BeEquivalentTo(new MyClassA()
            {
                Id = 1,
                ClassB = new MyClassB()
                {
                    Id = 2,
                    Name = "Test"
                }
            });
        }
    }
}