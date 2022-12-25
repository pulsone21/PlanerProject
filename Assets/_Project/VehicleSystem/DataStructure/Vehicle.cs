using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [System.Serializable]
    public class Vehicle : BaseVehicle
    {
        [SerializeField] private VehicleType type;
        [SerializeField] private bool canHandleTrailer;
        [SerializeField] private List<TrailerType> handleableTrailers;
        [SerializeField] private readonly string plateText;

        public Vehicle(VehicleSO vehicleSO, bool isNew = false) : base(vehicleSO, isNew)
        {
            type = vehicleSO.Type;
            canHandleTrailer = vehicleSO.CanHandleTrailer;
            handleableTrailers = vehicleSO.HandleableTrailers;
            plateText = VehicleFactory.GeneratePlateText();
        }

        public List<TrailerType> HandleableTrailers => handleableTrailers;
        public bool CanHandleTrailer => canHandleTrailer;
        public VehicleType Type => type;

        public string PlateText => plateText;

        public override string[] GetRowContent()
        {
            string[] content = new string[7];
            content[0] = name;
            content[1] = type.ToString();
            content[2] = capacity.ToString();
            content[3] = ConditionAsString();
            content[4] = constructionYear.ToString().Split("/")[1].Trim() + " (" + constructionYear.DifferenceToNowInYears() + ")";
            content[5] = Specialities();
            content[6] = GetCalculatedPrice().ToString();
            return content;
        }
    }
}
