using System.Collections.Generic;

namespace ScoreoidPortable
{
    public static class ExtensionMethods
    {
        public static void InsertItemToDictionary<T>(this Dictionary<string, string> dictionary, T item) 
            where T : class
        {
            var properties = item.GetType().GetProperties();

            foreach (var property in properties)
            {
                var value = property.GetValue(item, null);
                if (value != null)
                {
                    dictionary.Add(property.Name.ToLower(), value.ToString());
                }
            }
        }
    }
}
