using EmployeeSystem;
using VehicleSystem;

namespace PlanerTest
{
    public class EmployeeSystemBuilder
    {
        public Driver DriverNoVehicleNoTrailer()
        {
            Driver driver = EmplyoeeGenerator.GenerateDriver(1)[0];
            return driver;
        }
        public Driver DriverVehicleNoTrailer(Vehicle vehicle)
        {
            Driver driver = EmplyoeeGenerator.GenerateDriver(1)[0];
            driver.SetVehicle(vehicle);
            return driver;
        }
        public Driver DriverVehicleTrailer(Vehicle vehicle, Trailer trailer)
        {
            Driver driver = EmplyoeeGenerator.GenerateDriver(1)[0];
            driver.SetVehicle(vehicle);
            driver.SetVehicle(trailer);
            return driver;

        }
    }
}
