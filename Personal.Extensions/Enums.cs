using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Personal.Extensions
{
    public static class Enums
    {
        public static string GetDescription(this Enum enumValue)
        {
            return enumValue.GetType()
                       .GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description ?? string.Empty;
        }

        public static T GetAttribute<T>(this Enum enumValue) where T : Attribute
        {
            var type = enumValue.GetType();
            var memberInfo = type.GetMember(enumValue.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return attributes.Length > 0
              ? (T)attributes[0]
              : null;
        }
    }
}