using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ContractSystem;

namespace PlanerTest
{
    public class TransportGoodBuilder
    {
        private string Name = "Good";
        private TransportType transportType;
        private bool NeedsCrane = false;
        private bool NeedsCooling = false;
        private bool NeedsForkLif = false;

        public TransportGood FCLNeedsCrane()
        {
            transportType = TransportType.FCL;
            NeedsCrane = true;
            Name = "This Good needs Crane and is FCL";
            return Build();
        }
        public TransportGood FCLNeedsCooling()
        {
            transportType = TransportType.FCL;
            NeedsCooling = true;
            Name = "This Good needs cooling and is FCL";
            return Build();
        }

        public TransportGood WeightNeedsCrane()
        {
            transportType = TransportType.WEIGHT;
            NeedsCrane = true;
            Name = "This Good needs Crane and is EPWeightAL";
            return Build();
        }
        public TransportGood CubicNeedsCooling()
        {
            transportType = TransportType.CUBIC;
            NeedsCooling = true;
            Name = "This Good needs cooling and is CUBIC";
            return Build();
        }
        public TransportGood CubicNeedsNothing()
        {
            transportType = TransportType.CUBIC;
            Name = "This Good needs nothing and is CUBIC";
            return Build();
        }
        public TransportGood EPALNeedsForklift()
        {
            transportType = TransportType.EPAL;
            NeedsForkLif = true;
            Name = "This Good needs a forklift and is EPAL";
            return Build();
        }
        public TransportGood EPALNeedsNothing()
        {
            transportType = TransportType.EPAL;
            Name = "This Good needs nothing and is EPAL";
            return Build();
        }
        private TransportGood Build()
        {
            TransportGood newGood = ScriptableObject.CreateInstance<TransportGood>();
            newGood.Name = Name;
            newGood.transportType = transportType;
            newGood.NeedsCooling = NeedsCooling;
            newGood.NeedsCrane = NeedsCrane;
            newGood.NeedsForkLif = NeedsForkLif;
            return newGood;
        }

        public static implicit operator TransportGood(TransportGoodBuilder builder) => builder.Build();
    }
}
