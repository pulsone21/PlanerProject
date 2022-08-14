using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace VehicleSystem
{


    [CustomEditor(typeof(VehicleMarket))]
    public class VehicleMarketEditor : Editor
    {
        public override void OnInspectorGUI()
        {

            VehicleMarket market = (VehicleMarket)target;
            base.OnInspectorGUI();
        }
    }

}
