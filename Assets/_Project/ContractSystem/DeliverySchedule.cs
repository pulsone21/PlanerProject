using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimeSystem;
using RoadSystem;
using System;

namespace ContractSystem
{
    public class DeliverySchedule
    {
        private readonly string _startPosition, _endPostion;
        public readonly Contract Contract;
        public readonly TimeStamp StartTime, EndTime;
        public DeliverySchedule(string startPosition, string endPostion, Contract contract, TimeStamp startTime, TimeStamp endTime)
        {
            _startPosition = startPosition;
            _endPostion = endPostion;
            Contract = contract;
            StartTime = startTime;
            EndTime = endTime;
        }
        public CityController StartPosition => GetCity(_startPosition);
        public CityController EndPosition => GetCity(_endPostion);
        private CityController GetCity(string cityName)
        {
            if (CityManager.Instance.GetCityByName(cityName, out CityController cityController))
            {
                return cityController;
            }
            return default;
        }
    }
}
