using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class DataHandler
    {
        public static void SaveJSONToFile(string jsonAsString, string location = "", bool append = true)
        {
            string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets/Data", location);
            if (append)
            {
                System.IO.File.AppendAllText(path, jsonAsString);
            }
            else
            {
                System.IO.File.WriteAllText(path, jsonAsString);
            }

        }
        public static void SaveJSONToFile<T>(T obj, string location = "", bool append = true)
        {
            string json = JsonUtility.ToJson(obj, true);
            SaveJSONToFile(json, location, append);
        }

        public static string LoadFromJSON(string fileNameWithPath)
        {
            string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets/Data", fileNameWithPath);
            string jsonAsString = System.IO.File.ReadAllText(path);
            if (jsonAsString.Length > 0)
            {
                return jsonAsString;
            }
            return default;
        }

        public static T LoadFromJSON<T>(string fileNameWithPath)
        {
            string jsonAsString = LoadFromJSON(fileNameWithPath);
            if (jsonAsString.Length > 0)
            {
                return JsonUtility.FromJson<T>(jsonAsString);
            }
            return default;
        }

    }
}
