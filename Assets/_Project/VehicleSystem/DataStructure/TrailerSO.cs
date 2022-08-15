using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VehicleSystem
{
    [CreateAssetMenu(fileName = "Trailer", menuName = "SO/VehicleSystem/Trailer", order = 0)]
    public class TrailerSO : BaseVehicleSO
    {
        public TrailerType Type;

        public override string[] GetRowContent()
        {
            string[] content = new string[5];
            content[0] = Name;
            content[1] = Type.ToString();
            content[2] = Capacity.ToString();
            content[3] = Specialities();
            content[4] = GetCalculatedPrice().ToString();
            return content;
        }
    }
}
