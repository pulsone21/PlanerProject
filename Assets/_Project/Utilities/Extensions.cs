using System.Collections.Generic;
using UnityEngine;
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

        public static Stack<T> ToStack<T>(this T[] array)
        {
            Stack<T> newStack = new Stack<T>();
            foreach (T item in array)
            {
                newStack.Push(item);
            }
            return newStack;
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

        public static void ClearAllChildren(this Transform transform)
        {
            while (transform.childCount != 0)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

    }
}