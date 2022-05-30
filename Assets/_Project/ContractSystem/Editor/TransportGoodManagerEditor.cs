using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Utilities;


namespace ContractSystem
{
    [CustomEditor(typeof(TransportGoodManager))]
    public class TransportGoodEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            TransportGoodManager manager = (TransportGoodManager)target;
            if (GUILayout.Button("Load TransportGoods"))
            {
                manager.TransportGoods = manager.LoadTransportGoods();
            }
        }
    }
}
