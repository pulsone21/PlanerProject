using UnityEngine;
using Utilities;

namespace ContractSystem
{
    public enum TransportType { CUBIC, EPAL, FCL, WEIGHT }
    public enum GoodCategory { Electronics, Chemicals, Raw_Materials, Groceries, Pharmaceuticals, Furniture, Armory, Mobility, Fashion, Medical_Hardware, Equipment }

    [CreateAssetMenu(fileName = "TransportGood", menuName = "ScriptableObjects/TransportGood", order = 0)]
    public class TransportGood : ScriptableObject
    {
        public string Name;
        public TransportType transportType;
        public bool NeedsCooling, NeedsCrane, NeedsForkLif;
        public GoodCategory goodCategory;

        public int CalculatePrice(int ammount)
        {
            float price = 0;
            switch (transportType)
            {
                case TransportType.CUBIC:
                    price = 5 * ammount;
                    break;
                case TransportType.EPAL:
                    price = 10 * ammount;
                    break;
                case TransportType.FCL:
                    price = 15 * ammount;
                    break;
                case TransportType.WEIGHT:
                    price = 20 * ammount;
                    break;
                default: throw new System.NotImplementedException(transportType.ToString() + "Not mapped.");
            }
            if (NeedsCooling) price += 250;
            if (NeedsCrane) price += 500;
            if (NeedsForkLif) price += 350;
            return Mathf.RoundToInt(price);
        }

        public int GenerateGoodAmmount()
        {
            int ammount = 5;
            switch (transportType)
            {
                case TransportType.CUBIC:
                    ammount = Random.Range(1, 53);
                    break;
                case TransportType.EPAL:
                    ammount = Random.Range(1, 35);
                    break;
                case TransportType.FCL:
                    ammount = Random.Range(1, 4);
                    break;
                case TransportType.WEIGHT:
                    ammount = Random.Range(1, 41);
                    break;
                default:
                    throw new System.NotImplementedException(transportType.ToString() + "Not mapped.");
            }
            return ammount;
        }

        public int CalculateLoadingTime(int goodAmmount = 1)
        {
            int ammount = 5;
            switch (transportType)
            {
                case TransportType.CUBIC:
                    ammount = 2 * goodAmmount;
                    break;
                case TransportType.EPAL:
                    ammount = 5 * goodAmmount;
                    break;
                case TransportType.FCL:
                    ammount = 25;
                    break;
                case TransportType.WEIGHT:
                    ammount = 150;
                    break;
                default:
                    throw new System.NotImplementedException(transportType.ToString() + "Not mapped.");
            }
            if (NeedsCrane) ammount += 45;
            if (NeedsForkLif) ammount += 20;

            return ammount;
        }

    }
}