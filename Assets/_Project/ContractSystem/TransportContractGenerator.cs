using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using TimeSystem;
using UnityEngine;
using CompanySystem;

namespace ContractSystem
{

    public static class TransportContractGenerator
    {
        private const int MONTH_IN_MIN = 43800;
        private const int WEEK_IN_MIN = 10080;
        private const float PRICE_FOR_KM = 0.70f;
        private const int KM_FOR_HOUR = 65;
        public static List<RawContractDetails> GenerateContracts(int ammount)
        {
            Debug.Log($"Generating {ammount} Contracts");
            List<RawContractDetails> contracts = new List<RawContractDetails>();

            for (int i = 0; i < ammount; i++)
            {
                CityController startCity = CityManager.Instance.GetRndCity();
                GoodCompany goodCompamy = startCity.GetRndCompany();

                CityController destCity = CityManager.Instance.GetRndCityByCategory(goodCompamy.GoodCategory);
                while (destCity == startCity)
                {
                    destCity = CityManager.Instance.GetRndCity();
                }

                int minOffset = Random.Range(WEEK_IN_MIN, MONTH_IN_MIN * 2);
                TimeStamp pickUpTime = TimeStamp.GetTimeStampFromTotalMinutes(TimeManager.Instance.CurrentTimeStamp.InMinutes() + minOffset);

                TransportGood goodToTransport = GetRndTransportGoodByCategory(goodCompamy.GoodCategory);
                int goodAmmount = goodToTransport.GenerateGoodAmmount();
                int directDistance = GetDistance(startCity, destCity);
                int contractLength = CalculateContractLength(directDistance, goodToTransport, goodAmmount);
                float basePrice = CalculatePice(goodToTransport, goodAmmount, directDistance);
                contracts.Add(new(startCity.City.Name, destCity.City.Name, contractLength, goodAmmount, basePrice, pickUpTime, goodToTransport, goodCompamy));
            }
            return contracts;
        }
        private static TransportGood GetRndTransportGoodByCategory(GoodCategory goodCategory)
        {
            return TransportGoodManager.Instance.GetRndTransportGoodByCategory(goodCategory);
        }
        private static int GetDistance(CityController startCity, CityController destCity) => Mathf.RoundToInt(CityManager.Instance.GetDistance(startCity, destCity));
        private static float CalculatePice(TransportGood goodToTransport, int goodAmmount, int distance)
        {
            float price = 0;
            price += goodToTransport.CalculatePrice(goodAmmount);
            price += distance * PRICE_FOR_KM;
            return (float)System.Math.Round(price, 2);
        }
        private static int CalculateContractLength(int directDistance, TransportGood goodToTransport, int goodAmmount)
        {
            float length = goodToTransport.CalculateLoadingTime(goodAmmount);
            length += directDistance * KM_FOR_HOUR;
            return Mathf.RoundToInt(length);
        }
    }
    public struct RawContractDetails
    {
        public string startCityName, destCityName, ContractName;
        public int ContractLength, GoodAmmount;
        public float BasePrice;
        public TimeStamp PickUpTime;
        public TransportGood Good;
        public GoodCompany GoodCompany;


        public RawContractDetails(string startCityName, string destCityName, int contractLength, int goodAmmount, float basePrice, TimeStamp pickUpTime, TransportGood good, GoodCompany goodCompany)
        {
            this.startCityName = startCityName;
            this.destCityName = destCityName;
            ContractLength = contractLength;
            GoodAmmount = goodAmmount;
            BasePrice = basePrice;
            PickUpTime = pickUpTime;
            Good = good;
            GoodCompany = goodCompany;
            ContractName = $"{good.Name} from {startCityName} to {destCityName}";
        }
    }
}
