using System.Collections;
using System.Collections.Generic;
using RoadSystem;
using TimeSystem;
using UnityEngine;
using System;
using CompanySystem;
using UISystem;
using Utilities;
namespace ContractSystem
{
    [Serializable]
    public class TransportContract : Contract<TransportCompany>, ITableRow
    {
        private static Keyframe[] frames = new Keyframe[11] {
            new Keyframe(0f, 1f),
            new Keyframe(0.1f, 1f),
            new Keyframe(0.2f,0.9f ),
            new Keyframe(0.3f, 0.7f),
            new Keyframe(0.4f, 0.5f),
            new Keyframe(0.5f, 0.35f),
            new Keyframe(0.6f, 0.2f),
            new Keyframe(0.7f, 0.15f),
            new Keyframe(0.8f, 0.1f),
            new Keyframe(0.9f, 0.1f),
            new Keyframe(1f, 0.1f)
             };
        private static AnimationCurve _priceCurve = new AnimationCurve(frames);
        public enum State { available, open, assigned, inTransit, delivered, closed }
        [SerializeField] private string _originCity;
        public CityController OriginCity => GetCity(_originCity);
        [SerializeField] private string _destinationCity;
        public CityController DestinationCity => GetCity(_destinationCity);
        public TimeStamp DeliveryDate, PickUpDate;
        private TimeStamp actualDeliveryDate;
        private TransportGood _good;
        private int _goodAmmount;
        public float ContractPrice;
        public TransportType TransportType => _good.transportType;
        [SerializeField] private State _state;
        public State CurrentState => _state;
        public int GoodAmmount => _goodAmmount;
        public TransportGood Good => _good;
        public bool PickedUp { get; private set; }
        public bool Delivered { get; private set; }
        public RouteDestination StartDestination { get; private set; }
        public RouteDestination TargetDestination { get; private set; }
        public Action OnStateChange, OnClose;
        public void Initialize(string originCityName, string destinationCityName, TimeStamp pickUpDate, int contractLengthInMinutes, TransportGood good, int goodAmmount, float contractPrice, Company contractPartner1)
        {
            _originCity = originCityName;
            _destinationCity = destinationCityName;
            PickUpDate = pickUpDate;
            DeliveryDate = TimeStamp.GetTimeStampFromTotalMinutes(pickUpDate.InMinutes() + contractLengthInMinutes);
            _good = good;
            _goodAmmount = goodAmmount;
            _state = State.available;
            ContractPrice = contractPrice;
            ContractGiver = contractPartner1;
            StartDestination = new RouteDestination(OriginCity, RouteDestination.LoadingDirection.Load, this);
            TargetDestination = new RouteDestination(DestinationCity, RouteDestination.LoadingDirection.Unload, this);
            OnClose += () => ContractMarket.Instance.DeleteContract(this);
        }
        public void Initialize(RawContractDetails d) => Initialize(d.startCityName, d.destCityName, d.PickUpTime, d.ContractLength, d.Good, d.GoodAmmount, d.BasePrice, d.GoodCompany);
        public void SetState(State state)
        {
            _state = state;
            if (state == State.inTransit) PickedUp = true;
            if (state == State.delivered)
            {
                Delivered = true;
                actualDeliveryDate = TimeManager.Instance.CurrentTimeStamp;
                CloseContract();
            };
            if (state == State.closed) OnClose?.Invoke();
            // TODO Figure out a way to let the reciever Company know that the contract was fullfilled and pay the calculated price, also clean it up from any lists;
            OnStateChange?.Invoke();
        }
        private void CloseContract()
        {
            ContractReciever.FinanceManager.AddMoney(ClaculatePrice(), FinanceSystem.CostType.Payments);
            // TODO Create a Mail for the inbox -> ContractReciever.MailManager.AddMail(new MailSystem.Mail())
            SetState(State.closed);
        }
        private float ClaculatePrice()
        {
            float minutes = actualDeliveryDate.InMinutes() - DeliveryDate.InMinutes();
            minutes = Utils.NormalizeBetween0And1(minutes, 0f, 2880f);
            return ContractPrice * _priceCurve.Evaluate(minutes);
        }
        public override void SetCompanyReceiver(TransportCompany company)
        {
            base.SetCompanyReceiver(company);
            SetState(State.open);
        }
        public string[] GetRowContent()
        {
            string[] content = new string[6];
            content[0] = ContractGiver.Name;
            content[1] = _originCity;
            content[2] = _destinationCity;
            content[3] = _good.Name;
            content[4] = _goodAmmount.ToString();
            content[5] = ContractPrice.ToString();
            return content;
        }
        private CityController GetCity(string Name)
        {
            if (CityManager.Instance.GetCityByName(Name, out CityController cC))
            {
                return cC;
            }
            return default;
        }
        public override string ToString() => $"{GoodAmmount.ToString()} {Good.Unit} - {Good.ToString()}";

        public void ClearOnClose() => OnClose = null;
        public void ClearOnStateChanged() => OnStateChange = null;
    }
}
