using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Utilities;
using SLSystem;
namespace ContractSystem
{
    [ExecuteInEditMode]
    public class TransportGoodManager : MonoBehaviour, IPersistenceData
    {
        public static TransportGoodManager Instance;
        public TransportGood[] TransportGoods;
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
        public TransportGood GetRndTransportGood() => TransportGoods[Random.Range(0, TransportGoods.Length)];

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
        private void LoadGoods() => TransportGoods = LoadTransportGoods();
        public TransportGood[] LoadTransportGoods()
        {
            return Resources.LoadAll<TransportGood>("/ScriptableObjects/ContractSystem");
        }

        [Button("Serialize")]
        public void Serialize()
        {
            Debug.Log(JsonUtility.ToJson(TransportGoods));
        }

        public void Load(GameData gameData)
        {

        }

        public void Save(ref GameData gameData)
        {
            throw new System.NotImplementedException();
        }
    }
}
