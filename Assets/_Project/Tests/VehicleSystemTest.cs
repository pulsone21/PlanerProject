using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using VehicleSystem;
using ContractSystem;

namespace PlanerTest
{
    public class VehicleSystemTest
    {
        // A Test behaves as an ordinary method

        private static TransportGood CubicNothing = A.GoodBuilder.CubicNeedsNothing();
        private static TransportGood CubicCooling = A.GoodBuilder.CubicNeedsCooling();
        private static TransportGood EPALNothing = A.GoodBuilder.EPALNeedsNothing();
        private static TransportGood FCLCrane = A.GoodBuilder.FCLNeedsCrane();
        public static IEnumerable VehicleLoading
        {
            get
            {
                yield return new TestCaseData(A.VehicleBuilder.VanWithForklift(150f), (TransportGood)A.GoodBuilder.EPALNeedsForklift(), 100f).Returns(true);
                yield return new TestCaseData(A.VehicleBuilder.TruckWithCoolingTrailer(800f), (TransportGood)A.GoodBuilder.FCLNeedsCooling(), 790f).Returns(true);
                yield return new TestCaseData(A.VehicleBuilder.TracktorUnitWithCrane(), (TransportGood)A.GoodBuilder.FCLNeedsCrane(), 100f).Returns(false);
                yield return new TestCaseData(A.VehicleBuilder.VanWithForklift(150f), (TransportGood)A.GoodBuilder.EPALNeedsForklift(), 899f).Returns(false);
            }
        }
        public static IEnumerable VehicleTrailerConnections
        {
            get
            {
                yield return new TestCaseData(A.VehicleBuilder.TruckWithCoolingTrailer(800f), A.TrailerBuilder.SmallTrailerWithCooling(10f)).Returns(true);
                yield return new TestCaseData(A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerWithCrane(150f)).Returns(true);
                yield return new TestCaseData(A.VehicleBuilder.VanWithForklift(150f), A.TrailerBuilder.LargeTrailerWithCrane(150f)).Returns(false);
            }
        }

        public static IEnumerable LoadingAgent_Loadings
        {
            get
            {
                yield return new TestCaseData(A.GoodBuilder.EPALNeedsNothing(), 100f, A.VehicleBuilder.VanWithForklift(110f), null).Returns(true);
                yield return new TestCaseData(A.GoodBuilder.FCLNeedsCrane(), 1500f, A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerWithCrane(1500f)).Returns(true);
                yield return new TestCaseData(A.GoodBuilder.FCLNeedsCrane(), 1500f, A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerWithCrane(10f)).Returns(false);
                yield return new TestCaseData(CubicNothing, 150f, A.VehicleBuilder.TruckWithCubicTrailer(0f), A.TrailerBuilder.MediumTrailerCubicPatrlyLoaded(200f, 10f, CubicNothing)).Returns(true);
                yield return new TestCaseData(A.GoodBuilder.CubicNeedsNothing(), 150f, A.VehicleBuilder.TruckWithCubicTrailer(10f), A.TrailerBuilder.MediumTrailerCubicPatrlyLoaded(200f, 180f, A.GoodBuilder.CubicNeedsNothing())).Returns(false);
            }
        }

        public static IEnumerable LoadingAgent_Unloadings
        {
            get
            {
                yield return new TestCaseData(EPALNothing, 100f, A.VehicleBuilder.VanFullyLoaded(110f, EPALNothing), null).Returns(true);
                yield return new TestCaseData(FCLCrane, 1500f, A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerCraneLoaded(1500f, 1500f, FCLCrane)).Returns(true);
                yield return new TestCaseData(A.GoodBuilder.FCLNeedsCrane(), 1500f, A.VehicleBuilder.TracktorUnitWithCrane(), A.TrailerBuilder.LargeTrailerWithCrane(10f)).Returns(false);
                yield return new TestCaseData(CubicNothing, 150f, A.VehicleBuilder.TruckWithCubicTrailer(10f), A.TrailerBuilder.MediumTrailerCubicPatrlyLoaded(200f, 150f, CubicNothing)).Returns(true);
                yield return new TestCaseData(A.GoodBuilder.CubicNeedsNothing(), 150f, A.VehicleBuilder.TruckWithCubicTrailer(10f), A.TrailerBuilder.MediumTrailerCubicPatrlyLoaded(200f, 150f, CubicNothing)).Returns(false);
            }
        }
        [TestCaseSource(nameof(VehicleLoading))]
        public bool LoadVehiclesWithGoodTest(Vehicle vehicle, TransportGood good, float amount)
        {
            // Use the Assert class to test conditions
            bool check1 = vehicle.LoadTransportGood(good, amount, out float leftOver);
            return check1 && leftOver == 0f;
        }

        [TestCaseSource(nameof(VehicleTrailerConnections))]
        public bool VehicleTrailerConnection(Vehicle vehicle, Trailer trailer)
        {
            return vehicle.CanHandleTrailer(trailer.Type);
        }

        [TestCaseSource(nameof(LoadingAgent_Loadings))]
        public bool LoadingAgent_Loading(TransportGood good, float amount, Vehicle vehicle, Trailer trailer = null)
        {
            string trailerName = trailer != null ? trailer.Name : "null";
            Debug.Log($"TestCase: {good}, amount: {amount}, Vehicle {vehicle.Name}, Trailer {trailerName}");
            return LoadingAgent.Load(good, amount, vehicle, trailer);
        }

        [TestCaseSource(nameof(LoadingAgent_Unloadings))]
        public bool LoadingAgent_UnLoading(TransportGood good, float amount, Vehicle vehicle, Trailer trailer = null)
        {
            string trailerName = trailer != null ? trailer.Name : "null";
            Debug.Log($"TestCase: {good}, amount: {amount}, Vehicle {vehicle.Name}, Trailer {trailerName}");
            return LoadingAgent.Unload(good, amount, vehicle, trailer);
        }

    }
}
