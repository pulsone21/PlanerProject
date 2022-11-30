using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

namespace SLSystem
{
    [System.Serializable]
    public class GameData
    {
        [SerializeField] private string saveName;
        public Dictionary<string, string> Data;
        [SerializeField] private List<GameDataItem> savableList;
        public GameData(string name)
        {
            saveName = name;
            Data = new Dictionary<string, string>();
        }
        public string SaveName => saveName;
        public List<GameDataItem> GenerateSaveableList()
        {
            List<GameDataItem> items = new List<GameDataItem>();
            foreach (KeyValuePair<string, string> entry in Data) items.Add(new(entry.Value, entry.Key));
            savableList = items;
            return items;
        }
        public void SetDataDict()
        {
            Data = new Dictionary<string, string>();
            foreach (GameDataItem item in savableList) Data[item.ClassName] = item.Object;

        }
    }
}
