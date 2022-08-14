namespace VehicleSystem
{
    public interface ITrader
    {
        float Money { get; }
        bool CanAfford(float cost);
        void AddMoney(float money);
        void RemoveMoney(float money);
    }
}