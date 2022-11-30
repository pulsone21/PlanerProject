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

        public static List<TransportContract> GenerateContracts(int ammount)
        {
            Debug.Log($"Generating {ammount} Contracts");
            List<TransportContract> contracts = new List<TransportContract>();

            for (int i = 0; i < ammount; i++)
            {
                City startCity = CityManager.Instance.GetRndCity();
                GoodCompany goodCompamy = (GoodCompany)startCity.GetRndCompany();

                City destCity = CityManager.Instance.GetRndCityByCategory(goodCompamy.GoodCategory);
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

                TransportContract contract = new TransportContract(startCity, destCity, pickUpTime, contractLength, goodToTransport, goodAmmount, basePrice, goodCompamy);
                contracts.Add(contract);
            }
            Debug.Log("Contracts Generated");
            return contracts;
        }

        private static TransportGood GetRndTransportGoodByCategory(GoodCategory goodCategory)
        {
            return TransportGoodManager.Instance.GetRndTransportGoodByCategory(goodCategory);
        }
        private static int GetDistance(City startCity, City destCity) => Mathf.RoundToInt(CityManager.Instance.GetDistance(startCity, destCity));

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
}
