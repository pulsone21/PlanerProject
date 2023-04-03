using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [CreateAssetMenu(fileName = "Vehicle", menuName = "SO/VehicleSystem/Vehicle", order = 0)]
    public class VehicleSO : BaseVehicleSO
    {
        public VehicleType Type;
        public bool CanHandleTrailer;
        private const int trailerClutchPrice = 800;
        public List<TrailerType> HandleableTrailers;
        public override string[] GetRowContent()
        {
            string[] content = new string[8];
            content[0] = Name;
            content[1] = Type.ToString();
            content[2] = Capacity.ToString();
            content[3] = "Sehr Gut/100";
            content[4] = TimeSystem.TimeManager.Instance.CurrentTimeStamp.ToDateString() + " (0)";
            content[5] = Specialities();
            content[6] = PossibleTrailer();
            content[7] = GetCalculatedPrice().ToString();
            return content;
        }

        public override float GetCalculatedPrice()
        {
            float basePrice = base.GetBasePriceRaw();
            if (CanHandleTrailer) basePrice += trailerClutchPrice;
            return Mathf.FloorToInt(basePrice);
        }

        protected override string Specialities()
        {
            string outString = "";
            if (HasForklift) outString += ", Forklift";
            if (HasCrane) outString += ", Crane";
            if (HasCooling) outString += ", Cooling";
            if (CanHandleTrailer) outString += ", Trailer";
            if (outString.Length == 0)
            {
                outString = "-";
            }
            else
            {
                outString = outString.Substring(2);
            }
            return outString;
        }

        private string PossibleTrailer()
        {
            string outString = "";
            foreach (TrailerType type in HandleableTrailers)
            {
                outString = ", " + type.ToString();
            }
            if (outString.Length > 0)
            {
                outString = outString.Substring(2);
            }
            else
            {
                outString = "-";
            }
            return outString;
        }


    }
}
