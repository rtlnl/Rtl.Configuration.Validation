using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rtl.Configuration.Validation.Tests.Configs
{
    public class ComplexConfig
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
