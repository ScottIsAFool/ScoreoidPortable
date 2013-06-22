using System;
using ScoreoidPortable.Attributes;

namespace ScoreoidPortable
{
    internal static class Utils
    {
        internal static string GetDescriptionFromName(string name, Type type)
        {
            var property = type.GetProperty(name);

            var attrs = property.GetCustomAttributes(typeof(Description), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((Description)attrs[0]).Text;
            }

            return name;
        }
    }
}
