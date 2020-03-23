using FluentAssertions;
using Mapster;
using MapsterSampleTest.Model;
using Xunit;

namespace MapsterSampleTest
{
    public class InstanceConfigTest
    {
        [Fact]
        public void Basic()
        {
            var config = new TypeAdapterConfig();
            config.NewConfig<ClassA,ClassB>()
                  .Map(b=>b.Id,a=>a.Id+1)
                  .Map(b=>b.Name,a=>"Name");

            var classA = new ClassA()
            {
                Id = 1,
                Name = "Test"
            };
            var classB = classA.Adapt<ClassB>(config);
            classB.Should().BeEquivalentTo(new ClassB()
            {
                Id = 2,
                Name = "Name"
            });
        }
    }
}