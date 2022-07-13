using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    public class Vehicle : BaseVehicle
    {
        [SerializeField] private VehicleType type;
        [SerializeField] private bool canHandleTrailer;
        [SerializeField] private List<TrailerType> handleableTrailers;

        public Vehicle(VehicleSO vehicleSO) : base(vehicleSO)
        {
            type = vehicleSO.Type;
            canHandleTrailer = vehicleSO.CanHandleTrailer;
            handleableTrailers = vehicleSO.HandleableTrailers;
        }

        public List<TrailerType> HandleableTrailers => handleableTrailers;
        public bool CanHandleTrailer => canHandleTrailer;
        public VehicleType Type => type;

        public override string[] GetRowContent()
        {
            string[] content = new string[6];
            content[0] = name;
            content[1] = type.ToString();
            content[2] = capacity.ToString();
            content[3] = ConditionAsString();
            content[4] = constructionYear.ToString();
            content[5] = Specialities();
            content[6] = GetCalculatedPrice().ToString();
            return content;
        }
    }
}
