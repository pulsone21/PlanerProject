namespace VehicleSystem
{
    public interface Trader
    {
        float Money { get; }
        bool CanAfford(float cost);
        void AddMoney(float money);
        float GetMoney(float money);
    }
}