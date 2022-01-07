using System;

namespace Personal.Patterns.Converter
{
    public class SampleConverter : IOneWayConverter<SampleDto, SampleDatabase>
    {
        public SampleDatabase Convert(SampleDto data)
            => new()
            {
                Id = data.Id
            }; 
    }

    public class SampleDatabase
    {
        public Guid Id { get; set; }
    }

    public class SampleDto
    {
        public Guid Id { get; set; }

        public string Property { get; } = "AnyProperty";
    }
}