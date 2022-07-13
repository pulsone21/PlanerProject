using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace VehicleSystem
{
    public class Trailer : BaseVehicle
    {
        [SerializeField] private TrailerType type;

        public Trailer(TrailerSO TrailerSO) : base(TrailerSO)
        {
            type = TrailerSO.Type;
        }

        public TrailerType Type => type;

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
