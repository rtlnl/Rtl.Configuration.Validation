using Rtl.Configuration.Validation.Tests.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Xunit;

namespace Rtl.Configuration.Validation.Tests
{
    public class ComplexConfigTests
    {
        [Fact]
        public async Task ValidConfigDoesntThrow()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Id"] = "1",
                ["test:Person:Name"] = "john",
                ["test:Person:Age"] = "1",
                ["test:RequiredPerson:Name"] = "john2",
                ["test:RequiredPerson:Age"] = "2",
                ["test:People:0:Name"] = "smith",
                ["test:People:0:Age"] = "5",
                ["test:RequiredPeople:0:Name"] = "smith2",
                ["test:RequiredPeople:0:Age"] = "6",
            };

            await TestHelpers.ValidationSucceeds<Config>(settings);
        }

        [Fact]
        public async Task DontValidateComplexPropertyIfItsNotRequired()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Id"] = "1",
                ["test:RequiredPerson:Name"] = "john2",
                ["test:RequiredPerson:Age"] = "2",
                ["test:RequiredPeople:0:Name"] = "smith",
                ["test:RequiredPeople:0:Age"] = "5",
            };

            await TestHelpers.ValidationSucceeds<Config>(settings);
        }

        [Fact]
        public void ValidateComplexNotRequiredPropertyIfItsNotNull()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Id"] = "1",
                ["test:Person:Name"] = "john",
                ["test:RequiredPerson:Name"] = "john2",
                ["test:RequiredPerson:Age"] = "2",
                ["test:RequiredPeople:0:Name"] = "smith",
                ["test:RequiredPeople:0:Age"] = "5",
            };

            TestHelpers.ValidationThrows<Config>(settings);
        }

        [Fact]
        public void ValidateComplexNotRequiredPropertyFromCollectionIfItsNotNull()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Id"] = "1",
                ["test:RequiredPerson:Name"] = "john2",
                ["test:RequiredPerson:Age"] = "2",
                ["test:RequiredPeople:0:Name"] = "smith",
                ["test:RequiredPeople:0:Age"] = "5",
                ["test:People:0:Age"] = "5",
            };

            TestHelpers.ValidationThrows<Config>(settings);
        }

        [Fact]
        public void RequiredCollectionCannotBeNull()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Id"] = "1",
                ["test:RequiredPerson:Name"] = "john2",
                ["test:RequiredPerson:Age"] = "2"
            };

            TestHelpers.ValidationThrows<Config>(settings);
        }

        [Fact]
        public async Task RequiredCollectionCanBeEmpty()
        {
            var settings = new Dictionary<string, string>
            {
                ["test:Id"] = "1",
                ["test:RequiredPerson:Name"] = "john2",
                ["test:RequiredPerson:Age"] = "2",
                ["test:RequiredPeople:0:Name"] = "",
                ["test:RequiredPeople:0:Age"] = "",
            };

            await TestHelpers.ValidationSucceeds<Config>(settings);
        }
    }

    public class Config
    {
        public int Id { get; set; }
        public Person Person { get; set; }

        [Required]
        public Person RequiredPerson { get; set; }

        public IEnumerable<Person> People { get; set; }

        [Required]
        public IEnumerable<Person> RequiredPeople { get; set; }
    }

    public class Person
    {
        [Required]
        public string Name { get; set; }

        [Range(1, 18)]
        public int Age { get; set; }
    }
}
