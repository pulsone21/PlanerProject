using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace RoadSystem
{
    [CustomEditor(typeof(RoadVisualController))]
    public class RoadVisualControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            RoadVisualController rvc = (RoadVisualController)target;

            if (GUILayout.Button("Set To Night"))
            {
                rvc.ToogleMode(true);
            }

            if (GUILayout.Button("Set To Day"))
            {
                rvc.ToogleMode(false);
            }
        }
    }
}
