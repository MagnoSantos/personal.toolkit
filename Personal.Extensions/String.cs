namespace Personal.Extensions
{
    public static class String
    {
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        public static int ToInt(this string value)
        {
            _ = int.TryParse(value, out int _value);
            return _value;
        }
    }
}