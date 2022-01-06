namespace Personal.Patterns.Builder
{
    public interface ISampleBuilder
    {
        ISampleBuilder AddProperty(string sample);

        SampleDto Build();
    }
}