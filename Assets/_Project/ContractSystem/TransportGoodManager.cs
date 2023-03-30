using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Utilities;
using SLSystem;
using System.Linq;
namespace ContractSystem
{
    [ExecuteInEditMode]
    public class TransportGoodManager : MonoBehaviour
    {
        public static TransportGoodManager Instance;
        //TODO Make this loading more dynamic and not use resource folder
        public List<TransportGood> TransportGoods = new List<TransportGood>();
        private string _className;
        public GameObject This => this.gameObject;

        private void Awake()
        {
            if (Instance)
            {
                DestroyImmediate(this);
            }
            else
            {
                Instance = this;
            }
            _className = this.GetType().Name;
        }
        public TransportGood GetRndTransportGood() => TransportGoods[Random.Range(0, TransportGoods.Count)];

        public TransportGood GetRndTransportGoodByCategory(GoodCategory category)
        {
            List<TransportGood> goods = GetTransportGoodsByCategory(category);
            int rnd = Random.Range(0, goods.Count);
            return goods[rnd];
        }

        public List<TransportGood> GetRndListOfGoods(GoodCategory goodCategory)
        {
            List<TransportGood> newList = new List<TransportGood>();
            List<TransportGood> goods = GetTransportGoodsByCategory(goodCategory);

            while (newList.Count < Random.Range(1, goods.Count))
            {
                int rndIndex = Random.Range(0, goods.Count);
                TransportGood tG = goods[rndIndex];
                if (!newList.Contains(tG)) newList.Add(tG);
            }
            return newList;
        }

        private List<TransportGood> GetTransportGoodsByCategory(GoodCategory category)
        {
            List<TransportGood> list = new List<TransportGood>();
            foreach (TransportGood tG in TransportGoods)
            {
                if (tG.goodCategory == category) list.Add(tG);
            }
            return list;
        }

        [Button("LoadTransportGoods", ButtonSizes.Medium)]
        private void LoadGoods()
        {
            TransportGood[] goods = Resources.LoadAll("ScriptableObjects/ContractSystem/", typeof(TransportGood)).Cast<TransportGood>().ToArray();
            TransportGoods = goods.ToList();
        }

        [Button("Serialize Good")] public void SerializeGood() => Debug.Log(JsonUtility.ToJson(TransportGoods[0]));
    }
}
