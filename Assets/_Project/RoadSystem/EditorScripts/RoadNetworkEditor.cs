using UnityEngine;
using UnityEditor;


namespace RoadSystem
{
    [CustomEditor(typeof(RoadNetwork))]
    public class RoadNetworkEditor : Editor
    {

        private RoadNetwork roadNetwork;
        public override void OnInspectorGUI()
        {
            roadNetwork = (RoadNetwork)target;
            base.OnInspectorGUI();
        }

        private void OnSceneGUI()
        {
            Input();
        }


        private void Input()
        {
            Event guiEvent = Event.current;
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            if (guiEvent.type == EventType.MouseDown && guiEvent.shift)
            {
                Debug.Log("Test");
                Undo.RecordObject(roadNetwork.gameObject, "Add Segment");
                Vector3 pos = new Vector3(mousePos.x, mousePos.y, 0f);
                RoadCreator.Instance.CreateNode(pos);
            }
        }
    }
}