using UnityEngine;
using System;

namespace ContractSystem
{
    public enum TransportType { CUBIC, EPAL, FCL, WEIGHT }

    [CreateAssetMenu(fileName = "TransportGood", menuName = "ScriptableObjects/TransportGood", order = 0)]
    public class TransportGood : ScriptableObject
    {
        public string Name;
        public TransportType transportType;
        public bool NeedsCooling, NeedsCrane, NeedsForkLif;


        // public static int 
    }
}