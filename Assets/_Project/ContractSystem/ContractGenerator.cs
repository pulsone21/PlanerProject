using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using TimeSystem;
using UnityEngine;

namespace ContractSystem
{

    public static class ContractGenerator
    {
        private const int MONTH_IN_MIN = 43800;
        private const int WEEK_IN_MIN = 10080;

        public static List<Contract> GenerateContracts(int ammount)
        {
            List<Contract> contracts = new List<Contract>();

            for (int i = 0; i < ammount; i++)
            {
                City startCity = CityManager.Instance.GetRndCity();
                City destCity = CityManager.Instance.GetRndCity();

                while (destCity == startCity)
                {
                    destCity = CityManager.Instance.GetRndCity();
                }
                int minOffset = Random.Range(WEEK_IN_MIN, MONTH_IN_MIN * 2);
                TimeStamp pickUpTime = TimeStamp.GetTimeStampFromTotalMinutes(TimeManager.Instance.CurrentTimeStamp.InMinutes() + minOffset);

                TransportGood goodToTransport = GetTransportGood();
                int goodAmmount = CalculateGoodAmmount(goodToTransport);
                int directDistance = GetDistance(startCity, destCity);
                int contractLength = CalculateContractLength(directDistance, goodToTransport);
                int contractPrice = CalculatePice(goodToTransport, contractLength, goodAmmount, directDistance);

                Contract contract = new Contract(startCity, destCity, pickUpTime, contractLength, goodToTransport, goodAmmount, contractPrice);
                contracts.Add(contract);
            }
            return contracts;
        }

        private static int CalculatePice(TransportGood goodToTransport, int contractLength, int goodAmmount, int distance)
        {
            int price = 0;

            switch (goodToTransport.transportType)
            {
                case TransportType.CUBIC:
                    break;
                case TransportType.EPAL:
                    break;
                case TransportType.FCL:
                    break;
                case TransportType.WEIGHT:
                    break;
                default: throw new System.NotImplementedException(goodToTransport.transportType.ToString() + "Not mapped.");
            }




            throw new System.NotImplementedException();
        }

        private static int GetDistance(City startCity, City destCity) => CityManager.Instance.GetDistance(startCity, destCity);


        private static int CalculateGoodAmmount(TransportGood goodToTransport)
        {
            int ammount = 5;
            switch (goodToTransport.transportType)
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
                    throw new System.NotImplementedException(goodToTransport.transportType.ToString() + "Not mapped.");
            }
            return ammount;
        }

        private static int CalculateContractLength(int directDistance, TransportGood goodToTransport)
        {
            throw new System.NotImplementedException();
        }

        private static TransportGood GetTransportGood()
        {
            throw new System.NotImplementedException();
        }
    }
}
