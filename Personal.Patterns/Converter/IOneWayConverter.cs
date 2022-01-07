namespace Personal.Patterns.Converter
{
    public interface IOneWayConverter<in TType, out TOtherType>
    {
        TOtherType Convert(TType data);
    }
}