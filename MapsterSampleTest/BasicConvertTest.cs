using System.Collections.Generic;
using FluentAssertions;
using Mapster;
using MapsterSampleTest.Model;
using Xunit;

namespace MapsterSampleTest
{
    public class BasicConvertTest
    {
        [Fact]
        public void BasicConvert()
        {
            var classA = new ClassA()
            {
                Id = 123,
                Name = "",
                Price = 23.3m
            };

            var classB = classA.Adapt<ClassB>();
            classB.Should().BeEquivalentTo(new ClassB()
            {
                Id = 123,
                Name = "",
                Price = 23.3m
            });
        }

        [Fact]
        public void BasicConvert_Type_Not_Same()
        {
            var classA = new ClassA()
            {
                Id = 123,
                Name = "",
                Price = 23.3m,
                Count = "3",
            };

            var classC = classA.Adapt<ClassC>();
            classC.Should().BeEquivalentTo(new ClassC()
            {
                Id = "123",
                Name = "",
                Price = "23.3",
                Count = 3,
            });
        }

        [Fact]
        public void BasicConvert_Dictionary()
        {
            var dictionary = new Dictionary<string, string>()
            {
                { "Id", "1" },
                { "Name", "Test" }
            };

            var classA = dictionary.Adapt<ClassA>();
            classA.Should().BeEquivalentTo(new ClassA()
            {
                Id = 1,
                Name = "Test"
            });
        }

        [Fact]
        public void BasicConvert_List()
        {
            var classAs = new List<ClassA>()
            {
                new ClassA(){Id = 1,Name = "Test"},
                new ClassA(){Id = 2,Name = "Test2"},
            };

            var classBs = classAs.Adapt<List<ClassB>>();
            classBs.Should().BeEquivalentTo(new List<ClassB>()
            {
                new ClassB() { Id = 1, Name = "Test" },
                new ClassB() { Id = 2, Name = "Test2" },
            });
        }
    }
}