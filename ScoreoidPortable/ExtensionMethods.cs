using System;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;

namespace ScoreoidPortable
{
    internal static class ExtensionMethods
    {
        internal static void InsertItemToDictionary<T>(this Dictionary<string, string> dictionary, T item) 
            where T : class
        {
            var properties = item.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(item, null);
                if (value != null)
                {
                    dictionary.Add(Utils.GetDescriptionFromName(property.Name, typeof(T)), value.ToString());
                }
            }
        }

        internal static string GetDescription(this object en)
        {
            var type = en.GetType();

            var memInfo = type.GetMember(en.ToString());

            if (memInfo.Length <= 0) return en.ToString();

            var attrs = memInfo[0].GetCustomAttributes(typeof(Description), false);

            if (attrs != null && attrs.Length > 0)
            {
                return ((Description) attrs[0]).Text;
            }

            return en.ToString();
        }
    }
}
