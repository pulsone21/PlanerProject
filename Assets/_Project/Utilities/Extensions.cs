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


        public static List<T> ToList<T>(this HashSet<T> hashset)
        {
            List<T> list = new List<T>();
            foreach (T item in hashset)
            {
                list.Add(item);
            }
            return list;
        }

        public static HashSet<T> ToHashSet<T>(this List<T> list)
        {
            HashSet<T> hashset = new HashSet<T>();
            foreach (T item in list)
            {
                hashset.Add(item);
            }
            return hashset;
        }
    }
}