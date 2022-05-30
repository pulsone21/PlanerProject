using System.Collections.Generic;

namespace Utilities
{

    public static class Extensions
    {
        public static List<T> ToList<T>(this T[] array)
        {
            List<T> newList = new List<T>();
            foreach (T item in array)
            {
                newList.Add(item);
            }
            return newList;
        }
    }
}