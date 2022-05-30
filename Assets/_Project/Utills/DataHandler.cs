using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class DataHandler
    {
        public static void SaveJSONToFile(string jsonAsString, string location = "")
        {

            string defaultPath = Application.dataPath + "/Assets/Data/";
            System.IO.File.AppendAllText(defaultPath + location, jsonAsString);
        }
        public static void SaveJSONToFile<T>(T obj, string location = "")
        {
            string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets/Data", location);
            string json = JsonUtility.ToJson(obj);
            System.IO.File.AppendAllText(path, json);
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
            string path = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), "Assets/Data", fileNameWithPath);
            string jsonAsString = System.IO.File.ReadAllText(path);
            if (jsonAsString.Length > 0)
            {
                return JsonUtility.FromJson<T>(jsonAsString);
            }
            return default;
        }

    }
}
