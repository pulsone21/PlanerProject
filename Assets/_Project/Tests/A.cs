using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanerTest
{
    public static class A
    {
        public static VehicleBuilder VehicleBuilder => new VehicleBuilder();
        public static TrailerBuilder TrailerBuilder => new TrailerBuilder();
        public static TransportGoodBuilder GoodBuilder => new TransportGoodBuilder();
        public static EmployeeSystemBuilder EmployeeBuilder => new EmployeeSystemBuilder();
    }
}
