using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ContractSystem
{
    [CustomEditor(typeof(TransportGood))]
    public class TransportGoodEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            // TransportGood transportGood = (TransportGood)target;
            // if (GUILayout.Button("SaveToFile"))
            // {
            //     transportGood.SaveAsJson();
            // }
        }
    }
}
