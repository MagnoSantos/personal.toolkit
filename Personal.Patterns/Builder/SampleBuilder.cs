namespace Personal.Patterns.Builder
{
    public class SampleBuilder : ISampleBuilder
    {
        private readonly SampleDto _sampleDto;

        public SampleBuilder() => _sampleDto = new(); 
        

        public ISampleBuilder AddProperty(string sample)
        {
            _sampleDto.Sample = sample;
            return this;
        }

        public SampleDto Build() => _sampleDto;
    }

    public class SampleDto
    {
        public string Sample { get; set; }
    }
}