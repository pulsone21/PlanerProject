using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContractSystem
{
    [ExecuteInEditMode]
    public class TransportGoodManager : MonoBehaviour
    {
        public static TransportGoodManager Instance;
        public TransportGood[] TransportGoods;

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
            TransportGoods = LoadTransportGoods();
        }
        public TransportGood GetRndTransportGood() => TransportGoods[Random.Range(0, TransportGoods.Length)];

        public TransportGood GetRndTransportGoodByCategory(GoodCategory category)
        {
            List<TransportGood> goods = GetTransportGoodsByCategory(category);
            return goods[Random.Range(0, goods.Count)];
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

        public TransportGood[] LoadTransportGoods()
        {
            return Resources.LoadAll<TransportGood>("ScriptableObjects/ContractSystem/");
        }

    }
}
