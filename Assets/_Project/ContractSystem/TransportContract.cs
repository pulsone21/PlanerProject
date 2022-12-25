using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using TimeSystem;
using UnityEngine;
using System;
using CompanySystem;
using UISystem;
namespace ContractSystem
{
    [Serializable]
    public class TransportContract : Contract, ITableRow
    {
        public enum State { available, open, assigned, inTransit, delivered }
        [SerializeField] private string _originCity;
        public CityController OriginCity => GetCity(_originCity);
        [SerializeField] private string _destinationCity;
        public CityController DestinationCity => GetCity(_destinationCity);
        private CityController GetCity(string Name)
        {
            if (CityManager.Instance.GetCityByName(Name, out CityController cC))
            {
                return cC;
            }
            return default;
        }
        public TimeStamp DeliveryDate;
        public TimeStamp PickUpDate;
        public TransportGood Good;
        public int GoodAmmount;
        public float ContractPrice;
        public TransportType TransportType => Good.transportType;
        private State _state;
        public State CurrentState => _state;
        private Action OnStateChange;
        public void RegisterOnStateChange(Action action) => OnStateChange += action;
        public void UnregisterOnStateChange(Action action) => OnStateChange -= action;

        public TransportContract(string originCityName, string destinationCityName, TimeStamp pickUpDate, int contractLengthInMinutes, TransportGood good, int goodAmmount, float contractPrice, Company contractPartner1) : base(contractPartner1)
        {
            _originCity = originCityName;
            _destinationCity = destinationCityName;
            PickUpDate = pickUpDate;
            DeliveryDate = TimeStamp.GetTimeStampFromTotalMinutes(pickUpDate.InMinutes() + contractLengthInMinutes);
            Good = good;
            GoodAmmount = goodAmmount;
            _state = State.available;
            ContractPrice = contractPrice;
        }

        public void SetState(State state)
        {
            _state = state;
            OnStateChange?.Invoke();
        }

        public override void SetCompanyReceiver(Company company)
        {
            base.SetCompanyReceiver(company);
            SetState(State.open);
        }

        public void AssignVehicle()
        {
            SetState(State.assigned);
        }

        public string[] GetRowContent()
        {
            string[] content = new string[6];
            content[0] = ContractGiver.Name;
            content[1] = _originCity;
            content[2] = _destinationCity;
            content[3] = Good.Name;
            content[4] = GoodAmmount.ToString();
            content[5] = ContractPrice.ToString();
            return content;
        }

        public override string ToString() => $"Shipment: {GoodAmmount.ToString()} - {Good.ToString()}";
    }
}
