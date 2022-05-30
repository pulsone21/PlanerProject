using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ContractSystem
{
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
        }
        public TransportGood GetRndTransportGood() => TransportGoods[Random.Range(0, TransportGoods.Length)];
        public List<TransportGood> GetRndListOfGoods(GoodCategory goodCategory)
        {
            List<TransportGood> newList = new List<TransportGood>();

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
    }
}
