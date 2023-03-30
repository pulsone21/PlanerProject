using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleSystem
{
    [System.Serializable]
    public class Trailer : BaseVehicle
    {
        [SerializeField] private TrailerType type;

        public Trailer(TrailerSO TrailerSO, bool isNew = false) : base(TrailerSO, isNew)
        {
            type = TrailerSO.Type;
        }

        public TrailerType Type => type;

        public override string[] GetRowContent()
        {
            string[] content = new string[7];
            content[0] = name;
            content[1] = type.ToString();
            content[2] = maxCapacity.ToString();
            content[3] = ConditionAsString();
            content[4] = constructionYear.ToString().Split("/")[1].Trim() + " (" + constructionYear.DifferenceToNowInYears() + ")";
            content[5] = Specialities();
            content[6] = GetCalculatedPrice().ToString();
            return content;
        }
    }
}
