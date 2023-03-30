using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VehicleSystem;
using EmployeeSystem;

namespace PlanerTest
{
    public class EmployeeSystemTest
    {

        public static IEnumerable SetVehicleScenarios
        {
            get
            {
                yield return new TestCaseData(A.EmployeeBuilder.DriverNoVehicleNoTrailer(), A.VehicleBuilder.VanWithForklift(19f)).Returns(true);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleNoTrailer(A.VehicleBuilder.VanWithForklift(19f)), A.VehicleBuilder.VanWithForklift(19f)).Returns(true);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleTrailer(A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerWithCrane(199f)), A.VehicleBuilder.VanWithForklift(19f)).Returns(false);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleNoTrailer(A.VehicleBuilder.VanFullyLoaded(19f, A.GoodBuilder.EPALNeedsNothing())), A.VehicleBuilder.VanWithForklift(250f)).Returns(false);
            }
        }

        public static IEnumerable SetTrailerScenarios
        {
            get
            {
                yield return new TestCaseData(A.EmployeeBuilder.DriverNoVehicleNoTrailer(), A.TrailerBuilder.MediumTrailerCubic(19f)).Returns(false);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleNoTrailer(A.VehicleBuilder.TruckWithCoolingTrailer(19f)), A.TrailerBuilder.MediumTrailerForklift(19f)).Returns(true);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleNoTrailer(A.VehicleBuilder.VanWithForklift(19f)), A.TrailerBuilder.MediumTrailerForklift(19f)).Returns(false);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleTrailer(A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerWithCrane(199f)), A.TrailerBuilder.LargeTrailerWithCrane(199f)).Returns(true);
                yield return new TestCaseData(A.EmployeeBuilder.DriverVehicleTrailer(A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerFullyLoaded(199f)), A.TrailerBuilder.LargeTrailerWithCrane(199f)).Returns(false);
            }
        }
        [TestCaseSource(nameof(SetVehicleScenarios))]
        public bool SetVehicle(Driver driver, Vehicle vehicle) => driver.SetVehicle(vehicle);
        [TestCaseSource(nameof(SetTrailerScenarios))]
        public bool SetTrailer(Driver driver, Trailer trailer) => driver.SetVehicle(trailer);


        [TestCase(50, 0, ExpectedResult = 50)]
        [TestCase(-30, 50, ExpectedResult = 20)]
        [TestCase(-10, 0, ExpectedResult = 0)]
        [TestCase(110, 0, ExpectedResult = 100)]
        [TestCase(40, 60, ExpectedResult = 100)]
        [TestCase(50, 60, ExpectedResult = 100)]
        public int Skill_Change_Check(int newValue, int startValue)
        {
            Skill testSkill = new Skill(startValue, "testSkill", SkillType.Mental);
            testSkill.ChangeValue(newValue);
            return testSkill.Value;
        }


    }
}
