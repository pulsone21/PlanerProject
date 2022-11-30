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

        public static void SetActiveAllChildren(this Transform transform, bool state)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).gameObject.SetActive(state);
            }
        }
        public static float Normalized(this float value, float min, float max) => Utils.NormalizeBetween0And1(value, min, max);

        public static bool CompareColors(this Color A, Color B) => A.r * 1000 == B.r * 1000 && A.g * 1000 == B.g * 1000 && A.b * 1000 == B.b * 1000 && A.a * 1000 == B.a * 1000;
    }
}