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
            List<TransportGood> goods = GetRndListOfGoods(category);
            return goods[Random.Range(0, goods.Count)];
        }
        public List<TransportGood> GetRndListOfGoods(GoodCategory goodCategory)
        {
            List<TransportGood> newList = new List<TransportGood>();
            Debug.Log(TransportGoods.Length);

            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                bool added = false;
                while (!added)
                {
                    int rndNr = Random.Range(0, TransportGoods.Length);
                    if (TransportGoods[rndNr].goodCategory == goodCategory && !newList.Contains(TransportGoods[rndNr]))
                    {
                        newList.Add(TransportGoods[rndNr]);
                        added = true;
                    }
                }
            }
            return newList;
        }

        public TransportGood[] LoadTransportGoods()
        {
            return Resources.LoadAll<TransportGood>("ScriptableObjects/ContractSystem/");
        }

    }
}
