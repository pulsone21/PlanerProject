using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoadSystem
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(MapVisualController))]
    public class MapVisualControllerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MapVisualController mvc = (MapVisualController)target;

            if (GUILayout.Button("Toogle MapMode"))
            {
                mvc.ToogleMode();
            }
        }
    }
}
