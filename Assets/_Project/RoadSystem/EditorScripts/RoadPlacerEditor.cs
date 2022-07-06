using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace RoadSystem
{

    [CustomEditor(typeof(RoadPlacer))]
    public class RoadPlacerEditor : Editor
    {
        private RoadNetwork roadNetwork;
        private RoadPlacer rP;


        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            rP = (RoadPlacer)target;
            roadNetwork = rP.roadNetwork;
        }

        private void OnSceneGUI()
        {
            Input();
        }


        private void Input()
        {
            if (roadNetwork == null)
            {
                Debug.LogError("RoadNetwork not declared, please hook up the RoadNetwork");
                return;
            }

            Event guiEvent = Event.current;
            Vector2 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;

            if (guiEvent.type == EventType.MouseDown && guiEvent.control && guiEvent.alt)
            {
                GameObject go = new GameObject("Road " + roadNetwork.transform.childCount, typeof(Road));
                go.transform.SetParent(roadNetwork.transform, false);
                go.transform.position = Vector3.zero;
                rP.road = go.GetComponent<Road>();

                return;
            }

            if (guiEvent.type == EventType.MouseDown && guiEvent.control && !guiEvent.alt)
            {
                if (rP.road == null)
                {
                    Debug.LogError("Please create first a new Road, before placing RoadNodes");
                    return;
                }

                var iconContet = EditorGUIUtility.IconContent("P4_DeletedLocal@2x");
                GameObject go = new GameObject("RoadNode " + rP.road.transform.childCount, typeof(RoadNode));

                go.transform.SetParent(rP.road.transform, false);
                go.transform.localPosition = mousePos;
                EditorGUIUtility.SetIconForObject(go, (Texture2D)iconContet.image);

                RoadNode currentNode = go.GetComponent<RoadNode>();
                int prevNodeIndex = rP.road.roadNodes.Count - 1;
                rP.road.roadNodes.Add(currentNode);
                Debug.Log("prevIndex: " + prevNodeIndex);
                if (prevNodeIndex >= 0)
                {
                    Debug.Log("Adding nodes");
                    currentNode.ConnectedNodes.Add(rP.road.roadNodes[prevNodeIndex]);
                    Debug.Log("added prevNode to Current");
                    rP.road.roadNodes[prevNodeIndex].ConnectedNodes.Add(currentNode);
                    Debug.Log("added curr to prevNode");

                }
                Selection.activeGameObject = null;
            }


            Selection.activeGameObject = rP.gameObject;
        }
    }
}
