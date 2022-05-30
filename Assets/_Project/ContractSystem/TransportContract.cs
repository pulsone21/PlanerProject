using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using TimeSystem;
using UnityEngine;
using System;
using CompanySystem;

namespace ContractSystem
{
    [System.Serializable]
    public class TransportContract : Contract
    {
        public enum State { available, open, assigned, inTransit, delivered }
        public readonly City OriginCity;
        public readonly City DestinationCity;
        public readonly TimeStamp DeliveryDate;
        public readonly TimeStamp PickUpDate;
        public readonly TransportGood Good;
        public readonly int GoodAmmount;
        public readonly float ContractPrice;
        private State _state;
        public State CurrentState => _state;
        private Action OnStateChange;
        public void RegisterOnStateChange(Action action) => OnStateChange += action;
        public void UnregisterOnStateChange(Action action) => OnStateChange -= action;

        public TransportContract(City originCity, City destinationCity, TimeStamp pickUpDate, int contractLengthInMinutes, TransportGood good, int goodAmmount, float contractPrice, Company contractPartner1) : base(contractPartner1)
        {
            OriginCity = originCity;
            DestinationCity = destinationCity;
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
    }
}
