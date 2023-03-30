using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VehicleSystem;
using ContractSystem;

namespace PlanerTest
{
    public class VehicleBuilder
    {
        private string Name = "Vehicle";
        private float Capacity = 0f;
        private bool CanHandleCUBIC = false;
        private bool HasForklift = false;
        private bool HasCooling = false;
        private bool HasCrane = false;
        private int OriginalPrice = 0;
        private VehicleType Type;
        private bool CanHandleTrailer = false;
        private List<TrailerType> HandleableTrailers;

        public Vehicle VanWithForklift(float Capacity)
        {
            Name = $"VanWithForklift - {Capacity}";
            this.Capacity = Capacity;
            Type = VehicleType.Van;
            HasForklift = true;
            return Build();
        }
        public Vehicle VanFullyLoaded(float Capacity, TransportGood good)
        {
            Name = $"VanFullyLoaded - {Capacity}, {good}";
            this.Capacity = Capacity;
            Type = VehicleType.Van;
            Vehicle vehlice = Build();
            vehlice.LoadTransportGood(good, Capacity, out float leftOver);
            return vehlice;
        }
        public Vehicle TruckWithCoolingTrailer(float Capacity)
        {
            Name = $"TruckWithCoolingTrailer - {Capacity}";
            this.Capacity = Capacity;
            Type = VehicleType.Truck;
            HasCooling = true;
            CanHandleTrailer = true;
            HandleableTrailers = new List<TrailerType>() { TrailerType.small, TrailerType.medium };
            return Build();
        }
        public Vehicle TruckWithCubicTrailer(float Capacity)
        {
            Name = $"TruckWithCubicTrailer - {Capacity}";
            this.Capacity = Capacity;
            Type = VehicleType.Truck;
            HasCooling = true;
            CanHandleTrailer = true;
            HandleableTrailers = new List<TrailerType>() { TrailerType.small, TrailerType.medium };
            return Build();
        }

        public Vehicle TracktorUnitWithCrane()
        {
            Name = "TracktorUnitWithCrane";
            Type = VehicleType.TracktorUnit;
            HasCrane = true;
            CanHandleTrailer = true;
            HandleableTrailers = new List<TrailerType>() { TrailerType.medium, TrailerType.large };
            return Build();
        }

        private Vehicle Build()
        {
            VehicleSO newSO = ScriptableObject.CreateInstance<VehicleSO>();
            newSO.Name = Name;
            newSO.Capacity = Capacity;
            newSO.CanHandleCUBIC = CanHandleCUBIC;
            newSO.HasForklift = HasForklift;
            newSO.HasCooling = HasCooling;
            newSO.HasCrane = HasCrane;
            newSO.OriginalPrice = OriginalPrice;
            newSO.Type = Type;
            newSO.CanHandleTrailer = CanHandleTrailer;
            newSO.HandleableTrailers = HandleableTrailers;
            return new Vehicle(newSO, true);
        }
        public static implicit operator Vehicle(VehicleBuilder builder) => builder.Build();
    }
    public class TrailerBuilder
    {
        private string Name = "Trailer";
        private float Capacity = 0f;
        private bool CanHandleCUBIC = false;
        private bool HasForklift = false;
        private bool HasCooling = false;
        private bool HasCrane = false;
        private int OriginalPrice = 0;
        private TrailerType Type;

        public Trailer MediumTrailerForklift(float Capacity)
        {
            Name = $"MediumTrailerForklift - {Capacity}";
            this.Capacity = Capacity;
            Type = TrailerType.medium;
            HasForklift = true;
            return Build();
        }

        public Trailer SmallTrailerWithCooling(float Capacity)
        {
            Name = $"SmallTrailerWithCooling - {Capacity}";
            Type = TrailerType.small;
            this.Capacity = Capacity;
            HasCooling = true;
            return Build();
        }

        public Trailer SmallTrailerWithCoolingPartlyFull(float Capacity, float loaded)
        {
            Name = $"SmallTrailerWithCoolingPartlyFull - {Capacity}, {loaded}";
            Type = TrailerType.small;
            this.Capacity = Capacity;
            HasCooling = true;
            Trailer trailer = Build();
            trailer.LoadTransportGood(A.GoodBuilder.FCLNeedsCooling(), loaded, out float leftOver);
            return Build();
        }

        public Trailer LargeTrailerWithCrane(float Capacity)
        {
            Name = $"LargeTrailerWithCrane - {Capacity}";
            this.Capacity = Capacity;
            Type = TrailerType.large;
            HasCrane = true;
            return Build();
        }

        public Trailer LargeTrailerFullyLoaded(float Capacity)
        {
            Name = $"LargeTrailerFullyLoaded - {Capacity}";
            this.Capacity = Capacity;
            Type = TrailerType.large;
            Trailer trailer = Build();
            trailer.LoadTransportGood(A.GoodBuilder.EPALNeedsNothing(), Capacity - 10f, out float leftOver);
            return trailer;
        }
        public Trailer MediumTrailerCubic(float Capacity)
        {
            Name = $"MediumTrailerCubic - {Capacity}";
            Type = TrailerType.medium;
            this.Capacity = Capacity;
            CanHandleCUBIC = true;
            return Build();
        }
        public Trailer MediumTrailerCubicPatrlyLoaded(float Capacity, float loaded, TransportGood good)
        {
            Name = $"MediumTrailerCubicPatrlyLoaded - {Capacity}, {loaded}, {good}";
            Type = TrailerType.medium;
            this.Capacity = Capacity;
            CanHandleCUBIC = true;
            Trailer trailer = Build();
            trailer.LoadTransportGood(good, loaded, out float leftOver);
            return trailer;
        }
        public Trailer LargeTrailerCubicCoolingPartlyFull(float Capacity, float loaded)
        {
            Name = $"LargeTrailerCubicCoolingPartlyFull - {Capacity}, {loaded}";
            Type = TrailerType.large;
            this.Capacity = Capacity;
            CanHandleCUBIC = true;
            HasCooling = true;
            Trailer trailer = Build();
            trailer.LoadTransportGood(A.GoodBuilder.CubicNeedsCooling(), loaded, out float leftOver);
            return trailer;
        }
        public Trailer LargeTrailerCraneLoaded(float Capacity, float loaded, TransportGood good)
        {
            Name = $"LargeTrailerCraneLoaded - {Capacity}, {loaded}, {good}";
            Type = TrailerType.large;
            this.Capacity = Capacity;
            HasCrane = true;
            Trailer trailer = Build();
            trailer.LoadTransportGood(good, loaded, out float leftOver);
            return trailer;
        }
        private Trailer Build()
        {
            TrailerSO newSO = ScriptableObject.CreateInstance<TrailerSO>();
            newSO.Name = Name;
            newSO.Capacity = Capacity;
            newSO.CanHandleCUBIC = CanHandleCUBIC;
            newSO.HasForklift = HasForklift;
            newSO.HasCooling = HasCooling;
            newSO.HasCrane = HasCrane;
            newSO.OriginalPrice = OriginalPrice;
            newSO.Type = Type;
            return new(newSO, true);
        }
        public static implicit operator Trailer(TrailerBuilder builder) => builder.Build();
    }
}